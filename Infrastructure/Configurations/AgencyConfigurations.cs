using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test_bp.Shared.Domain;

namespace test_bp.Infrastructure.Configurations
{
    public class AgencyConfigurations : IEntityTypeConfiguration<Agency>
    {
        public void Configure(EntityTypeBuilder<Agency> builder)
        {
            builder.ToTable("Agency");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .UseIdentityColumn();

            builder.Property(a => a.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(a => a.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasIndex(a => a.Name)
                .IsUnique()
                .HasDatabaseName("IX_Agency_Name");
        }
    }
}
