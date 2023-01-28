using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Services;

namespace Sigv.Mobile.Laudo.Views;

public partial class PageHome : ContentPage
{
	public PageHome()
	{
		InitializeComponent();

		listView.ItemsSource = ListarVeiculos();


    }

	private List<Veiculo> ListarVeiculos()
	{
		try
		{
			var lista = new List<Veiculo>();

			using (var srv = new HttpService<List<Veiculo>>())
			{

				lista = srv.ReturnService("api/veiculo/listar");
			}

			return lista;
		}
		catch (Exception ex)
		{
			throw ex;
		}
	}
}