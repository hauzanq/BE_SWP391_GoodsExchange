using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace GoodsExchange.BusinessLogic.Extensions
{
    public static class HttpContextAccessorExtensions
    {
        public static string GetCurrentUserName(this IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            return user?.FindFirstValue("username");
        }

        public static string GetCurrentUserId(this IHttpContextAccessor httpContextAccessor)
        {
            var user = httpContextAccessor.HttpContext?.User;
            return user?.FindFirstValue("id");
        }
    }
}
