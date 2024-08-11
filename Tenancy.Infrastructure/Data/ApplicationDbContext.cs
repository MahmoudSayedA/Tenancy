using Microsoft.EntityFrameworkCore;
using Tenancy.Core.Contracts;
using Tenancy.Core.Models;

namespace Tenancy.Infrastructure.Data
{
    internal class ApplicationDbContext : DbContext
    {
        public DbSet<Report> Reports { get; set; }
        private string _tenantId;
        private readonly ITenantService _tenantService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService) : base(options)
        {
            _tenantService = tenantService;
            var currentTenant = tenantService.GetCurrentTenant()
                ?? throw new ArgumentNullException(nameof(tenantService));

            _tenantId = currentTenant.TenantId;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _tenantService.GetConnectionString();
            if (!string.IsNullOrEmpty(connectionString))
            {
                var dbProvider = _tenantService.GetProvider()!.ToLower();
                if (dbProvider == "sqlserver")
                {
                    optionsBuilder.UseSqlServer(connectionString);
                }
                //else if (dbProvider == "sqlite")
                //{
                //    optionsBuilder.UseSqlite(connectionString);
                //}
                //else if (dbProvider == "mysql")
                //{
                //    optionsBuilder.UseMySql(connectionString);
                //}
                //else if (dbProvider == "postgres")
                //{
                //    optionsBuilder.UseNpgsql(connectionString);
                //}
                //else
                //{
                //    throw new NotSupportedException("The provider is not supported");
                //}
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<IHaveTenant>().HasQueryFilter(r => r.TenantId == _tenantId);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IHaveTenant>().Where(e => e.State == EntityState.Added))
            {
                entry.Entity.TenantId = _tenantId;
            }

            return base.SaveChangesAsync(cancellationToken);
        }



    }
}
