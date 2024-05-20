using GoodsExchange.BusinessLogic.RequestModels.Report;
using GoodsExchange.BusinessLogic.ViewModels;

namespace GoodsExchange.BusinessLogic.Services
{

    public interface IReportService
    {
        Task<ReportViewModel> CreateReport(CreateReportRequestModel reportCreate);
        Task<ReportViewModel> UpdateReport(UpdateReportRequestModel reportUpdate);
        Task<bool> DeleteReport(int idTmp);
        Task<List<ReportViewModel>> GetAll();
        Task<ReportViewModel> GetById(int idTmp);
    }

    public class ReportService : IReportService
    {
        public Task<ReportViewModel> CreateReport(CreateReportRequestModel reportCreate)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteReport(int idTmp)
        {
            throw new NotImplementedException();
        }

        public Task<List<ReportViewModel>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<ReportViewModel> GetById(int idTmp)
        {
            throw new NotImplementedException();
        }

        public Task<ReportViewModel> UpdateReport(UpdateReportRequestModel reportUpdate)
        {
            throw new NotImplementedException();
        }
    }
}
