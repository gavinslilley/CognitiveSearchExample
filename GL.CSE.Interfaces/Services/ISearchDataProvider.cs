using GL.CSE.Models;

namespace GL.CSE.Interfaces.Services;
public interface ISearchDataProvider
{
    IEnumerable<SearchResult> GetSearchData();
}
