using AutoMapper;
using GoodsExchange.BusinessLogic.RequestModels.Product;
using GoodsExchange.BusinessLogic.ViewModels;
using GoodsExchange.Data.Models;


namespace GoodsExchange.BusinessLogic.AutoMapperModule
{

    public static class ProductModule
    {
        public static void ConfigProductModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Product, ProductViewModel>().ReverseMap();
            mc.CreateMap<Product, CreateProductRequestModel>().ReverseMap();
            mc.CreateMap<Product, UpdateProductRequestModel>().ReverseMap();
        }
    }

}
