using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DealHub.Models
{
    public class VerificationDemand
    {
        public int Id { get; set; }
        public string EnterpriseName { get; set; }
        public string Description { get; set; }
        public string Siret { get; set; }
        public string WebsiteUrl { get; set; }
        public DateTime? SubmitAt { get; set; }
        public DateTime? ResolvedAt { get; set; }
        public string RefuseReason { get; set; }
        public string AdminSolverFullName { get; set; }
        [ValidateNever]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

    }
}
