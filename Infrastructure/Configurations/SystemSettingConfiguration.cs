using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test_bp.Shared.Domain;

namespace test_bp.Infrastructure.Configurations
{
    public class SystemSettingConfiguration : IEntityTypeConfiguration<SystemSetting>
    {
        public void Configure(EntityTypeBuilder<SystemSetting> builder)
        {
            builder.ToTable("SystemSettings");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Id)
                .UseIdentityColumn();

            builder.Property(s => s.Key)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Value)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(s => s.Description)
                .HasMaxLength(500);

            builder.Property(s => s.UpdatedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(s => s.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasIndex(s => s.Key)
                .IsUnique()
                .HasDatabaseName("IX_SystemSettings_Key");
        }
    }
}
