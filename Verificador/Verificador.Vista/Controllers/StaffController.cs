using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Verificador.Vista.Models;
using Kendo.Mvc.Extensions;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Web.Hosting;
using Verificador.Vista.Models.Servicios;
using Verificador.Vista.Models.ViewModels;
using Verificador.Vista.Models;


namespace Verificador.Vista.Controllers
{
    public class StaffController : Controller
    {
        [Autorizacion(Permiso = "StaffEventosxEmpresario")]
        public ActionResult StaffEventosxEmpresario()
        {
            return View();
        }

        public ActionResult Eventos_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioStaff servicio = new ServicioStaff();
            int clienteID = Utilidades.GetCurrentUser().ClienteID;
            int? empresarioId = null;
            if (clienteID != 0) 
            {
                empresarioId = new ServicioEmpresarios().ObtenerEmpresarioXClienteId(clienteID);
            }
            return Json(servicio.ReadEventos(empresarioId).ToDataSourceResult(request));
            
        }

        public ActionResult AsociarPersonasEmpresasEvento()
        {
            return View();
        }

        public ActionResult Staff_ReadByIdentificacion(int tipoId, string identificacion)
        {
            var cliente = new ServicioCliente().ReadByIdenTipo(tipoId, identificacion);
            if (cliente != null) 
            {
                if (cliente.Direcciones.Count == 0)
                {
                    List<DireccionViewModel> objdireccion = new List<DireccionViewModel>();
                    objdireccion.Add(new DireccionViewModel { Ciudad_id = 0, CodigoPostal = "", Direccion = "" });
                    cliente.Direcciones = objdireccion;
                }
            }
            
            return Json(new {objeto = cliente }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Staff_Save(StaffViewModel obj)
        {
            try
            {
                if (obj != null)
                {
                    ServicioStaff servicio= new ServicioStaff();
                    Random grdn = new Random();
                    //para identificar si es boleta 1 - boleta virtual 2 - manilla 3 - escarapela 4
                    var identificardor = "4";
                    var year = DateTime.Now.Year.ToString().Substring(2,2);
                    var month = DateTime.Now.Month.ToString();
                    var day = DateTime.Now.Day.ToString();
                    var min = DateTime.Now.Minute.ToString();
                    var seg = DateTime.Now.Second.ToString();
                    obj.Auditoria.IdCreatorby = Utilidades.GetCurrentUser().Id;
                    obj.Auditoria.DateCreated = DateTime.Now;
                    obj.CodigoUnico = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", identificardor, grdn.Next(1000), year, grdn.Next(1000), month, grdn.Next(1000),day,min,seg);
                    servicio.AsociarStaff(obj);

                    return Json(new 
                    {
                        execute = (obj.Id>0)?true:false,
                        messagge= "El usuario a sido asociado al evento"
                    }, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            catch (Exception)
            {   
                throw;
            }
        }

        public ActionResult ReadStaff([DataSourceRequest]DataSourceRequest request, int idEvento, int idEmpresario, int? idEmpresa) 
        {
            //List<StaffViewModel> OK sin Data source.
            ServicioStaff servicio = new ServicioStaff();
            return Json(servicio.ReadStaffEventoEmprearioEmpresa(idEvento,idEmpresario,idEmpresa).ToDataSourceResult(request));;
        }

        [HttpPost]
        public  JsonResult StaffNewPersonSave(NewClienteStaff obj)
        {
            try
            {
                if (obj != null)
                {
                    if (obj.Cliente.strImagen != null)
                    {
                        string data = obj.Cliente.strImagen;
                        var base64Data = Regex.Match(data, @"data:image/(?<type>.+?),(?<data>.+)").Groups["data"].Value;
                        var compare = Encoding.UTF8.GetBytes(obj.Cliente.strImagen);
                        obj.Cliente.imagen = Convert.FromBase64String(base64Data);
                        string archivo = string.Format("{0}.jpg", obj.Cliente.NombreCompleto);
                        var PathString = HostingEnvironment.MapPath(string.Format("~/Images/{0}", archivo));
                        System.IO.File.WriteAllBytes(PathString, obj.Cliente.imagen);
                    }
                    obj.Cliente.Identificacion = obj.Cliente.Identificacion.TrimStart('0');
                    obj.Cliente.TipoPersona_id = 1;
                    obj.Cliente.PrimerNombres = !string.IsNullOrWhiteSpace(obj.Cliente.PrimerNombres) ? obj.Cliente.PrimerNombres.Trim() : null;
                    obj.Cliente.SegundoNombres = !string.IsNullOrWhiteSpace(obj.Cliente.SegundoNombres) ? obj.Cliente.SegundoNombres.Trim() : null;
                    obj.Cliente.PrimerApellido = !string.IsNullOrWhiteSpace(obj.Cliente.PrimerApellido) ? obj.Cliente.PrimerApellido.Trim() : null;
                    obj.Cliente.SegundoApellido = !string.IsNullOrWhiteSpace(obj.Cliente.SegundoApellido) ? obj.Cliente.SegundoApellido.Trim() : null;
                    ServicioCliente servicioCliente = new ServicioCliente();
                    obj.Cliente.Direcciones = new List<DireccionViewModel>();
                    obj.Cliente.Auditoria.IdCreatorby = Utilidades.GetCurrentUser().Id;
                    obj.Cliente.Auditoria.DateCreated = DateTime.Now;
                    servicioCliente.Save(obj.Cliente);
                    obj.Staff.IdCliente = obj.Cliente.Id;
                    ServicioStaff servicio = new ServicioStaff();
                    Random grdn = new Random();
                    //para identificar si es boleta 1 - boleta virtual 2 - manilla 3 - escarapela 4
                    var identificardor = "4";
                    var year = DateTime.Now.Year.ToString().Substring(2, 2);
                    var month = DateTime.Now.Month.ToString();
                    var day = DateTime.Now.Day.ToString();
                    var min = DateTime.Now.Minute.ToString();
                    var seg = DateTime.Now.Second.ToString();
                    obj.Staff.Auditoria.IdCreatorby = Utilidades.GetCurrentUser().Id;
                    obj.Staff.Auditoria.DateCreated = DateTime.Now;
                    obj.Staff.CodigoUnico = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", identificardor, grdn.Next(1000), year, grdn.Next(1000), month, grdn.Next(1000), day, min, seg);
                    servicio.AsociarStaff(obj.Staff);
                    return Json(new
                    {
                        execute = (obj.Staff.Id > 0) ? true : false,
                        messagge = "El usuario a sido asociado al evento"
                    }, JsonRequestBehavior.AllowGet);
                }
                return null;
            }
            catch (Exception)
            {
                return Json(new
                {
                    execute = (obj.Staff.Id > 0) ? true : false,
                    messagge = "El Cliente ya existe, ingreselo por medio del buscar de esta pantalla"
                }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult Staff_Destroy([DataSourceRequest] DataSourceRequest request, StaffViewModel obj)
        {
            if (obj != null)
            {
                obj.Auditoria.IdRetiredby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateRetired = DateTime.Now;
                 new ServicioStaff().Destroy(obj);
            }
            return Json(new[] { obj }.ToDataSourceResult(request));   
        }

    }
}
