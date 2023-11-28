using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace Form_HW.Data.Entities
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
        }
    }
}