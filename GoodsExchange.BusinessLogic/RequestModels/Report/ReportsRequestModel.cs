using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.RequestModels.Report
{
    public class ReportsRequestModel
    {
        public string? Sender { get; set; }
        public string? Receiver { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
    }
}
