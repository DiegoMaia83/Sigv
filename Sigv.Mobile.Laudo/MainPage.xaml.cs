using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Services;

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

        private void Teste()
        {
            try
            {
                using (var client = new HttpClient())
                {
                    var senhaEncr = Aplicacao.Util.AESEncrytDecry.EncryptStringAES("Martins.1101");

                    var loginEfetuado = (MensagemRetorno)new AuthService().RetornarLoginToken("diego", senhaEncr);

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenPreferences.Token);

                    var urlService = "http://api.devmaia.com.br/api/usuario/listar";
                    var response = client.GetAsync(urlService).Result;

                    var content = response.Content.ReadAsStringAsync().Result;

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