using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shared.DonationRequestDtos;

namespace Shared.FluentValidation.DonationRequest
{
    public class CreateDonationRequestDtoValidator:AbstractValidator<CreateDonationRequestDto>
    {
        public CreateDonationRequestDtoValidator()
        {
            RuleFor(dr=>dr.PatientName)
                .NotEmpty().WithMessage("Name is Require")
                .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(dr => dr.ContactPhone)
                 .NotEmpty().WithMessage("Phone is required.")
                 .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Phone number is not valid.");

            RuleFor(dr => dr.HospitalAddress)
                 .NotEmpty().WithMessage("Address is required.")
                 .MaximumLength(200).WithMessage("Address cannot exceed 200 characters.");


            RuleFor(dr => dr.ContactEmail)
               .NotEmpty().WithMessage("Email is required.")
               .EmailAddress().WithMessage("Email is not valid.")
               .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(dr => dr.NeededBloodType)
               .IsInEnum().WithMessage("BloodType is not valid.");

            RuleFor(dr => dr.Status)
               .IsInEnum().WithMessage("Status is not valid.");

            RuleFor(dr => dr.HospitalName)
               .NotEmpty().WithMessage("Name is Require")
               .MaximumLength(100).WithMessage("Name cannot exceed 100 characters.");

            RuleFor(dr => dr.RequiredUnits)
                .NotEmpty().WithMessage("counts is require");


        }
    }
}
