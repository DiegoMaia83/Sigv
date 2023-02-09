using Sigv.Domain;
using Sigv.Mobile.Laudo.Services;

namespace Sigv.Mobile.Laudo.Views.Laudos;

public partial class PageFotos : ContentPage
{
	public PageFotos(LaudoVeiculo laudo)
	{
		InitializeComponent();

        bindingContextLaudo.BindingContext = laudo;

        lbPlaca.Text = laudo.Veiculo.Placa.ToString();
        lbMarca.Text = laudo.Veiculo.Marca + "/" + laudo.Veiculo.Modelo;
        lbAno.Text = laudo.Veiculo.AnoFabricacao + "/" + laudo.Veiculo.AnoModelo;
        lbVeiculoId.Text = laudo.VeiculoId.ToString();

    }

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
                    ultimaInserida = srv.ReturnService("api/veiculo-foto/retornar-ultima-inserida?veiculoId=1&tipo=PUB");
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

    private void BtnCapture_Clicked(object sender, EventArgs e)
    {
        try
        {
            var veiculoId = Convert.ToInt32(lbVeiculoId.Text);

            // /data/user/0/com.companyname.sigv.mobile.laudo/files/LaudoApp
            var diretorio = Path.Combine(FileSystem.AppDataDirectory, "LaudoApp");
        
            if (!System.IO.Directory.Exists(diretorio))
            {
                System.IO.Directory.CreateDirectory(diretorio);
            }


            TakePhoto(veiculoId);
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }
}