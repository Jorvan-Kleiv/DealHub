using DealHub.Models.enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DealHub.Models
{
    public class Merchant
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Le nom de la compagnie est requis")]
        [MinLength(1, ErrorMessage = "Le nom doit avoir au moins 1 caractere")]
        public string Name { get; set; } = string.Empty;
        [Required(ErrorMessage = "L'Url de la compagnie est requis")]
        [Url(ErrorMessage = "L'Url de ce site doit etre correct")]
        public string WebsiteUrl { get; set; } = string.Empty;
        [Required(ErrorMessage = "Le siret de la compagnie est requis")]
        [MinLength(4, ErrorMessage = "Le siret doit avoir au moins 4 caractere")]
        public string Siret { get; set; } = string.Empty;
        public string Status { get; set; } = MerchantStatusEnum.APPROVED.ToString();
        public string? Document { get; set; }
        public string? LogUrl { get; set; }
        public bool IsVerified { get; set; } = false;
        public string? RefueReason { get; set; }
        public List<Deal> Deals { get; set; } = new();
        [ValidateNever]
        public string OwnerId { get; set; } = string.Empty;
        [ValidateNever]
        public ApplicationUser? Owner { get; set; }
        public DateTime SubmitAt { get; set; } = DateTime.Now;
        public DateTime? AcceptedAt { get; set; }
        public DateTime? RefutedAt { get; set; }
    }
}
