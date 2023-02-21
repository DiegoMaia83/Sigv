namespace Sigv.Mobile.Laudo.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PageMenu : FlyoutPage
{
    public PageMenu()
	{
		InitializeComponent();

        this.Detail = new NavigationPage(new PageHome(0));
        IsPresented = false;
    }

    private void btHome_Clicked(object sender, EventArgs e)
    {
        this.Detail = new NavigationPage(new PageHome(0));
        IsPresented = false;
    }

    private void btLaudosPendentes_Clicked(object sender, EventArgs e)
    {
        this.Detail = new NavigationPage(new PageHome(0));
        IsPresented = false;
    }

    private void btLaudosAbertos_Clicked(object sender, EventArgs e)
    {
        this.Detail = new NavigationPage(new PageHome(1));
        IsPresented = false;
    }

    private void btLaudosFinalizados_Clicked(object sender, EventArgs e)
    {
        this.Detail = new NavigationPage(new PageHome(2));
        IsPresented = false;
    }
}