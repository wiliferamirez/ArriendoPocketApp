using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using ArriendoPocketApp.Models;
using ArriendoPocketApp.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
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
        }

        public ObservableCollection<LogEntry> Logs { get; }

        public IAsyncRelayCommand LoadLogsCommand { get; }

        private async Task CargarLogsAsync()
        {
            Logs.Clear();
            var list = await _logService.GetAllAsync();
            foreach (var log in list)
                Logs.Add(log);
        }
    }
}