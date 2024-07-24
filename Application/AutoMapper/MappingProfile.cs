using Infrastructure.Domain;
using Communication.DTOs;
using AutoMapper;
using Communication.DTOs.Responses;
using Communication.DTOs.Requests;

namespace Application.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile() 
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
        CreateMap<Product, RequestProductDTO>().ReverseMap();
        CreateMap<Product, RequestProductDTO>().ReverseMap();  
        CreateMap<Category, RequestCategoryDTO>().ReverseMap();
        CreateMap<Category, ResponseCategoryDTO>().ReverseMap();

        CreateMap<List<ResponseCategoryStatsDTO>, ResponseCategoriesStatsDTO>().ConvertUsing((src, dest, context) =>
        {
            return new ResponseCategoriesStatsDTO
            {
                CategoriesStats = src
            };
        });
    }
}
