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
        public async Task<ActionResult<ReportViewModel>> CreateReport(CreateReportRequestModel reportCreate)
        {
            var reportCreated =  await _reportService.CreateReport(reportCreate);

            if (reportCreated == null)
            {
                return NotFound("");
            }
            return reportCreated;
        }

        [HttpGet]
        public async Task<ActionResult<List<ReportViewModel>>> GetAll()
        {
            var reportList = await _reportService.GetAll();

            if (reportList == null)
            {
                return NotFound("");
            }
            return reportList;
        }

        [HttpGet("idTmp")]
        public async Task<ActionResult<ReportViewModel>> GetById(int idTmp)
        {
            var reportDetail = await _reportService.GetById(idTmp);

            if (reportDetail == null)
            {
                return NotFound("");
            }
            return reportDetail;
        }

        [HttpDelete]
        public async Task<ActionResult<bool>> DeleteReport(int idTmp)
        {
            var check = await _reportService.DeleteReport(idTmp);

            if (check == false)
            {
                return NotFound("");
            }
            return check;
        }

        [HttpPut]
        public async Task<ActionResult<ReportViewModel>> UpdateReport(UpdateReportRequestModel reportCreate)
        {
            var reportUpdated = await _reportService.UpdateReport(reportCreate);

            if (reportUpdated == null)
            {
                return NotFound("");
            }
            return reportUpdated;
        }
    }

}
