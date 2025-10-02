using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shared.AuthDtos;

namespace Shared.FluentValidation.Auth
{
    public class LoginDtoValidator: AbstractValidator<LogInDto>
    {
        public LoginDtoValidator()
        {
            RuleFor(r => r.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters")
                .MaximumLength(100).WithMessage("Password cannot exceed 100 characters");
            RuleFor(r => r.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Email is not valid")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters");
        }
    }
}
