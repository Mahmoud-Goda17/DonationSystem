using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models
{
    public class NotificationLog : BaseEntity
    {
        public Donor Donor { get; set; }

        
        public int DonorId { get; set; }

        public DonationRequest DonationRequest { get; set; }


        public int DonationRequestId { get; set; }
        public string Channel { get; set; } = "SMS";//  Email, SMS,WhatsApp

       
        public DateTime SentAt { get; set; }
        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }
    }
}
