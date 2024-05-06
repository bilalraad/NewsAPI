using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NewsAPI.Helpers;
using NewsAPI.Interfaces;
using NewsAPI.Repositories;
using NewsAPI.Services;
using Swashbuckle.AspNetCore.Filters;

namespace NewsAPI.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
        {
            opt.SwaggerDoc("v1", new OpenApiInfo { Title = "NewsApi", Version = "v1" });

            // opt.AddSecurityDefinition(
            //     "",
            //     new OpenApiSecurityScheme
            //     {
            //         Flows = new OpenApiOAuthFlows
            //         {
            //             Password = new OpenApiOAuthFlow
            //             {
            //                 TokenUrl = new Uri("/api/auth/login", UriKind.Relative),
            //                 Scopes = new Dictionary<string, string>
            //                 {
            //                     { "api", "Access the API" }
            //                 }

            //             }
            //         }

            //     }

            // );

            opt.OperationFilter<SecurityRequirementsOperationFilter>();
            // using System.Reflection;
            var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            opt.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
        });
            //*: this is add to init the database
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            //*: this is add to detect all controllers
            services.AddControllers();
            services.AddCors();
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            services.Configure<CloudinarySettings>(configuration.GetSection("CloudinarySettings"));
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<INewsRepository, NewsRepository>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUploadRepository, UploadRepository>();
            // services.AddScoped<PhotoResolver>();

            return services;
        }
    }
}