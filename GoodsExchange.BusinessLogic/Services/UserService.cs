using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.ViewModels;

namespace GoodsExchange.BusinessLogic.Services
{

    public interface IUserService {
        public UserViewModel CreateUser(CreateUserRequestModel userCreate);
        public UserViewModel UpdateUser(UpdateUserRequestModel userUpdate);
        public bool DeleteUser(int idTmp);
        public List<UserViewModel> GetAll();
        public UserViewModel GetById(int idTmp);
    }

    public class UserService : IUserService {

        public UserService()
        {
                
        }
        public UserViewModel CreateUser(CreateUserRequestModel userCreate)
        {
            throw new NotImplementedException();
        }

        public UserViewModel UpdateUser(UpdateUserRequestModel userUpdate) 
        {
            throw new NotImplementedException();
        }

        public bool DeleteUser(int idTmp)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> GetAll() 
        {
            throw new NotImplementedException();
        }

        public UserViewModel GetById(int idTmp) 
        {
            throw new NotImplementedException();
        }

    }

}
