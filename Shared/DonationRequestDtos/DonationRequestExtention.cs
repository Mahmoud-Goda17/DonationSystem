using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Shared.DonationRequestDtos
{
   public static class DonationRequestExtention
    {
        public static DonationRequestDto ToDonationRequestDto(this DonationRequest donationRequest)
        {
            return new DonationRequestDto
            {
                ContactEmail = donationRequest.ContactEmail,
                ContactPhone = donationRequest.ContactPhone,
                Status = donationRequest.Status,
                HospitalAddress = donationRequest.HospitalAddress,
                HospitalName = donationRequest.HospitalName,
                IsUrgent = donationRequest.IsUrgent,
                NeededBloodType = donationRequest.NeededBloodType,
                PatientName = donationRequest.PatientName,
                RequiredUnits = donationRequest.RequiredUnits,
            };
        }
        public static DonationRequest ToCreateDonationRequest(this CreateDonationRequestDto createDonationRequestDto)
        {
            return new DonationRequest
            {
                ContactEmail = createDonationRequestDto.ContactEmail,
                ContactPhone = createDonationRequestDto.ContactPhone,
                Status = createDonationRequestDto.Status,
                HospitalAddress = createDonationRequestDto.HospitalAddress,
                HospitalName = createDonationRequestDto.HospitalName,
                IsUrgent = createDonationRequestDto.IsUrgent,
                NeededBloodType = createDonationRequestDto.NeededBloodType,
                PatientName = createDonationRequestDto.PatientName,
                RequiredUnits = createDonationRequestDto.RequiredUnits,
            };
        }
        public static void ToUpdateDonationRequest(UpdateDonationRequestDto UpdateDonationRequestDto, DonationRequest donationRequest)
        {
            donationRequest.Id = UpdateDonationRequestDto.Id;
            donationRequest.ContactEmail = UpdateDonationRequestDto.ContactEmail;
            donationRequest.ContactPhone = UpdateDonationRequestDto.ContactPhone;
            donationRequest.Status = UpdateDonationRequestDto.Status;
            donationRequest.HospitalAddress = UpdateDonationRequestDto.HospitalAddress;
            donationRequest.HospitalName = UpdateDonationRequestDto.HospitalName;
            donationRequest.IsUrgent = UpdateDonationRequestDto.IsUrgent;
            donationRequest.NeededBloodType = UpdateDonationRequestDto.NeededBloodType;
            donationRequest.PatientName = UpdateDonationRequestDto.PatientName;
            donationRequest.RequiredUnits = UpdateDonationRequestDto.RequiredUnits;
        }
    }
}
