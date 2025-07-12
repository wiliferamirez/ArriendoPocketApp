using System.Threading.Tasks;
using ArriendoPocketApp.Models;
using ArriendoPocketApp.Data;


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
    }
}
