using AutoMapper;
using Azure.Search.Documents.Indexes;
using GL.CSE.AzureSearch.Services;
using GL.CSE.Interfaces.Services;
using GL.CSE.Models;
using Microsoft.Extensions.Logging;

namespace GL.CSE.AzureSearch;

public class SearchIndexService : BaseIndexService<CaseSearch>
{
    protected override string IndexName => SearchIndexes.ExampleSearch;

    private const int PageSize = 2000;

    private readonly IMapper _mapper;
    private readonly ISearchDataProvider _searchDataProvider;

    public SearchIndexService(
        SearchIndexClient searchClient,
        IMapper mapper,
        ISearchDataProvider searchDataProvider)
        : base(searchClient)
    {
        _mapper = mapper;
        _searchDataProvider = searchDataProvider;
    }

    public override async Task PopulateIndexAsync(ILogger logger)
    {
        logger.LogDebug("Get Data from GetSearchData()");
        var data = _searchDataProvider.GetSearchData();

        logger.LogDebug("Map Data");
        var indexData = _mapper.Map<IEnumerable<SearchResult>, IEnumerable<CaseSearch>>(data).ToList();

        var pages = indexData.Count / PageSize;

        if (indexData.Count % PageSize != 0)
        {
            pages += 1;
        }

        logger.LogDebug("Loop through pages and index batch");
        for (var i = 0; i < pages; i++)
        {
            var dataBatch = indexData
                .Skip(i * PageSize)
                .Take(PageSize);

            await IndexBatchAsync(i, dataBatch, logger);
        }
    }
}