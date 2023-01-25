using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Services;

namespace Sigv.Mobile.Laudo.Views;

public partial class PageLogin : ContentPage
{
	public PageLogin()
	{
		InitializeComponent();
	}

    private void LoginButton_Clicked(object sender, EventArgs e)
    {
        var userName = UsernameEntry.Text;
        var password = PasswordEntry.Text;


        try
        {
            using (var client = new HttpClient())
            {
                var senhaEncr = Aplicacao.Util.AESEncrytDecry.EncryptStringAES(password);

                var loginEfetuado = (MensagemRetorno)new AuthService().RetornarLoginToken(userName, senhaEncr);

                if (loginEfetuado.Sucesso)
                {
                    using (var srv = new HttpService<Acesso>())
                    {
                        var acesso = new Acesso()
                        {
                            UsuarioId = Convert.ToInt32(UserPreferences.Logado.UsuarioId),
                            Ip = UserPreferences.Logado.Ip,
                            Data = DateTime.Now
                        };

                        srv.ExecuteService(acesso, "api/acesso/salvar");


                        Application.Current.MainPage = new PageHome();
                    }

                }
                else
                {
                    DisplayAlert("Resultado da operação", loginEfetuado.Mensagem, "OK");
                }
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Resultado da operação", "Houve um erro ao processar a rotina", "OK");
        }
    }
}