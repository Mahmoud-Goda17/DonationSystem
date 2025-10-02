using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Shared.DonorDtos
{
    public static class DonorExtention
    {
        public static DonorDto ToDonorDto(this Donor donor)
        {
            return new DonorDto
            {
                Name = donor.Name,
                Phone = donor.Phone,
                Address = donor.Address,
                Email = donor.Email,
                LastDonation = donor.LastDonation,
                BloodType = donor.BloodType
            };
        }
        public static Donor ToCreateDonor(this CreateDonorDto createDonorDto)
        {
            return new Donor
            {
                Name = createDonorDto.Name,
                Phone = createDonorDto.Phone,
                Address = createDonorDto.Address,
                Email = createDonorDto.Email,
                LastDonation = createDonorDto.LastDonation,
                BloodType = createDonorDto.BloodType

            };
        }
        public static void ToUpdateDonor(this UpdateDonorDto updateDonorDto, Donor donor)
        {
            donor.Name = updateDonorDto.Name;
            donor.Phone = updateDonorDto.Phone;
            donor.Address = updateDonorDto.Address;
            donor.Email = updateDonorDto.Email;
            donor.LastDonation = updateDonorDto.LastDonation;
            donor.BloodType = updateDonorDto.BloodType;
            
        }
    }
}
