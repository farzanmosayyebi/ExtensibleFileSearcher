using Microsoft.EntityFrameworkCore;

namespace FileSearcher
{
    public class HistoryDbContext : DbContext
    {
        public DbSet<HistoryEntry> HistoryEntries { get; set; }

        public HistoryDbContext(DbContextOptions<HistoryDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string historyFilePath = Path.Combine(Directory.GetCurrentDirectory(), "History.db");
            optionsBuilder.UseSqlite($"Filename=History.db");
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HistoryEntry>().HasKey(e => e.Id);
            base.OnModelCreating(modelBuilder);
        }


    }
}
