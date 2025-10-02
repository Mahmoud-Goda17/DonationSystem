using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Exceptions.NotFound.DonationRequest
{
    public class NotAvalibaleDonorForRequestNotFoundException(BloodTypes bloodTypes):NotFoundException($"There is no avalible donor wiht this blood {bloodTypes}")
    {
    }
}
