using Microsoft.EntityFrameworkCore;
using test_bp.Shared.Domain;

namespace test_bp.Infrastructure.Persistense
{
    public static class BpSeeder
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            SeedRoles(modelBuilder);
            SeedSystemSettings(modelBuilder);
        }

        private static void SeedRoles(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Roles>().HasData(
                new { Id = 1, Name = "client_admin", IsActive = true },
                new { Id = 2, Name = "register_transaction", IsActive = true },
                new { Id = 3, Name = "admin_platform", IsActive = true }
            );
        }

        private static void SeedSystemSettings(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<SystemSetting>().HasData(
                new
                {
                    Id = 1,
                    Key = "MINIMUM_BALANCE",
                    Value = "500",
                    Description = (string?)"Saldo mínimo permitido en las cuentas",
                    UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsActive = true
                },
                new
                {
                    Id = 2,
                    Key = "MAXIMUM_WITHDRAW",
                    Value = "10",
                    Description = (string?)"Cantidad máxima de retiros permitidos",
                    UpdatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc),
                    IsActive = true
                }
            );
        }
    }
}
