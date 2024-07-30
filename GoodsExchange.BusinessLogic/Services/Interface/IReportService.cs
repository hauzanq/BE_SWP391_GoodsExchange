using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Report;
using GoodsExchange.BusinessLogic.ViewModels.Report;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IReportService
    {
        Task<ResponseModel<ReportViewModel>> SendReportAsync(CreateReportRequestModel request);
        Task<ResponseModel<bool>> ApproveReportAsync(Guid id);
        Task<ResponseModel<bool>> DenyReportAsync(Guid id);
        Task<ResponseModel<PageResult<ReportViewModel>>> GetReportsAsync(PagingRequestModel paging, ReportsRequestModel request);
        Task<ResponseModel<ReportViewModel>> GetReportByIdAsync(Guid id);
        Task<int> CountReportsReceivedOfUserAsync(Guid userId);
    }
}
