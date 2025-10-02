using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.NotificationLogDtos
{
    public class NotificationLogDto
    {
        public string Channel { get; set; }

        public bool IsSuccess { get; set; }
        public string ErrorMessage { get; set; }

    }
}
