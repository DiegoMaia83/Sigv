using Sigv.Application;
using Sigv.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Sigv.ApiFullOwin.Controllers
{
    public class PermissaoController : ApiController
    {
        private readonly PermissaoApp _permissaoApp = new PermissaoApp();

        [Authorize]
        [HttpGet]
        [Route("api/permissao/listar")]
        public List<Permissao> Listar()
        {
            try
            {
                return _permissaoApp.Listar();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}