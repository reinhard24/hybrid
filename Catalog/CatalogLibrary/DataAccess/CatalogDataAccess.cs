using CatalogLibrary.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CatalogLibrary.DataAccess
{
    public partial class CatalogDataAccess : DbContext
    {
        public CatalogDataAccess()
        {
        }

        public CatalogDataAccess(DbContextOptions<CatalogDataAccess> options) :
            base(options)
        {

        }

        public virtual DbSet<CatalogModel> Catalog { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=hybrid");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<CatalogModel>(entity =>
            {
                entity.ToTable("catalogs");

                entity.Property(e => e.catalog_id).HasColumnName("catalog_id");

                entity.Property(e => e.catalog_name).HasColumnName("catalog_name");

                entity.Property(e => e.product_qty).HasColumnName("product_qty");

                entity.Property(e => e.sold).HasColumnName("sold");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
