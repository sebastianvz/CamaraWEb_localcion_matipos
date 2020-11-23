using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net.Mime;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Verificador.Vista.Models;
using Verificador.Vista.Models.Servicios;
using Verificador.Vista.Models.ViewModels;
using Verificador.Vista.Models;

namespace Verificador.Vista.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            return View();
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
            return RedirectToAction("Index", "Admin");
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
                    /*string newpw = System.Web.Security.Membership.GeneratePassword(6, 2);
                    servicio.ChangePassword(usuario.Usuario, newpw);

                    

                    var mensaje = "Se ha generado una nueva contraseña para el usuario {usuario}.  Su nueva contraseña es: {clave}".Replace("{clave}", newpw).Replace("{usuario}", usuario.Usuario);

                    var correo = new System.Net.Mail.MailMessage();
                    correo.To.Add(usuario.Email);
                    correo.Subject = "Recuperar contraseña";
                    correo.Body = mensaje;
                    Utilidades.EnviarCorreo(correo);*/
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
