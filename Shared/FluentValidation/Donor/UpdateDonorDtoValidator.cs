using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shared.DonorDtos;

namespace Shared.FluentValidation.Donor
{
   public class UpdateDonorDtoValidator : AbstractValidator<UpdateDonorDto>
    {
        public UpdateDonorDtoValidator()
        {
            RuleFor(d => d.Id)
            .GreaterThan(0).WithMessage("Id must be greater than 0");

            RuleFor(d => d.Name)
                .NotEmpty().WithMessage("Name is required.")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");
            RuleFor(d => d.Phone)
                .NotEmpty().WithMessage("Phone is required.")
                .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Phone number is not valid.");
            RuleFor(d => d.Address)
                .NotEmpty().WithMessage("Address is required.")
                .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");
            RuleFor(d => d.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Email is not valid.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");
            RuleFor(d => d.BloodType)
                .IsInEnum().WithMessage("BloodType is not valid.");
            RuleFor(d => d.LastDonation)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("LastDonation cannot be in the future.")
                .When(d => d.LastDonation.HasValue);

        }
    }
}
