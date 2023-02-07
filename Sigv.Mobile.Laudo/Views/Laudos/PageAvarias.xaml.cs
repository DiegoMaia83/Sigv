using Microsoft.Maui.Controls;
using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Aplicacao.App;
using Sigv.Mobile.Laudo.Services;
using System.ComponentModel;

namespace Sigv.Mobile.Laudo.Views.Laudos;

public partial class PageAvarias : ContentPage
{
    private readonly LaudoApp _laudoApp = new LaudoApp();

    public PageAvarias(LaudoVeiculo laudo)
    {
        InitializeComponent();

        bindingContextLaudo.BindingContext = laudo; 

        listViewAvarias.ItemsSource = ListarAvariasChecked(laudo);

    }


    // ---------- Page Actions ---------- //
    private void BtnSalvarAvarias_Clicked(object sender, EventArgs e)
    {
        try
        {
            btnSalvarAvarias.IsEnabled = true;

            LaudoVeiculo laudo = (LaudoVeiculo)bindingContextLaudo.BindingContext;
            var listaItens = listViewAvarias.ItemsSource;
            var listaApontamentos = _laudoApp.ListarAvariasApontamentos(laudo.LaudoId);

            foreach (LaudoAvaria avaria in listaItens)
            {
                if (listaApontamentos.Where(x => x.AvariaId == avaria.AvariaId).FirstOrDefault() == null)
                {
                    if (avaria.IsChecked)
                    {
                        var apontamento = new LaudoAvariaApontamento()
                        {
                            LaudoId = laudo.LaudoId,
                            AvariaId = avaria.AvariaId,
                            UsuarioCadastro = UserPreferences.Logado.Login,
                            DataCadastro = DateTime.Now
                        };

                        _laudoApp.InserirAvariaApontamento(apontamento);
                    }
                }
                else
                {
                    if (!avaria.IsChecked)
                    {
                        var apontamento = new LaudoAvariaApontamento()
                        {
                            LaudoId = laudo.LaudoId,
                            AvariaId = avaria.AvariaId,
                            UsuarioCadastro = UserPreferences.Logado.Login,
                            DataCadastro = DateTime.Now
                        };

                        _laudoApp.RemoverAvariaApontamento(apontamento);
                    }
                }
            }

            DisplayAlert("Alerta", "Operação realizada com sucesso!", "OK");
        }
        catch
        {
            DisplayAlert("Alerta", "Houve um erro ao processar a roltina!", "OK");
        }
    }

    private void BtnGoToLaudo_Clicked(object sender, EventArgs e)
    {
        var laudo = (LaudoVeiculo)bindingContextLaudo.BindingContext;

        FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
        page.Detail = new NavigationPage(new PageLaudo(laudo));
    }

    private List<LaudoAvaria> ListarAvariasChecked(LaudoVeiculo laudo)
    {
        var listaAvarias = _laudoApp.ListarAvarias();
        var listaApontamentos = _laudoApp.ListarAvariasApontamentos(laudo.LaudoId);

        var listaAvariasChecked = new List<LaudoAvaria>();

        // Verifica se existe apontamento salvos para popular a lista com checbox selecionado
        foreach (var item in listaAvarias)
        {
            if (listaApontamentos.Any(x => x.AvariaId == item.AvariaId))
            {
                item.IsChecked = true;
            }

            listaAvariasChecked.Add(item);
        }

        return listaAvariasChecked.OrderBy(x => x.Descricao).ToList();
    }

}