using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using UpMo.Data;
using UpMo.Entities;
using UpMo.Services.Abstract;
using UpMo.Services.Concrete;

namespace UpMo.WebAPI.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthWithIdentity(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:SECRET"])),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                };
            });

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyz123456789";
                options.User.RequireUniqueEmail = true;
                options.Password.RequiredLength = 4;
                options.Password.RequireDigit = false;
                options.Password.RequiredUniqueChars = 0;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
            }).AddEntityFrameworkStores<UpMoContext>();

            return services;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection services)
        {
            services.AddSingleton<ITokenService, TokenService>();

            services.AddTransient<IUserService, UserService>();
            services.AddTransient<IOrganizationService, OrganizationService>();
            services.AddTransient<IOrganizationManagerService, OrganizationManagerService>();
            services.AddTransient<IMonitorService, MonitorService>();
            services.AddTransient<IMonitorPostDataService, MonitorPostDataService>();



            services.AddAutoMapper(typeof(AutoMapperProfile));

            return services;
        }

        public static IServiceCollection AddSwaggerWithSecurityScheme(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                var securityScheme = new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    Reference = new OpenApiReference
                    {
                        Id = JwtBearerDefaults.AuthenticationScheme,
                        Type = ReferenceType.SecurityScheme
                    }
                };
                c.AddSecurityDefinition(securityScheme.Reference.Id, securityScheme);
                c.AddSecurityRequirement(new OpenApiSecurityRequirement { { securityScheme, new string[] { } } });
            });

            return services;
        }
    }
}