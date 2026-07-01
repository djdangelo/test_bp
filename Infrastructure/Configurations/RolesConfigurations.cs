using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test_bp.Shared.Domain;

namespace test_bp.Infrastructure.Configurations
{
    public class RolesConfigurations : IEntityTypeConfiguration<Roles>
    {
        public void Configure(EntityTypeBuilder<Roles> builder)
        {
            builder.ToTable("Roles");

            builder.HasKey(r => r.Id);

            builder.Property(r => r.Id)
                .UseIdentityColumn();

            builder.Property(r => r.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(r => r.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasIndex(r => r.Name)
                .IsUnique()
                .HasDatabaseName("IX_Roles_Name");
        }
    }
}
