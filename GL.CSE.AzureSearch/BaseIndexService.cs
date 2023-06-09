﻿using Azure.Search.Documents.Indexes;
using Azure.Search.Documents.Indexes.Models;
using Azure.Search.Documents.Models;
using GL.CSE.Interfaces.AzureSearch;
using GL.CSE.Models;
using Microsoft.Extensions.Logging;
using System.Diagnostics.CodeAnalysis;

namespace GL.CSE.AzureSearch;

public abstract class BaseIndexService<T> : IIndexService
{
    private readonly SearchIndexClient _searchClient;

    protected abstract string IndexName { get; }

    protected BaseIndexService(
        SearchIndexClient searchClient)
    {
        _searchClient = searchClient;
    }

    [ExcludeFromCodeCoverage]
    public async Task CreateIndexAsync(ILogger logger)
    {

        var fieldBuilder = new FieldBuilder();
        var searchFields = fieldBuilder.Build(typeof(T));

        var definition = new SearchIndex(IndexName, searchFields);

        try
        {
            await _searchClient.CreateOrUpdateIndexAsync(definition);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Failed to create index {IndexName}");
        }
    }

    [ExcludeFromCodeCoverage]
    public async Task DeleteIndexAsync(ILogger logger)
    {
        try
        {
            var index = await _searchClient.GetIndexAsync(IndexName);

            if (index != null)
            {
                await _searchClient.DeleteIndexAsync(index);
            }
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Failed to delete index {IndexName}");
        }
    }

    public abstract Task PopulateIndexAsync(ILogger logger);

    [ExcludeFromCodeCoverage]
    protected async Task IndexBatchAsync(int page, IEnumerable<CaseSearch> data, ILogger logger)
    {
        try
        {
            var uploaderClient = _searchClient.GetSearchClient(IndexName);

            IndexDocumentsResult result = await uploaderClient.MergeOrUploadDocumentsAsync(data);

            Thread.Sleep(2000);
        }
        catch (Exception ex)
        {
            logger.LogError(ex, $"Failed to index page: {page}");
        }
    }
}