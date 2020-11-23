using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Verificador.Vista.Models;
using System.IO;
using System.Web.Security;
using Verificador.Vista.Models.Servicios;
using Verificador.Vista.Models.ViewModels;
using Verificador.Vista.Models;

namespace Verificador.Vista.Controllers
{

    public class AdminController : Controller
    {
        
        public ActionResult Index()
        {
            ViewBag.message = Utilidades.Decrypt("VdZKGz06MWF17iGo60WGuQ==");
            return View("Login", "_LayoutLogin");
        }

        [HttpPost]
        public ActionResult Ingresar(string username, string password)
        {
            try
            {

                UsuarioViewModel usuario = new ServicioUsuarios().Login(username, password);
                if (usuario != null)
                {
                    Utilidades.SetCurrentUser(usuario);
                    FormsAuthentication.SetAuthCookie(username, false);
                    return Json(new { Ejecuto = true, Mensaje = string.Empty }, JsonRequestBehavior.AllowGet);                    
                }
                else
                {
                    throw new Exception("Usuario y contraseña no validos");                    
                }
            }
            catch (Exception ex)
            {
                return Json(new { Ejecuto = false, Mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
                
            }            
        }

        public ActionResult Direct(string username, string password)
        {
            var usuario = new ServicioUsuarios().Login(username, password);

            Utilidades.SetCurrentUser(usuario);
            FormsAuthentication.SetAuthCookie(username, false);
            return RedirectToAction("Index", "TicketsManagement", new { eventID = 55 });
        }

        /// <summary>
        /// ACtion para descontectar al usuario
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Abandon();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult CambiarPW(string password, string passwordnuevo, string passwordnuevorepetir)
        {
            ServicioUsuarios servicio = new ServicioUsuarios();
            var usuario = Utilidades.GetCurrentUser() as UsuarioViewModel;
            try
            {
                servicio.ChangePassword(usuario.Usuario, password, passwordnuevo, passwordnuevorepetir);
                return Json(new { Guardo = true, Mensaje = "Se ha realizado el cambio de contraseña." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception err)
            {
                return Json(new { Guardo = false, Mensaje = err.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        /// <summary>
        /// Action para recuperar la contraseña
        /// </summary>
        /// <param name="email">Email o usuario para recuperar la contraseña</param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Recuperar(string email)
        {

            try
            {
                var servicio = new ServicioUsuarios();
                var usuario = servicio.ObtenerUsuarioPorEmail(email);
                if (usuario != null)
                {
                    string newpw = System.Web.Security.Membership.GeneratePassword(8,3);
                    servicio.ChangePassword(usuario.Usuario, newpw);
                    var mensaje = "<p align='justify'> Se ha generado una nueva contraseña para el usuario {usuario}. Su nueva contraseña es: <b>{clave}</b> </p>".Replace("{clave}", newpw).Replace("{usuario}", usuario.Usuario);
                    var correo = new System.Net.Mail.MailMessage();
                    correo.IsBodyHtml = true;
                    correo.BodyEncoding = System.Text.Encoding.UTF8;
                    correo.To.Add(usuario.Email);
                    correo.Subject = "Recuperar contraseña Usuario Matipos S.A.S";
                    correo.Body = mensaje;
                    Utilidades.EnviarCorreo(correo);
                    return Json(new { Ejecuto = true, Mensaje = "Se ha enviado la contraseña al correo del usuario." }, JsonRequestBehavior.AllowGet);                    
                }
                else
                {
                    throw new Exception("No se encontro el usuario asociado al email.");
                }
            }
            catch (Exception ex)
            {
                return Json(new { Ejecuto = false, Mensaje = ex.Message }, JsonRequestBehavior.AllowGet);
            }            
        }
    }
}
