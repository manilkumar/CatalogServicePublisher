using CatalogService.API.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.API.Data
{
    public class CatalogDBContext : DbContext
    {
        public CatalogDBContext(DbContextOptions<CatalogDBContext> options) : base(options)
        {

        }

        public virtual DbSet<Item> Items { get; set; }
        public virtual DbSet<Catalog> Catalogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=ECommerceDB");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Catalog>(entity =>
            {
                entity.HasKey(e => new { e.Id });
            });

            modelBuilder.Entity<Item>(entity =>
            {
                entity.HasKey(e => new { e.Id });
            });

        }
    }
}
