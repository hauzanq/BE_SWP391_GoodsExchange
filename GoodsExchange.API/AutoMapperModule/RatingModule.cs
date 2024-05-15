using AutoMapper;
using GoodsExchange.BusinessLogic.RequestModels.Rating;
using GoodsExchange.BusinessLogic.ViewModels;
using GoodsExchange.Data.Models;


namespace GoodsExchange.BusinessLogic.AutoMapperModule
{

    public static class RatingModule
    {
        public static void ConfigRatingModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Rating, RatingViewModel>().ReverseMap();
            mc.CreateMap<Rating, CreateRatingRequestModel>().ReverseMap();
            mc.CreateMap<Rating, UpdateRatingRequestModel>().ReverseMap();
        }
    }

}
