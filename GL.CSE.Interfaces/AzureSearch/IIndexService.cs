using Microsoft.Extensions.Logging;

namespace GL.CSE.Interfaces.AzureSearch;
public interface IIndexService
{
    Task CreateIndexAsync(ILogger logger);

    Task DeleteIndexAsync(ILogger logger);

    Task PopulateIndexAsync(ILogger logger);

}