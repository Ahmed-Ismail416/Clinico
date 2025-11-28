using Core.DomainLayer.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.ToTable("Payments");

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)");

            // هذه السطر اختياري — العلاقة معرفة من جهة Appointment
            // builder.HasOne(p => p.Appointment)
            //     .WithOne(a => a.Payment)
            //     .HasForeignKey<Payment>(p => p.AppointmentId);
        }
    }
}
