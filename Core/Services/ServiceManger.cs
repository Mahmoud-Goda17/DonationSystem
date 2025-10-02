using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Domain.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Services.Abstractions;
using Shared.FluentValidation.Auth;

namespace Services
{
    public class ServiceManger(IunitOfWork iunitOfWork,UserManager<AppUser> userManager,RoleManager<IdentityRole> roleManager, IOptions<JwtOpt> options) : IServiceManger
    {
        public IDonorService DonorService { get; } = new DonorService(iunitOfWork);

        public IDonationRequestService DonationRequestService { get; }=new DonationRequestService(iunitOfWork);
        public IDonationHistoryService DonationHistoryService { get; } = new DonationHistoryService(iunitOfWork);

        public INotificationLogService NotificationLogService { get; }=new NotificationLogService(iunitOfWork);

        public IAuthService AuthService { get; } = new AuthService(userManager, roleManager, options);
    }
}
