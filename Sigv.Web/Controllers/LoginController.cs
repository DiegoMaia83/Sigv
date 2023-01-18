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
        private readonly ComumApp _comumAplicacao = new ComumApp();

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

        public ActionResult RecuperarSenha()
        {
            return View();
        }

        [HttpPost]
        public ActionResult RecuperarSenha(string login, string email)
        {
            try
            {
                using (var srv = new HttpService<Usuario>())
                {                    
                    var usuario = srv.ReturnService("api/usuario/recuperar-senha-loggedout?login=" + login + "&email=" + email);

                    if (usuario != null)
                    {
                        var password = _comumAplicacao.GetRandomPassword();
                        usuario.Password = _comumAplicacao.HashMD5(password);

                        var usuarioAlterado = srv.ExecuteService(usuario, "api/usuario/alterar-senha-loggedout");

                        Mail.EnviarSenha(usuarioAlterado, password, Request.ServerVariables["REMOTE_ADDR"]);

                        return Json(new MensagemRetorno { Id = usuarioAlterado.UsuarioId, Sucesso = true, Mensagem = "Dados de acesso enviado para o e-mail informado!" });
                    }
                    else
                    {
                        return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Usuário não localizado!" });
                    }
                }
            }
            catch
            {
                return Json(new MensagemRetorno { Sucesso = false, Mensagem = "Houve um erro ao processar a rotina!" });
            }
        }

        public ActionResult ValidaToken()
        {
            var token = Token.isOk();
            var usuarioLogado = UsuarioLogado.isOk();

            if (token || usuarioLogado)
            {
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else
            {
                SessionCookie.Logoff();
                TokenCookie.Logoff();

                return Json(false, JsonRequestBehavior.AllowGet);
            }            
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