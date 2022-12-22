using Sigv.Domain;
using Sigv.Web.Services;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Sigv.Web.Controllers
{
    public class VeiculoController : Controller
    {
        // GET: Veiculo
        public ActionResult Home()
        {
            return View();
        }

        [HttpGet]
        public ActionResult ListarVeiculos()
        {
            try
            {
                var listaVeiculos = new List<Veiculo>();

                using (var srv = new HttpService<List<Veiculo>>())
                {
                    listaVeiculos = srv.ReturnService("api/veiculo/listar");
                }

                return View("_ListarVeiculos", listaVeiculos);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}