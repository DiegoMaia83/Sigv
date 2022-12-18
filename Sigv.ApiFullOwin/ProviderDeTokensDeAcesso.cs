using Microsoft.Owin.Security.OAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;

namespace Sigv.ApiFullOwin
{
    public class ProviderDeTokensDeAcesso : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            var usuario = Seguranca.Acesso.Login(context.UserName, context.Password);

            if (usuario != null)
            {
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);

                identity.AddClaim(new Claim("UsuarioId", usuario.UsuarioId.ToString()));
                identity.AddClaim(new Claim("Nome", usuario.Nome));
                identity.AddClaim(new Claim("Login", usuario.Login));
                identity.AddClaim(new Claim("GrupoId", usuario.GrupoId.ToString()));

                context.Validated(identity);
            }
            else
            {
                context.SetError("Acesso negado!", "As credenciais informadas não conferem.");
                return;
            }
        }
    }
}