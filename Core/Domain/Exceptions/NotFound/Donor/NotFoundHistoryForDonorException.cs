using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFound.Donor
{
    public class NotFoundHistoryForDonorException(string name) : NotFoundException($"No history found for donor with name: {name}")
    {
    }
}
