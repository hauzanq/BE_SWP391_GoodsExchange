using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.RequestModels.Product
{
    public class GetAllProductRequestModel
    {
        public string? ProductName { get; set; }
        public float? MinPrice { get; set; }
        public float? MaxPrice { get; set; }
        public DateTime? StartUploadDate { get; set; }
        public DateTime? EndUploadDate { get; set; }
        public DateTime? StartApprovedDate { get; set; }
        public DateTime? EndApprovedDate { get; set; }
        public string? CategoryName { get; set; }
    }
}
