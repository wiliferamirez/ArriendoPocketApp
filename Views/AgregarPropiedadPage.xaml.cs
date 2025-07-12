using CommunityToolkit.Mvvm.DependencyInjection;
using ArriendoPocketApp.ViewModels;

namespace ArriendoPocketApp.Views;

public partial class AgregarPropiedadPage : ContentPage
{
	public AgregarPropiedadPage()
	{
		InitializeComponent();
		BindingContext = Ioc.Default.GetRequiredService<AgregarPropiedadViewModel>();
    }
}