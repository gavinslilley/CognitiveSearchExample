using AutoMapper;
using Azure.Search.Documents;
using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Models;
using GL.CSE.Interfaces.AzureSearch;
using GL.CSE.Models;

namespace GL.CSE.AzureSearch.Services;
public class ExampleQueryService : IExampleQueryService
{
    private readonly IMapper _mapper;

    private readonly SearchIndexClient _indexClient;

    public ExampleQueryService(IMapper mapper,
        SearchIndexClient indexClient)
    {
        _indexClient = indexClient;
        _mapper = mapper;
    }


    public async Task<SearchResults> SearchIndexAsync(SearchModel searchModel)
    {

        var searchTerm = String.Format("(FirstName eq {0})", searchModel.SearchTerm);

        var options = new SearchOptions
        {
            QueryType = SearchQueryType.Full,
            SearchMode = SearchMode.Any,
            IncludeTotalCount = true,
            Size = 10000
        };

        var result = (await SearchIndexAsync<CaseSearch, SearchResult>(searchTerm, options)).ToList();

        var results = new SearchResults
        {
            TotalResults = result.Count,
            Results = result.ToList(),
        };

        return results;
    }

    public async Task<IEnumerable<TR>> SearchIndexAsync<T, TR>(string searchTerm, SearchOptions options)
    {
        var searchClient = _indexClient.GetSearchClient(SearchIndexes.ExampleSearch);
        var result = await searchClient.SearchAsync<T>(searchTerm, options);

        var mappedResults = result.Value.GetResults().Select(r => r.Document)
            .Select(d => _mapper.Map<T, TR>(d));

        return mappedResults;
    }
}
