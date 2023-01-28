using Newtonsoft.Json;
using Sigv.Mobile.Laudo.Aplicacao;

namespace Sigv.Mobile.Laudo.Services
{
    public class AuthService
    {
        //Faz Login e armazena o token no cookie
        public object ApiAuthGetToken(string login, string password)
        {

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    var urlService = Preferences.Get("Api", "") + "/auth/token";

                    var dict = new Dictionary<string, string>();
                    dict.Add("username", login);
                    dict.Add("password", password);
                    dict.Add("grant_type", "password");

                    var content = new FormUrlEncodedContent(dict);
                    var request = new HttpRequestMessage(HttpMethod.Post, urlService) { Content = content };

                    var response = client.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var token = JsonConvert.DeserializeObject<Token>(response.Content.ReadAsStringAsync().Result);
                        TokenPreferences.Token = token.access_token;
                        return new MensagemRetorno { Sucesso = true, Mensagem = "Token armazenado." };
                    }

                    return new MensagemRetorno { Sucesso = false, Mensagem = "Não foi possível autenticar na API. Verifique os dados e tente novamente", Erro = response.Content.ReadAsStringAsync().Result };

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //Verifica o estado do token na API
        /*
        public bool RetornarTokenState()
        {

            try
            {

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenCookie.Token);


                    var urlService = _apiUri + "/api/login/token-state";
                    var response = client.GetAsync(urlService).Result;

                    var content = response.Content.ReadAsStringAsync().Result;

                    //Se retornar com o token expirado ou algum erro de http
                    if ((content.IndexOf("Authorization has been denied") > -1) || !response.IsSuccessStatusCode)
                    {
                        return false;
                    }


                    return true;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        */

        //Chama a authenticação via token e traz os dados do usuário para armazenar em cookies
        public object RetornarLoginToken(string login, string password)
        {
            try
            {
                //Pega e armazena o token
                var tokenGerado = ApiAuthGetToken(login, password);

                //Se não armazenou o token retona erro
                if (!((MensagemRetorno)tokenGerado).Sucesso)
                {
                    return Task.FromResult(tokenGerado).Result;
                }

                using (HttpClient client = new HttpClient())
                {
                    var urlService = Preferences.Get("Api", "") + "/api/login/in";

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenPreferences.Token);


                    var request = new HttpRequestMessage(HttpMethod.Post, urlService);

                    var response = client.SendAsync(request).Result;

                    if (response.IsSuccessStatusCode)
                    {
                        var usuario = JsonConvert.DeserializeObject<UsuarioLogado>(response.Content.ReadAsStringAsync().Result);
                        UserPreferences.SetPreferences(usuario);
                        return new MensagemRetorno { Sucesso = true, Mensagem = "Login efetuado com sucesso!" };
                    }
                    else
                    {
                        return new MensagemRetorno { Sucesso = false, Mensagem = "Não foi possível efetuar o login. Tente mais tarde.", Erro = response.Content.ReadAsStringAsync().Result };

                    }
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
