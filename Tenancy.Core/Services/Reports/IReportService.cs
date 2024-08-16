using Tenancy.Core.Dtos.Reports;

namespace Tenancy.Core.Services.Reports
{
    public interface IReportService
    {
        Task<int> AddReport(AddReportDto addReportDto);
        Task<GetReportDto?> GetReportById(int reportId);
        Task<List<GetReportDto>> GetAllReports();
    }
}
