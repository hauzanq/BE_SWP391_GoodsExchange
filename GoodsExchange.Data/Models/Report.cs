using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.Data.Models
{
    public class Report
    {
        public Guid ReportId { get; set; }
        public string Reason { get; set; }
        public DateTime CreateDate { get; set; }
        public Guid ReportingUserId { get; set; }
        public User ReportMade { get; set; }
        public Guid TargetUserId { get; set; }
        public User ReportReceived { get; set; }
        public Guid ProductId { get; set; }
        public Product Product { get; set; }
        public bool IsApprove { get; set; }
        public bool IsActive { get; set; }
    }
}
