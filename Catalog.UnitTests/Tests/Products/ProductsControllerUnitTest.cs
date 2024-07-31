using Catalog.Application.AutoMapper.Profiles;
using Catalog.Application.Interfaces;
using Catalog.Application.Interfaces.Providers.Products;
using Catalog.Application.Providers.Products;
using Catalog.Application.Repositories;
using AutoMapper;
using Catalog.Configuration.Resources;
using Configuration.Settings;
using Catalog.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Catalog.Application.Validations.Products;
using Catalog.Application.Interfaces.Managers;
using Catalog.Application.Managers;

namespace Catalog.UnitTests.Tests.Products;

public class ProductsControllerUnitTest
{
    public IMapper Mapper;
    public IProductUseCaseManager UseCaseManager;

    public static IUnitOfWork UnitOfWork;
    public static DbContextOptions<AppDbContext> DbOptions { get; }

    static ProductsControllerUnitTest()
    {
        var connectionString = AppConfiguration.GetConnectionString(DefaultConnectionStringResource.NAME);

        var dbOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString);

        DbOptions = dbOptionsBuilder.Options;

        var context = new AppDbContext(DbOptions);

        UnitOfWork = new UnitOfWork(context);
    }

    public ProductsControllerUnitTest()
    {
        MapperConfiguration mapperConfig = new MapperConfiguration(config =>
        config.AddProfile(new MappingProfile()));

        Mapper = mapperConfig.CreateMapper();

        var productValidation = new ProductValidation();

        IGetProductUseCaseProvider GetProvider = new GetProductUseCaseProvider(UnitOfWork, Mapper);
        IPostProductUseCaseProvider PostProvider = new PostProductUseCaseProvider(UnitOfWork, Mapper, productValidation);
        IPatchProductUseCaseProvider PatchProvider = new PatchProductUseCaseProvider(UnitOfWork, Mapper, new RequestProductDTOValidation());
        IPutProductUseCaseProvider PutProvider = new PutProductUseCaseProvider(UnitOfWork, Mapper, productValidation);
        IDeleteProductUseCaseProvider DeleteProvider = new DeleteProductUseCaseProvider(UnitOfWork, Mapper);

        UseCaseManager = new ProductUseCaseManager(GetProvider, PostProvider, PatchProvider, PutProvider, DeleteProvider);
    }
}
