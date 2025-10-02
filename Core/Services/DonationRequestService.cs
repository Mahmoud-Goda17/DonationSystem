using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Exceptions.BadRequest;
using Domain.Exceptions.NotFound.DonationRequest;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Shared.DonationRequestDtos;

namespace Services
{
    public class DonationRequestService(IunitOfWork iunitOfWork) : IDonationRequestService
    {
        public async Task<IEnumerable<DonationRequestDto>> GetAllDonationRequestAsync()
        {
            var donationRequests = await iunitOfWork.GetRepository<DonationRequest>().GetAllAsync();
            if (!donationRequests.Any()) throw new NoDonationRequestFound();
            return donationRequests.Select(dr => dr.ToDonationRequestDto());
        }
        public async Task<DonationRequestDto?> GetDonationRequestByIdAsync(int id)
        {
            var donationRequest = await iunitOfWork.GetRepository<DonationRequest>().GetByIdAsync(id);
            if (donationRequest is null) throw new DonationRequestNotFoundException(id);
            return donationRequest.ToDonationRequestDto();
        }
        public async Task<int> AddDonationRequestAsync(CreateDonationRequestDto createDonationRequestDto)
        {
            var exist = await iunitOfWork.GetRepository<DonationRequest>()
                .GetAll(dr => dr.ContactEmail == createDonationRequestDto.ContactEmail
                           || dr.ContactPhone == createDonationRequestDto.ContactPhone)
                .AnyAsync();
            if (exist) throw new ExistDonatioRequestException();

            var avalibaleDonor= await iunitOfWork.GetRepository<Donor>()
                                .GetAll(d=>d.BloodType==createDonationRequestDto.NeededBloodType)
                                .AnyAsync();
            if (!avalibaleDonor) throw new NotAvalibaleDonorForRequestNotFoundException(createDonationRequestDto.NeededBloodType);
            var donationRequest = createDonationRequestDto.ToCreateDonationRequest();
            await iunitOfWork.GetRepository<DonationRequest>().AddAsync(donationRequest);
            return  await iunitOfWork.SaveChangesAsync();

        }
        public async Task<int> UpdateDonationRequestAsync(UpdateDonationRequestDto updateDonationRequestDto)
        {
            var donationRequest = await iunitOfWork.GetRepository<DonationRequest>().GetByIdAsync(updateDonationRequestDto.Id);
            if (donationRequest is null) throw new DonationRequestNotFoundException(updateDonationRequestDto.Id);

            var exist = await iunitOfWork.GetRepository<DonationRequest>()
                .GetAll(dr => (dr.ContactEmail == updateDonationRequestDto.ContactEmail
                           || dr.ContactPhone == updateDonationRequestDto.ContactPhone)&&dr.Id!=updateDonationRequestDto.Id)
                .AnyAsync();
            if (exist) throw new ExistDonatioRequestException();

            var avalibaleDonor = await iunitOfWork.GetRepository<Donor>()
                                .GetAll(d => d.BloodType == updateDonationRequestDto.NeededBloodType)
                                .AnyAsync();
            if (!avalibaleDonor) throw new NotAvalibaleDonorForRequestNotFoundException(updateDonationRequestDto.NeededBloodType);

            DonationRequestExtention.ToUpdateDonationRequest(updateDonationRequestDto, donationRequest);
            iunitOfWork.GetRepository<DonationRequest>().Update(donationRequest);
            return await iunitOfWork.SaveChangesAsync();
        }
        public async Task<int> DeleteDonationRequestAsync(int id)
        {
            var deleted = await iunitOfWork.GetRepository<DonationRequest>().DeleteAsync(id);
            if (!deleted) throw new DonationRequestNotFoundException(id);
            return await iunitOfWork.SaveChangesAsync();
        }
        public async Task<IEnumerable<DonationRequestDto>> GetDonationRequestByUrgent(bool urgent)
        {
            var donationRequests = await iunitOfWork.GetRepository<DonationRequest>()
                                        .GetAll(dr => dr.IsUrgent == urgent)
                                        .ToListAsync();
            if (!donationRequests.Any()) throw new NoDonationRequestFound();
            return donationRequests.Select(dr => dr.ToDonationRequestDto());
        }
        public async Task<IEnumerable<DonationRequestDto>> GetDonationRequestByBloodType(BloodTypes bloodType)
        {
            var donationRequests = await iunitOfWork.GetRepository<DonationRequest>()
                                        .GetAll(dr => dr.NeededBloodType == bloodType)
                                        .ToListAsync();
            if (!donationRequests.Any()) throw new NoDonationRequestFound();
            return donationRequests.Select(dr => dr.ToDonationRequestDto());
        }
        public async Task<IEnumerable<DonationRequestDto>> GetDonationRequesrByHospitalName(string hospitalName)
        {
            var donationRequests = await iunitOfWork.GetRepository<DonationRequest>()
                                        .GetAll(dr => dr.HospitalName == hospitalName)
                                        .ToListAsync();
            if (!donationRequests.Any()) throw new NoDonationRequestFound();
            return donationRequests.Select(dr => dr.ToDonationRequestDto());
        }
        public async Task<IEnumerable<DonationRequestDto>> GetPendingRequest()
        {
            var donationRequests = await iunitOfWork.GetRepository<DonationRequest>()
                                        .GetAll(dr => dr.Status == Status.pending)
                                        .ToListAsync();
            if (!donationRequests.Any()) throw new NoDonationRequestFound();
            return donationRequests.Select(dr => dr.ToDonationRequestDto());
        }
        public async Task<int> CompleteRequestAsync(int id)
        {
            var donationRequest = await iunitOfWork.GetRepository<DonationRequest>().GetByIdAsync(id);
            if (donationRequest is null)
                throw new DonationRequestNotFoundException(id);

            if (donationRequest.Status != Status.pending)
                throw new InvalidDonationRequestStatusException(id, donationRequest.Status);

            donationRequest.Status = Status.fulfilled;
            iunitOfWork.GetRepository<DonationRequest>().Update(donationRequest);

            return await iunitOfWork.SaveChangesAsync();
        }
        public async Task<int> CancelRequestAsync(int id)
        {
            var donationRequest = await iunitOfWork.GetRepository<DonationRequest>().GetByIdAsync(id);
            if (donationRequest is null)
                throw new DonationRequestNotFoundException(id);
            if (donationRequest.Status != Status.pending)
                throw new InvalidDonationRequestStatusException(id, donationRequest.Status);
            donationRequest.Status = Status.cancelled;
            iunitOfWork.GetRepository<DonationRequest>().Update(donationRequest);
            return await iunitOfWork.SaveChangesAsync();
        }


    }
}
