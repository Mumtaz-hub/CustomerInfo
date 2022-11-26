using Data.Configurations.CoreConfigurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Configurations
{
    
    public class UserConfigurations : BaseEntityTypeConfiguration<User>
    {
        public override void Configure(EntityTypeBuilder<User> entityTypeBuilder)
        {
            ConfigureUserSchema(entityTypeBuilder);
            base.Configure(entityTypeBuilder);
        }

        private static void ConfigureUserSchema(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(e => e.MobileNumber).IsUnique();
            builder.HasIndex(e => e.EmailAddress).IsUnique();

            builder.Property(e => e.EmailAddress)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Password)
                .IsRequired();

            builder.Property(e => e.MobileNumber)
                .IsRequired()
                .HasMaxLength(50);

        }
    }
}
