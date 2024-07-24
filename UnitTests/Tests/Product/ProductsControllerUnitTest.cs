using Application.AutoMapper;
using Infrastructure.Data;
using Application.Interfaces;
using Application.Repositories;
using AutoMapper;
using Configuration.Resources;
using Configuration.Settings;
using Microsoft.EntityFrameworkCore;

namespace UnitTests.Tests.Products;

public class ProductsControllerUnitTest
{
    public IMapper Mapper;
    public IUnitOfWork UnitOfWork;
    public static DbContextOptions<AppDbContext> DbOptions { get; }

    static ProductsControllerUnitTest()
    {
        var connectionString = AppConfiguration.GetConnectionString(DefaultConnectionStringResource.NAME);

        var dbOptionsBuilder = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(connectionString);

        DbOptions = dbOptionsBuilder.Options;
    }

    public ProductsControllerUnitTest()
    {
        MapperConfiguration mapperConfig = new MapperConfiguration(config =>
        config.AddProfile(new MappingProfile()));

        Mapper = mapperConfig.CreateMapper();

        var context = new AppDbContext(DbOptions);

        UnitOfWork = new UnitOfWork(context);
    }
}
