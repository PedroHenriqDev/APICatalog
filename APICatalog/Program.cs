using APICatalog.Extensions;
using APICatalog.Filters;
using Application.AutoMapper.Profiles;
using Application.Interfaces;
using Application.Interfaces.Managers;
using Application.Interfaces.Providers.Categories;
using Application.Interfaces.Providers.Products;
using Application.Logging;
using Application.Managers;
using Application.Providers.Categories;
using Application.Providers.Products;
using Application.Repositories;
using Application.Services;
using Application.Services.Tokens;
using Application.Services.Users;
using Application.Validations.Categories;
using Application.Validations.Products;
using Asp.Versioning;
using Configuration.Options;
using Infrastructure.Data;
using Infrastructure.Domain;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers(options =>
{
    options.Filters.Add(typeof(ApiExceptionFilter));
}).AddJsonOptions(options =>
                  options.JsonSerializerOptions
  .ReferenceHandler = ReferenceHandler.IgnoreCycles).AddNewtonsoftJson();

builder.Services.AddEndpointsApiExplorer();

//Swagger gen Config
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "apicatalog",
        Version = "v1" 
    });

    c.SwaggerDoc("v2", new OpenApiInfo
    {
        Title = "apicatalog",
        Version = "v2",
    });

    var fileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, fileName));

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Bearer Json Web Token (JWT)"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new List<string>()
        }
    });
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("Admin"));

    options.AddPolicy("UserOnly", policy => policy.RequireAssertion(context =>
                      context.User.IsInRole("User") || context.User.IsInRole("Admin")));
});

// Filters
builder.Services.AddScoped<ApiLoggingFilter>();
builder.Services.AddScoped<ApiExceptionFilter>();

// Services
builder.Services.AddScoped<IUserClaimService, UserClaimService>();
builder.Services.AddScoped<ITokenService, TokenService>();

// Unit of Work
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Category Validations
builder.Services.AddScoped<CategoryValidation>();
builder.Services.AddScoped<RequestPatchCategoryDTOValidation>();

//Product Validations
builder.Services.AddScoped<ProductValidation>();
builder.Services.AddScoped<RequestProductDTOValidation>();

//Managers
builder.Services.AddScoped<ICategoryUseCaseManager, CategoryUseCaseManager>();
builder.Services.AddScoped<IProductUseCaseManager, ProductUseCaseManager>();

//Providers Category
builder.Services.AddScoped<IGetCategoryUseCaseProvider, GetCategoryUseCaseProvider>();
builder.Services.AddScoped<IPostCategoryUseCaseProvider, PostCategoryUseCaseProvider>();
builder.Services.AddScoped<IPutCategoryUseCaseProvider, PutCategoryUseCaseProvider>();
builder.Services.AddScoped<IPatchCategoryUseCaseProvider, PatchCategoryUseCaseProvider>();
builder.Services.AddScoped<IDeleteCategoryUseCaseProvider, DeleteCategoryUseCaseProvider>();
builder.Services.AddScoped<IStatsCategoryUseCaseProvider, StatsCategoryUseCaseProvider>();

//Providers Product
builder.Services.AddScoped<IGetProductUseCaseProvider, GetProductUseCaseProvider>();
builder.Services.AddScoped<IPostProductUseCaseProvider, PostProductUseCaseProvider>();
builder.Services.AddScoped<IPutProductUseCaseProvider, PutProductUseCaseProvider>();
builder.Services.AddScoped<IPatchProductUseCaseProvider, PatchProductUseCaseProvider>();
builder.Services.AddScoped<IDeleteProductUseCaseProvider, DeleteProductUseCaseProvider>();

builder.Services.AddAutoMapper(typeof(MappingProfile));

string npgConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                            .AddEntityFrameworkStores<AppDbContext>()
                            .AddDefaultTokenProviders();

builder.Services.AddDbContext<AppDbContext>(options =>
                    options.UseNpgsql(npgConnection));

builder.Services.AddTransient<ISalutationService, SalutationService>();

var secretKey = builder.Configuration["JWT:SecretKey"]
    ?? throw new ArgumentException("Invalid secret key");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ClockSkew = TimeSpan.Zero,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
    };
});

builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
{
    LogLevel = LogLevel.Information,
}));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.DisableImplicitFromServicesParameters = true;
});


builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.AddFixedWindowLimiter("fixedwindow", options =>
    {
        options.PermitLimit = ApiRateLimitOptions.PermitLimit;
        options.Window = TimeSpan.FromSeconds(ApiRateLimitOptions.Window);
        options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
        options.QueueLimit = (ApiRateLimitOptions.QueuLimit);
    });
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
});

builder.Services.AddRateLimiter(rateLimiterOptions =>
{
    rateLimiterOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;

    rateLimiterOptions.GlobalLimiter = PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
        RateLimitPartition.GetFixedWindowLimiter(
            partitionKey: httpContext.User.Identity?.Name ??
                          httpContext.Request.Headers.Host.ToString(),
            factory: partition => new FixedWindowRateLimiterOptions
            {
                PermitLimit = 1,
                Window = TimeSpan.FromSeconds(5),
                QueueLimit = 0
            }));
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("allowedorigin", policy =>
    {
        policy.WithOrigins("https://localhost:7151")
        .WithMethods("GET", "POST")
        .AllowAnyHeader();
    });
});

builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;
    options.ApiVersionReader = ApiVersionReader.Combine(
                               new QueryStringApiVersionReader(),
                               new UrlSegmentApiVersionReader());
}).AddApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV";
    options.SubstituteApiVersionInUrl = true;
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
app.UseRouting();
app.UseRateLimiter();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
