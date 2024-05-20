using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.Data.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string ProductImageUrl { get; set; }
        public float Price { get; set; }
        public bool Status { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Rate Rate { get; set; }
        public Report Report { get; set; }
    }
}
