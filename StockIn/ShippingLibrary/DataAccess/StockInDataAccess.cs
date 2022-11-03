using Microsoft.EntityFrameworkCore;
using StockInLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockInLibrary.DataAccess
{
    public partial class StockInDataAccess : DbContext
    {
        public StockInDataAccess()
        {
        }

        public StockInDataAccess(DbContextOptions<StockInDataAccess> options) :
            base(options)
        {

        }

        public virtual DbSet<StockInModel> stockins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=hybrid");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockInModel>(entity =>
            {
                entity.ToTable("stockins");

                entity.Property(e => e.stockin_id).HasColumnName("stockin_id");

                entity.Property(e => e.product_id).HasColumnName("product_id");

                entity.Property(e => e.stockin_qty).HasColumnName("stockin_qty");

                entity.Property(e => e.entry_date).HasColumnName("entry_date");

                entity.Property(e => e.notes).HasColumnName("notes");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
