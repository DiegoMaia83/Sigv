using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Aplicacao.App;
using Sigv.Mobile.Laudo.Services;
using Sigv.Mobile.Laudo.Views.Comum;
using Sigv.Mobile.Laudo.Views.Laudos;

namespace Sigv.Mobile.Laudo.Views;

[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class PageHome : ContentPage
{   
    private readonly LaudoApp _laudoApp = new LaudoApp();

    public PageHome(int statusId)
    {
        InitializeComponent();

        loadingOverlay.IsVisible = true;
        lbTitleLaudo.Text = _laudoApp.RetornarTituloLista(statusId);

        Task.Run(() =>
        {
            try
            {
                var listaVeiculos = _laudoApp.ListarVeiculos(statusId);  

                Dispatcher.Dispatch(() =>
                {
                    listViewVeiculos.ItemsSource = listaVeiculos;
                    loadingOverlay.IsVisible = false;                    
                });
            }
            catch (Exception ex)
            {
                DisplayAlert("Alerta", "Houve um erro ao processar a rotina! " + ex.Message, "OK");
                loadingOverlay.IsVisible = false;
            }
        });      
    }

    private void ListarVeiculosLaudos_Clicked(object sender, EventArgs e)
    {
        try
        {
            var button = (Button)sender;
		    var statusId = Convert.ToInt32(button.CommandParameter);

            FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
            page.Detail = new NavigationPage(new PageHome(statusId));
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private async void listViewVeiculos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
    {
        try
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
        catch (Exception ex)
        {
            await DisplayAlert("Alerta", "Houve um erro ao processar a rotina! " + ex.Message, "OK");
        }
    }
}