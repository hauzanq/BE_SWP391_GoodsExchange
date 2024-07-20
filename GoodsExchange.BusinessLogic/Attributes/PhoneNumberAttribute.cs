using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace GoodsExchange.BusinessLogic.Attributes
{
    public class PhoneNumberAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("Phone number is required.");
            }

            string phoneNumber = value.ToString();
            string pattern = @"^0\d{9}$"; 

            if (!Regex.IsMatch(phoneNumber, pattern))
            {
                return new ValidationResult("Phone number must start with 0 and be exactly 10 digits long.");
            }

            return ValidationResult.Success;
        }
    }
}
