using ArriendoPocketApp.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace ArriendoPocketApp.Views;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
		BindingContext = Ioc.Default.GetRequiredService<LoginViewModel>();
    }
}