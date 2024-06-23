using GoodsExchange.API.Middlewares;

namespace GoodsExchange.API.Extensions
{
    public static class IApplicationBuilderExtension
    {
        public static IApplicationBuilder UseExceptionHandlingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
