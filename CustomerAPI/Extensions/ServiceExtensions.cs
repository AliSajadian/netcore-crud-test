using Microsoft.EntityFrameworkCore;
using System.Text;
using System.Text.Json;
using Microsoft.OpenApi.Models;

using Entities.Models;
using Contracts; 
using Repository;
using CustomerAPI.Presentation.Controllers; 

namespace CustomerAPI.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection services) => 
        services.AddCors(options => 
    { 
        options.AddPolicy("CorsPolicy", builder => 
        builder.AllowAnyOrigin() 
               .AllowAnyMethod() 
               .AllowAnyHeader() 
               .WithExposedHeaders("X-Pagination")); 
    });
    
    public static void ConfigureIIS(this IServiceCollection services) =>
        services.Configure<IISOptions>(options =>
        {
        });

    public static void ConfigureUnitOfWork(this IServiceCollection services) => 
        services.AddScoped<IUnitOfWork, UnitOfWork>();

    public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) => 
        services.AddDbContext<RepositoryContext>(opts =>    
            opts.UseSqlServer(
                configuration.GetConnectionString("AppDbConnection"),
                b => b.MigrationsAssembly("CustomerAPI"))
                );

    public static void ConfigureResponseCaching(this IServiceCollection services) => 
            services.AddResponseCaching();

    public static void ConfigureHttpCacheHeaders(this IServiceCollection services) => 
            services.AddHttpCacheHeaders(
                (expirationModelOptions) => 
                { 
                    expirationModelOptions.MaxAge = 65; 
                    expirationModelOptions.SharedMaxAge = 300;
                    //expirationModelOptions.CacheLocation = CacheLocation.Private; 
                }, 
                (validationModelOptions) => 
                { 
                    validationModelOptions.MustRevalidate = true; 
                });

    public static void ConfigureSwagger(this IServiceCollection services) 
    { 
        services.AddSwaggerGen(s => 
        { 
            s.SwaggerDoc("v1", new OpenApiInfo 
            { 
                Title = "Customer API", 
                Version = "v1",
                Description = "Customer API by Ali Sajadian", 
                TermsOfService = new Uri("https://example.com/terms"), 
                Contact = new OpenApiContact 
                { 
                    Name = "Ali Sajadian", 
                    Email = "sajadian.ali@gmail.com", 
                    Url = new Uri("https://github.com/AliSajadian"), 
                }, 
                License = new OpenApiLicense 
                { 
                    Name = "Customer API LICX", 
                    Url = new Uri("https://example.com/license"), 
                }
            }); 
            s.SwaggerDoc("v2", new OpenApiInfo { Title = "Customer API", Version = "v2" }); 
            // var xmlFile = $"{typeof(Presentation.AssemblyReference).Assembly.GetName().Name}.xml"; 
            // var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile); 
            // s.IncludeXmlComments(xmlPath);       
        });
    }
 }