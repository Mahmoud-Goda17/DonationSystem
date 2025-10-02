using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Abstractions
{
    public interface IServiceManger
    {
        IDonorService DonorService { get; }
        IDonationRequestService DonationRequestService { get; }
        IDonationHistoryService DonationHistoryService { get; }
        INotificationLogService NotificationLogService { get; }
        IAuthService AuthService { get; }
    }
}
