using CommunityToolkit.Mvvm.DependencyInjection;
using ArriendoPocketApp.ViewModels;
using Microsoft.Maui.Controls;

namespace ArriendoPocketApp.Views;

public partial class LogsPage : ContentPage
{
    readonly LogsViewModel _vm;

    public LogsPage()
    {
        InitializeComponent();
        _vm = Ioc.Default.GetRequiredService<LogsViewModel>();
        BindingContext = _vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        _ = _vm.LoadLogsCommand.ExecuteAsync(null);
    }
}
