using Microsoft.AspNetCore.Mvc;
using Tenancy.Core.Dtos.Reports;
using Tenancy.Core.Services.Reports;

namespace Tenancy.WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportsController : ControllerBase
    {
        private readonly IReportService _reportService;

        public ReportsController(IReportService reportService)
        {
            _reportService = reportService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var reports = await _reportService.GetAllReports();
            return Ok(reports);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            var report = await _reportService.GetReportById(id);
            return Ok(report);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddReportDto report)
        {
            var id = await _reportService.AddReport(report);
            return CreatedAtAction(nameof(Get), new { id }, report);
        }
    }
}
