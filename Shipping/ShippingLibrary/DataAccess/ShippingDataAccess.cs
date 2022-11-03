using Microsoft.EntityFrameworkCore;
using ShippingLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShippingLibrary.DataAccess
{ 
    public partial class ShippingDataAccess : DbContext
    {
        public ShippingDataAccess()
        {
        }

        public ShippingDataAccess(DbContextOptions<ShippingDataAccess> options):
            base(options)
        {

        }

        public virtual DbSet<ShippingModel> Shipping { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=hybrid");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ShippingModel>(entity =>
            {
                entity.ToTable("shippings");

                entity.Property(e => e.shipping_id).HasColumnName("shipping_id");

                entity.Property(e => e.name).HasColumnName("name");
                
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
