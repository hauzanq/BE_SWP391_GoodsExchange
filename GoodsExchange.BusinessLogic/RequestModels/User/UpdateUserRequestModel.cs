using GoodsExchange.BusinessLogic.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GoodsExchange.BusinessLogic.RequestModels.User
{
    public class UpdateUserRequestModel
    {
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(gmail\.com|fpt\.edu\.vn)$", ErrorMessage = "Email domain must be gmail.com or fpt.edu.vn.")]
        public string Email { get; set; }

        [CustomDateOfBirth]
        public DateTime DateOfBirth { get; set; }

        [PhoneNumber]
        public string PhoneNumber { get; set; }
    }
}
