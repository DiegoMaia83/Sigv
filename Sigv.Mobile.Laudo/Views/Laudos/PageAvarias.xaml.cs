using Sigv.Domain;
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

        BindingContext = new { Laudo = laudo };

        listViewAvarias.ItemsSource = ListarAvarias();
    }

    private void CheckBoxAvaria_CheckedChanged(object sender, CheckedChangedEventArgs e)
    {
        var checkBox = (CheckBox)sender;

        var dados = BindingContext as LaudoAvariaApontamento;

        var apontamento = new LaudoAvariaApontamento();

        if (checkBox.IsChecked)
        {
            // Checkbox foi selecionado
        }
        else
        {
            // Checkbox não foi selecionado
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
                return srv.ExecuteService(apontamento, "api/laudo/removeravaria-apontamento");
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
		catch (Exception ex) 
		{
			DisplayAlert("Atenção", "Houve um erro ao processar a rotina. " +  ex.Message, "OK");

			return null;
		}
	}


}