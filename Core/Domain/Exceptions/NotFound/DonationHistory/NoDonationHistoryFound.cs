using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFound.DonationHistory
{
    public class NoDonationHistoryFound():NotFoundException("No DonationHistory Found")
    {
    }
}
