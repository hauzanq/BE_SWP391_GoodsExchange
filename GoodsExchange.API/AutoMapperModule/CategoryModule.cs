using AutoMapper;
using GoodsExchange.BusinessLogic.RequestModels.Category;
using GoodsExchange.BusinessLogic.ViewModels;
using GoodsExchange.BusinessLogic.ViewModels.Category;
using GoodsExchange.Data.Models;


namespace GoodsExchange.BusinessLogic.AutoMapperModule 
{

   public static class CategoryModule
    {
        public static void ConfigCategoryModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Category, CategoryViewModel>().ReverseMap();
            mc.CreateMap<Category, CreateCategoryRequestModel>().ReverseMap();
            mc.CreateMap<Category, UpdateCategoryRequestModel>().ReverseMap();
        }
    }

}
