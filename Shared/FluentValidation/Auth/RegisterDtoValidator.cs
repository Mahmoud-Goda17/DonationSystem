using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shared.AuthDtos;

namespace Shared.FluentValidation.Auth
{
    public class RegisterDtoValidator: AbstractValidator<RegisterDto>
    {
        public RegisterDtoValidator()
        {
            RuleFor(r => r.Username)
                .NotEmpty().WithMessage("Username is required")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters")
                .MaximumLength(20).WithMessage("Username cannot exceed 20 characters");
            RuleFor(r => r.DisplayName)
                .NotEmpty().WithMessage("DisplayName is required")
                .MaximumLength(50).WithMessage("DisplayName cannot exceed 50 characters");
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
