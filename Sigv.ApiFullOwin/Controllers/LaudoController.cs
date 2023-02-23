using Sigv.Application;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace Sigv.ApiFullOwin.Controllers
{
    public class LaudoController : ApiController
    {
        private readonly LaudoApp _laudoApp = new LaudoApp();

        [Authorize]
        [HttpGet]
        [Route("api/laudo/retornar")]
        public LaudoVeiculo Retornar(int laudoId)
        {
            try
            {
                return _laudoApp.Retornar(laudoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/laudo/listar")]
        public IEnumerable<LaudoVeiculo> Listar()
        {
            try
            {
                return _laudoApp.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/laudo/listar-por-status")]
        public IEnumerable<LaudoVeiculo> Listar(int statusId)
        {
            try
            {
                return _laudoApp.Listar(statusId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/laudo/inserir")]
        public LaudoVeiculo InserirLaudo(LaudoVeiculo laudo)
        {
            try
            {
                return _laudoApp.InserirLaudo(laudo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/laudo/finalizar")]
        public LaudoVeiculo FinalizarLaudo(LaudoVeiculo laudo)
        {
            try
            {
                return _laudoApp.FinalizarLaudo(laudo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/laudo/reabrir")]
        public LaudoVeiculo ReabrirLaudo(LaudoVeiculo laudo)
        {
            try
            {
                return _laudoApp.ReabrirLaudo(laudo);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [Authorize]
        [HttpGet]
        [Route("api/laudo/listar-avarias")]
        public IEnumerable<LaudoAvaria> ListarAvarias()
        {
            try
            {
                return _laudoApp.ListarAvarias();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/laudo/listar-avarias-apontamentos")]
        public IEnumerable<LaudoAvariaApontamento> ListarAvariasApontamentos(int laudoId)
        {
            try
            {
                return _laudoApp.ListarAvariasApomtamentos(laudoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/laudo/inserir-avaria-apontamento")]
        public LaudoAvariaApontamento InserirAvariaApontamento(LaudoAvariaApontamento apontamento)
        {
            try
            {
                return _laudoApp.InserirAvariaApontamento(apontamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/laudo/remover-avaria-apontamento")]
        public LaudoAvariaApontamento RemoverAvariaApontamento(LaudoAvariaApontamento apontamento)
        {
            try
            {
                return _laudoApp.RemoverAvariaApontamento(apontamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/laudo/retornar-avarias-resumo")]
        public string RetornarResumoAvarias(int laudoId)
        {
            try
            {
                return _laudoApp.RetornarResumoAvarias(laudoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/laudo/listar-opcionais")]
        public IEnumerable<LaudoOpcional> ListarOpcionais()
        {
            try
            {
                return _laudoApp.ListarOpcionais();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/laudo/listar-opcionais-apontamentos")]
        public IEnumerable<LaudoOpcionalApontamento> ListarOpcionaisApontamentos(int laudoId)
        {
            try
            {
                return _laudoApp.ListarOpcionaisApontamentos(laudoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/laudo/inserir-opcional-apontamento")]
        public LaudoOpcionalApontamento InserirOpcionalApontamento(LaudoOpcionalApontamento apontamento)
        {
            try
            {
                return _laudoApp.InserirOpcionalApontamento(apontamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/laudo/remover-opcional-apontamento")]
        public LaudoOpcionalApontamento RemoverOpcionalApontamento(LaudoOpcionalApontamento apontamento)
        {
            try
            {
                return _laudoApp.RemoverOpcionalApontamento(apontamento);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpGet]
        [Route("api/laudo/retornar-opcionais-resumo")]
        public string RetornarResumoOpcionais(int laudoId)
        {
            try
            {
                return _laudoApp.RetornarResumoOpcionais(laudoId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [Authorize]
        [HttpPost]
        [Route("api/laudo/alterar-sync-status-foto")]
        public bool AlterarSyncStatusFoto(VeiculoFoto foto)
        {
            try
            {
                return _laudoApp.AlterarSyncStatusFoto(foto);                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("api/fotos/sync")]
        public async Task<IHttpActionResult> SincronizarImagem()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                return BadRequest("O tipo de conteúdo da solicitação não é suportado.");
            }

            var provider = await Request.Content.ReadAsMultipartAsync();

            // Obtém o valor do parâmetro de arquivo de imagem
            var file = await provider.Contents[0].ReadAsByteArrayAsync();

            // Obtém o valor do parâmetro do veículo ID
            int veiculoId = Convert.ToInt32(await provider.Contents[1].ReadAsStringAsync());

            // Obtém o valor do parâmetro do veículo ID
            string nomeFoto = await provider.Contents[2].ReadAsStringAsync();


            // Verifica se o arquivo não é nulo
            if (file == null)
            {
                return BadRequest("Nenhuma imagem encontrada na solicitação.");
            }

            var buffer = file;       


            var targetPath = System.Web.HttpContext.Current.Server.MapPath("~/Content/Imagens/" + veiculoId.ToString("000000"));

            // Verifica se o diretório existe. Se não o cria.
            if (!Directory.Exists(targetPath))
                Directory.CreateDirectory(targetPath);

            var filePath = Path.Combine(targetPath, nomeFoto);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await stream.WriteAsync(buffer, 0, buffer.Length);
            }

            return Ok("Imagem recebida com sucesso!");
        }
    }
}