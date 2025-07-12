using Microsoft.EntityFrameworkCore;
using ArriendoPocketApp.Models;

namespace ArriendoPocketApp.Data
{
    public class LogsDbContext : DbContext
    {
        public LogsDbContext(DbContextOptions<LogsDbContext> options)
            : base(options)
        {
        }

        public DbSet<LogEntry> Logs { get; set; }

    }
}
