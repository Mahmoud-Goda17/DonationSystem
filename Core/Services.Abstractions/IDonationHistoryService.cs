using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DonationHistoryDtos;

namespace Services.Abstractions
{
    public interface IDonationHistoryService
    {
        Task<IEnumerable<DonationHistoryDto>> GetAllDonationHistoryAsync();
        Task<DonationHistoryDto?> GetDonationHistoryById(int id);   
        Task<int> AddDonationHistory (CreateDonationHistoryDto createDonationHistoryDto);
        Task<int> UpdateDonationHistory(UpdateDonationHistoryDto updateDonationHistoryDto);
        Task<int> DeleteDonationHistoryAsync(int id);
    }
}
