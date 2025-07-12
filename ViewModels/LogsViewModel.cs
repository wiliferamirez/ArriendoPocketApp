using System;
using System.Collections.ObjectModel;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ArriendoPocketApp.Models;
using ArriendoPocketApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;
using Microsoft.Maui.Controls;

namespace ArriendoPocketApp.ViewModels
{
    public class LogsViewModel : ObservableObject
    {
        readonly LogService _logService;

        public LogsViewModel(LogService logService)
        {
            _logService = logService;
            Logs = new ObservableCollection<LogEntry>();
            LoadLogsCommand = new AsyncRelayCommand(CargarLogsAsync);
            ExportLogsCommand = new AsyncRelayCommand(ExportarLogsAsync);
        }

        public ObservableCollection<LogEntry> Logs { get; }

        public IAsyncRelayCommand LoadLogsCommand { get; }
        public IAsyncRelayCommand ExportLogsCommand { get; } 

        private async Task CargarLogsAsync()
        {
            Logs.Clear();
            var list = await _logService.GetAllAsync();
            foreach (var log in list)
                Logs.Add(log);
        }

        private async Task ExportarLogsAsync()
        {
            var list = await _logService.GetAllAsync();
            var sb = new StringBuilder();
            foreach (var l in list)
            {
                sb
                  .AppendLine($"[{l.Timestamp:yyyy-MM-dd HH:mm:ss}] {l.Level}: {l.Message}")
                  .AppendLine($"    Endpoint: {l.Endpoint}")
                  .AppendLine($"    Payload:  {l.Payload}")
                  .AppendLine($"    UserId:   {l.UserId}")
                  .AppendLine(new string('-', 60));
            }

            var path = Path.Combine(FileSystem.AppDataDirectory, "logs_export.txt");
            File.WriteAllText(path, sb.ToString());

            await Application.Current.MainPage.DisplayAlert(
                "Exportación completada",
                $"Se exportaron {list.Count} logs a:\n{path}",
                "OK");
        }
    }
}
