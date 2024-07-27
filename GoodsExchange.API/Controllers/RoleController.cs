using GoodsExchange.API.Middlewares;
using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.Services.Interface;
using GoodsExchange.BusinessLogic.ViewModels.Role;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace GoodsExchange.API.Controllers
{
    [ApiController]
    [Route("/api/v1/roles")]
    public class RoleController : ControllerBase
    {
        private readonly IServiceWrapper _serviceWrapper;
        public RoleController(IServiceWrapper serviceWrapper)
        {
            _serviceWrapper = serviceWrapper;
        }

        [HttpGet]
        [ProducesResponseType(typeof(ResponseModel<PageResult<RoleViewModel>>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDetails), (int)HttpStatusCode.BadRequest)]
        public async Task<ActionResult<RoleViewModel>> GetAll()
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _serviceWrapper.RoleServices.GetAllRole();
            return Ok(result);
        }
    }
}
