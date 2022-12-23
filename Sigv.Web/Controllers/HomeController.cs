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
        [Filtro(Roles = "4")]
        public ActionResult Index()
        {
            return View();
        }
    }
}