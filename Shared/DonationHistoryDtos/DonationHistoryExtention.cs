using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Shared.DonationHistoryDtos
{
    public static class DonationHistoryExtention
    {
        public static DonationHistoryDto ToDonationHistoryDto(this DonationHistory donationHistory)
        {
            return new DonationHistoryDto
            {
                BloodUnitsDonated = donationHistory.BloodUnitsDonated,
                Notes = donationHistory.Notes,
            };
        }
        public static DonationHistory ToCreateDonationHistory(this CreateDonationHistoryDto createDonationHistoryDto)
        {
            return new DonationHistory
            {
                DonorId = createDonationHistoryDto.DonorId,
                DonationRequestId = createDonationHistoryDto.DonationRequestId,
                BloodUnitsDonated = createDonationHistoryDto.BloodUnitsDonated,
                Notes = createDonationHistoryDto.Notes,
            };
        }
        public static void ToUpdateDonationHistory(this UpdateDonationHistoryDto updateDonationHistoryDto,DonationHistory donationHistory )
        {
            donationHistory.Id = updateDonationHistoryDto.Id;
            donationHistory.DonorId = updateDonationHistoryDto.DonorId;
            donationHistory.DonationRequestId = updateDonationHistoryDto.DonationRequestId;
            donationHistory.BloodUnitsDonated = updateDonationHistoryDto.BloodUnitsDonated;
            donationHistory.Notes = updateDonationHistoryDto.Notes;
        }
    }
}
