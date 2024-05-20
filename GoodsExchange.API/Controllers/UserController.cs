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
        public async Task<ActionResult<UserViewModel>> CreateUser(CreateUserRequestModel userCreate)
        {
            var userCreated = await _userService.CreateUser(userCreate);

            if (userCreated == null)
            {
                return NotFound("");
            }
            return userCreated;
        }

        [HttpGet]
        public async Task<ActionResult<List<UserViewModel>>> GetAll()
        {
            var userList = await  _userService.GetAll();

            if (userList == null)
            {
                return NotFound("");
            }
            return userList;
        }

        [HttpGet("idTmp")]
        public async Task<ActionResult<UserViewModel>> GetById(int idTmp)
        {
            var userDetail =await _userService.GetById(idTmp);

            if (userDetail == null)
            {
                return NotFound("");
            }
            return userDetail;
        }

        [HttpDelete]
        public async Task< ActionResult<bool>> DeleteUser(int idTmp)
        {
            var check = await _userService.DeleteUser(idTmp);

            if (check == false)
            {
                return NotFound("");
            }
            return check;
        }

        [HttpPut]
        public async Task <ActionResult<UserViewModel>> UpdateUser(UpdateUserRequestModel userCreate)
        {
            var userUpdated = await  _userService.UpdateUser(userCreate);

            if (userUpdated == null)
            {
                return NotFound("");
            }
            return userUpdated;
        }
    }

}
