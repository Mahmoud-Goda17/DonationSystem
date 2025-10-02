using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistance.Data.Configurations
{
    public class DonationRequestConfiguration : IEntityTypeConfiguration<DonationRequest>
    {
        public void Configure(EntityTypeBuilder<DonationRequest> builder)
        {
            builder.HasKey(dr => dr.Id);
            builder.Property(dr => dr.PatientName).IsRequired().HasMaxLength(100);
            builder.Property(dr => dr.NeededBloodType).HasConversion<string>().IsRequired();
            builder.Property(dr => dr.RequiredUnits).IsRequired();
            builder.Property(dr => dr.HospitalName).IsRequired().HasMaxLength(200);
            builder.Property(dr => dr.HospitalAddress).IsRequired().HasMaxLength(300);
            builder.Property(dr => dr.ContactPhone).IsRequired().HasMaxLength(15);
            builder.Property(dr => dr.ContactEmail).HasMaxLength(100);
            builder.Property(dr => dr.Status).HasConversion<string>().IsRequired();
            builder.Property(dr => dr.RequestDate).IsRequired();
            builder.HasMany(dr => dr.DonationHistories)
                   .WithOne(dh => dh.DonationRequest)
                   .HasForeignKey(dh => dh.DonationRequestId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(dr => dr.NotificationLogs)
                     .WithOne(nl => nl.DonationRequest)
                     .HasForeignKey(nl => nl.DonationRequestId)
                     .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
