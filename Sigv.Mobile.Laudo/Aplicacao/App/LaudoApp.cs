using Sigv.Domain;
using Sigv.Mobile.Laudo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Mobile.Laudo.Aplicacao.App
{
    public class LaudoApp
    {
        // /storage/emulated/0/Android/data/com.companyname.sigv.mobile.laudo/files/Pictures
        private string _diretorioLocal = Path.Combine(Android.App.Application.Context.GetExternalFilesDir(Android.OS.Environment.DirectoryPictures).AbsolutePath, "LaudoApp");

        // --------- Laudo ---------- //
        public LaudoVeiculo RetornarLaudo(int laudoId)
        {
            try
            {
                using (var srv = new HttpService<LaudoVeiculo>())
                {
                    return srv.ReturnService("api/laudo/retornar?LaudoId=" + laudoId);
                }
            }
            catch
            {
                return null;
            }
        }

        public LaudoVeiculo IniciarLaudo(Veiculo veiculo)
        {
            try
            {
                var laudo = new LaudoVeiculo
                {
                    VeiculoId = veiculo.VeiculoId,
                    StatusId = 1,
                    UsuarioAbertura = UserPreferences.Logado.Login,
                    DataAbertura = DateTime.Now
                };

                using (var srv = new HttpService<LaudoVeiculo>())
                {
                    return srv.ExecuteService(laudo, "api/laudo/inserir");
                }
            }
            catch
            {
                return null;
            }
        }

        public LaudoVeiculo ReabrirLaudo(int laudoId)
        {
            try
            {
                var laudo = new LaudoVeiculo
                {
                    LaudoId = laudoId,
                    UsuarioReabre = UserPreferences.Logado.Login,
                    DataReabre = DateTime.Now
                };

                using (var srv = new HttpService<LaudoVeiculo>())
                {
                    return srv.ExecuteService(laudo, "api/laudo/reabrir");
                }
            }
            catch
            {
                return null;
            }
        }

        public LaudoVeiculo FinalizarLaudo(int laudoId)
        {
            try
            {
                var laudo = new LaudoVeiculo
                {
                    LaudoId = laudoId,
                    UsuarioFechamento = UserPreferences.Logado.Login,
                    DataFechamento = DateTime.Now
                };

                using (var srv = new HttpService<LaudoVeiculo>())
                {
                    return srv.ExecuteService(laudo, "api/laudo/finalizar");
                }
            }
            catch
            {
                return null;
            }
        }




        // --------- Avarias ---------- //
        public LaudoAvariaApontamento InserirAvariaApontamento(LaudoAvariaApontamento apontamento)
        {
            try
            {
                using (var srv = new HttpService<LaudoAvariaApontamento>())
                {
                    return srv.ExecuteService(apontamento, "api/laudo/inserir-avaria-apontamento");
                }
            }
            catch
            {
                return null;
            }
        }

        public LaudoAvariaApontamento RemoverAvariaApontamento(LaudoAvariaApontamento apontamento)
        {
            try
            {
                using (var srv = new HttpService<LaudoAvariaApontamento>())
                {
                    return srv.ExecuteService(apontamento, "api/laudo/remover-avaria-apontamento");
                }
            }
            catch
            {
                return null;
            }
        }

        public List<LaudoAvaria> ListarAvarias()
        {
            try
            {
                var listaAvarias = new List<LaudoAvaria>();

                using (var srv = new HttpService<List<LaudoAvaria>>())
                {
                    listaAvarias = srv.ReturnService("api/laudo/listar-avarias");
                }

                return listaAvarias;
            }
            catch
            {
                return null;
            }
        }

        public List<LaudoAvariaApontamento> ListarAvariasApontamentos(int laudoId)
        {
            try
            {
                var listaApontamentos = new List<LaudoAvariaApontamento>();

                using (var srv = new HttpService<List<LaudoAvariaApontamento>>())
                {
                    listaApontamentos = srv.ReturnService("api/laudo/listar-avarias-apontamentos?laudoId=" + laudoId);
                }

                return listaApontamentos;
            }
            catch
            {
                return null;
            }
        }

        public string RetornarResumoAvarias(int laudoId)
        {
            try
            {
                using (var srv = new HttpService<string>())
                {
                    return srv.ReturnService("api/laudo/retornar-avarias-resumo?laudoId=" + laudoId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        // --------- Opcionais ---------- //
        public LaudoOpcionalApontamento InserirOpcionalApontamento(LaudoOpcionalApontamento apontamento)
        {
            try
            {
                using (var srv = new HttpService<LaudoOpcionalApontamento>())
                {
                    return srv.ExecuteService(apontamento, "api/laudo/inserir-opcional-apontamento");
                }
            }
            catch
            {
                return null;
            }
        }

        public LaudoOpcionalApontamento RemoverOpcionalApontamento(LaudoOpcionalApontamento apontamento)
        {
            try
            {
                using (var srv = new HttpService<LaudoOpcionalApontamento>())
                {
                    return srv.ExecuteService(apontamento, "api/laudo/remover-opcional-apontamento");
                }
            }
            catch
            {
                return null;
            }
        }

        public List<LaudoOpcional> ListarOpcionais()
        {
            try
            {
                var listaOpcionais = new List<LaudoOpcional>();

                using (var srv = new HttpService<List<LaudoOpcional>>())
                {
                    listaOpcionais = srv.ReturnService("api/laudo/listar-opcionais");
                }

                return listaOpcionais;
            }
            catch
            {
                return null;
            }
        }

        public List<LaudoOpcionalApontamento> ListarOpcionaisApontamentos(int laudoId)
        {
            try
            {
                var listaApontamentos = new List<LaudoOpcionalApontamento>();

                using (var srv = new HttpService<List<LaudoOpcionalApontamento>>())
                {
                    listaApontamentos = srv.ReturnService("api/laudo/listar-opcionais-apontamentos?laudoId=" + laudoId);
                }

                return listaApontamentos;
            }
            catch
            {
                return null;
            }
        }



        // --------- Fotos ---------- //
        public IEnumerable<VeiculoFoto> ListarFotos(int veiculoId)
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

        public int RetornarUltimaFotoInserida(int veiculoId, string tipoFoto)
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

        public VeiculoFoto SalvarFoto(VeiculoFoto foto)
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

        public VeiculoFoto RemoverFoto(VeiculoFoto foto)
        {
            try
            {
                using (var srv = new HttpService<VeiculoFoto>())
                {
                    return srv.ExecuteService(foto, "api/veiculo-foto/remover");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
