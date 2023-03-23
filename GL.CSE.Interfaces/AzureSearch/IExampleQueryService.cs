using GL.CSE.Models;

namespace GL.CSE.Interfaces.AzureSearch;

public interface IExampleQueryService
{
    Task<SearchResults> SearchIndexAsync(SearchModel searchModel);
}

