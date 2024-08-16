namespace Tenancy.Core.Dtos.Reports
{
    public class AddReportDto
    {
        public string Title { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
    }
}
