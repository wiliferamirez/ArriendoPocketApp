using ArriendoPocketApp.ViewModels;

namespace ArriendoPocketApp.Views
{
    public partial class PropiedadesPage : ContentPage
    {
        public PropiedadesPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            if (BindingContext is PropiedadesViewModel vm)
            {
                await vm.CargarPropiedades();
            }
        }
    }
}
