using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Verificador.Vista.Models
{
    public class Autorizacion : AuthorizeAttribute
    {
        public string Permiso { set; get; }

        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (string.IsNullOrEmpty(Permiso) || Utilidades.Usuario == null)
                return false;
            else
            {

                var result = Utilidades.Usuario.TienePermiso(Permiso);
                return result;
            }   
        }

        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            base.HandleUnauthorizedRequest(filterContext);
            if (Utilidades.Usuario != null)
            {

                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Administracion", action = "AccesoNoAutorizado" }));
            }
            else
            {
                filterContext.Result = new RedirectResult("~/404.html");
            }
        }
    }
}