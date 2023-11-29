using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace EF_Form_HW.Data.Entities
{
    public class DataContext : DbContext
    {
        public DbSet<Entities.User> Users { get; set; } = null!;
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasDefaultSchema("EF_FORM_HW");
            modelBuilder.Entity<User>()
               .Property(u => u.RegisterTime)
               .HasDefaultValueSql("NOW()");
        }

        
    }
}