using Sigv.Application;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace Sigv.ApiFullOwin.Controllers
{
    public class LaudoController : ApiController
    {
        private readonly LaudoApp _laudoApp = new LaudoApp();

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
    }
}