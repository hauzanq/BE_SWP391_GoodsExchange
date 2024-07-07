using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GoodsExchange.Data.Context;
using GoodsExchange.Data.Models;
using GoodsExchange.API.Middlewares;
using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.ViewModels.Category;
using System.Net;
using GoodsExchange.BusinessLogic.ViewModels.Role;
using GoodsExchange.BusinessLogic.Services.Implementation;
using GoodsExchange.BusinessLogic.Services.Interface;

namespace GoodsExchange.API.Controllers
{
    [ApiController]
    [Route("/api/v1/roles")]
    public class RoleController : ControllerBase
    {
        private readonly GoodsExchangeDbContext _context;
        private readonly IRoleService _roleService;

        public RoleController(GoodsExchangeDbContext context, IRoleService roleService)
        {
            _context = context;
            _roleService = roleService;
        }

        // GET: api/Role
        [HttpGet]
        [ProducesResponseType(typeof(EntityResponse<PageResult<RoleViewModel>>), (int)HttpStatusCode.OK)]
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
