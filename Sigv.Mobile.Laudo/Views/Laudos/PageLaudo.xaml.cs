using Sigv.Domain;

namespace Sigv.Mobile.Laudo.Views.Laudos;

public partial class PageLaudo : ContentPage
{
	public PageLaudo()
	{
		InitializeComponent();
	}

    public PageLaudo(LaudoVeiculo laudo)
    {
        lbPlaca.Text = laudo.Veiculo.Placa;
        lbMarca.Text = laudo.Veiculo.Marca;
        lbAno.Text = laudo.Veiculo.AnoFabricacao.ToString();

        InitializeComponent();
    }
}