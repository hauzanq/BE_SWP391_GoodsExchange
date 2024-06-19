using GoodsExchange.BusinessLogic.Services.Implementation;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.Data.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace GoodsExchange.API.Extensions
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddAuthenticationServicesConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = configuration["JWTAuthentication:Issuer"],
                    ValidAudience = configuration["JWTAuthentication:Issuer"],
                    IssuerSigningKey =
                        new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTAuthentication:Key"]))

                };
            });
            return services;
        }
        public static IServiceCollection AddSwaggerConfigurations(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization using the Bearer scheme. \"Bearer\" is not needed.Just paste the jwt"
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
            return services;
        }

        public static IServiceCollection AddDbContextsWithConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection")!;
            services.AddDbContext<GoodsExchangeDbContext>(option => option.UseSqlServer(connectionString));
            return services;
        }

        public static IServiceCollection AddCorsConfigurations(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowReactApp", builder =>
                {
                    builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
                });
            });
            return services;
        }

        public static IServiceCollection RegisterService(this IServiceCollection services)
        {
            services.AddScoped<IServiceWrapper, ServiceWrapper>();
            
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IFirebaseStorageService, FirebaseStorageService>();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IRatingService, RatingService>();

            services.AddScoped<IReportService, ReportService>();

            services.AddScoped<IRoleService, RoleService>();
            

            return services;
        }
    }
}
