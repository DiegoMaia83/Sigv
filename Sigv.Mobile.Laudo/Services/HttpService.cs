using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Sigv.Mobile.Laudo.Aplicacao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Mobile.Laudo.Services
{
    public class HttpService<T> : IDisposable
    {
        //Retorna um httpService 
        public T ReturnService(string uri)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenPreferences.Token);

                    var urlService = Preferences.Get("Api", "") + "/" + uri;
                    var response = client.GetAsync(urlService).Result;

                    var content = response.Content.ReadAsStringAsync().Result;

                    if (content.IndexOf("Authorization has been denied") > -1)
                    {
                        throw new InvalidOperationException("Token expirado!");
                    }

                    //Erro gerado manualmente para retornar na api
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new InvalidOperationException(JsonConvert.DeserializeObject<MensagemApi>(content).ExceptionMessage);
                    }

                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        //Retorna um httpService com retorno de texto
        public string ReturnServiceAsString(string uri)
        {
            try
            {

                using (var client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenPreferences.Token);

                    var urlService = Preferences.Get("Api", "") + "/" + uri;
                    var response = client.GetAsync(urlService).Result;

                    var content = response.Content.ReadAsStringAsync().Result;

                    if (content.IndexOf("Authorization has been denied") > -1)
                    {
                        throw new InvalidOperationException("Token expirado!");
                    }

                    //Erro gerado manualmente para retornar na api
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new InvalidOperationException(JsonConvert.DeserializeObject<MensagemApi>(content).ExceptionMessage);
                    }

                    return response.Content.ReadAsStringAsync().Result;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        //Executa um httpService 
        public T ExecuteService(object obj, string uri)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    var stream = JsonConvert.SerializeObject(obj);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(stream);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenPreferences.Token);

                    var urlService = Preferences.Get("Api", "") + "/" + uri;
                    var response = client.PostAsync(urlService, byteContent).Result;

                    var content = response.Content.ReadAsStringAsync().Result;

                    if (content.IndexOf("Authorization has been denied") > -1)
                    {
                        throw new InvalidOperationException("Token expirado!");
                    }

                    //Erro gerado manualmente para retornar na api
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new InvalidOperationException(JsonConvert.DeserializeObject<MensagemApi>(content).ExceptionMessage);
                    }


                    return JsonConvert.DeserializeObject<T>(response.Content.ReadAsStringAsync().Result);
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        //Executa um httpService com retorno de texto
        public string ExecuteServiceAsString(object obj, string uri)
        {

            try
            {
                using (var client = new HttpClient())
                {
                    var stream = JsonConvert.SerializeObject(obj);
                    var buffer = System.Text.Encoding.UTF8.GetBytes(stream);
                    var byteContent = new ByteArrayContent(buffer);
                    byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

                    client.DefaultRequestHeaders.Clear();
                    client.DefaultRequestHeaders.Add("Authorization", "Bearer " + TokenPreferences.Token);

                    var urlService = Preferences.Get("Api", "") + "/" + uri;
                    var response = client.PostAsync(urlService, byteContent).Result;

                    var content = response.Content.ReadAsStringAsync().Result;

                    //Erro gerado manualmente para retornar na api
                    if (!response.IsSuccessStatusCode)
                    {
                        throw new InvalidOperationException(JsonConvert.DeserializeObject<MensagemApi>(content).ExceptionMessage);
                    }

                    return response.Content.ReadAsStringAsync().Result;
                }


            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public void Dispose()
        {
            //Nothing to do
        }

    }
}
