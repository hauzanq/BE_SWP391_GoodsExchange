using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.Services;
using GoodsExchange.BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/users")]
    public class UserController : ControllerBase
    {

        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost]
        public ActionResult<UserViewModel> CreateUser(CreateUserRequestModel userCreate)
        {
            var userCreated = _userService.CreateUser(userCreate);

            if (userCreated == null)
            {
                return NotFound("");
            }
            return userCreated;
        }

        [HttpGet]
        public ActionResult<List<UserViewModel>> GetAll()
        {
            var userList = _userService.GetAll();

            if (userList == null)
            {
                return NotFound("");
            }
            return userList;
        }

        [HttpGet("idTmp")]
        public ActionResult<UserViewModel> GetById(int idTmp)
        {
            var userDetail = _userService.GetById(idTmp);

            if (userDetail == null)
            {
                return NotFound("");
            }
            return userDetail;
        }

        [HttpDelete]
        public ActionResult<bool> DeleteUser(int idTmp)
        {
            var check = _userService.DeleteUser(idTmp);

            if (check == false)
            {
                return NotFound("");
            }
            return check;
        }

        [HttpPut]
        public ActionResult<UserViewModel> UpdateUser(UpdateUserRequestModel userCreate)
        {
            var userUpdated = _userService.UpdateUser(userCreate);

            if (userUpdated == null)
            {
                return NotFound("");
            }
            return userUpdated;
        }
    }

}
