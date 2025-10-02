using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.BadRequest
{
    public class ExistDonatioRequestException():BadRequestException("DonationRequest with the same email or phone already ")
    {
    }
}
