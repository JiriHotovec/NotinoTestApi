using DocumentService.Core.Abstractions;
using DocumentService.Web.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace DocumentService.Web.Controllers;

[ApiController]
[Route("[controller]")]
public class DocumentsController : ControllerBase
{
    private readonly List<IDocumentFormatter> _documentFormatters;
    private readonly IDocumentStorage _documentStorage;

    public DocumentsController(IDocumentStorage documentStorage, IEnumerable<IDocumentFormatter> documentFormatters)
    {
        ArgumentNullException.ThrowIfNull(documentFormatters);
        _documentStorage = documentStorage ?? throw new ArgumentNullException(nameof(documentStorage));
        _documentFormatters = documentFormatters.ToList();
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> Create([FromBody] DocumentDto documentDto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(documentDto);

        var documentId = new DocumentId(documentDto.Id);
        if (await _documentStorage.ExistsAsync(documentId, cancellationToken))
        {
            return Conflict($"Document {documentId} already exists");
        }

        var document = CreateDocumentFromDto(documentDto, documentId);

        await _documentStorage.UpsertAsync(document, cancellationToken);

        var documentInfo = new {id = documentId.ToString()};
        return CreatedAtAction(nameof(Get), "Documents", documentInfo, documentInfo);
    }
    
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Update([FromBody] DocumentDto documentDto,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(documentDto);

        var documentId = new DocumentId(documentDto.Id);
        if (!await _documentStorage.ExistsAsync(documentId, cancellationToken))
        {
            return CreateNotFound(documentId);
        }

        var document = CreateDocumentFromDto(documentDto, documentId);

        await _documentStorage.UpsertAsync(document, cancellationToken);

        return Ok();
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get([FromRoute] string id, CancellationToken cancellationToken = default)
    {
        var documentId = new DocumentId(id);
        var document = await _documentStorage.GetAsync(documentId, cancellationToken);
        if (document is null)
        {
            return CreateNotFound(documentId);
        }
        
        var acceptHeader = Request.Headers.Accept.FirstOrDefault();
        if (acceptHeader is null)
        {
            return BadRequest($"Exactly one accept header is required");
        }
        
        var formatter = _documentFormatters.FirstOrDefault(i => i.Handles(acceptHeader));
        if (formatter is null)
        {
            return BadRequest($"Requested document format {acceptHeader} is not supported");
        }

        return File(formatter.FormatDocument(document), formatter.OutputContentType);
    }

    private IActionResult CreateNotFound(DocumentId documentId)
    {
        var documentInfo = new {id = documentId.ToString()};
        return NotFound(documentInfo);
    }

    private static Document CreateDocumentFromDto(DocumentDto documentDto, DocumentId documentId)
    {
        ArgumentNullException.ThrowIfNull(documentDto);
        ArgumentNullException.ThrowIfNull(documentDto.Data);
        ArgumentNullException.ThrowIfNull(documentDto.Tags);
        ArgumentNullException.ThrowIfNull(documentId);

        var data = new Person(documentDto.Data.FirstName, documentDto.Data.LastName, documentDto.Data.DateOfBirth);
        var tags = documentDto.Tags.Select(s => new Tag(s)).ToArray();
        return new(documentId, data, tags);
    }
}