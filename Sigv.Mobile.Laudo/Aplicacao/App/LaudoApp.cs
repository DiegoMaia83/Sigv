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
    }
}
