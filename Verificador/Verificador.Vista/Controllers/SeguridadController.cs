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
using Verificador.Vista.Models.Servicios;
using Verificador.Vista.Models.ViewModels;
using Verificador.Vista.Models;


namespace Verificador.Vista.Controllers
{
    public class SeguridadController : Controller
    {
        public ActionResult Index()
        {            

            return View();
        }

        #region Permisos
        [Autorizacion(Permiso = "Permisos")]
        public ActionResult Permisos()
        {
            return View("Permisos");
        }

        public ActionResult Permisos_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioPermisos servicio = new ServicioPermisos();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Permisos_Create([DataSourceRequest] DataSourceRequest request, PermisoViewModel obj)
        {
            ServicioPermisos servicio = new ServicioPermisos();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Permisos_Update([DataSourceRequest] DataSourceRequest request, PermisoViewModel obj)
        {
            ServicioPermisos servicio = new ServicioPermisos();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Permisos_Destroy([DataSourceRequest] DataSourceRequest request, PermisoViewModel obj)
        {
            ServicioPermisos servicio = new ServicioPermisos();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Roles

        public ActionResult GuardarPermisosRol(int idRol, string permisos)
        {
            try
            {
                ServicioPermisos servicio = new ServicioPermisos();
                servicio.GuardarPermisosRol(idRol, permisos);
                return Json(string.Empty, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult PermisosRol_Read([DataSourceRequest] DataSourceRequest request, int? idRol)
        {
            ServicioPermisos servicio = new ServicioPermisos();
            if (idRol.HasValue)
            {

                return Json(servicio.ReadRol(idRol.Value).ToDataSourceResult(request));
            }
            else
            {
                return Json(servicio.Read().ToDataSourceResult(request));
            }
        }

        [Autorizacion(Permiso = "Roles")]
        public ActionResult Roles()
        {
            return View("Roles");
        }

        public ActionResult Roles_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioRol servicio = new ServicioRol();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Roles_Create([DataSourceRequest] DataSourceRequest request, RoleViewModel obj)
        {
            ServicioRol servicio = new ServicioRol();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Roles_Update([DataSourceRequest] DataSourceRequest request, RoleViewModel obj)
        {
            ServicioRol servicio = new ServicioRol();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Roles_Destroy([DataSourceRequest] DataSourceRequest request, RoleViewModel obj)
        {
            ServicioRol servicio = new ServicioRol();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Usuarios
        [Autorizacion(Permiso = "Usuarios")]
        public ActionResult Usuarios()
        {
            return View("Usuarios");
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
            ServicioRol servicio = new ServicioRol();
            if (idUsuario.HasValue)
            {

                return Json(servicio.ReadUsuario(idUsuario.Value).ToDataSourceResult(request));
            }
            else
            {
                return Json(servicio.Read().ToDataSourceResult(request));
            }
        }

        public ActionResult Usuarios_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioUsuarios servicio = new ServicioUsuarios();
            var lista = servicio.Read();
            return Json(lista.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult GetInfoUserbyIden([DataSourceRequest] DataSourceRequest request, int tipoID, string iden)
        {
            ServicioUsuarios servicio = new ServicioUsuarios();
            var user = servicio.ReadByIden(tipoID, iden);
            //return Json(user.ToDataSourceResult(request));
            return Json(new[] { user }.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult GetInfoByUserEmail(string user, string email)
        {
            ServicioUsuarios servicio = new ServicioUsuarios();
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

        [HttpPost]
        public ActionResult Usuarios_Create([DataSourceRequest] DataSourceRequest request, UsuarioViewModel obj)
        {
            ServicioUsuarios servicio = new ServicioUsuarios();
            if (obj != null && ModelState.IsValid)
            {
                if (obj.strImagen !=null)
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
                if (obj.TipoPersona_id == 1)
                {
                    obj.EsEmpresa = false;
                }
                else
                {
                    obj.EsEmpresa = true;
                }

                if (obj.Password == obj.ConfriPass)
                {
                    if (obj.IdClietne >= 0) { servicio.Create(obj); } else { servicio.CreateFromCC(obj); }
                    
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
                new ServicioUsuarios().Update(obj);
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
