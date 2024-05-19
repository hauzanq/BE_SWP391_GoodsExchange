using AutoMapper;
using GoodsExchange.BusinessLogic.AutoMapperModule;
using GoodsExchange.BusinessLogic.Services;
using GoodsExchange.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Generations.DependencyInjection
{
    public static class DependencyInjectionResolverGen
    {
        public static void InitializerDependencyInjection(this IServiceCollection services)
        {
            services.AddScoped<DbContext, GoodsExchangeDbContext>();
        
            services.AddScoped<ICategoryService, CategoryService>();
        
            services.AddScoped<IProductService, ProductService>();
        
            services.AddScoped<IRatingService, RatingService>();
        
            services.AddScoped<IReportService, ReportService>();
        
            services.AddScoped<IRoleService, RoleService>();
        
            services.AddScoped<IUserService, UserService>();

            services.AddAutoMapper(cfg => cfg.ConfigUserModule());
        }
    }
}
