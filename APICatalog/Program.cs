using APICatalog.Context;
using APICatalog.Extensions;
using APICatalog.Filters;
using APICatalog.Interfaces;
using APICatalog.Logging;
using APICatalog.Repositories;
using APICatalog.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options => 
{
    options.Filters.Add(typeof(ApiExceptionFilter));
}).AddJsonOptions(options =>
                  options.JsonSerializerOptions
  .ReferenceHandler = ReferenceHandler.IgnoreCycles);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddScoped<ApiExceptionFilter>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

string npgConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(npgConnection));   

builder.Services.AddTransient<ISalutationService, SalutationService>();

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information,
})); 

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.DisableImplicitFromServicesParameters = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.ConfigureExceptionHandler();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();