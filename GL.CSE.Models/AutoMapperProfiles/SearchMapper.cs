using AutoMapper;

namespace GL.CSE.Models.AutoMapperProfiles;
public class SearchMapper : Profile
{
    public SearchMapper()
    {
        CreateMap<CaseSearch, SearchResult>();
        CreateMap<SearchResult, CaseSearch>();
    }
}
