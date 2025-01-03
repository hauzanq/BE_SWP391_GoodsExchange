﻿using GoodsExchange.Data.Enums;

namespace GoodsExchange.Data.Models
{
    public class Product
    {
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public ProductStatus Status { get; set; }
        public DateTime UploadDate { get; set; }
        public Guid UserUploadId { get; set; }
        public User UserUpload { get; set; }
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }
        public Rating Rate { get; set; }
        public List<Report> Reports { get; set; }
        public List<ProductImage> ProductImages { get; set; }
        public List<ExchangeRequest> ExchangeRequestsSent { get; set; }
        public List<ExchangeRequest> ExchangeRequestsReceived { get; set; }
    }
}
