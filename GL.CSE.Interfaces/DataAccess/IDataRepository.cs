using GL.CSE.Models;

namespace GL.CSE.Interfaces.DataAccess;
public interface IDataRepository
{
    IEnumerable<SearchResult> BuildSearchIndex();
}
