using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest
{
    public class NotAvalibaleDonorBadRequestException(int id) : BadRequestException($"This donor With Id {id} is not avalibale to donate now")
    {
    }
}
