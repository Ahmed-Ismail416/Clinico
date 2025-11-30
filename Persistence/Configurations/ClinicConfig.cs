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
    public class ClinicConfiguration : IEntityTypeConfiguration<Clinic>
    {
        public void Configure(EntityTypeBuilder<Clinic> builder)
        {
            builder.ToTable("Clinics");

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Address)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(c => c.Phone)
                .IsRequired();
        }
    }
}
