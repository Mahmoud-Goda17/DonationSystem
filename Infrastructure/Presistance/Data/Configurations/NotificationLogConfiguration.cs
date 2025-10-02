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
    public class NotificationLogConfiguration : IEntityTypeConfiguration<NotificationLog>
    {
        public void Configure(EntityTypeBuilder<NotificationLog> builder)
        {
            builder.HasKey(nl => nl.Id);
            builder.Property(nl => nl.Channel).IsRequired().HasMaxLength(20);
            builder.Property(nl => nl.SentAt).IsRequired();
            builder.Property(nl => nl.ErrorMessage).HasMaxLength(300);
            builder.Property(nl=> nl.DonorId).IsRequired();
            builder.Property(nl => nl.DonationRequestId).IsRequired();
            builder.HasIndex(nl => new { nl.DonorId, nl.DonationRequestId, nl.Channel }).IsUnique();
        }
    }
    
    
}
