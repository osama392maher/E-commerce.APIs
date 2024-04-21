using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class MappingProfiles : Profile
    {

        public MappingProfiles()
        {

            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d => d.Category, o => o.MapFrom(s => s.Category.Name))
                .ForMember(d => d.Brand, o => o.MapFrom(s => s.Brand.Name))
            .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductPictureUrlResolver>());

        }
    }

}
