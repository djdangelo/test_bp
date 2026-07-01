using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test_bp.Shared.Domain;

namespace test_bp.Infrastructure.Configurations
{
    public class ClientsConfigurations : IEntityTypeConfiguration<Clients>
    {
        public void Configure(EntityTypeBuilder<Clients> builder)
        {
            builder.ToTable("Clients");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .UseIdentityColumn();

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.LastName)
                .IsRequired()
                .HasMaxLength(150);

            builder.Property(c => c.Email)
                .IsRequired()
                .HasMaxLength(250);

            builder.Property(c => c.Phone)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.Dni)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(c => c.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(c => c.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(c => c.UpdatedAt)
                .HasColumnType("datetime2");

            // FK -> Users (CreatedBy)
            builder.HasOne(c => c.CreatedByUser)
                .WithMany()
                .HasForeignKey(c => c.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // FK -> Users (UpdatedBy)
            builder.HasOne(c => c.UpdateByUser)
                .WithMany()
                .HasForeignKey(c => c.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(c => c.Dni)
                .IsUnique()
                .HasDatabaseName("IX_Clients_Dni");

            builder.HasIndex(c => c.Email)
                .IsUnique()
                .HasDatabaseName("IX_Clients_Email");
        }
    }
}
