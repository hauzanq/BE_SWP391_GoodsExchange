using GoodsExchange.BusinessLogic.Services;

namespace GoodsExchange.API.StartConfigurations
{
    public static class RegisterServiceConfiguration
    {
        public static void RegisterService(this IServiceCollection services)
        {

            services.AddScoped<ICategoryService, CategoryService>();

            services.AddScoped<IProductService, ProductService>();

            services.AddScoped<IRatingService, RatingService>();

            services.AddScoped<IReportService, ReportService>();

            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IUserService, UserService>();
        }
    }
}
