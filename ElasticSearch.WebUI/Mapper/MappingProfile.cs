using AutoMapper;
using ElasticSearch.WebUI.Models;
using ElasticSearch.WebUI.ViewModels;

namespace ElasticSearch.WebUI.Mapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<BlogCreateViewModel, Blog>().ReverseMap();
            CreateMap<ECommerceViewModel, ECommerce>().ReverseMap();
            CreateMap<List<ECommerceViewModel>, List<ECommerce>>().ReverseMap();
        }
    }
}
