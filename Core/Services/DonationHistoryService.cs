using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound.DonationHistory;
using Domain.Exceptions.NotFound.DonationRequest;
using Domain.Exceptions.NotFound.Donor;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Shared.DonationHistoryDtos;

namespace Services
{
    public class DonationHistoryService(IunitOfWork iunitOfWork) : IDonationHistoryService
    {
        public async Task<IEnumerable<DonationHistoryDto>> GetAllDonationHistoryAsync()
        {
            var donationhistory = await iunitOfWork.GetRepository<DonationHistory>().GetAllAsync();
            if (!donationhistory.Any()) throw new NoDonationHistoryFound();
            return donationhistory.Select(dh => dh.ToDonationHistoryDto());
        }

        public async Task<DonationHistoryDto?> GetDonationHistoryById(int id)
        {
            var donationhistory = await iunitOfWork.GetRepository<DonationHistory>().GetByIdAsync(id);
            if (donationhistory == null) throw new DonationHistoryNotFoundExceotion(id);
            return donationhistory.ToDonationHistoryDto();
        }

        public async Task<int> AddDonationHistory(CreateDonationHistoryDto createDonationHistoryDto)
        {
            var donor = await iunitOfWork.GetRepository<Donor>().GetByIdAsync(createDonationHistoryDto.DonorId);
            if (donor == null) throw new DonorNotFoundException(createDonationHistoryDto.DonorId);

            var threeMonthsAgo = DateTime.UtcNow.AddMonths(-3);
            if (donor.LastDonation != null && donor.LastDonation > threeMonthsAgo)
                throw new NotAvalibaleDonorBadRequestException(donor.Id);


            var donationRequest = await iunitOfWork.GetRepository<DonationRequest>().GetByIdAsync(createDonationHistoryDto.DonationRequestId);
            if (donationRequest == null) throw new DonationRequestNotFoundException(createDonationHistoryDto.DonationRequestId);

            var totalDonatedUnits = await  iunitOfWork.GetRepository<DonationHistory>()
                .GetAll(dh => dh.DonationRequestId == createDonationHistoryDto.DonationRequestId)
                .SumAsync(dh=>dh.BloodUnitsDonated);
            totalDonatedUnits += createDonationHistoryDto.BloodUnitsDonated;
            if (totalDonatedUnits >= donationRequest.RequiredUnits)
            {
                donationRequest.Status = Status.fulfilled;
                iunitOfWork.GetRepository<DonationRequest>().Update(donationRequest);
            }

            var donationHistory = createDonationHistoryDto.ToCreateDonationHistory();
            await iunitOfWork.GetRepository<DonationHistory>().AddAsync(donationHistory);

            donor.LastDonation = DateTime.UtcNow;
            iunitOfWork.GetRepository<Donor>().Update(donor);

            return await iunitOfWork.SaveChangesAsync();
        }

        public async Task<int>  UpdateDonationHistory(UpdateDonationHistoryDto updateDonationHistoryDto)
        {
            var donationhistory = await iunitOfWork.GetRepository<DonationHistory>().GetByIdAsync(updateDonationHistoryDto.Id);
            if (donationhistory == null) throw new DonationHistoryNotFoundExceotion(updateDonationHistoryDto.Id);

            var donor = await iunitOfWork.GetRepository<Donor>().GetByIdAsync(updateDonationHistoryDto.DonorId);
            if (donor == null) throw new DonorNotFoundException(updateDonationHistoryDto.DonorId);


            var donationRequest = await iunitOfWork.GetRepository<DonationRequest>().GetByIdAsync(updateDonationHistoryDto.DonationRequestId);
            if (donationRequest == null) throw new DonationRequestNotFoundException(updateDonationHistoryDto.DonationRequestId);

            var totalDonatedUnits = await iunitOfWork.GetRepository<DonationHistory>()
                .GetAll(dh => dh.DonationRequestId == updateDonationHistoryDto.DonationRequestId)
                .SumAsync(dh => dh.BloodUnitsDonated);
            totalDonatedUnits += updateDonationHistoryDto.BloodUnitsDonated;
            if (totalDonatedUnits >= donationRequest.RequiredUnits)
            {
                donationRequest.Status = Status.fulfilled;
                iunitOfWork.GetRepository<DonationRequest>().Update(donationRequest);
            }
            updateDonationHistoryDto.ToUpdateDonationHistory(donationhistory);
            iunitOfWork.GetRepository<DonationHistory>().Update(donationhistory);
            return await iunitOfWork.SaveChangesAsync();
        }
        public async Task<int> DeleteDonationHistoryAsync(int id)
        {
            var deleted = await iunitOfWork.GetRepository<DonationHistory>().DeleteAsync(id);
            if (!deleted) throw new DonationHistoryNotFoundExceotion(id);
            return await iunitOfWork.SaveChangesAsync();
        }
    }
}
