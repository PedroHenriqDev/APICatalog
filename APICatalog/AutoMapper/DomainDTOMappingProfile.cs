using APICatalog.Domain;
using APICatalog.DTOs;
using AutoMapper;

namespace APICatalog.AutoMapper;

public class DomainDTOMappingProfile : Profile
{
    public DomainDTOMappingProfile() 
    {
        CreateMap<Category, CategoryDTO>().ForMember(dest => dest.Products, opt => opt.MapFrom(src => src.Products.Select(product => new ProductDTO
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Stock = product.Stock,
            DateRegister = product.DateRegister
        }))).ReverseMap();

        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Product, ProductDTORequest>().ReverseMap();
        CreateMap<Product, ProductDTOResponse>().ReverseMap();  
        CreateMap<Category, CategoryDTORequest>().ReverseMap();
        CreateMap<Category, CategoryDTOResponse>().ReverseMap();
    }
}
