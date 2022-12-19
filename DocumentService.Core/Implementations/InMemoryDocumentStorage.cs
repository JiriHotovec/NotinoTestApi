using System.Collections.Concurrent;
using DocumentService.Core.Abstractions;

namespace DocumentService.Core.Implementations;

public sealed class InMemoryDocumentStorage : IDocumentStorage
{
    private readonly ConcurrentDictionary<DocumentId, Document> _documents = new();

    public Task UpsertAsync(Document document, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(document);

        _documents.AddOrUpdate(document.Id, document, (_, _) => document);
        return Task.CompletedTask;
    }

    public Task<Document?> GetAsync(DocumentId id, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(id);

        _documents.TryGetValue(id, out var document);

        return Task.FromResult(document);
    }

    public Task<bool> ExistsAsync(DocumentId id, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(id);

        return Task.FromResult(_documents.ContainsKey(id));
    }
}