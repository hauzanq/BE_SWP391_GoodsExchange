using AutoMapper;
using GoodsExchange.BusinessLogic.RequestModels.Report;
using GoodsExchange.BusinessLogic.ViewModels;
using GoodsExchange.Data.Models;


namespace GoodsExchange.BusinessLogic.AutoMapperModule
{

    public static class ReportModule
    {
        public static void ConfigReportModule(this IMapperConfigurationExpression mc)
        {
            mc.CreateMap<Report, ReportViewModel>().ReverseMap();
            mc.CreateMap<Report, CreateReportRequestModel>().ReverseMap();
            mc.CreateMap<Report, UpdateReportRequestModel>().ReverseMap();
        }
    }

}
