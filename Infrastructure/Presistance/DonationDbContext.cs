using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Domain.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace Presistance
{
   public class DonationDbContext :IdentityDbContext<AppUser>
    {
        public DonationDbContext(DbContextOptions<DonationDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(AssemblyRefrence).Assembly);
        }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<DonationRequest> DonationRequests { get; set; }
        public DbSet<DonationHistory> DonationHistories { get; set; }
        public DbSet<NotificationLog> NotificationLogs { get; set; }
    }
}
