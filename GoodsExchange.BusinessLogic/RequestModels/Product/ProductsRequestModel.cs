using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.RequestModels.Product
{
    public class ProductsRequestModel
    {
        public string? ProductName { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "Min Price must be a positive number.")]
        public float? MinPrice { get; set; }

        [Range(0, float.MaxValue, ErrorMessage = "Max Price must be a positive number.")]
        public float? MaxPrice { get; set; }
        public DateTime? StartUploadDate { get; set; }
        public DateTime? EndUploadDate { get; set; }
        public DateTime? StartApprovedDate { get; set; }
        public bool? IsApproved { get; set; }

        public bool? IsActive { get; set; }
        public DateTime? EndApprovedDate { get; set; }
        public string? CategoryName { get; set; }
    }
}
