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

        private IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public ActionResult<RoleViewModel> CreateRole(CreateRoleRequestModel roleCreate)
        {
            var roleCreated = _roleService.CreateRole(roleCreate);

            if (roleCreated == null)
            {
                return NotFound("");
            }
            return roleCreated;
        }

        [HttpGet]
        public ActionResult<List<RoleViewModel>> GetAll()
        {
            var roleList = _roleService.GetAll();

            if (roleList == null)
            {
                return NotFound("");
            }
            return roleList;
        }

        [HttpGet("idTmp")]
        public ActionResult<RoleViewModel> GetById(int idTmp)
        {
            var roleDetail = _roleService.GetById(idTmp);

            if (roleDetail == null)
            {
                return NotFound("");
            }
            return roleDetail;
        }

        [HttpDelete]
        public ActionResult<bool> DeleteRole(int idTmp)
        {
            var check = _roleService.DeleteRole(idTmp);

            if (check == false)
            {
                return NotFound("");
            }
            return check;
        }

        [HttpPut]
        public ActionResult<RoleViewModel> UpdateRole(UpdateRoleRequestModel roleCreate)
        {
            var roleUpdated = _roleService.UpdateRole(roleCreate);

            if (roleUpdated == null)
            {
                return NotFound("");
            }
            return roleUpdated;
        }
    }

}
