using AutoMapper;
using AutoMapper.Execution;
using Talabat.APIs.Dtos;
using Talabat.Core.Entities;

namespace Talabat.APIs.Helper
{
    public class ProductPictureUrlResolver : IValueResolver<Product, ProductToReturnDtos, string>
    {
        private readonly IConfiguration _configration;

        public ProductPictureUrlResolver(IConfiguration configration)
        {
            _configration = configration;
        }
        public string Resolve(Product source, ProductToReturnDtos destination, string destMember, ResolutionContext context)
        {
            if (!string.IsNullOrEmpty(source.PictureUrl))
            {
                return $"{_configration["ApiBaseUrl"]}/{source.PictureUrl}";
            }
            return string.Empty;
        }

    }
}
