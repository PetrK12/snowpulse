using Application.DataTransferObject;
using AutoMapper;
using AutoMapper.Execution;
using Domain.Entities.BusinessEntities;
using Microsoft.Extensions.Configuration;

namespace Application.MappingProfiles;

public class ProductUrlResolver : IValueResolver<Product, ProductClientDto, string>
{
    private readonly IConfiguration _configuration;
    
    public ProductUrlResolver(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    public string Resolve(Product source, ProductClientDto destination, string destMember, ResolutionContext context)
    {
        if (!string.IsNullOrEmpty(source.PictureUrl))
        {
            return _configuration["ApiUrl"] + source.PictureUrl;
        }
        return null;
    }
}