using GL.CSE.Interfaces.DataAccess;
using GL.CSE.Interfaces.Services;
using GL.CSE.Models;

namespace GL.CSE.Services;
public class SearchDataProvider : ISearchDataProvider
{
    private readonly IDataRepository _repository;

    public SearchDataProvider(IDataRepository repository)
    {
        _repository = repository;
    }

    public IEnumerable<SearchResult> GetSearchData()
    {
        return _repository.BuildSearchIndex();
    }
}