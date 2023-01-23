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
                    string senhaDecr = Aplicacao.Util.AESEncrytDecry.DecryptStringAES("fOEbHAaCQv62wfw/UArvTg==");
                    var senhaEncr = Aplicacao.Util.AESEncrytDecry.EncryptStringAES("Martins.1101");


                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer W00rp12awhaF-DmpfIlylfnI9UgM6glxbqNZon5PW5M37tZ8QUQGZRAIJneFXBtpmDiOTw0lLBHRGP3sHB1fPLbFk2pdFPkBjfHk3vGtNJqT0odPCVB6DB42mnIjfAnGKXROmcdBwVqFBlo7ayJNHotKXh9txEiNF0SiW_-4Jvk1864UFM7hmVFtizM-VZ0vxwz2z6Ec0SuZhK-YP5dXgg6efqizVSqZM25nMhNhYx8ulU8Hq2-hwFbTSJWOcHHw6LaqTUU6Lv9hxTu9iXLzqw");

                    var urlService = "http://api.devmaia.com.br/api/usuario/listar";
                    var response = client.GetAsync(urlService).Result;

                    var content = response.Content.ReadAsStringAsync().Result;
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