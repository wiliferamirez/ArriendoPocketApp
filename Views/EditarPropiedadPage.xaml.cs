using CommunityToolkit.Mvvm.DependencyInjection;
using ArriendoPocketApp.ViewModels;

namespace ArriendoPocketApp.Views;

public partial class EditarPropiedadPage : ContentPage
{
	public EditarPropiedadPage()
	{
		InitializeComponent();
        BindingContext = Ioc.Default.GetRequiredService<EditarPropiedadViewModel>();
    }
}