using Microsoft.EntityFrameworkCore;
using NewsAPI.Interfaces;
using NewsAPI.Services;

namespace NewsAPI.Extensions
{
    public static class AppServiceExtensions
    {
        public static IServiceCollection AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            //*: this is add to init the database
            services.AddDbContext<Context>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
            });

            //*: this is add to detect all controllers
            services.AddControllers();
            services.AddCors();
            services.AddScoped<ITokenService, TokenService>();
            return services;
        }
    }
}