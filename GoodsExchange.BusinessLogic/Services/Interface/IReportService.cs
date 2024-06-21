using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Report;
using GoodsExchange.BusinessLogic.ViewModels.Report;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IReportService
    {
        Task<ApiResult<ReportViewModel>> SendReport(CreateReportRequestModel request);
        Task<ApiResult<bool>> ApproveReport(Guid id);
        Task<ApiResult<bool>> DenyReport(Guid id);
        Task<PageResult<ReportViewModel>> GetAll(PagingRequestModel paging, ReportsRequestModel request);
        Task<ApiResult<ReportViewModel>> GetById(Guid id);
    }
}
