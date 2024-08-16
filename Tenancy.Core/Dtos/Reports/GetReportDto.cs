namespace Tenancy.Core.Dtos.Reports
{
    public class GetReportDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }
        public string Status { get; set; } = null!;
    }
}
