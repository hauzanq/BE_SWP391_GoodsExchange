using FluentValidation;
using GoodsExchange.BusinessLogic.RequestModels.User;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsExchange.BusinessLogic.Validations.User
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {
            RuleFor(x => x.UserName).NotEmpty().WithMessage("UserName is required");
            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is required")
                                    .MinimumLength(6).WithMessage("Password is at least 6 characters");
        }
    }
}
