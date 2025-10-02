using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest
{
    public class ExcistDonorBadRequestException() : BadRequestException("Donor with the same email or phone already ")
    {
    }
}
