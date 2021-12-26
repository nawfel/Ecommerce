using API.Dtos;
using AutoMapper;
using Core.Entities;
using Microsoft.Extensions.Configuration;

namespace API.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductsToReturnDto, string>
    {
        private readonly IConfiguration _config;
        public ProductUrlResolver(IConfiguration config)
        {
            _config = config;

        }
        public string Resolve(Product source, ProductsToReturnDto destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PicutreUrl))
            {
                return _config["ApiUrl"] + source.PicutreUrl;
            }
            return null;
        }
    }
}