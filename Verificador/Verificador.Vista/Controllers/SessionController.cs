using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Verificador.Vista.Models;
using System.IO;
using System.Text;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.Security;

namespace Verificador.Vista.Controllers
{
    public class SessionController : Controller
    {

        /// <summary>
        /// Action para validar si la sesion esta activa
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult ValidateSession()
        {
            if (Utilidades.GetCurrentUser() == null)
            {
                FormsAuthentication.SignOut();
                Utilidades.SetCurrentUser(null);
                return Json(new { Ready = false }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { Ready = true }, JsonRequestBehavior.AllowGet);
        }
        
    }
}
