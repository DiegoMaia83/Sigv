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

        string resumoAvarias = _laudoApp.RetornarResumoAvarias(laudo.LaudoId);
        string resumoOpcionais = _laudoApp.RetornarResumoOpcionais(laudo.LaudoId);
        int qtdFotos = _laudoApp.ListarFotos(laudo.VeiculoId).Count();

        lbResumoAvarias.Text = !String.IsNullOrEmpty(resumoAvarias) ? resumoAvarias : "Nenhuma avaria selecionada";
        lbResumoOpcionais.Text = !String.IsNullOrEmpty(resumoOpcionais) ? resumoOpcionais : "Nenhum opcional selecionado";
        lbResumoFotos.Text = qtdFotos > 0 ? qtdFotos + " fotos adicionadas" : "Nenhuma foto adicionada";
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
                var log = new Log
                {
                    CodReferencia = result.VeiculoId,
                    Processo = "Veiculo",
                    UsuarioId = Convert.ToInt32(UserPreferences.Logado.UsuarioId),
                    Ip = UserPreferences.Logado.Ip,
                    DataLog = DateTime.Now,
                    Descricao = "Finalizou o laudo id [ " + result.LaudoId + " ]"
                };

                using (var conn = new HttpService<Log>())
                {
                    conn.ExecuteService(log, "api/log/salvar");
                };

                await DisplayAlert("Laudo finalizado", "Laudo finalizado com sucesso!", "OK");

                FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
                page.Detail = new NavigationPage(new PageHome(2));
            }
            else
            {
                await DisplayAlert("Alerta", "Houve um erro ao finalizar o Laudo!", "OK");
            }
            
        }
    }

}