using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class Donor:BaseEntity
    {
        public string Name { get; set; }
        [Phone]
        public string Phone { get; set; }
        public string Address { get; set; }
        [ EmailAddress]
        public string Email { get; set; }
        public DateTime? LastDonation { get; set; }
        public DateTime CreateedAt { get; set; }= DateTime.UtcNow;

        public BloodTypes BloodType { get; set; }
        public ICollection<DonationHistory> DonationHistories { get; set; } = new HashSet<DonationHistory>();
        public ICollection<NotificationLog> NotificationLogs { get; set; } = new HashSet<NotificationLog>();
    }
}
