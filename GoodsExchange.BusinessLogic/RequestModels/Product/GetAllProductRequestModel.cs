using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.RequestModels.Product
{
    public class SearchRequestModel
    {
        public string? KeyWords { get; set; }
    }
    public class GetAllProductRequestModel
    {
        public string? ProductName { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public bool? Status { get; set; }
        public DateTime? StartUploadDate { get; set; }
        public DateTime? EndUploadDate { get; set; }
        public bool? IsApproved { get; set; }
        public DateTime? StartApprovedDate { get; set; }
        public DateTime? EndApprovedDate { get; set; }
        public string CategoryName { get; set; }
    }
}
