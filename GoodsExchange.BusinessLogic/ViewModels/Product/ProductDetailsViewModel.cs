using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.ViewModels.Product
{
    public class ProductDetailsViewModel
    {
        public string ProductName { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }
        public DateTime ApprovedDate { get; set; }
        public string UserUpload { get; set; }
        public string UserImageUrl { get; set; }
        public int NumberOfRatings { get; set; }
        public float AverageNumberStars { get; set; }
        public string UserPhoneNumber { get; set; }
    }
}
