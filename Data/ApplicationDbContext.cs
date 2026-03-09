
using DealHub.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DealHub.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options): IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Deal> Deal { get; set; } = default!;
        public DbSet<Category> Category { get; set; } = default!;
        public DbSet<VerificationDemand> VerificationDemand { get; set; } = default!;
        public DbSet<DealHub.Models.Report> Report { get; set; } = default!;

        
    }
}
