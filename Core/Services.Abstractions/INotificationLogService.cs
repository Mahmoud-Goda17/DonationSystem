using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.NotificationLogDtos;

namespace Services.Abstractions
{
    public interface INotificationLogService
    {
        Task<IEnumerable<NotificationLogDto>> GetAllNotificationLogAsync();
        Task<NotificationLogDto?> GetNotificationLogByIdAsync(int id);
        Task<IEnumerable<NotificationLogDto>> GetAllNotificationLogByDonorIdAsync(int donorid);
        Task<IEnumerable<NotificationLogDto>> GetAllNotificationLogByRequestIdAsync(int requestid);

    }
}
