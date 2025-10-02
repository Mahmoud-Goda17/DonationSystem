using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Shared.DonorDtos
{
    public class DonorDto
    {
        public string Name { get; set; }
        
        public string Phone { get; set; }
        public string Address { get; set; }
        
        public string Email { get; set; }
        public DateTime? LastDonation { get; set; }
        public BloodTypes BloodType { get; set; }

    }
}
