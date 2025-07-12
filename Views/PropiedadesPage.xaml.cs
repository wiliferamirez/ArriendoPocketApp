using ArriendoPocketApp.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace ArriendoPocketApp.Views
{
    public partial class PropiedadesPage : ContentPage
    {
        readonly PropiedadesViewModel _vm;
        public PropiedadesPage()
        {
            InitializeComponent();
            _vm = Ioc.Default.GetRequiredService<PropiedadesViewModel>();
            BindingContext = _vm;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await _vm.CargarPropiedades();
        }
    }
}
