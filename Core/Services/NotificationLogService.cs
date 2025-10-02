using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Exceptions.NotFound.DonationRequest;
using Domain.Exceptions.NotFound.Donor;
using Domain.Exceptions.NotFound.NotificationLog;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Services.Abstractions;
using Shared.NotificationLogDtos;

namespace Services
{
    public class NotificationLogService(IunitOfWork iunitOfWork) : INotificationLogService
    {
        public async Task<IEnumerable<NotificationLogDto>> GetAllNotificationLogAsync()
        {
            var notifications = await iunitOfWork.GetRepository<NotificationLog>().GetAllAsync();
            if (!notifications.Any()) throw new NoNotificationLogFound();
            return notifications.Select(n => n.ToNotificationLogDto());
        }
        public async Task<NotificationLogDto?> GetNotificationLogByIdAsync(int id)
        {
            var notification = await iunitOfWork.GetRepository<NotificationLog>().GetByIdAsync(id);
            if (notification is null) throw new NotificationLogNotFoundException(id);
            return notification.ToNotificationLogDto();
        }

        public async Task<IEnumerable<NotificationLogDto>> GetAllNotificationLogByDonorIdAsync(int donorid)
        {
            var donor = await iunitOfWork.GetRepository<Donor>().GetByIdAsync(donorid);
            if (donor is null) throw new DonorNotFoundException(donorid);

            var notifications = await iunitOfWork.GetRepository<NotificationLog>()
                .GetAll(n=>n.DonorId==donorid).ToListAsync();
            if (!notifications.Any()) throw new NoNotificationLogFound();

            return notifications.Select(n => n.ToNotificationLogDto());
        }

        public async Task<IEnumerable<NotificationLogDto>> GetAllNotificationLogByRequestIdAsync(int requestid)
        {
            var request = await iunitOfWork.GetRepository<DonationRequest>().GetByIdAsync(requestid);
            if (request is null) throw new DonationRequestNotFoundException(requestid);

            var notifications = await iunitOfWork.GetRepository<NotificationLog>()
                        .GetAll(n => n.DonationRequestId == requestid).ToListAsync();
            if (!notifications.Any()) throw new NoNotificationLogFound();

            return notifications.Select(n => n.ToNotificationLogDto());

        }

    }
}
