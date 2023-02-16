using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Aplicacao.App;
using Sigv.Mobile.Laudo.Services;

namespace Sigv.Mobile.Laudo.Views.Laudos;

public partial class PageLaudo : ContentPage
{
    private readonly LaudoApp _laudoApp = new LaudoApp();

    public PageLaudo(LaudoVeiculo laudo)
    {
        InitializeComponent();

        bindingContextLaudo.BindingContext = laudo;

        lbPlaca.Text = laudo.Veiculo.Placa.ToString();
        lbMarca.Text = laudo.Veiculo.Marca + "/" + laudo.Veiculo.Modelo;
        lbAno.Text = laudo.Veiculo.AnoFabricacao + "/" + laudo.Veiculo.AnoModelo;


        resumoAvarias.Text = _laudoApp.RetornarResumoAvarias(laudo.LaudoId);
        resumoOpcionais.Text = "Avarias";
        resumoFotos.Text = "Avarias";
    }

    private void BtnAvarias_Clicked(object sender, EventArgs e)
    {
        var laudo = (LaudoVeiculo)bindingContextLaudo.BindingContext;

        FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
        page.Detail = new NavigationPage(new PageAvarias(laudo));
    }

    private void BtnOpcionais_Clicked(object sender, EventArgs e)
    {
        var laudo = (LaudoVeiculo)bindingContextLaudo.BindingContext;

        FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
        page.Detail = new NavigationPage(new PageOpcionais(laudo));
    }

    private void BtnFotos_Clicked(object sender, EventArgs e)
    {
        var laudo = (LaudoVeiculo)bindingContextLaudo.BindingContext;

        FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
        page.Detail = new NavigationPage(new PageFotos(laudo));
    }

    private async void BtnFinalizarLaudo_Clicked(object sender, EventArgs e)
    {
        LaudoVeiculo laudo = (LaudoVeiculo)bindingContextLaudo.BindingContext;

        var confirm = await DisplayAlert("Finalizar laudo", "Deseja realmente finalizar o Laudo desse veículo?", "FINALIZAR", "CANCELAR");

        if (confirm)
        {            
            var result = _laudoApp.FinalizarLaudo(laudo.LaudoId);

            if (result != null)
            {
                await DisplayAlert("Laudo finalizado", "Laudo finalizado com sucesso!", "OK");

                FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
                page.Detail = new NavigationPage(new PageHome());
            }
            else
            {
                await DisplayAlert("Alerta", "Houve um erro ao finalizar o Laudo!", "OK");
            }
            
        }
    }

}