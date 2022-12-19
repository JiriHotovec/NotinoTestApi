using DocumentService.Core.Abstractions;
using DocumentService.Core.Implementations;
using FluentAssertions;

namespace DocumentService.Core.Tests.Implementations;

public class InMemoryDocumentStorageTests
{
    [Fact]
    public async Task GetAsync_ExistingItem_ReturnsItem()
    {
        var documentId = new DocumentId("123");
        var data = new Person("FirstName", "LastName", DateTimeOffset.Now);
        var tags = Array.Empty<Tag>();
        var expected = new Document(documentId, data, tags);
        var repo = new InMemoryDocumentStorage();
        await repo.UpsertAsync(expected);

        var actual = await repo.GetAsync(documentId);

        actual.Should().NotBeNull();
        actual.Should().BeEquivalentTo(expected);
    }
}