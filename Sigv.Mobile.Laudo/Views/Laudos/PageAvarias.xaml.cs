using Microsoft.Maui.Controls;
using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Services;
using System.ComponentModel;

namespace Sigv.Mobile.Laudo.Views.Laudos;

public partial class PageAvarias : ContentPage
{
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
            var listaApontamentos = ListarApontamentos(laudo.LaudoId);

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

                        InserirAvariaApontamento(apontamento);
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

                        RemoverAvariaApontamento(apontamento);
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
        var listaAvarias = ListarAvarias();
        var listaApontamentos = ListarApontamentos(laudo.LaudoId);

        var listaAvariasChecked = new List<LaudoAvaria>();
        // Verifica se existe apontamento salvo para popular a lista com checbox selecionado
        foreach (var item in ListarAvarias())
        {
            if (listaApontamentos.Any(x => x.AvariaId == item.AvariaId))
            {
                item.IsChecked = true;
            }

            listaAvariasChecked.Add(item);
        }

        return listaAvariasChecked.OrderBy(x => x.Descricao).ToList();
    }


    // --------- Context Methods ---------- //
    private LaudoAvariaApontamento InserirAvariaApontamento(LaudoAvariaApontamento apontamento)
	{
		try
		{
			using (var srv = new HttpService<LaudoAvariaApontamento>())
			{
				return srv.ExecuteService(apontamento, "api/laudo/inserir-avaria-apontamento");
			}
		}
		catch
		{
			return null;
		}
	}

    private LaudoAvariaApontamento RemoverAvariaApontamento(LaudoAvariaApontamento apontamento)
    {
        try
        {
            using (var srv = new HttpService<LaudoAvariaApontamento>())
            {
                return srv.ExecuteService(apontamento, "api/laudo/remover-avaria-apontamento");
            }
        }
        catch
        {
            return null;
        }
    }

    private List<LaudoAvaria> ListarAvarias()
	{
		try
		{
			var listaAvarias = new List<LaudoAvaria>();

			using (var srv = new HttpService<List<LaudoAvaria>>()) 
			{
				listaAvarias = srv.ReturnService("api/laudo/listar-avarias");
			}

			return listaAvarias;
		}
		catch
		{
			return null;
		}
	}

    private List<LaudoAvariaApontamento> ListarApontamentos(int laudoId)
    {
        try
        {
            var listaApontamentos = new List<LaudoAvariaApontamento>();

            using (var srv = new HttpService<List<LaudoAvariaApontamento>>())
            {
                listaApontamentos = srv.ReturnService("api/laudo/listar-avarias-apontamentos?laudoId=" + laudoId);
            }

            return listaApontamentos;
        }
        catch
        {
            return null;
        }
    }

}