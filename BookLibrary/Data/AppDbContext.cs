using BookLibrary.Entities;
using Microsoft.EntityFrameworkCore;
using BookLibrary.Data;

namespace BookLibrary.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<BookLibraryMaster> BookLibraryMasters { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
