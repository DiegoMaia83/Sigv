using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Services;

namespace Sigv.Mobile.Laudo.Views;

public partial class PageLogin : ContentPage
{
	public PageLogin()
	{
		InitializeComponent();

        ApiEntry.Text = Preferences.Get("Api", "") == "" ? "http://api.devmaia.com.br" : Preferences.Get("Api", "");

        //Api em Produ��o Servidor
        //Preferences.Set("Api", "http://api.devmaia.com.br");

        //Api em Produ��o Local
        //Preferences.Set("Api", "http://192.168.1.108:8000");

        UsernameEntry.Text = Preferences.Get("Username", "");
        PasswordEntry.Text = Preferences.Get("Password", "");

    }

    private void LoginButton_Clicked(object sender, EventArgs e)
    {
        var userName = UsernameEntry.Text;
        var password = PasswordEntry.Text;

        Preferences.Set("Username", UsernameEntry.Text);
        Preferences.Set("Password", PasswordEntry.Text);
        Preferences.Set("Api", ApiEntry.Text);        

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

    private void btShowApi_Clicked(object sender, TappedEventArgs e)
    {
        if (ApiEntryContent.IsVisible == false)
        {
            ApiEntryContent.IsVisible = true;
        }
        else
        {
            ApiEntryContent.IsVisible = false;
        }
    }
}