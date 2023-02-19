using Core.Interfaces.Repositories;
using Core.Interfaces.Services;
using Core.Services;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Security.Principal;
using System.Text;
using Core.Models;
using Core.Utilities;
using Microsoft.OpenApi.Models;

namespace Web.Extensions
{
    internal static class ServicesInjection
    {
        public static void AddAuth(this IServiceCollection services, ConfigurationManager configManager)
        {
            services.AddIdentity<User, IdentityRole<Guid>>(opt =>
            {
                // Some of this fields are set to false only for easier testing purposes, because this is only a test project
                opt.Password.RequiredLength = 6;
                opt.Password.RequireNonAlphanumeric = false;
                opt.Password.RequireLowercase = false;
                opt.Password.RequireUppercase = false;
                opt.Password.RequireDigit = false; 
                opt.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<MeetupsDbContext>();

            services.AddAuthentication(authOpt =>
            {
                authOpt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOpt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                authOpt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, jwtOpt =>
                {
                    jwtOpt.TokenValidationParameters = new()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = configManager["Authentication:Jwt:Issuer"],
                        ValidAudience = configManager["Authentication:Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configManager["Authentication:Jwt:Key"]))
                    };
                });

            services.AddAuthorization();

            services.Configure<JwtConfigModel>(configManager.GetSection("Authentication").GetSection("Jwt"));
            services.Configure<JwtConfigModel>(opt =>
                opt.Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configManager["Authentication:Jwt:Key"])));
        }

        public static void AddInfrastructure(this IServiceCollection services, string dbConnString)
        {
            services.AddDbContext<MeetupsDbContext>(opt =>
            {
                opt.UseSqlServer(dbConnString);
            });

            services.AddScoped<IMeetupsRepository, MeetupsRepository>();
            services.AddScoped<ITokensRepository, TokensRepository>();
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddScoped<IMeetupsService, MeetupsService>();
            services.AddScoped<IUsersService, UsersService>();
            services.AddScoped<ITokensService, TokensService>();
        }

        public static void AddUtilities(this IServiceCollection services, ConfigurationManager configManager)
        {
            services.AddAutoMapper(typeof(MapperProfile));

            services.AddMemoryCache();

            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MEETUPS-API", Version = "v1" });
                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 1safsfsdfdfd\"",
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                        new string[] { }
                    }
                });
            });
        }
    }
}
