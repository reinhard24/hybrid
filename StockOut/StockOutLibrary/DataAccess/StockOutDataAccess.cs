using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockOutLibrary.Models;

namespace StockOutLibrary.DataAccess
{
    public partial class StockOutDataAccess : DbContext
    {
        public StockOutDataAccess()
        {
        }

        public StockOutDataAccess(DbContextOptions<StockOutDataAccess> options) :
            base(options)
        {

        }

        public virtual DbSet<StockOutModel> stockouts { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=hybrid");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockOutModel>(entity =>
            {
                entity.ToTable("stockouts");

                entity.Property(e => e.stockout_id).HasColumnName("stockout_id");

                entity.Property(e => e.product_id).HasColumnName("product_id");

                entity.Property(e => e.stockout_qty).HasColumnName("stockout_qty");

                entity.Property(e => e.out_date).HasColumnName("out_date");

                entity.Property(e => e.notes).HasColumnName("notes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
