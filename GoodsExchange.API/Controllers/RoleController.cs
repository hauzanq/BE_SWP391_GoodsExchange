using GoodsExchange.BusinessLogic.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/roles")]
    public class RoleController : ControllerBase
    {

        private IRoleService  _roleService;

        public RoleController(IRoleService roleService)
        {
             _roleService = roleService;
        }

    }

}
