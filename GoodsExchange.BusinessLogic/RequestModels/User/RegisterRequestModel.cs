using GoodsExchange.BusinessLogic.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GoodsExchange.BusinessLogic.RequestModels.User
{
    public class RegisterRequestModel
    {
        [Required(ErrorMessage = "First Name is required.")]
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name is required.")]
        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid Email Address.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@(gmail\.com|fpt\.edu\.vn)$", ErrorMessage = "Email domain must be gmail.com or fpt.edu.vn.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Date of Birth is required.")]
        [CustomDateOfBirth(ErrorMessage = "Date of Birth cannot be in the future.")]
        public DateTime DateOfBirth { get; set; }

        [Required(ErrorMessage = "Phone Number is required.")]
        [PhoneNumber]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "User Name is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "Password is required.")]
        [DataType(DataType.Password)]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be between 6 and 100 characters.")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{6,}$",
            ErrorMessage = "Password must be at least 6 characters long and contain at least one uppercase letter, one lowercase letter, one digit, and one special character.")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Confirm Password is required.")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password do not match.")]
        public string ConfirmPassword { get; set; }
    }
}
