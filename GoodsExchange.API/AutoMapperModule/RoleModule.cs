using AutoMapper;
using GoodsExchange.BusinessLogic.RequestModels.Role;
using GoodsExchange.BusinessLogic.ViewModels;
using GoodsExchange.Data.Models;


namespace GoodsExchange.BusinessLogic.AutoMapperModule
{

    public static class RoleModule
    {
        public static void ConfigRoleModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Role, RoleViewModel>().ReverseMap();
            mc.CreateMap<Role, CreateRoleRequestModel>().ReverseMap();
            mc.CreateMap<Role, UpdateRoleRequestModel>().ReverseMap();
        }
    }

}
