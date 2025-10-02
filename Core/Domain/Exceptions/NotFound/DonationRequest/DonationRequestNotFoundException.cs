using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFound.DonationRequest
{
    public class DonationRequestNotFoundException(int id):NotFoundException($"Donation Request with Id {id} was not found.")
    {
    }
}
