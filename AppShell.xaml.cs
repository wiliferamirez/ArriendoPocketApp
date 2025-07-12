using ArriendoPocketApp.Views;

namespace ArriendoPocketApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("editarpropiedad", typeof(EditarPropiedadPage));
        }
    }
}
