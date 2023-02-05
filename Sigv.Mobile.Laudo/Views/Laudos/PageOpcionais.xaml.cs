using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Services;

namespace Sigv.Mobile.Laudo.Views.Laudos;

public partial class PageOpcionais : ContentPage
{
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
            var listaApontamentos = ListarApontamentos(laudo.LaudoId);

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

                        InserirOpcionalApontamento(apontamento);
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

                        RemoverOpcionalApontamento(apontamento);
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

    private List<LaudoOpcional> ListarOpcionaisChecked(LaudoVeiculo laudo)
    {
        var listaAvarias = ListarOpcionais();
        var listaApontamentos = ListarApontamentos(laudo.LaudoId);

        var listaOpcionaisChecked = new List<LaudoOpcional>();
        // Verifica se existe apontamento salvo para popular a lista com checbox selecionado
        foreach (var item in ListarOpcionais())
        {
            if (listaApontamentos.Any(x => x.OpcionalId == item.OpcionalId))
            {
                item.IsChecked = true;
            }

            listaOpcionaisChecked.Add(item);
        }

        return listaOpcionaisChecked.OrderBy(x => x.Descricao).ToList();
    }


    // --------- Context Methods ---------- //
    private LaudoOpcionalApontamento InserirOpcionalApontamento(LaudoOpcionalApontamento apontamento)
    {
        try
        {
            using (var srv = new HttpService<LaudoOpcionalApontamento>())
            {
                return srv.ExecuteService(apontamento, "api/laudo/inserir-opcional-apontamento");
            }
        }
        catch
        {
            return null;
        }
    }

    private LaudoOpcionalApontamento RemoverOpcionalApontamento(LaudoOpcionalApontamento apontamento)
    {
        try
        {
            using (var srv = new HttpService<LaudoOpcionalApontamento>())
            {
                return srv.ExecuteService(apontamento, "api/laudo/remover-opcional-apontamento");
            }
        }
        catch
        {
            return null;
        }
    }

    private List<LaudoOpcional> ListarOpcionais()
    {
        try
        {
            var listaOpcionais = new List<LaudoOpcional>();

            using (var srv = new HttpService<List<LaudoOpcional>>())
            {
                listaOpcionais = srv.ReturnService("api/laudo/listar-opcionais");
            }

            return listaOpcionais;
        }
        catch
        {
            return null;
        }
    }

    private List<LaudoOpcionalApontamento> ListarApontamentos(int laudoId)
    {
        try
        {
            var listaApontamentos = new List<LaudoOpcionalApontamento>();

            using (var srv = new HttpService<List<LaudoOpcionalApontamento>>())
            {
                listaApontamentos = srv.ReturnService("api/laudo/listar-opcionais-apontamentos?laudoId=" + laudoId);
            }

            return listaApontamentos;
        }
        catch
        {
            return null;
        }
    }
}