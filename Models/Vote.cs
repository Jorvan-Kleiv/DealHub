using DealHub.Models.enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DealHub.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public VoteTypeEnum VoteType { get; set; }
        [ValidateNever]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        [ValidateNever]
        public Deal Deal { get; set; }
        public int DealId { get; set; }
        public DateTime VotedAt { get; set; }
    }
}
