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
    public class WorkingHourConfiguration : IEntityTypeConfiguration<WorkingHour>
    {
        public void Configure(EntityTypeBuilder<WorkingHour> builder)
        {
            builder.ToTable("WorkingHours");

            builder.HasOne(wh => wh.Doctor)
                .WithMany(d => d.WorkingHours)
                .HasForeignKey(wh => wh.DoctorId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
