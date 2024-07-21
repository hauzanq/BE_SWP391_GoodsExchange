using GoodsExchange.BusinessLogic.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GoodsExchange.BusinessLogic.RequestModels.User
{
    public class UpdateUserRequestModel
    {
        [StringLength(50, ErrorMessage = "First Name cannot exceed 50 characters.")]
        public string? FirstName { get; set; }

        [StringLength(50, ErrorMessage = "Last Name cannot exceed 50 characters.")]
        public string? LastName { get; set; }

        [CustomDateOfBirth]
        public DateTime? DateOfBirth { get; set; }

        [RegularExpression(@"^0\d{9}$", ErrorMessage = "Phone number must be valid.")]
        public string? PhoneNumber { get; set; }
    }
}
