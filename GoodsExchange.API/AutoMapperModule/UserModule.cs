using AutoMapper;
using GoodsExchange.BusinessLogic.RequestModels.User;
using GoodsExchange.BusinessLogic.ViewModels;
using GoodsExchange.Data.Models;


namespace GoodsExchange.BusinessLogic.AutoMapperModule
{

    public static class UserModule
    {
        public static void ConfigUserModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<User, UserViewModel>().ReverseMap();
            mc.CreateMap<User, CreateUserRequestModel>().ReverseMap();
            mc.CreateMap<User, UpdateUserRequestModel>().ReverseMap();
        }
    }

}
