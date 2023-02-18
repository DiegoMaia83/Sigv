using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Aplicacao.App;
using Sigv.Mobile.Laudo.Services;
using Sigv.Mobile.Laudo.Views.Laudos;

namespace Sigv.Mobile.Laudo.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PageHome : ContentPage
{   
    private readonly LaudoApp _laudoApp = new LaudoApp();

    public PageHome()
	{
		InitializeComponent();

		listViewVeiculos.ItemsSource = ListarVeiculos(0);
    }

    public PageHome(int statusId)
    {
        InitializeComponent();

        listViewVeiculos.ItemsSource = ListarVeiculos(statusId);
    }

    private List<Veiculo> ListarVeiculos(int statusId)
	{
		try
		{
			var lista = new List<Veiculo>();

			using (var srv = new HttpService<List<Veiculo>>())
			{

				lista = srv.ReturnService("api/veiculo/listar-veiculos-status-laudo?statusId=" + statusId);
			}

			return lista;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}

    private void ListarVeiculosLaudos_Clicked(object sender, EventArgs e)
    {
        var button = (Button)sender;
		var statusId = Convert.ToInt32(button.CommandParameter);

        FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
        page.Detail = new NavigationPage(new PageHome(statusId));

    }

    private async void listViewVeiculos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Veiculo veiculo = (Veiculo)listViewVeiculos.SelectedItem;

		if (veiculo.LaudoStatusId == 0)
		{
            var confirm = await DisplayAlert("Iniciar Laudo", "Deseja iniciar o Laudo desse veículo?", "INICIAR", "CANCELAR");

            if (confirm)
            {
                var result = _laudoApp.IniciarLaudo(veiculo);

                if (result != null)
                {
                    var laudo = _laudoApp.RetornarLaudo(result.LaudoId);

                    FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
                    page.Detail = new NavigationPage(new PageLaudo(laudo));
                }
                else
                {
                    await DisplayAlert("Alerta", "Houve um erro ao iniciar o Laudo!", "OK");
                }

            }
        }
        else if (veiculo.LaudoStatusId == 1)
        {
            var laudo = _laudoApp.RetornarLaudo(veiculo.LaudoId);

            FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
            page.Detail = new NavigationPage(new PageLaudo(laudo));
        }
        else if (veiculo.LaudoStatusId == 2)
        {
            var confirm = await DisplayAlert("Laudo finalizado", "Esse laudo foi finalizado. Deseja reabrir o Laudo desse veículo?", "REABRIR", "CANCELAR");

            if (confirm)
            {
                var result = _laudoApp.ReabrirLaudo(veiculo.LaudoId);

                if (result != null)
                {
                    var laudo = _laudoApp.RetornarLaudo(result.LaudoId);

                    FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
                    page.Detail = new NavigationPage(new PageLaudo(laudo));
                }
                else
                {
                    await DisplayAlert("Alerta", "Houve um erro ao reabrir o Laudo!", "OK");
                }
            }
        }

    }
}