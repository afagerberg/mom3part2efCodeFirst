using dt191gMom3Part2.Models;
using Microsoft.EntityFrameworkCore;

namespace dt191gMom3Part2.Data
{
    public class CollectionContext : DbContext
    {
        public CollectionContext(DbContextOptions<CollectionContext> options) : base(options)
        {    
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            _ = modelBuilder.Entity<CDalbum>()
                .HasOne(c => c.Artist).WithMany(a => a.CDalbums);
            _ = modelBuilder.Entity<Lending>().HasOne(l => l.User).WithMany(u => u.Loans);

            _ = modelBuilder.Entity<Lending>().HasOne(l => l.CDalbum).WithOne(c => c.Lending);
        }

        public DbSet<Artist>? Artist { get; set; }
        public DbSet<CDalbum>? CDalbum { get; set; }
        public DbSet<User>? User { get; set; }
        public DbSet<Lending>? Lending { get; set; }
    }
}
