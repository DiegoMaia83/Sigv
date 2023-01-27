namespace Sigv.Mobile.Laudo.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PageMenu : FlyoutPage
{
    public PageMenu()
	{
		InitializeComponent();

        this.Detail = new NavigationPage(new PageHome());
        IsPresented = false;
    }

    private void btHome_Clicked(object sender, EventArgs e)
    {
        this.Detail = new NavigationPage(new PageHome());
        IsPresented = false;
    }
}