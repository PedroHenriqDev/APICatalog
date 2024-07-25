using Application.AutoMapper.Profiles;
using Application.Interfaces;
using Application.Interfaces.Providers.Products;
using Application.Providers.Products;
using Application.Repositories;
using AutoMapper;
using Configuration.Resources;
using Configuration.Settings;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.Validations.Products;
using Application.Interfaces.Managers;
using Application.Managers;

namespace UnitTests.Tests.Products;

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
