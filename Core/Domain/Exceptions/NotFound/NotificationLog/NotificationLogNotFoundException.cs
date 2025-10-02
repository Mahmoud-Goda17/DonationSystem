using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Exceptions.NotFound.NotificationLog
{
    public class NotificationLogNotFoundException(int id) : NotFoundException($"Notification Log with id {id} not found")
    {
    }
}
