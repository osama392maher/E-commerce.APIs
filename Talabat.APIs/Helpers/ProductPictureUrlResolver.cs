using AutoMapper;
using Talabat.APIs.DTOs;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helpers
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDTO, string>
    {
        private readonly IConfiguration config;

        public ProductPictureUrlResolver(IConfiguration config)
        {
            this.config = config;
        }

        public string Resolve(Product source, ProductToReturnDTO destination, string destMember, ResolutionContext context)
        {
            if (string.IsNullOrEmpty(source.PictureUrl))
                return string.Empty;
            return $"{config["ApiUrl"]}/{source.PictureUrl}";
        }
    }
}