using System.Xml.Serialization;
using DocumentService.Core.Abstractions;

namespace DocumentService.Core.Implementations;

public sealed class XmlDocumentFormatter : IDocumentFormatter
{
    private static readonly XmlSerializer Serializer = new(typeof(Person));
    private static readonly string[] SupportedTypes = { @"*/*", @"text/xml", @"application/xml" };
    
    public Stream FormatDocument(Document document)
    {
        ArgumentNullException.ThrowIfNull(document);

        var stream = new MemoryStream();
        Serializer.Serialize(stream, document.Data);
        stream.Seek(0, SeekOrigin.Begin);
        
        // Do not dispose
        return stream;
    }

    public bool Handles(string contentType)
    {
        if (string.IsNullOrWhiteSpace(contentType))
            throw new ArgumentException("Value cannot be null or whitespace.", nameof(contentType));

        return SupportedTypes.Contains(contentType, StringComparer.OrdinalIgnoreCase);
    }

    public string OutputContentType => "application/xml";
}