using Sigv.Domain;

namespace Sigv.Mobile.Laudo.Views.Laudos;

public partial class PageFotos : ContentPage
{
	public PageFotos(LaudoVeiculo laudo)
	{
		InitializeComponent();

        bindingContextLaudo.BindingContext = laudo;

        lbPlaca.Text = laudo.Veiculo.Placa.ToString();
        lbMarca.Text = laudo.Veiculo.Marca + "/" + laudo.Veiculo.Modelo;
        lbAno.Text = laudo.Veiculo.AnoFabricacao + "/" + laudo.Veiculo.AnoModelo;
    }
}