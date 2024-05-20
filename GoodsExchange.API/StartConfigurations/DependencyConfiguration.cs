using GoodsExchange.BusinessLogic.Services;
using GoodsExchange.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace GoodsExchange.BusinessLogic.Generations.DependencyInjection
{
    public static class DependencyConfiguration
    {
        public static void InitializerDependency(this IServiceCollection services)
        {

            services.AddScoped<DbContext, GoodsExchangeDbContext>();
            
            services.AddScoped<ICategoryService, CategoryService>();
        
            services.AddScoped<IProductService, ProductService>();
        
            services.AddScoped<IRatingService, RatingService>();
        
            services.AddScoped<IReportService, ReportService>();
        
            services.AddScoped<IRoleService, RoleService>();
        
            services.AddScoped<IUserService, UserService>();
        }
    }
}
