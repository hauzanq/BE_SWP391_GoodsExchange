using GoodsExchange.BusinessLogic.Common;
using GoodsExchange.BusinessLogic.RequestModels.Report;
using GoodsExchange.BusinessLogic.ViewModels.Report;

namespace GoodsExchange.BusinessLogic.Services.Interface
{
    public interface IReportService
    {
        Task<ResponseModel<ReportViewModel>> SendReport(CreateReportRequestModel request);
        Task<ResponseModel<bool>> ApproveReport(Guid id);
        Task<ResponseModel<bool>> DenyReport(Guid id);
        Task<ResponseModel<PageResult<ReportViewModel>>> GetReports(PagingRequestModel paging, ReportsRequestModel request);
        Task<ResponseModel<ReportViewModel>> GetById(Guid id);
        Task<int> CountReportsReceivedOfUserAsync(Guid userId);
    }
}
