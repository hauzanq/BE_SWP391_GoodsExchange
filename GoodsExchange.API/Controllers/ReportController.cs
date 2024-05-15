using GoodsExchange.BusinessLogic.RequestModels.Report;
using GoodsExchange.BusinessLogic.Services;
using GoodsExchange.BusinessLogic.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GoodsExchange.API.Controllers
{

    [ApiController]
    [Route("/api/v1/reports")]
    public class ReportController : ControllerBase
    {

        private IReportService _reportService;

        public ReportController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpPost]
        public ActionResult<ReportViewModel> CreateReport(CreateReportRequestModel reportCreate)
        {
            var reportCreated = _reportService.CreateReport(reportCreate);

            if (reportCreated == null)
            {
                return NotFound("");
            }
            return reportCreated;
        }

        [HttpGet]
        public ActionResult<List<ReportViewModel>> GetAll()
        {
            var reportList = _reportService.GetAll();

            if (reportList == null)
            {
                return NotFound("");
            }
            return reportList;
        }

        [HttpGet("idTmp")]
        public ActionResult<ReportViewModel> GetById(int idTmp)
        {
            var reportDetail = _reportService.GetById(idTmp);

            if (reportDetail == null)
            {
                return NotFound("");
            }
            return reportDetail;
        }

        [HttpDelete]
        public ActionResult<bool> DeleteReport(int idTmp)
        {
            var check = _reportService.DeleteReport(idTmp);

            if (check == false)
            {
                return NotFound("");
            }
            return check;
        }

        [HttpPut]
        public ActionResult<ReportViewModel> UpdateReport(UpdateReportRequestModel reportCreate)
        {
            var reportUpdated = _reportService.UpdateReport(reportCreate);

            if (reportUpdated == null)
            {
                return NotFound("");
            }
            return reportUpdated;
        }
    }

}
