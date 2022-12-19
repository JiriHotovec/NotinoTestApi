namespace DocumentService.Core.Abstractions;

public interface IDocumentFormatter
{
    Stream FormatDocument(Document document);

    bool Handles(string contentType);
    
    string OutputContentType { get; }
}