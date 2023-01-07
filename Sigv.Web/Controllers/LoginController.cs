using Sigv.Domain;
using Sigv.Web.App;
using Sigv.Web.Services;
using System;
using System.EnterpriseServices;
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
                using (var srv = new HttpService<Acesso>())
                {
                    var acesso = new Acesso()
                    {
                        UsuarioId = Convert.ToInt32(SessionCookie.Logado.UsuarioId),
                        Ip = Request.ServerVariables["REMOTE_ADDR"],
                        Data = DateTime.Now
                    };

                    srv.ExecuteService(acesso, "api/acesso/salvar");
                }

                return RedirectToAction("../Home/Dashboard");
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

        public ActionResult AccessDenied()
        {
            return View("_AccessDenied");
        }
    }
}