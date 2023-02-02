using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Services;

namespace Sigv.Mobile.Laudo.Views.Laudos;

public partial class PageAvarias : ContentPage
{
	public PageAvarias()
	{
		InitializeComponent();

        listViewAvarias.ItemsSource = ListarAvarias();
	}

    public PageAvarias(LaudoVeiculo laudo)
    {
        InitializeComponent();

        bindingContextLaudo.BindingContext = laudo;

        var listaAvariasChecked = new List<LaudoAvaria>();

        var listaAvarias = ListarAvarias();
        var listaApontamentos = ListarApontamentos(laudo.LaudoId);

        foreach (var item in ListarAvarias())
        {
            if (listaApontamentos.Any(x => x.AvariaId == item.AvariaId))
            {
                item.IsChecked = true;
            }

            listaAvariasChecked.Add(item);
        }

        listViewAvarias.ItemsSource = listaAvariasChecked;
    }

    private void CheckBoxAvaria_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkBox = (CheckBox)sender;

        var laudo = (LaudoVeiculo)bindingContextLaudo.BindingContext;
        var avaria = (LaudoAvaria)checkBox.BindingContext;

        var apontamento = new LaudoAvariaApontamento()
        {
            LaudoId = laudo.LaudoId,
            AvariaId = avaria.AvariaId,
            UsuarioCadastro = UserPreferences.Logado.Login,
            DataCadastro = DateTime.Now
        };

        if (checkBox.IsChecked)
        {
            InserirAvariaApontamento(apontamento);
        }
        else
        {
            RemoverAvariaApontamento(apontamento);
        }
    }

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