using DealHub.Models.enums;
using System.ComponentModel.DataAnnotations;

namespace DealHub.Models
{
    public class Report
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The reason of report is mandatory")]
        public string ReasonPhrase { get; set; } = string.Empty;
        public ReportStatusEnum ReportStatusEnum { get; set; }
        public DateTime ReportedAt { get; set; }
        public DateTime ResolvedAt { get; set; }

    }
}
