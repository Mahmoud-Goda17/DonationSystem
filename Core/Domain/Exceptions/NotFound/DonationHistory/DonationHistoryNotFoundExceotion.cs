using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFound.DonationHistory
{
    public class DonationHistoryNotFoundExceotion(int id):NotFoundException($"DonationHistory with id {id} not found")
    {
    }
}
