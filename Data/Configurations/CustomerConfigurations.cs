using Data.Configurations.CoreConfigurations;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Data.Configurations
{
    public class CustomerConfigurations : BaseEntityTypeConfiguration<Customer>
    {
        public override void Configure(EntityTypeBuilder<Customer> entityTypeBuilder)
        {
            ConfigureCustomerSchema(entityTypeBuilder);
            base.Configure(entityTypeBuilder);
        }

        private static void ConfigureCustomerSchema(EntityTypeBuilder<Customer> builder)
        {
            builder.Property(e => e.FirstName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.LastName)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(e => e.Age)
                .IsRequired();
                

        }
    }
}
