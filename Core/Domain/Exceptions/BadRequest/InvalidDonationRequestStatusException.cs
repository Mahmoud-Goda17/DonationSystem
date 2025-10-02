using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Domain.Exceptions.BadRequest
{
    public class InvalidDonationRequestStatusException(int id,Status status):BadRequestException($"Cannot complete Donation Request with Id {id} because its status is {status}, only pending requests can be completed.")
    {
    }
}
