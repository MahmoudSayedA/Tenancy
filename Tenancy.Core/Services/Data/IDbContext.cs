using Microsoft.EntityFrameworkCore;

namespace Tenancy.Core.Services.Data
{
    public interface IDbContext
    {
        DbSet<Report> Reports { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
