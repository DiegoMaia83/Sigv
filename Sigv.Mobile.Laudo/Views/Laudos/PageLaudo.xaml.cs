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
        InitializeComponent();

        lbPlaca.Text = laudo.Veiculo.Placa.ToString();
        lbMarca.Text = laudo.Veiculo.Marca + "/" + laudo.Veiculo.Modelo;
        lbAno.Text = laudo.Veiculo.AnoFabricacao + "/" + laudo.Veiculo.AnoModelo;
    }
}