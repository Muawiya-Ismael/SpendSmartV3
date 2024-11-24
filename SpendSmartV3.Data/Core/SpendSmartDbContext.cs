using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SpendSmartV3.Objects.Models.Expense;

namespace SpendSmartV3.Data.Core
{
    public class SpendSmartDbContext: IdentityDbContext
    {  
        public SpendSmartDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Expense> Expenses { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Expense>()
                        .Property(e => e.Value)
                        .HasColumnType("decimal(18, 2)");
        }
    }
}
