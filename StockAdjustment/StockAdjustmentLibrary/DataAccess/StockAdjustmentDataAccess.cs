using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using StockAdjustmentLibrary.Models;

namespace StockAdjustmentLibrary.DataAccess
{
    public partial class StockAdjustmentDataAccess : DbContext
    {
        public StockAdjustmentDataAccess()
        {
        }

        public StockAdjustmentDataAccess(DbContextOptions<StockAdjustmentDataAccess> options) :
            base(options)
        {

        }

        public virtual DbSet<StockAdjustmentModel> stockadjustments { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=hybrid");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StockAdjustmentModel>(entity =>
            {
                entity.ToTable("stockadjusment");

                entity.Property(e => e.stockadjusment_id).HasColumnName("stockadjusment_id");

                entity.Property(e => e.adjusment_date).HasColumnName("adjusment_date");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

    }
}
