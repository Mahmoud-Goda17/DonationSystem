using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DonationHistoryDtos
{
    public class UpdateDonationHistoryDto
    {
        public int Id { get; set; }
        public int DonorId { get; set; }
        public int DonationRequestId { get; set; }
        public int BloodUnitsDonated { get; set; }
        public string Notes { get; set; }

    }
}
