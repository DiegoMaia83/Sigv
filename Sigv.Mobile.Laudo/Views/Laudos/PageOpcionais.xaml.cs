using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Aplicacao.App;
using Sigv.Mobile.Laudo.Services;

namespace Sigv.Mobile.Laudo.Views.Laudos;

public partial class PageOpcionais : ContentPage
{
    private readonly LaudoApp _laudoApp = new LaudoApp();

	public PageOpcionais(LaudoVeiculo laudo)
	{
		InitializeComponent();

        bindingContextLaudo.BindingContext = laudo;

        listViewOpcionais.ItemsSource = ListarOpcionaisChecked(laudo);
    }


    // ---------- Page Actions ---------- //
    private void BtnSalvarOpcionais_Clicked(object sender, EventArgs e)
    {
        try
        {
            btnSalvarOpcionais.IsEnabled = true;

            LaudoVeiculo laudo = (LaudoVeiculo)bindingContextLaudo.BindingContext;
            var listaItens = listViewOpcionais.ItemsSource;
            var listaApontamentos = _laudoApp.ListarOpcionaisApontamentos(laudo.LaudoId);

            foreach (LaudoOpcional opcional in listaItens)
            {
                if (listaApontamentos.Where(x => x.OpcionalId == opcional.OpcionalId).FirstOrDefault() == null)
                {
                    if (opcional.IsChecked)
                    {
                        var apontamento = new LaudoOpcionalApontamento()
                        {
                            LaudoId = laudo.LaudoId,
                            OpcionalId = opcional.OpcionalId,
                            UsuarioCadastro = UserPreferences.Logado.Login,
                            DataCadastro = DateTime.Now
                        };

                        _laudoApp.InserirOpcionalApontamento(apontamento);
                    }
                }
                else
                {
                    if (!opcional.IsChecked)
                    {
                        var apontamento = new LaudoOpcionalApontamento()
                        {
                            LaudoId = laudo.LaudoId,
                            OpcionalId = opcional.OpcionalId,
                            UsuarioCadastro = UserPreferences.Logado.Login,
                            DataCadastro = DateTime.Now
                        };

                        _laudoApp.RemoverOpcionalApontamento(apontamento);
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
        try
        {
            var laudo = (LaudoVeiculo)bindingContextLaudo.BindingContext;

            FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
            page.Detail = new NavigationPage(new PageLaudo(laudo));
        }
        catch
        {
            DisplayAlert("Alerta", "Houve um erro ao processar a roltina!", "OK");
        }
    }

    private List<LaudoOpcional> ListarOpcionaisChecked(LaudoVeiculo laudo)
    {
        var listaOpcionais = _laudoApp.ListarOpcionais();
        var listaApontamentos = _laudoApp.ListarOpcionaisApontamentos(laudo.LaudoId);

        var listaOpcionaisChecked = new List<LaudoOpcional>();

        // Verifica se existe apontamento salvos para popular a lista com checbox selecionado
        foreach (var item in listaOpcionais)
        {
            if (listaApontamentos.Any(x => x.OpcionalId == item.OpcionalId))
            {
                item.IsChecked = true;
            }

            listaOpcionaisChecked.Add(item);
        }

        return listaOpcionaisChecked.OrderBy(x => x.Descricao).ToList();
    }

}