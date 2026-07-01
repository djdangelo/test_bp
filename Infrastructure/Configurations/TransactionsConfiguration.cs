using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using test_bp.Shared.Domain;

namespace test_bp.Infrastructure.Configurations
{
    public class TransactionsConfiguration : IEntityTypeConfiguration<Transactions>
    {
        public void Configure(EntityTypeBuilder<Transactions> builder)
        {
            builder.ToTable("Transactions");

            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id)
                .ValueGeneratedNever();

            builder.Property(t => t.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(t => t.Type)
                .IsRequired()
                .HasConversion<int>();

            builder.Property(t => t.CreatedAt)
                .IsRequired()
                .HasColumnType("datetime2");

            // FK -> Accounts
            builder.HasOne<Accounts>()
                .WithMany()
                .HasForeignKey(t => t.IdAccount)
                .OnDelete(DeleteBehavior.Restrict);

            // FK -> Users (CreatedBy)
            builder.HasOne(t => t.CreatedByUser)
                .WithMany()
                .HasForeignKey(t => t.CreatedBy)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasIndex(t => t.IdAccount)
                .HasDatabaseName("IX_Transactions_IdAccount");

            builder.HasIndex(t => t.CreatedAt)
                .HasDatabaseName("IX_Transactions_CreatedAt");
        }
    }
}
