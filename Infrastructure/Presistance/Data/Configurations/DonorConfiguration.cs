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
    public class DonorConfiguration : IEntityTypeConfiguration<Donor>
    {
        public void Configure(EntityTypeBuilder<Donor> builder)
        {
            builder.HasKey(d => d.Id);
            builder.Property(d => d.BloodType).HasConversion<string>().IsRequired();
            builder.Property(d => d.Name).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Phone).IsRequired().HasMaxLength(15);
            builder.Property(d => d.Address).IsRequired().HasMaxLength(200);
            builder.Property(d => d.Email).IsRequired().HasMaxLength(100);
            builder.HasMany(d => d.DonationHistories)
                   .WithOne(dh => dh.Donor)
                   .HasForeignKey(dh => dh.DonorId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(d => d.NotificationLogs)
                   .WithOne(nl => nl.Donor)
                   .HasForeignKey(nl => nl.DonorId)
                   .OnDelete(DeleteBehavior.Cascade);
            builder.HasIndex(d => d.Email).IsUnique();
            builder.HasIndex(d => d.Phone).IsUnique();

        }
    }
}
