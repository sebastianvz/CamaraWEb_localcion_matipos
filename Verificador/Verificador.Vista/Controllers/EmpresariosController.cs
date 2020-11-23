using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Verificador.Vista.Models;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.Hosting;
using System.Web.Security;
using Verificador.Vista.Models.Servicios;
using Verificador.Vista.Models.ViewModels;
using Verificador.Vista.Models;

namespace Verificador.Vista.Controllers
{
    public class EmpresariosController : Controller
    {

        #region Login Empresario
        public ActionResult Index()
        {
            return View("LoginEmpresarios", "_LayoutLogin");
        }

        [HttpPost]
        public ActionResult Ingresar(string username, string password)
        {
            try
            {

                UsuarioViewModel usuario = new ServicioUsuariosEmpresarios().Login(username, password);
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

        [HttpPost]
        public ActionResult CambiarPW(string password, string passwordnuevo, string passwordnuevorepetir)
        {
            ServicioUsuariosEmpresarios servicio = new ServicioUsuariosEmpresarios();
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
                var servicio = new ServicioEmpresarios();
                var usuario = servicio.ObtenerUsuarioPorEmail(email);
                if (usuario != null)
                {
                    string newpw = System.Web.Security.Membership.GeneratePassword(8, 3);
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
        #endregion

        #region Usuarios
        [Autorizacion(Permiso = "UsuariosEmpreario")]
        public ActionResult UsuariosEmpreario()
        {
            return View("UsuariosEmpreario");
        }

        public ActionResult Usuarios_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioUsuariosEmpresarios servicio = new ServicioUsuariosEmpresarios();
            var lista = servicio.Read();
            return Json(lista.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult GetInfoUserbyIden([DataSourceRequest] DataSourceRequest request, int tipoID, string iden)
        {
            ServicioUsuariosEmpresarios servicio = new ServicioUsuariosEmpresarios();
            var user = servicio.ReadByIden(tipoID, iden);
            //return Json(user.ToDataSourceResult(request));
            return Json(new[] { user }.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult GetInfoByUserEmail(string user, string email)
        {
            ServicioUsuariosEmpresarios servicio = new ServicioUsuariosEmpresarios();
            var isUser = servicio.ReadUserByuserNameEmail(user, email);
            if (!string.IsNullOrWhiteSpace(isUser))
            {
                return Json(new
                {
                    Estado = true,
                    flag = isUser,
                    Message = string.Format("El {0} existe ", isUser)
                }, JsonRequestBehavior.AllowGet);
            }

            return null;

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Usuarios_Create([DataSourceRequest] DataSourceRequest request, UsuarioViewModel obj)
        {
            ServicioUsuariosEmpresarios servicio = new ServicioUsuariosEmpresarios();

            if (obj != null && ModelState.IsValid)
            {
                if (obj.strImagen != null)
                {
                    string data = obj.strImagen;
                    var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                    var compare = Encoding.UTF8.GetBytes(obj.strImagen);
                    obj.imagen = Convert.FromBase64String(base64Data);
                    string archivo = string.Format("{0}.jpg", obj.NombreCompleto);
                    var PathString = HostingEnvironment.MapPath(string.Format("~/Images/{0}", archivo));
                    System.IO.File.WriteAllBytes(PathString, obj.imagen);
                }
                obj.Direcciones = DireccionesCliente;
                if (obj.Password == obj.ConfriPass)
                {
                    if (obj.IdClietne != 0)
                    { servicio.CreateFromCC(obj); } 
                    else
                    { servicio.Create(obj); } 
                }
                else
                {
                    string error = "Las contraseñas no coinciden";
                    ModelState.AddModelError("ServerError", error);
                }
            }
            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Usuarios_Update([DataSourceRequest] DataSourceRequest request, UsuarioViewModel obj)
        {
            if (obj != null)
            {
                if (obj.imagen == null)
                {
                    string data = obj.strImagen;
                    var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                    if (base64Data != "")
                    {
                        var compare = Encoding.UTF8.GetBytes(obj.strImagen);
                        obj.imagen = Convert.FromBase64String(base64Data);
                        string archivo = string.Format("{0}.jpg", obj.NombreCompleto);
                        var PathString = HostingEnvironment.MapPath(string.Format("~/Images/{0}", archivo));
                        System.IO.File.WriteAllBytes(PathString, obj.imagen);
                    }
                }
                obj.Direcciones = DireccionesCliente;
                new ServicioUsuariosEmpresarios().Update(obj);
            }
            return Json(new[] { obj }.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Usuarios_Destroy([DataSourceRequest] DataSourceRequest request, UsuarioViewModel obj)
        {
            ServicioUsuarios servicio = new ServicioUsuarios();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult GuardarRolesUsuario(int idUsuario, string roles)
        {
            try
            {
                ServicioRol servicio = new ServicioRol();
                servicio.GuardarRolesUsuario(idUsuario, roles);
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult RolesUsuario_Read([DataSourceRequest] DataSourceRequest request, int? idUsuario)
        {
            ServicioUsuariosEmpresarios servicio = new ServicioUsuariosEmpresarios();
            if (idUsuario.HasValue)
            {

                return Json(servicio.ReadUsuario(idUsuario.Value).ToDataSourceResult(request));
            }
            else
            {
                return Json(servicio.ReadRol().ToDataSourceResult(request));
            }
        }

        #endregion

        #region DireccionesClientes

        public ActionResult LimpiarDireccion()
        {
            DireccionesCliente.Clear();
            return Json(new { Ejecuto = true }, JsonRequestBehavior.AllowGet);
        }

        public List<DireccionViewModel> DireccionesCliente
        {
            get
            {
                var SessionDirecciones = HttpContext.Session["DireccionesCliente"] as List<DireccionViewModel>;
                if (SessionDirecciones == null)
                {
                    SessionDirecciones = new List<DireccionViewModel>();
                    HttpContext.Session["DireccionesCliente"] = SessionDirecciones;
                }
                return SessionDirecciones;
            }
            set
            {
                HttpContext.Session["DireccionesCliente"] = value;
            }
        }

        //[HttpPost]
        //public ActionResult get_DireccionesCliente(int id)
        //{
        //    DireccionesCliente = null;
        //    return LeerDireccionesClientes(id);
        //}

        public ActionResult LeerDireccionesClientes(int id)
        {
            try
            {
                var item = new ServicioDireccion().getDireccionesByCliente(id);
                if (item.Count == 0 && DireccionesCliente.FirstOrDefault().Id != -1)
                {
                    DireccionesCliente.Clear();
                }
                else
                {
                    if (item.Count > 0)
                    {
                        DireccionesCliente = item;
                    }
                }

                if (DireccionesCliente.Count > 0 && item != null)
                {
                    if (DireccionesCliente.FirstOrDefault().IdCliente == id)
                    {
                        bool existe = false;
                        foreach (var enDb in item)
                        {
                            foreach (var enSession in DireccionesCliente)
                            {
                                if (enSession.FullAdress.Equals(enDb.FullAdress))
                                {
                                    existe = true;
                                }
                            }
                            if (!existe)
                            {
                                DireccionesCliente.Add(enDb);
                            }
                        }
                    }
                    else if (item != null)
                    {
                        DireccionesCliente.Clear();
                    }
                }

                return Json(new { Ejecuto = true }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception err)
            {
                return Json(new { Ejecuto = false, Mensaje = err.Message }, JsonRequestBehavior.AllowGet);
            }

        }

        //Intervenido Sebastian
        public ActionResult get_DireccionesByCliente([DataSourceRequest] DataSourceRequest request, int cliente_id)
        {
            //ServicioCliente servicio = new ServicioCliente();
            //var resultado = servicio.getDireccionesByCliente(cliente_id);
            LeerDireccionesClientes(cliente_id);
            return Json(DireccionesCliente.ToDataSourceResult(request));

        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DireccionesCliente_Create([DataSourceRequest] DataSourceRequest request, DireccionViewModel obj, int cliente_id)
        {
            try
            {
                if (obj != null && ModelState.IsValid)
                {
                    if (DireccionesCliente.Count == 0)
                    {
                        obj.Id = -1;
                    }
                    else
                    {
                        var item = (from d in DireccionesCliente
                                    where d.Id < 0
                                    orderby d.Id descending
                                    select d).FirstOrDefault();
                        if (item != null)
                        {
                            obj.Id = item.Id - 1; ;
                        }
                        else
                        {
                            obj.Id = -1;
                        }
                    }
                    DireccionesCliente.Add(obj);
                    if (cliente_id > 0)
                    {
                        obj.IdCliente = cliente_id;
                        new ServicioDireccion().RelacionarDireccion(obj, true);
                    }
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("ServerError", ex.Message);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DireccionesCliente_Update([DataSourceRequest] DataSourceRequest request, DireccionViewModel obj)
        {
            if (obj != null)
            {
                var item = DireccionesCliente.First(c => c.Id == obj.Id);
                if (item != null)
                {
                    item.Direccion = obj.Direccion;
                    item.CodigoPostal = obj.CodigoPostal;
                    item.Ciudad_id = obj.Ciudad_id;
                    if (item.IdCliente > 0 && item.Id > 0)
                    {
                        new ServicioDireccion().RelacionarDireccion(obj, false);
                    }
                }
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult DireccionesPaciente_Destroy([DataSourceRequest] DataSourceRequest request, DireccionViewModel obj)
        {

            if (obj != null)
            {
                var item = DireccionesCliente.FirstOrDefault(c => c.Id == obj.Id);
                if (item != null)
                {
                    DireccionesCliente.Remove(item);
                }
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion


    }
}
