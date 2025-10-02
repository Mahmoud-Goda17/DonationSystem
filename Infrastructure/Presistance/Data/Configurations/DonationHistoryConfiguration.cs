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
    public class DonationHistoryConfiguration : IEntityTypeConfiguration<DonationHistory>
    {
        public void Configure(EntityTypeBuilder<DonationHistory> builder)
        {
            builder.HasKey(dh => dh.Id);
            builder.Property(dh => dh.BloodUnitsDonated).IsRequired();
            builder.Property(dh => dh.DonationDate).IsRequired();
            builder.Property(dh => dh.Notes).HasMaxLength(500);
            builder.Property(dh => dh.DonorId).IsRequired();
            builder.Property(dh => dh.DonationRequestId).IsRequired();
            builder.HasIndex(dh => new { dh.DonorId, dh.DonationRequestId }).IsUnique();
        }
    }
    
    
}
