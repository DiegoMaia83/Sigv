using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Web.Http;

namespace Sigv.ApiFullOwin.Controllers
{
    public class LoginController : ApiController
    {
        [Authorize] //Authenticação via Token //O token é gerado na chamada pela rota "/auth/token" no provider antes desta ação pelo cliente
        [HttpPost]
        [Route("api/login/in")]
        public object RetornarLoginToken()
        {
            try
            {

                ClaimsPrincipal currentPrincipal = Thread.CurrentPrincipal as ClaimsPrincipal;

                //Identifica a lista de de Claims gravadas ao gerar o token
                var claims = ClaimsPrincipal.Current.Identities.First().Claims.ToList();

                //identifica usuário logado
                var usuarioId = claims?.FirstOrDefault(x => x.Type.Equals("UsuarioId", StringComparison.OrdinalIgnoreCase))?.Value;
                var nome = claims?.FirstOrDefault(x => x.Type.Equals("Nome", StringComparison.OrdinalIgnoreCase))?.Value;
                var login = claims?.FirstOrDefault(x => x.Type.Equals("Login", StringComparison.OrdinalIgnoreCase))?.Value;
                var grupoId = claims?.FirstOrDefault(x => x.Type.Equals("GrupoId", StringComparison.OrdinalIgnoreCase))?.Value;

                var obj = new
                {
                    usuarioId,
                    nome,
                    login,
                    grupoId
                };

                return obj;

            }
            catch
            {
                return "Erro ao recuperar dos dados de login.";
            }
        }
    }
}