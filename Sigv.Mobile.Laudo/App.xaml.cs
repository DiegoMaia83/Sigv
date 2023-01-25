using Sigv.Mobile.Laudo.Views;

namespace Sigv.Mobile.Laudo
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new PageLogin());
        }
    }
}