using Microsoft.EntityFrameworkCore;
using Tenancy.Core.Contracts;
using Tenancy.Core.Models;
using Tenancy.Core.Services.Data;

namespace Tenancy.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext, IDbContext
    {
        public DbSet<Report> Reports { get; set; }
        public string TenantId { get; set;} = string.Empty;
        private readonly ITenantService _tenantService;
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, ITenantService tenantService) : base(options)
        {
            _tenantService = tenantService;
            var currentTenant = _tenantService.GetCurrentTenant();

            TenantId = currentTenant?.TenantId ?? string.Empty;
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
                base.OnConfiguring(optionsBuilder);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Report>().HasQueryFilter(r => r.TenantId == TenantId);
            base.OnModelCreating(modelBuilder);
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<IHaveTenant>().Where(e => e.State == EntityState.Added))
            {
                entry.Entity.TenantId = TenantId;
            }

            return base.SaveChangesAsync(cancellationToken);
        }



    }
}
