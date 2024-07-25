using Infrastructure.Domain;
using Communication.DTOs;
using AutoMapper;
using Communication.DTOs.Responses;
using Communication.DTOs.Requests;
using Application.Pagination;
using Application.AutoMapper.Converters;

namespace Application.AutoMapper.Profiles;

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

        CreateMap(typeof(PagedList<>), typeof(PagedList<>)).ConvertUsing(typeof(PagedListConverter<,>));

        CreateMap<Product, ProductDTO>().ReverseMap();
        CreateMap<Product, RequestPatchProductDTO>().ReverseMap();
        CreateMap<Product, RequestPatchProductDTO>().ReverseMap();
        CreateMap<Category, RequestPatchCategoryDTO>().ReverseMap();
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
