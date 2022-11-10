using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LoginLibrary.Models;

namespace LoginLibrary.DataAccess
{
    public partial class LoginDataAccess : DbContext
    {
        public LoginDataAccess()
        {
        }

        public LoginDataAccess(DbContextOptions<LoginDataAccess> options) :
            base(options)
        {

        }

        public virtual DbSet<LoginModel> logins { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Name=hybrid");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserDetailsModel>(entity =>
            {
                entity.ToTable("users");

                entity.Property(e => e.user_id).HasColumnName("user_id");
                entity.Property(e => e.user_name).HasColumnName("user_name");
                entity.Property(e => e.email).HasColumnName("email");
                entity.Property(e => e.user_password).HasColumnName("user_password");
                entity.Property(e => e.phone).HasColumnName("phone");
                entity.Property(e => e.address).HasColumnName("address");
                entity.Property(e => e.role_id).HasColumnName("role_id");

            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
