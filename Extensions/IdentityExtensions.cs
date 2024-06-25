using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using NewsAPI.Entities;

namespace NewsAPI.Extensions
{
    public static class IdentityExtensions
    {
        public static IServiceCollection addJWT(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentityCore<AppUser>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequiredLength = 8;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireUppercase = false;
                options.Password.RequireLowercase = false;
                options.User.RequireUniqueEmail = true;

            })
                .AddRoles<AppRole>()
                .AddRoleManager<RoleManager<AppRole>>()
                .AddRoleValidator<RoleValidator<AppRole>>()
                .AddEntityFrameworkStores<Context>()
                .AddDefaultTokenProviders();

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = false,
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["secretKey"] ?? "YouShouldChangeThis"))
                    };
                });

            services.AddAuthorizationBuilder()
                .AddPolicy(AppPolicy.RequireAdminRole, policy => policy.RequireRole(AppRoles.Admin.Name()))
                .AddPolicy(AppPolicy.RequireModeratorRole, policy => policy.RequireRole(AppRoles.Admin.Name(), AppRoles.Moderator.Name()));


            return services;
        }
    }
}