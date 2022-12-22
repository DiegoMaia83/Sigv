using System;
using System.Web;

namespace Sigv.Web.App
{
    public class TokenCookie
    {
        public static string Token
        {
            get
            {
                //return HttpContext.Current.Session["token"]?.ToString();
                return HttpContext.Current.Request.Cookies["token-sigv"] != null ? HttpContext.Current.Request.Cookies["token-sigv"].Value.ToString() : "";
            }
            set
            {
                //HttpContext.Current.Session["token"] = value;
                HttpContext.Current.Response.Cookies["token-sigv"].Value = value;

                //HttpContext.Current.Response.Cookies["token"].Expires = DateTime.Now.AddHours(8);
                //HttpContext.Current.Request.Cookies["token"].Expires = DateTime.Now.AddHours(8);
            }
        }

        public static void Logoff()
        {

            try
            {
                HttpContext.Current.Response.Cookies["token-sigv"].Expires = DateTime.Now.AddDays(-1);
                HttpContext.Current.Request.Cookies["token-sigv"].Expires = DateTime.Now.AddDays(-1);
            }
            catch { }

        }

    }
}