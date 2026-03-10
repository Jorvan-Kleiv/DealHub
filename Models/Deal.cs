using DealHub.Models.enums;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace DealHub.Models
{
    public class Deal
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "The category name is required")]
        [MinLength(2, ErrorMessage = "The category name must be at least 2")]
        [MaxLength(100, ErrorMessage = "The category name must be less than 100")]
        public string Title { get; set; } = string.Empty;
        [Required(ErrorMessage = "The description is required")]
        [MinLength(1, ErrorMessage = "The description must be at least 1")]
        [MaxLength(3000, ErrorMessage = "The description must be less than 3000")]
        public string Description { get; set; } = string.Empty;
        public DealStatusEnum Status { get; set; } = DealStatusEnum.ACTIVE;
        [Display(Name = "Lien")]
        public string Url { get; set; } = string.Empty;
        [Required(ErrorMessage = "The image url is required")]
        [Display(Name = "Image")]
        public string ImageUrl { get; set; } = string.Empty;
        [Display(Name = "Prix Original")]
        public double OriginalPrice { get; set; }
        [Display(Name = "Prix final")]
        public double FinalPrice { get; set; }
        [Display(Name = "Score de vote")]
        public int VoteScore { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [ValidateNever]
        public ApplicationUser User { get; set; }
        public string UserId { get; set; }
        [Required(ErrorMessage = "Category is required.")]
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        [ValidateNever]
        public Merchant Merchant { get; set; }
        public int MerchantId { get; set; }
    }
}
