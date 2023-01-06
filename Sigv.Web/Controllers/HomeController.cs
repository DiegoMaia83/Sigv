using Sigv.Domain;
using Sigv.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static Sigv.Web.App.ConfigProvider;

namespace Sigv.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public JsonResult RetornarStatusVeiculos()
        {
            try
            {
                var lista = new List<StatsVeiculo>();

                using (var srv = new HttpService<List<StatsVeiculo>>())
                {
                    lista = srv.ReturnService("api/stats/retornar-status-veiculos");
                }

                return Json(lista.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public JsonResult RetornarEspeciesVeiculos()
        {
            try
            {
                var lista = new List<StatsVeiculo>();

                using (var srv = new HttpService<List<StatsVeiculo>>())
                {
                    lista = srv.ReturnService("api/stats/retornar-especies-veiculos");
                }

                return Json(lista.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}