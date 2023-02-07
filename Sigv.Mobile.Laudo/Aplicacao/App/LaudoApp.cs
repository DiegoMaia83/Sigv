using Sigv.Domain;
using Sigv.Mobile.Laudo.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sigv.Mobile.Laudo.Aplicacao.App
{
    public class LaudoApp
    {
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
    }
}
