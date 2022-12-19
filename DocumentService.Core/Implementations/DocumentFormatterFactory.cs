using DocumentService.Core.Abstractions;

namespace DocumentService.Core.Implementations;

public class DocumentFormatterFactory : IDocumentFormatterFactory
{
    public IDocumentFormatter Create(string contentType)
    {
        if (contentType == null) throw new ArgumentNullException(nameof(contentType));
        
        throw new NotImplementedException();
    }
}