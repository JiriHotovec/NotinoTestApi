using DocumentService.Core.Abstractions;
using DocumentService.Core.Implementations;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace DocumentService.Core.Extensions;

public static class IServiceCollectionsExtensions
{
    public static IServiceCollection AddDocumentService(this IServiceCollection sc)
    {
        ArgumentNullException.ThrowIfNull(sc);
        
        sc.TryAddSingleton<IDocumentStorage, InMemoryDocumentStorage>();
        sc.TryAddSingleton<IDocumentFormatter, XmlDocumentFormatter>();

        return sc;
    }
}