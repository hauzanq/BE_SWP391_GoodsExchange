using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.RequestModels.User
{
    public  class UpdateProfileUserRequestModel
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

        //public string userName { get; set; }


    }
}
