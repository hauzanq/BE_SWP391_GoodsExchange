using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.ViewModels.Category
{
    public class CategoriesDetailViewModel
    {
        public Guid CategoryId { get; set; }
        public string CategoryName { get; set; }
        public Guid ProductID { get; set; }
        public string ProductName { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }
        public DateTime ApprovedDate { get; set; }
        public Guid UserUploadId { get; set; }
       

    }
}
