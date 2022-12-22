using Sigv.Web.App;
using Sigv.Web.Services;
using System.Web.Mvc;

namespace Sigv.Web.Controllers
{
    public class LoginController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(string login, string senha)
        {

            var loginEfetuado = (MensagemRetorno)new AuthService().RetornarLoginToken(login, senha);

            if (loginEfetuado.Sucesso)
            {

                return RedirectToAction("../Home/Index");
            }

            ViewBag.MensagemRetorno = loginEfetuado;

            return View();
        }

        public ActionResult Logoff()
        {
            SessionCookie.Logoff();
            TokenCookie.Logoff();

            return RedirectToAction("Index", "Login");
        }
    }
}