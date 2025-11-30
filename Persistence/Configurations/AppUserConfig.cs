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
    public class AppUserConfig : IEntityTypeConfiguration<AppUser>
    {
        public void Configure(EntityTypeBuilder<AppUser> builder)
        {
            builder.ToTable("AppUsers");
            builder.HasKey(t => t.Id);

            builder.Property(u => u.FullName)
           .IsRequired()
           .HasMaxLength(100);

            builder.Property(u => u.ProfilePictureUrl)
                .HasMaxLength(500);

            builder.HasIndex(u => u.Email);
        }
    }
}
