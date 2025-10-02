using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Shared.DonorDtos;

namespace Services.Abstractions
{
    public interface IDonorService
    {
        Task<IEnumerable<DonorDto>> GetAllAsync();
        Task<DonorDto?> GetByIdAsync(int id);
        Task<int> AddAsync(CreateDonorDto createDonorDto);
        Task<int> UpdateAsync( UpdateDonorDto updateDonorDto);
        Task<int> DeleteAsync(int id);
        Task<IEnumerable<DonorDto>> GetByBloodTypeAsync(BloodTypes type);
        Task<IEnumerable<DonorDto>> GetAvalibaleDonorsAsync();
        Task<IEnumerable<DonorDto>> GetDonorsByLocationAsync(string address);
        Task<IEnumerable<DonorDto>> GetDonorsByEmailAsync(string email);
        Task<DonorwithDonationHistoryDto> GetDonorsWithHistory(int id);

    }
}
