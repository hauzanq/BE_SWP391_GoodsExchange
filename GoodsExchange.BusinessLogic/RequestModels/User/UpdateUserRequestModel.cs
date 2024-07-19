using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace GoodsExchange.BusinessLogic.RequestModels.User
{
    public class UpdateUserRequestModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(gmail\.com|fpt\.edu\.vn)$", ErrorMessage = "Email domain must be gmail.com or fpt.edu.vn.")]
        public string Email { get; set; }

        
        public DateTime DateOfBirth { get; set; }

      
        [RegularExpression(@"^\d{10,11}$", ErrorMessage = "Phone Number must be 10 or 11 digits.")]
        public string PhoneNumber { get; set; }
        public IFormFile Image { get; set; }
        public string UserName { get; set; }

        
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
       ErrorMessage = "Password must be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; } 
    }
}
