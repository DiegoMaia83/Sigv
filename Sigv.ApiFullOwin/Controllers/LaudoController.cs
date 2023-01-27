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
        public Laudo Retornar(int laudoId)
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
        public IEnumerable<Laudo> Listar()
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
        public IEnumerable<Laudo> Listar(int statusId)
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
        public Laudo InserirLaudo(Laudo laudo)
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
        public Laudo FinalizarLaudo(Laudo laudo)
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
        public Laudo ReabrirLaudo(Laudo laudo)
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
    }
}