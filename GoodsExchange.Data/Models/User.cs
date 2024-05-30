using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.Data.Models
{
    public class User
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string PhoneNumber { get; set; }
        public string UserImageUrl { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool Status { get; set; }
        public List<UserRole> UserRoles { get; set; }
        public List<Rate> RatingsGiven { get; set; }
        public List<Rate> RatingsReceived { get; set; }
        public List<Report> ReportsMade { get; set; }
        public List<Report> ReportsReceived { get; set; }
        public List<Product> Products { get; set; }
    }
}
