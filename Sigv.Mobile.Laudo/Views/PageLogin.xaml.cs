using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Services;

namespace Sigv.Mobile.Laudo.Views;

public partial class PageLogin : ContentPage
{
	public PageLogin()
	{
		InitializeComponent();
        
        //Api em Produ��o Servidor
        Preferences.Set("Api", "http://api.devmaia.com.br");

        //Api em Produ��o Local
        //Preferences.Set("Api", "http://192.168.1.108:8000");

        UsernameEntry.Text = "willie";
        PasswordEntry.Text = "abc.123";

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

                        Application.Current.MainPage = new PageMenu();

                    }

                }
                else
                {
                    DisplayAlert("Resultado da opera��o", loginEfetuado.Mensagem, "OK");
                }
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Resultado da opera��o", "Houve um erro ao processar a rotina", "OK");
        }
    }
}