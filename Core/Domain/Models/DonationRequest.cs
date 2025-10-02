using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class DonationRequest: BaseEntity
    {
        
        public string PatientName { get; set; }

        
        public BloodTypes NeededBloodType { get; set; }


        [ Range(1, 20)]
        public int RequiredUnits { get; set; } // Number of blood units needed

        public string HospitalName { get; set; }

        public string HospitalAddress { get; set; }


        public DateTime RequestDate { get; set; } = DateTime.UtcNow; // Date when the request was made
        public bool IsUrgent { get; set; } // Indicates if the request is urgent(مستعجل)


        [ Phone]
        public string ContactPhone { get; set; } // Phone number for contact

        [ EmailAddress]
        public string ContactEmail { get; set; } // Email for contact

        
        public Status Status { get; set; }
        public ICollection<DonationHistory> DonationHistories { get; set; } = new HashSet<DonationHistory>();
        public ICollection<NotificationLog> NotificationLogs { get; set; } = new HashSet<NotificationLog>();
    }
}
