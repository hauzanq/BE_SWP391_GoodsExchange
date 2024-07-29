using GoodsExchange.BusinessLogic.RequestModels.User;
using Microsoft.AspNetCore.Http;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IFirebaseStorageService
    {
        Task<string> UploadProductImage(string sellerName, IFormFile image);
        Task<string> UploadUserImage(string fullName,IFormFile image);
    }
}
