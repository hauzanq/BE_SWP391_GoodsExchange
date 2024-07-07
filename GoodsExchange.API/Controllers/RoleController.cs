using GoodsExchange.API.Middlewares;
using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Role;
using GoodsExchange.Data.Context;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoodsExchange.API.Controllers
{
    [ApiController]
    [Route("/api/v1/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        // GET: api/Role
        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<PageResult<RoleViewModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RoleViewModel>> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _roleService.GetAllRole();
            return Ok(result);
        }

     
      
    }
}
