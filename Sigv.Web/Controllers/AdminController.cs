using Sigv.Domain;
using Sigv.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Sigv.Web.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Permissoes()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListarPermissoes()
        {
            try
            {
                var listaPermissoes = new List<Permissao>();

                using (var srv = new HttpService<List<Permissao>>())
                {
                    listaPermissoes = srv.ReturnService("api/permissao/listar-permissoes");
                }

                return View("_ListarPermissoes", listaPermissoes);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}