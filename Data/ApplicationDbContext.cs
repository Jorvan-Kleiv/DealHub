
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
        public DbSet<Report> Report { get; set; } = default!;


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "High-Tech", Description = "Accessoires high-tech", IsActive = true },
                new Category { Id = 2, Name = "Jeux Vidéo", Description = "Console et autres", IsActive = true },
                new Category { Id = 3, Name = "Mode", Description = "Mode", IsActive = true },
                new Category { Id = 4, Name = "Maison", Description = "Produits de maison", IsActive = true },
                new Category { Id = 5, Name = "Deco", Description = "Décoration", IsActive = true },
                new Category { Id = 6, Name = "Alimentaire", Description = "Produit de consommation", IsActive = true },
                new Category { Id = 7, Name = "Voyages", Description = "Voyage a prix reduis", IsActive = true }
            );
        }

    }
}
