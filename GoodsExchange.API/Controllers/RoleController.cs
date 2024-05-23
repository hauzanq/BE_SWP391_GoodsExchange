using GoodsExchange.BusinessLogic.RequestModels.Role;
using GoodsExchange.BusinessLogic.ViewModels.Role;
using GoodsExchange.BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using GoodsExchange.BusinessLogic.ViewModels;

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
