namespace ArriendoPocketApp.Views;
using CommunityToolkit.Mvvm.DependencyInjection;
using ArriendoPocketApp.ViewModels;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
		BindingContext = Ioc.Default.GetRequiredService<RegisterViewModel>();
    }
}