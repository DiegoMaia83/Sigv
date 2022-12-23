using Sigv.Web.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;

namespace Sigv.Web.App
{
    public class ConfigProvider : RoleProvider
    {
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            try
            {
                using (var srv = new HttpService<string[]>())
                {
                    return srv.ReturnService("api/permissao/listar-permissoes-usuario?username=" + username);
                }
            }
            catch
            {
                return null;
            }
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public class Filtro : System.Web.Mvc.AuthorizeAttribute
        {
            public override void OnAuthorization(AuthorizationContext filterContext)
            {
                try
                {
                    base.OnAuthorization(filterContext);

                    if (filterContext.Result is HttpUnauthorizedResult)
                    {
                        if (filterContext.HttpContext.Request.IsAjaxRequest())
                        {
                            // Retorna um JSON se a requisição for feita por ajax
                            var viewResult = new JsonResult();
                            viewResult.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                            viewResult.Data = (new MensagemRetorno { Sucesso = false, Mensagem = "Acesso negado na rotina!" });
                            filterContext.Result = viewResult;
                        }
                        else
                        {
                            // Retorna para um página padrão de acesso negado
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "AccessDenied", controller = "Login" }));
                        }
                    }
                }
                catch
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { action = "Logoff", controller = "Login" }));
                }

            }
        }
    }
}