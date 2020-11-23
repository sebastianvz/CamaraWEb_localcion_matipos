using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Verificador.OpenAccess;
using Verificador.Vista.Models;

namespace Verificador.Vista
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*allsvc}", new { allsvc = @".*\.svc(/.*)?" });
            routes.IgnoreRoute("{*allashx}", new { allsvc = @".*\.ashx(/.*)?" });


            routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Admin",
                url: "admin/{controller}/{action}/{id}",
                defaults: new { controller = "Login", action = "Index", id = UrlParameter.Optional }
            );
            
            /*routes.MapRoute(
                name: "Admin",
                url: "Admin/{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );*/

            /*routes.MapRoute(
                "Admin",
                "Admin/Login",
                new { controller = "Login", action = "Index" }
            );*/
        }

        protected void Application_Start()
        {


            AreaRegistration.RegisterAllAreas();

            //RegisterGlobalFilters(GlobalFilters.Filters);
            //RegisterRoutes(RouteTable.Routes);

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
            WebApiConfig.Register(GlobalConfiguration.Configuration);
        }


        void Application_BeginRequest(object sender, EventArgs e)
        {


            string nombreCultura = "es-ES";

            /*if (Request.Cookies.AllKeys.Contains("Lenguaje"))
            {
                nombreCultura = Request.Cookies["Lenguaje"].Value;
            }
            else
            {
                if (Request.UserLanguages != null && Request.UserLanguages.Length > 1)
                {
                    nombreCultura = Request.UserLanguages[1];
                    if (nombreCultura.IndexOf(";") >= 0)
                    {
                        nombreCultura = nombreCultura.Split(";".ToCharArray())[0];
                    }
                }
            }


            if (nombreCultura.ToUpper() != "ES-ES" && nombreCultura.ToUpper() != "EN-US")
            {
                nombreCultura = "es-ES";
            }*/
            //nombreCultura = "en-US";



            var separadorDeMiles = ",";
            var separadorDeDecimales = ".";
            var simboloMoneda = "$";

            CultureInfo cultura = CultureInfo.CreateSpecificCulture(nombreCultura);//(CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();

            cultura.NumberFormat.CurrencySymbol = simboloMoneda;
            cultura.NumberFormat.NumberDecimalSeparator = separadorDeDecimales;
            cultura.NumberFormat.NumberGroupSeparator = separadorDeMiles;

            cultura.NumberFormat.CurrencyDecimalSeparator = separadorDeDecimales;
            cultura.NumberFormat.CurrencyGroupSeparator = separadorDeMiles;

            cultura.NumberFormat.PercentDecimalSeparator = separadorDeDecimales;
            cultura.NumberFormat.PercentGroupSeparator = separadorDeMiles;

            cultura.DateTimeFormat.LongDatePattern = "yyyy-MM-dd HH:mm:ss";
            cultura.DateTimeFormat.ShortDatePattern = "yyyy-MM-dd";

            System.Threading.Thread.CurrentThread.CurrentCulture = cultura;
            System.Threading.Thread.CurrentThread.CurrentUICulture = cultura;

            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.UtcNow.AddHours(-1));
            Response.Cache.SetNoStore();


        }

        void Application_Error(object sender, EventArgs e)
        {
            Utilidades.RegistrarError(Server.GetLastError());
        }
    }
}