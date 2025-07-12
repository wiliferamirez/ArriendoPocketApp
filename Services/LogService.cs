using ArriendoPocketApp.Data;
using ArriendoPocketApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;


namespace ArriendoPocketApp.Services
{
    public class LogService
    {
        private readonly LogsDbContext _db;
        public LogService(LogsDbContext db)
        {
            _db = db;
        }
        public async Task AddAsync(LogEntry entry)
        {
            try
            {
                _db.Logs.Add(entry);
                await _db.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error al guardar el log" + ex.InnerException?.Message ?? ex.Message);
                throw;
            }

        }
        public async Task<List<LogEntry>> GetAllAsync()
        {
            return await _db.Logs
                .OrderByDescending(l => l.Timestamp)
                .ToListAsync();
        }
    }
}
