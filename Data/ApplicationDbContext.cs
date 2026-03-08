
using DealHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DealHub.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<DealHub.Models.Deal> Deal { get; set; } = default!;
        public DbSet<DealHub.Models.Category> Category { get; set; } = default!;
    }
}
