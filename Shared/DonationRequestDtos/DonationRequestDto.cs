using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Shared.DonationRequestDtos
{
    public class DonationRequestDto
    {
        public string PatientName { get; set; }
        public BloodTypes NeededBloodType { get; set; }

        public int RequiredUnits { get; set; }

        public string HospitalName { get; set; }

        public string HospitalAddress { get; set; }
        public bool IsUrgent { get; set; } 

        public string ContactPhone { get; set; } 
        public string ContactEmail { get; set; } 
        public Status Status { get; set; }

    }
}
