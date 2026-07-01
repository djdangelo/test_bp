using Microsoft.EntityFrameworkCore;
using test_bp.Shared.Domain;

namespace test_bp.Infrastructure.Persistense
{
    public class BpContext : DbContext
    {
        public BpContext(DbContextOptions<BpContext> options) : base(options) { }

        public DbSet<Users> Users => Set<Users>();
        public DbSet<Roles> Roles => Set<Roles>();
        public DbSet<Agency> Agency => Set<Agency>();
        public DbSet<Transactions> Transaction => Set<Transactions>();
        public DbSet<Accounts> Accounts => Set<Accounts>();
        public DbSet<Clients> Clients => Set<Clients>();
        public DbSet<SystemSetting> SystemSettings => Set<SystemSetting>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BpContext).Assembly);
            BpSeeder.Seed(modelBuilder);
        }
    }
}
