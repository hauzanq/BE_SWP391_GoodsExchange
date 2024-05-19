using AutoMapper;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.ViewModels;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;

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

        private readonly GoodsExchangeDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public UserService(GoodsExchangeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }
        public UserViewModel CreateUser(CreateUserRequestModel userCreate)
        {
            Role existedRole = _dbContext.Roles.Where(role => role.RoleName.ToUpper() == userCreate.RoleName.ToUpper()).FirstOrDefault();
            if (existedRole == null) {
                Console.WriteLine("role name not found!!");
            }
            User user = _mapper.Map<User>(userCreate);
            user.RoleId = existedRole.RoleId;
            _dbContext.Users.Add(user);
            _dbContext.SaveChanges();
            return _mapper.Map<UserViewModel>(user);
        }

        public UserViewModel UpdateUser(UpdateUserRequestModel userUpdate) 
        {
            User existedUser = _dbContext.Users.Where(user => user.UserId == userUpdate.UserId).FirstOrDefault();
            if (existedUser == null)
            {
                Console.WriteLine("User not found!!");
            }
            existedUser.RoleId = userUpdate.RoleId;
            existedUser.FirstName = userUpdate.FirstName;
            existedUser.LastName  = userUpdate.LastName;
            existedUser.Email = userUpdate.Email;
            existedUser.DateOfBirth = userUpdate.DateOfBirth;
            existedUser.PhoneNumber = userUpdate.PhoneNumber;
            existedUser.UserImageUrl= userUpdate.UserImageUrl;
            existedUser.UserName= userUpdate.UserName;
            existedUser.Password= userUpdate.Password;
            existedUser.Status = userUpdate.Status;
            _dbContext.Users.Update(existedUser);
            _dbContext.SaveChanges();
            return _mapper.Map<UserViewModel>(existedUser);
        }

        public bool DeleteUser(Guid idTmp)
        {
            throw new NotImplementedException();
        }

        public List<UserViewModel> GetAll() 
        {
            return _mapper.Map<List<UserViewModel>>(_dbContext.Users.ToList());
        }

        public UserViewModel GetById(int idTmp) 
        {
            throw new NotImplementedException();
        }

    }

}
