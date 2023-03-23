using Azure.Search.Documents.Indexes;

namespace GL.CSE.Models;
public class CaseSearch
{

    [SearchableField(IsSortable = true, IsKey = true)]
    public string CaseNumber { get; set; }

    [SearchableField(IsSortable = true)]
    public string FirstName { get; set; }

    [SimpleField]
    public string MiddleName { get; set; }

    [SearchableField(IsSortable = true)]
    public string FamilyName { get; set; }

    [SimpleField]
    public string Title { get; set; }
}
