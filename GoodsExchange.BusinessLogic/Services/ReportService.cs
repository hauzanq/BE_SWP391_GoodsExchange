using GoodsExchange.BusinessLogic.RequestModels.Report;
using GoodsExchange.BusinessLogic.ViewModels;

namespace GoodsExchange.BusinessLogic.Services
{

    public interface IReportService
    {
        public ReportViewModel CreateReport(CreateReportRequestModel reportCreate);
        public ReportViewModel UpdateReport(UpdateReportRequestModel reportUpdate);
        public bool DeleteReport(int idTmp);
        public List<ReportViewModel> GetAll();
        public ReportViewModel GetById(int idTmp);
    }

    public class ReportService : IReportService
    {
        public ReportService()
        {
                
        }
        public ReportViewModel CreateReport(CreateReportRequestModel reportCreate)
        {
            throw new NotImplementedException();
        }

        public ReportViewModel UpdateReport(UpdateReportRequestModel reportUpdate)
        {
            throw new NotImplementedException();
        }

        public bool DeleteReport(int idTmp)
        {
            throw new NotImplementedException();
        }

        public List<ReportViewModel> GetAll()
        {
            throw new NotImplementedException();
        }

        public ReportViewModel GetById(int idTmp)
        {
            throw new NotImplementedException();
        }

    }

}
