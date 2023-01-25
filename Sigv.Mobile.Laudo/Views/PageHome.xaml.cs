using Sigv.Mobile.Laudo.Aplicacao;

namespace Sigv.Mobile.Laudo.Views;

public partial class PageHome : ContentPage
{
	public PageHome()
	{
		InitializeComponent();

		lbGrupoId.Text = UserPreferences.Logado.GrupoId.ToString();
		lbNome.Text= UserPreferences.Logado.Nome.ToString();
		lbUsuario.Text = UserPreferences.Logado.Login.ToString();
		lbUsuarioId.Text = UserPreferences.Logado.UsuarioId.ToString();
		lbToken.Text = TokenPreferences.Token.ToString();
	}
}