namespace DocumentService.Core.Abstractions;

public interface IDocumentStorage
{
    Task UpsertAsync(Document document, CancellationToken cancellationToken = default);

    Task<Document?> GetAsync(DocumentId id, CancellationToken cancellationToken = default);

    Task<bool> ExistsAsync(DocumentId id, CancellationToken cancellationToken = default);
}