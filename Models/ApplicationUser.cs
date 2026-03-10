

using DealHub.Models.enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;

namespace DealHub.Models
{
    public class ApplicationUser: IdentityUser
    {
        [PersonalData]
        public string FirstName { get; set; } = string.Empty;
        [PersonalData]
        public string LastName { get; set; } = string.Empty;
        [PersonalData]
        public bool IsBanned { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [NotMapped]
        public IList<string> RoleNames { get; set; } = null;
        public int Reputation {  get; set; } = 0;
        public List<Deal> Deals { get; set; } = new();
        public List<Alert> Alerts { get; set; } = new();
        public VerificationDemand? VerificationDemand { get; set; }
    }
}
