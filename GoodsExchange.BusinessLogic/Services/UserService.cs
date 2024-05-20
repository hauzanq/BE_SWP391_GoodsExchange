using AutoMapper;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.ViewModels;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;

namespace GoodsExchange.BusinessLogic.Services
{

    public interface IUserService
    {
        Task<UserViewModel> CreateUser(CreateUserRequestModel userCreate);
        Task<UserViewModel> UpdateUser(UpdateUserRequestModel userUpdate);
        Task<bool> DeleteUser(int idTmp);
        Task<List<UserViewModel>> GetAll();
        Task<UserViewModel> GetById(int idTmp);
    }

    public class UserService : IUserService
    {
        public Task<UserViewModel> CreateUser(CreateUserRequestModel userCreate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteUser(int idTmp)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<UserViewModel> GetById(int idTmp)
        {
            throw new NotImplementedException();
        }

        public Task<UserViewModel> UpdateUser(UpdateUserRequestModel userUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
