using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Services;
using Sigv.Mobile.Laudo.Views.Laudo;

namespace Sigv.Mobile.Laudo.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PageHome : ContentPage
{
	public PageHome()
	{
		InitializeComponent();

		listViewVeiculos.ItemsSource = ListarVeiculos(0);
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

        listViewVeiculos.ItemsSource = ListarVeiculos(statusId);
    }

    private void listViewVeiculos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        Veiculo veiculo = (Veiculo)listViewVeiculos.SelectedItem;


		if (veiculo.LaudoStatusId == 0)
		{
            DisplayAlert("Iniciar Laudo", "Deseja iniciar o Laudo desse veículo?", "OK");
        }

        if (veiculo.LaudoStatusId == 1)
        {
            FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
            page.Detail = new NavigationPage(new PageLaudo());
        }

        if (veiculo.LaudoStatusId == 2)
        {
            DisplayAlert("Laudo finalizado", "Esse laudo foi finalizado. Deseja reabrir o Laudo desse veículo?", "OK");
        }

    }
}