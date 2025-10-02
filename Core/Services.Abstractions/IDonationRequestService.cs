using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared.DonationRequestDtos;

namespace Services.Abstractions
{
    public interface IDonationRequestService
    {
        Task<IEnumerable<DonationRequestDto>> GetAllDonationRequestAsync();
        Task<DonationRequestDto?> GetDonationRequestByIdAsync(int id);
        Task<int> AddDonationRequestAsync(CreateDonationRequestDto createDonationRequestDto);
        Task<int> UpdateDonationRequestAsync(UpdateDonationRequestDto updateDonationRequestDto);
        Task<int> DeleteDonationRequestAsync(int id);
        Task<IEnumerable<DonationRequestDto>> GetDonationRequestByUrgent(bool urgent );
        Task<IEnumerable<DonationRequestDto>> GetDonationRequestByBloodType(BloodTypes bloodType);
        Task<IEnumerable<DonationRequestDto>> GetDonationRequesrByHospitalName(string hospitalName);
        Task<IEnumerable<DonationRequestDto>> GetPendingRequest();
        Task<int> CompleteRequestAsync(int id);
        Task<int> CancelRequestAsync(int id);
    }
}
