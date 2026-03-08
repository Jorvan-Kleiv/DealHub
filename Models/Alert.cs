using DealHub.Models.enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DealHub.Models
{
    public class Alert
    {
        public int Id { get; set; }
        public string KeyWord { get; set; } = string.Empty;
        public NotificationTypeEnum NotificationType { get; set; }
        public bool IsActive { get; set; }
        public string UserId { get; set; }
        [ValidateNever]
        public ApplicationUser User { get; set; }
    }
}
