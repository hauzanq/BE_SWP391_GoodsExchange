using Firebase.Storage;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.Services.Interface;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace GoodsExchange.BusinessLogic.Services.Implementation
{
    public class FirebaseStorageService : IFirebaseStorageService
    {
        private readonly IConfiguration _configuration;

        public FirebaseStorageService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> UploadProductImage(string sellerName, string productName, IFormFile image)
        {
            string firebaseBucket = _configuration["Firebase:StorageBucket"];

            var firebaseStorage = new FirebaseStorage(firebaseBucket);

            string filename = Guid.NewGuid().ToString() + "_" + image.FileName;

            var task = firebaseStorage.Child("Products").Child(sellerName).Child(productName).Child(filename);

            var stream = image.OpenReadStream();
            await task.PutAsync(stream);

            return await task.GetDownloadUrlAsync();
        }

        public async Task<string> UploadUserImage(string fullName, IFormFile image)
        {
            string firebaseBucket = _configuration["Firebase:StorageBucket"];

            var firebaseStorage = new FirebaseStorage(firebaseBucket);

            string filename = Guid.NewGuid().ToString() + "_" + image.FileName;

            var task = firebaseStorage.Child("Users").Child(fullName).Child(filename);

            var stream = image.OpenReadStream();
            await task.PutAsync(stream);

            return await task.GetDownloadUrlAsync();
        }
    }
}
