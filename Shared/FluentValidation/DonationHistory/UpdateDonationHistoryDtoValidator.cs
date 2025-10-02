using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Shared.DonationHistoryDtos;

namespace Shared.FluentValidation.DonationHistory
{
    public class UpdateDonationHistoryDtoValidator: AbstractValidator<UpdateDonationHistoryDto>
    {
        public UpdateDonationHistoryDtoValidator()
        {
            RuleFor(dh => dh.Id)
                .NotEmpty().WithMessage("Id is required")
                .GreaterThan(0).WithMessage("Id must be greater than 0");
            RuleFor(dh => dh.DonorId)
                .NotEmpty().WithMessage("DonorId is required")
                .GreaterThan(0).WithMessage("DonorId must be greater than 0");
            RuleFor(dh => dh.DonationRequestId)
                .NotEmpty().WithMessage("DonationRequestId is required")
                .GreaterThan(0).WithMessage("DonationRequestId must be greater than 0");
            RuleFor(dh => dh.BloodUnitsDonated)
                .NotEmpty().WithMessage("BloodUnitsDonated is required")
                .GreaterThan(0).WithMessage("BloodUnitsDonated must be greater than 0");
            RuleFor(dh => dh.Notes)
                .MaximumLength(500).WithMessage("Notes cannot exceed 500 characters")
                .When(dh => !string.IsNullOrEmpty(dh.Notes));
        }
    }
}
