using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Services;
using System.Net;
using System.Net.Sockets;

namespace Sigv.Mobile.Laudo
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();

            Teste();
        }

        private async void Teste()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var senhaEncr = Aplicacao.Util.AESEncrytDecry.EncryptStringAES("Martins.1101");

                    var loginEfetuado = (MensagemRetorno)new AuthService().RetornarLoginToken("diego", senhaEncr);

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
                        }
                    }

                    lbToken.Text = TokenPreferences.Token;

                    lbNome.Text = UserPreferences.Logado.Nome;
                    lbLogin.Text = UserPreferences.Logado.Login;
                    lbUsuarioId.Text = UserPreferences.Logado.UsuarioId;
                    lbGrupoId.Text = UserPreferences.Logado.GrupoId;
                    
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void OnCounterClicked(object sender, EventArgs e)
        {
            count++;

            if (count == 1)
                CounterBtn.Text = $"Clicked {count} time";
            else
                CounterBtn.Text = $"Clicked {count} times";

            SemanticScreenReader.Announce(CounterBtn.Text);
        }
    }
}