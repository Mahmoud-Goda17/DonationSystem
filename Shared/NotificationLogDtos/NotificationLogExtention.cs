using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;

namespace Shared.NotificationLogDtos
{
    public static class NotificationLogExtention
    {
        public static NotificationLogDto ToNotificationLogDto(this NotificationLog notificationLog)
        {
            return new NotificationLogDto
            {
                Channel = notificationLog.Channel,
                IsSuccess = notificationLog.IsSuccess,
                ErrorMessage = notificationLog.ErrorMessage
            };
        }
    }
}
