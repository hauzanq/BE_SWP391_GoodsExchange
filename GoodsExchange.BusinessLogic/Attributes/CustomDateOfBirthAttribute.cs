using System.ComponentModel.DataAnnotations;

namespace GoodsExchange.BusinessLogic.Attributes
{
    public class CustomDateOfBirthAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value is DateTime dateOfBirth)
            {
                if (dateOfBirth > DateTime.Today)
                {
                    return new ValidationResult(ErrorMessage ?? "Date of Birth cannot be in the future.");
                }
            }
            return ValidationResult.Success;
        }
    }
}
