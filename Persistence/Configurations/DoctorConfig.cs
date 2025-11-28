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
    public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
    {
        public void Configure(EntityTypeBuilder<Doctor> builder)
        {
            builder.ToTable("Doctors");

            builder.Property(d => d.Specialty)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(d => d.ConsultationFee)
                .HasColumnType("decimal(18,2)");

            // علاقة واحدة-لواحد مع AppUser
            builder.HasOne(d => d.AppUser)
                .WithOne(u => u.DoctorProfile)
                .HasForeignKey<Doctor>(d => d.AppUserId)
                .OnDelete(DeleteBehavior.Cascade);

            // علاقة مع Clinic
            builder.HasOne(d => d.Clinic)
                .WithMany(c => c.Doctors)
                .HasForeignKey(d => d.ClinicId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
