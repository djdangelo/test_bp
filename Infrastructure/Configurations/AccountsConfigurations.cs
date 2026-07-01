using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test_bp.Shared.Domain;

namespace test_bp.Infrastructure.Configurations
{
    public class AccountsConfigurations : IEntityTypeConfiguration<Accounts>
    {
        public void Configure(EntityTypeBuilder<Accounts> builder)
        {
            builder.ToTable("Accounts");

            builder.HasKey(a => a.Id);

            builder.Property(a => a.Id)
                .UseIdentityColumn();

            builder.Property(a => a.CurrentBalance)
                .IsRequired()
                .HasColumnType("float");

            builder.Property(a => a.LastBalance)
                .IsRequired()
                .HasColumnType("float");

            builder.Property(a => a.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.Property(a => a.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            builder.Property(a => a.UpdateAt)
                .HasColumnType("datetime2");

            // FK -> Clients
            builder.HasOne(a => a.Client)
                .WithMany()
                .HasForeignKey(a => a.IdClient)
                .OnDelete(DeleteBehavior.Restrict);

            // FK -> Users (CreatedBy)
            builder.HasOne(a => a.CreatedByUser)
                .WithMany()
                .HasForeignKey(a => a.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            // FK -> Users (UpdatedBy)
            builder.HasOne(a => a.UpdatedByUser)
                .WithMany()
                .HasForeignKey(a => a.UpdatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(a => a.IdClient)
                .HasDatabaseName("IX_Accounts_IdClient");
        }
    }
}
