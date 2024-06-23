using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Report;
using GoodsExchange.BusinessLogic.ViewModels.Report;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IReportService
    {
        Task<EntityResponse<ReportViewModel>> SendReport(CreateReportRequestModel request);
        Task<EntityResponse<bool>> ApproveReport(Guid id);
        Task<EntityResponse<bool>> DenyReport(Guid id);
        Task<PageResult<ReportViewModel>> GetReports(PagingRequestModel paging, ReportsRequestModel request);
        Task<EntityResponse<ReportViewModel>> GetById(Guid id);
    }
}
