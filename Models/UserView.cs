using Microsoft.AspNetCore.Identity;

namespace DealHub.Models
{
    public class UserView
    {
        public IEnumerable<ApplicationUser> Users { get; set; } = null;
        public IEnumerable<IdentityRole> Roles { get; set; } = null;
    }
}
