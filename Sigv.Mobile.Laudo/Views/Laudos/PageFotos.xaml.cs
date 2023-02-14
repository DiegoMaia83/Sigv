using Microsoft.Maui.Controls.PlatformConfiguration;
using Plugin.Media.Abstractions;
using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Aplicacao.App;
using Sigv.Mobile.Laudo.Services;
using System.Globalization;
using System.Security.AccessControl;

namespace Sigv.Mobile.Laudo.Views.Laudos;

public partial class PageFotos : ContentPage
{
    private readonly LaudoApp _laudoApp = new LaudoApp();

    // /storage/emulated/0/Android/data/com.companyname.sigv.mobile.laudo/files/Pictures
    private string _diretorioLocal = Path.Combine(Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryPictures).AbsolutePath, "LaudoApp");


    public PageFotos(LaudoVeiculo laudo)
	{
		InitializeComponent();

        if (!System.IO.Directory.Exists(_diretorioLocal))
        {
            System.IO.Directory.CreateDirectory(_diretorioLocal);            
        }

        bindingContextLaudo.BindingContext = laudo;

        lbPlaca.Text = laudo.Veiculo.Placa.ToString();
        lbMarca.Text = laudo.Veiculo.Marca + "/" + laudo.Veiculo.Modelo;
        lbAno.Text = laudo.Veiculo.AnoFabricacao + "/" + laudo.Veiculo.AnoModelo;
        lbVeiculoId.Text = laudo.VeiculoId.ToString();

        listViewFotos.ItemsSource = ListarFotos(laudo.VeiculoId);

    }   

    private IEnumerable<VeiculoFoto> ListarFotos(int veiculoId)
    {
        try
        {
            var listaFotos = new List<VeiculoFoto>();

            using (var srv = new HttpService<List<VeiculoFoto>>())
            {
                listaFotos = srv.ReturnService("api/veiculo-foto/listar-por-tipo?veiculoId=" + veiculoId + "&tipo=LAU");
            }

            foreach (var item in listaFotos)
            {
                string nomeFoto = String.Format("{0}{1}", item.Identificador, item.Extensao);
                item.SourcePath = Path.Combine(_diretorioLocal, nomeFoto);
            }

            return listaFotos;
        }
        catch
        {
            return null;
        }
    }


    private async void BtnCapturar_Clicked(object sender, EventArgs e)
    {
        try
        {
            var veiculoId = Convert.ToInt32(lbVeiculoId.Text);            

            if (MediaPicker.Default.IsCaptureSupported)
            {   
                // Captura a foto
                FileResult fileResult = await MediaPicker.Default.CapturePhotoAsync();

                if (fileResult != null)
                {
                    // Recupera o n�mero da �ltima foto e monta o nome da foto.
                    var numFoto = _laudoApp.RetornarUltimaFotoInserida(veiculoId, "LAU") + 1;
                    var extensao = Path.GetExtension(fileResult.FullPath);


                    // Define o diret�rio completo do arquivo, e salva o arquivo no diret�rio local
                    string localFilePath = Path.Combine(_diretorioLocal, fileResult.FileName);
                    using Stream sourceStream = await fileResult.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);
                    await sourceStream.CopyToAsync(localFileStream);


                    // Salva a foto no banco de dados
                    var foto = new VeiculoFoto
                    {
                        VeiculoId = veiculoId,
                        NumeroFoto = numFoto,
                        Tipo = "LAU",
                        Extensao = extensao,
                        UsuCriacao = UserPreferences.Logado.Login,
                        DataCriacao = DateTime.Now,
                        Excluida = false,
                        Identificador = fileResult.FileName.Split('.')[0]
                    };

                    var fotoSalva = _laudoApp.SalvarFoto(foto);

                    // Salva o log
                    if (fotoSalva != null)
                    {
                        var log = new Log()
                        {
                            CodReferencia = veiculoId,
                            Processo = "Veiculo",
                            UsuarioId = Convert.ToInt32(UserPreferences.Logado.UsuarioId),
                            Ip = UserPreferences.Logado.Ip,
                            DataLog = DateTime.Now,
                            Descricao = "Inseriu o arquivo id [ " + fotoSalva.FotoId + " ]"
                        };

                        using (var conn = new HttpService<Log>())
                        {
                            conn.ExecuteService(log, "api/log/salvar");
                        };
                    }

                    this.OnAppearing();
                }
            }            
        }
        catch (Exception ex)
        {
            await DisplayAlert("Aten��o", "Houve um erro ao processar a rotina! " + ex.Message, "OK");
        }
    }

    private void BtnRemover_Clicked(object sender, EventArgs e)
    {
        try
        {
            var button = (Button)sender;
            int fotoId = (int)button.CommandParameter;

            if (fotoId > 0)
            {
                // Remove a foto do banco de dados
                var foto = new VeiculoFoto
                {
                    FotoId = fotoId,
                    UsuExclusao = UserPreferences.Logado.Login,
                    DataExclusao = DateTime.Now
                };

                var result = _laudoApp.RemoverFoto(foto);

                // Salva o log
                if (result.FotoId > 0)
                {
                    var log = new Log()
                    {
                        CodReferencia = 2,
                        Processo = "Veiculo",
                        UsuarioId = Convert.ToInt32(UserPreferences.Logado.UsuarioId),
                        Ip = UserPreferences.Logado.Ip,
                        DataLog = DateTime.Now,
                        Descricao = "Removeu o arquivo id [ " + foto.FotoId + " ]"
                    };

                    using (var conn = new HttpService<Log>())
                    {
                        conn.ExecuteService(log, "api/log/salvar");
                    };
                }

                DisplayAlert("Aten��o", "Foto removida com sucesso!", "OK");

                OnAppearing();
            }
            else
            {
                DisplayAlert("Aten��o", "Foto n�o localizada!", "OK");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Aten��o", "Houve um erro ao processar a rotina! " + ex.Message, "OK");
        }
    }


    private void BtnGoToLaudo_Clicked(object sender, EventArgs e)
    {
        var laudo = (LaudoVeiculo)bindingContextLaudo.BindingContext;

        FlyoutPage page = (FlyoutPage)Application.Current.MainPage;
        page.Detail = new NavigationPage(new PageLaudo(laudo));
    }



    protected override void OnAppearing()
    {
        LaudoVeiculo laudo = (LaudoVeiculo)bindingContextLaudo.BindingContext;

        base.OnAppearing();

        listViewFotos.ItemsSource = listViewFotos.ItemsSource = ListarFotos(laudo.VeiculoId);
    }
}