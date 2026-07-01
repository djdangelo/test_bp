using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test_bp.Shared.Domain;

namespace test_bp.Infrastructure.Configurations
{
    public class UsersConfigurations : IEntityTypeConfiguration<Users>
    {
        public void Configure(EntityTypeBuilder<Users> builder)
        {
            builder.ToTable("Users");

            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id)
                .UseIdentityColumn();

            builder.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(u => u.PasswordHash)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(u => u.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(u => u.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(u => u.UpdatedAt)
                .HasColumnType("datetime2");

            // FK -> Agency
            builder.HasOne(u => u.Agency)
                .WithMany()
                .HasForeignKey(u => u.AgencyId)
                .OnDelete(DeleteBehavior.Restrict);

            // FK -> Roles
            builder.HasOne(u => u.Roles)
                .WithMany()
                .HasForeignKey(u => u.IdRole)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(u => u.UserName)
                .IsUnique()
                .HasDatabaseName("IX_Users_UserName");
        }
    }
}
