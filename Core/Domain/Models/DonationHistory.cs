using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DonationHistory: BaseEntity
    {
        public Donor Donor { get; set; }

        public int DonorId { get; set; }

        public DonationRequest DonationRequest { get; set; }
        public int DonationRequestId { get; set; }

        [Range(1,10)]
        public int BloodUnitsDonated { get; set; }

        
        public DateTime DonationDate = DateTime.UtcNow;


        public string Notes { get; set; }
    }
}
