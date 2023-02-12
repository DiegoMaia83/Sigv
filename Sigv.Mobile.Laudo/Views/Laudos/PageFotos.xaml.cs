using Microsoft.Maui.Controls.PlatformConfiguration;
using Sigv.Domain;
using Sigv.Mobile.Laudo.Aplicacao;
using Sigv.Mobile.Laudo.Services;

namespace Sigv.Mobile.Laudo.Views.Laudos;

public partial class PageFotos : ContentPage
{
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

        

        //ListarFotos();

    }

    /*

    public async void TakePhoto(int veiculoId)
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

            if (photo != null)
            {
                var ultimaInserida = 0;

                using (var srv = new HttpService<int>())
                {
                    ultimaInserida = srv.ReturnService("api/veiculo-foto/retornar-ultima-inserida?veiculoId=1&tipo=LAU");
                }
                var numFoto = ultimaInserida + 1;
                //var extensao = Path.GetExtension(arq.SourcePath);
                var arquivo = "LAU" + veiculoId.ToString("000000") + "_" + numFoto.ToString("00");

                // save the file into local storage
                string localFilePath = Path.Combine(FileSystem.CacheDirectory, photo.FileName);

                using Stream sourceStream = await photo.OpenReadAsync();
                using FileStream localFileStream = File.OpenWrite(localFilePath);

                await sourceStream.CopyToAsync(localFileStream);
            }
        }
    }
    */

    //FileSystem.CacheDirectory
    //'/data/user/0/com.companyname.sigv.mobile.laudo/cache/


    private IEnumerable<VeiculoFoto> ListarFotos(int veiculoId)
    {
        try
        {
            var listaFotos = new List<VeiculoFoto>();

            using (var srv = new HttpService<List<VeiculoFoto>>())
            {
                listaFotos = srv.ReturnService("api/veiculo-foto/listar-por-tipo?veiculoId=" + veiculoId + "&tipo=LAU");
            }

            var listaFotosLaudo = new List<VeiculoFoto>();

            foreach (var item in listaFotos)
            {
                var foto = new VeiculoFoto();
                foto.VeiculoId = item.VeiculoId;
                foto.NumeroFoto = item.NumeroFoto;
                foto.Tipo = item.Tipo;
                foto.Extensao = item.Extensao;

                string nomeFoto = String.Format("{0}{1}{2}{3}{4}", foto.Tipo, foto.VeiculoId.ToString("000000"), "_", foto.NumeroFoto.ToString("00"), foto.Extensao);
                foto.SourcePath = Path.Combine(_diretorioLocal, nomeFoto);

                listaFotosLaudo.Add(foto);

            }

            return listaFotosLaudo;
        }
        catch(Exception ex)
        {
            throw ex;
        }

        //string path = _diretorioLocal;

        //var files = Directory.GetFiles(path);
        //var lista = files.Where(file => file.EndsWith(".jpg") || file.EndsWith(".jpeg") || file.EndsWith(".png"));
        
        /*
        foreach (var file in files)
        {
            Console.WriteLine(file);
        }
        */
    }



    private async void BtnCapture_Clicked(object sender, EventArgs e)
    {
        try
        {
            var veiculoId = Convert.ToInt32(lbVeiculoId.Text);

            if (MediaPicker.Default.IsCaptureSupported)
            {
                FileResult photo = await MediaPicker.Default.CapturePhotoAsync();

                if (photo != null)
                {
                    // Recupera o número da última fotó e monta o no da foto.
                    var numFoto = RetornarUltimaFotoInserida(veiculoId, "LAU") + 1;
                    var extensao = Path.GetExtension(photo.FullPath);
                    var arquivo = "LAU" + veiculoId.ToString("000000") + "_" + numFoto.ToString("00") + extensao;

                    photo.FileName = arquivo;

                    var foto = new VeiculoFoto
                    {
                        VeiculoId = veiculoId,
                        NumeroFoto = numFoto,
                        Tipo = "LAU",
                        Extensao = extensao,
                        UsuCriacao = UserPreferences.Logado.Login,
                        DataCriacao = DateTime.Now,
                        Excluida = false
                    };

                    SalvarFoto(foto);

                    // save the file into local storage
                    string localFilePath = Path.Combine(_diretorioLocal, photo.FileName);

                    using Stream sourceStream = await photo.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);

                    await sourceStream.CopyToAsync(localFileStream);
                }
            }
            //TakePhoto(veiculoId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private int RetornarUltimaFotoInserida(int veiculoId, string tipoFoto)
    {
        try
        {
            var ultimaInserida = 0;
            
            using (var srv = new HttpService<int>())
            {
                ultimaInserida = srv.ReturnService("api/veiculo-foto/retornar-ultima-inserida?veiculoId=" + veiculoId + "&tipo=" + tipoFoto);
            }
            
            return ultimaInserida;
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    private VeiculoFoto SalvarFoto(VeiculoFoto foto)
    {
        try
        {
            using (var srv = new HttpService<VeiculoFoto>())
            {
                return srv.ExecuteService(foto, "api/veiculo-foto/salvar");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}