namespace GL.CSE.Models;
public class SearchResults
{
    public SearchResults()
    {
        Results = new List<SearchResult>();
    }
    public int TotalResults { get; set; }
    public List<SearchResult> Results { get; set; }
}