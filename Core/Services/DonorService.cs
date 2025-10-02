using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound;
using Domain.Models;
using Services.Abstractions;
using Shared.DonorDtos;
using Microsoft.EntityFrameworkCore;
using Domain.Exceptions.NotFound.Donor;


namespace Services
{
    public class DonorService(IunitOfWork iunitOfWork) : IDonorService
    {
        public async Task<IEnumerable<DonorDto>> GetAllAsync()
        {
            var donors = await iunitOfWork.GetRepository<Donor>().GetAllAsync();
            if (!donors.Any()) throw new NoDonorFound();
            return donors.Select(d => d.ToDonorDto());
        }

        public async Task<DonorDto?> GetByIdAsync(int id)
        {
            var donor = await iunitOfWork.GetRepository<Donor>().GetByIdAsync(id);
            if (donor is null) throw new DonorNotFoundException(id);
            return donor.ToDonorDto();
        }

        public async Task<int> AddAsync(CreateDonorDto createDonorDto)
        {
            var exist = await iunitOfWork.GetRepository<Donor>().GetAll
                (d => d.Email == createDonorDto.Email 
                || d.Phone == createDonorDto.Phone).AnyAsync();
            if (exist) throw new ExcistDonorBadRequestException();
            var donor = createDonorDto.ToCreateDonor();
            await iunitOfWork.GetRepository<Donor>().AddAsync(donor);
            return await iunitOfWork.SaveChangesAsync();
        }
        public async Task<int> UpdateAsync(UpdateDonorDto updateDonorDto)
        {
            var donor = await iunitOfWork.GetRepository<Donor>().GetByIdAsync(updateDonorDto.Id);
            if (donor is null) throw new DonorNotFoundException(updateDonorDto.Id);
            var exist = await iunitOfWork.GetRepository<Donor>().GetAll
                (d =>( d.Email == updateDonorDto.Email
                || d.Phone == updateDonorDto.Phone)&&d.Id!=updateDonorDto.Id).AnyAsync();
            if (exist) throw new ExcistDonorBadRequestException();
            updateDonorDto.ToUpdateDonor(donor);
            iunitOfWork.GetRepository<Donor>().Update(donor);
            return await iunitOfWork.SaveChangesAsync();
        }
        public async Task<int> DeleteAsync(int id)
        {
            var deleted = await iunitOfWork.GetRepository<Donor>().DeleteAsync(id);
            if (!deleted) throw new DonorNotFoundException(id);
            return await iunitOfWork.SaveChangesAsync();

        }

        public async Task<IEnumerable<DonorDto>> GetByBloodTypeAsync(BloodTypes type)
        {
            var donors = await iunitOfWork
                    .GetRepository<Donor>()
                    .GetAll(d => d.BloodType == type)
                    .ToListAsync();
            if (!donors.Any()) throw new NoDonorFound();

            return donors.Select(d => d.ToDonorDto());
        }
        public async Task<IEnumerable<DonorDto>> GetAvalibaleDonorsAsync()
        {
            var threeMonthsAgo = DateTime.UtcNow.AddMonths(-3);

            var donors = await iunitOfWork
                .GetRepository<Donor>()
                .GetAll(d => d.LastDonation == null || d.LastDonation <= threeMonthsAgo)
                .ToListAsync();
            if (!donors.Any()) throw new NoDonorFound();

            return donors.Select(d => d.ToDonorDto());
        }
        public async Task<IEnumerable<DonorDto>> GetDonorsByLocationAsync(string address)
        {
            var donors = await iunitOfWork
                .GetRepository<Donor>()
                .GetAll(d => d.Address.Contains(address))
                .ToListAsync();
            if (!donors.Any()) throw new NoDonorFound();
            return donors.Select(d => d.ToDonorDto());
        }

        public async Task<IEnumerable<DonorDto>> GetDonorsByEmailAsync(string email)
        {
            var donors = await iunitOfWork
                .GetRepository<Donor>()
                .GetAll(d => d.Email.Contains(email))
                .ToListAsync();
            if (!donors.Any()) throw new NoDonorFound();
            return donors.Select(d => d.ToDonorDto());

        }

        public async Task<DonorwithDonationHistoryDto> GetDonorsWithHistory(int id)
        {
            var dto = await iunitOfWork.GetRepository<Donor>().GetAll(d => d.Id == id)
                .Select(d => new DonorwithDonationHistoryDto
                {
                    Name = d.Name,
                    Address = d.Address,
                    BloodType = d.BloodType,
                    Email = d.Email,
                    LastDonation = d.LastDonation,
                    Phone = d.Phone,
                    DonationHistoryForDonors = d.DonationHistories.Select(h => new DonationHistoryForDonor
                    {
                        BloodUnitsDonated = h.BloodUnitsDonated,
                        Notes = h.Notes
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            if (dto is null) throw new DonorNotFoundException(id);
            if(dto.DonationHistoryForDonors.Count == 0) throw new NotFoundHistoryForDonorException(dto.Name);
            return dto;
        }
    }
}
