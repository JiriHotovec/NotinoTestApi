namespace DocumentService.Core.Abstractions;

public interface IDocumentFormatterFactory
{
    IDocumentFormatter Create(string contentType);
}