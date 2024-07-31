using Catalog.Application.AutoMapper.Profiles;
using Catalog.Application.Interfaces.Managers;
using Catalog.Application.Interfaces.Providers.Categories;
using Catalog.Application.Interfaces.Providers.Products;
using Catalog.Application.Interfaces;
using Catalog.Application.Managers;
using Catalog.Application.Providers.Categories;
using Catalog.Application.Providers.Products;
using Catalog.Application.Repositories;
using Catalog.Application.Services.Tokens;
using Catalog.Application.Services.Users;
using Catalog.Application.Validations.Categories;
using Catalog.Application.Validations.Products;
using Catalog.Infrastructure.Data;
using Catalog.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace Catalog.CrossCutting.IoC;

public static class DependencyInjectionAPI
{
    public static IServiceCollection AddInfrasctureAPI(this IServiceCollection serviceCollection, IConfiguration configuration)
    {

        // Services
        serviceCollection.AddScoped<IUserClaimService, UserClaimService>();
        serviceCollection.AddScoped<ITokenService, TokenService>();

        // Unit of Work
        serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();

        // Category Validations
        serviceCollection.AddScoped<CategoryValidation>();
        serviceCollection.AddScoped<RequestPatchCategoryDTOValidation>();

        //Product Validations
        serviceCollection.AddScoped<ProductValidation>();
        serviceCollection.AddScoped<RequestProductDTOValidation>();

        //Managers
        serviceCollection.AddScoped<ICategoryUseCaseManager, CategoryUseCaseManager>();
        serviceCollection.AddScoped<IProductUseCaseManager, ProductUseCaseManager>();

        //Providers Category
        serviceCollection.AddScoped<IGetCategoryUseCaseProvider, GetCategoryUseCaseProvider>();
        serviceCollection.AddScoped<IPostCategoryUseCaseProvider, PostCategoryUseCaseProvider>();
        serviceCollection.AddScoped<IPutCategoryUseCaseProvider, PutCategoryUseCaseProvider>();
        serviceCollection.AddScoped<IPatchCategoryUseCaseProvider, PatchCategoryUseCaseProvider>();
        serviceCollection.AddScoped<IDeleteCategoryUseCaseProvider, DeleteCategoryUseCaseProvider>();

        //Providers Product
        serviceCollection.AddScoped<IGetProductUseCaseProvider, GetProductUseCaseProvider>();
        serviceCollection.AddScoped<IPostProductUseCaseProvider, PostProductUseCaseProvider>();
        serviceCollection.AddScoped<IPutProductUseCaseProvider, PutProductUseCaseProvider>();
        serviceCollection.AddScoped<IPatchProductUseCaseProvider, PatchProductUseCaseProvider>();
        serviceCollection.AddScoped<IDeleteProductUseCaseProvider, DeleteProductUseCaseProvider>();


        //AutoMapper
        serviceCollection.AddAutoMapper(typeof(MappingProfile));

        string npgConnection = configuration.GetConnectionString("DefaultConnection");

        serviceCollection.AddIdentity<ApplicationUser, IdentityRole>()
                                    .AddEntityFrameworkStores<AppDbContext>()
                                    .AddDefaultTokenProviders();

        serviceCollection.AddDbContext<AppDbContext>(options =>
                            options.UseNpgsql(npgConnection));

        return serviceCollection;

    }
}
