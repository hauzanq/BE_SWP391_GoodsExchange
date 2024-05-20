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

        [HttpPost]
        public async Task<ActionResult<RoleViewModel>> CreateRole(CreateRoleRequestModel roleCreate)
        {
            var roleCreated = await _roleService.CreateRole(roleCreate);

            if (roleCreated == null)
            {
                return NotFound("");
            }
            return roleCreated;
        }

        [HttpGet]
        public async Task<ActionResult<List<RoleViewModel>>> GetAll()
        {
            var roleList = await _roleService.GetAll();

            if (roleList == null)
            {
                return NotFound("");
            }
            return roleList;
        }

        [HttpGet("idTmp")]
        public async Task<ActionResult<RoleViewModel>> GetById(int idTmp)
        {
            var roleDetail = await _roleService.GetById(idTmp);

            if (roleDetail == null)
            {
                return NotFound("");
            }
            return roleDetail;
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteRole(int idTmp)
        {
            var check = await _roleService.DeleteRole(idTmp);

            if (check == false)
            {
                return NotFound("");
            }
            return check;
        }

        [HttpPut]
        public async Task<ActionResult<RoleViewModel>> UpdateRole(UpdateRoleRequestModel roleCreate)
        {
            var roleUpdated = await _roleService.UpdateRole(roleCreate);

            if (roleUpdated == null)
            {
                return NotFound("");
            }
            return roleUpdated;
        }
    }

}
