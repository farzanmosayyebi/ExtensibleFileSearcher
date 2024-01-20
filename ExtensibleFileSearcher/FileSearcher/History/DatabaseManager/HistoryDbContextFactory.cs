using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileSearcher
{
    public class HistoryDbContextFactory : IDesignTimeDbContextFactory<HistoryDbContext>
    {
        public HistoryDbContext CreateDbContext(string[] args)
        {
            DbContextOptionsBuilder<HistoryDbContext> optionsBuilder = new DbContextOptionsBuilder<HistoryDbContext>();

            string historyFilePath = Path.Combine(Directory.GetCurrentDirectory(), "History.db");
            optionsBuilder.UseSqlite($"Filename=History.db");
            
            return new HistoryDbContext(optionsBuilder.Options);
        }
    }
}
