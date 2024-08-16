using Microsoft.EntityFrameworkCore;
using Tenancy.Core.Dtos.Reports;

namespace Tenancy.Core.Services.Reports
{
    internal class ReportService(IDbContext dbContext) : IReportService
    {
        private readonly IDbContext _dbContext = dbContext;

        public async Task<int> AddReport(AddReportDto addReportDto)
        {
            var report = new Report()
            {
                Title = addReportDto.Title,
                Description = addReportDto.Description,
                UserId = addReportDto.UserId,
                CreatedAt = DateTime.Now,
                Status = "Pending",
            };
            await _dbContext.Reports.AddAsync(report);
            await _dbContext.SaveChangesAsync();
            return report.Id;
        }

        public async Task<List<GetReportDto>> GetAllReports()
        {
            var reports = await _dbContext.Reports.Select(r => new GetReportDto
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                UserId = r.UserId,
                CreatedAt = r.CreatedAt,
                Status = r.Status,
            }).ToListAsync();

            return reports;
        }

        public Task<GetReportDto?> GetReportById(int reportId)
        {
            var report = _dbContext.Reports.Select(r => new GetReportDto
            {
                Id = r.Id,
                Title = r.Title,
                Description = r.Description,
                UserId = r.UserId,
                CreatedAt = r.CreatedAt,
                Status = r.Status,
            }).FirstOrDefaultAsync(r => r.Id == reportId);
            return report;
        }
    }
}
