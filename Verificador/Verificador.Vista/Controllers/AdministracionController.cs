using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Verificador.Vista.Models;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Hosting;
using Verificador.Vista.Models.Servicios;
using Verificador.Vista.Models.ViewModels;

namespace Verificador.Vista.Controllers
{
    public class AdministracionController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        #region PrintersByComputers
        public ActionResult PrintersByComputers()
        {
            return View("PrintersByComputers");
        }

        public ActionResult PrintersByComputers_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioPrintersByComputers servicio = new ServicioPrintersByComputers();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PrintersByComputers_Create([DataSourceRequest] DataSourceRequest request, PrintersByComputersViewModel obj)
        {
            ServicioPrintersByComputers servicio = new ServicioPrintersByComputers();
            if (obj != null && ModelState.IsValid)
            {
                obj.MacAddress = Utilidades.GetClientMac(Request);
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PrintersByComputers_Update([DataSourceRequest] DataSourceRequest request, PrintersByComputersViewModel obj)
        {
            ServicioPrintersByComputers servicio = new ServicioPrintersByComputers();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult PrintersByComputers_Destroy([DataSourceRequest] DataSourceRequest request, PrintersByComputersViewModel obj)
        {
            ServicioPrintersByComputers servicio = new ServicioPrintersByComputers();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region EstadoImpresions

        public ActionResult EstadoImpresions()
        {
            return View("EstadoImpresions");
        }

        public ActionResult EstadoImpresions_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioEstadoImpresion servicio = new ServicioEstadoImpresion();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EstadoImpresions_Create([DataSourceRequest] DataSourceRequest request, EstadoImpresionViewModel obj)
        {
            ServicioEstadoImpresion servicio = new ServicioEstadoImpresion();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EstadoImpresions_Update([DataSourceRequest] DataSourceRequest request, EstadoImpresionViewModel obj)
        {
            ServicioEstadoImpresion servicio = new ServicioEstadoImpresion();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EstadoImpresions_Destroy([DataSourceRequest] DataSourceRequest request, EstadoImpresionViewModel obj)
        {
            ServicioEstadoImpresion servicio = new ServicioEstadoImpresion();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region UVTs
        [Autorizacion(Permiso = "UVTs")]
        public ActionResult UVTs()
        {
            return View("UVTs");
        }

        public ActionResult UVTs_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioUVT servicio = new ServicioUVT();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UVTs_Create([DataSourceRequest] DataSourceRequest request, UVTViewModel obj)
        {
            ServicioUVT servicio = new ServicioUVT();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UVTs_Update([DataSourceRequest] DataSourceRequest request, UVTViewModel obj)
        {
            ServicioUVT servicio = new ServicioUVT();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UVTs_Destroy([DataSourceRequest] DataSourceRequest request, UVTViewModel obj)
        {
            ServicioUVT servicio = new ServicioUVT();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region TipoEventos
        [Autorizacion(Permiso = "TipoEventos")]
        public ActionResult TipoEventos()
        {
            return View("TipoEventos");
        }

        public ActionResult TipoEventos_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioTipoEvento servicio = new ServicioTipoEvento();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoEventos_Create([DataSourceRequest] DataSourceRequest request, TipoEventoViewModel obj)
        {
            ServicioTipoEvento servicio = new ServicioTipoEvento();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoEventos_Update([DataSourceRequest] DataSourceRequest request, TipoEventoViewModel obj)
        {
            ServicioTipoEvento servicio = new ServicioTipoEvento();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoEventos_Destroy([DataSourceRequest] DataSourceRequest request, TipoEventoViewModel obj)
        {
            ServicioTipoEvento servicio = new ServicioTipoEvento();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region TipoServicios
        [Autorizacion(Permiso = "TipoServicios")]
        public ActionResult TipoServicios()
        {
            return View("TipoServicios");
        }

        public ActionResult TipoServicios_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioTipoServicio servicio = new ServicioTipoServicio();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoServicios_Create([DataSourceRequest] DataSourceRequest request, TipoServicioViewModel obj)
        {
            ServicioTipoServicio servicio = new ServicioTipoServicio();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoServicios_Update([DataSourceRequest] DataSourceRequest request, TipoServicioViewModel obj)
        {
            ServicioTipoServicio servicio = new ServicioTipoServicio();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoServicios_Destroy([DataSourceRequest] DataSourceRequest request, TipoServicioViewModel obj)
        {
            ServicioTipoServicio servicio = new ServicioTipoServicio();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Impuestos
        [Autorizacion(Permiso = "Impuestos")]
        public ActionResult Impuestos()
        {
            return View("Impuestos");
        }

        public ActionResult Impuestos_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioImpuesto servicio = new ServicioImpuesto();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Impuestos_Create([DataSourceRequest] DataSourceRequest request, ImpuestoViewModel obj)
        {
            ServicioImpuesto servicio = new ServicioImpuesto();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }



        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Impuestos_Update([DataSourceRequest] DataSourceRequest request, ImpuestoViewModel obj)
        {
            ServicioImpuesto servicio = new ServicioImpuesto();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Impuestos_Destroy([DataSourceRequest] DataSourceRequest request, ImpuestoViewModel obj)
        {
            ServicioImpuesto servicio = new ServicioImpuesto();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Generos
        [Autorizacion(Permiso = "Generos")]
        public ActionResult Generos()
        {
            return View("Generos");
        }

        public ActionResult Generos_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioGenero servicio = new ServicioGenero();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Generos_Create([DataSourceRequest] DataSourceRequest request, GeneroViewModel obj)
        {
            ServicioGenero servicio = new ServicioGenero();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Generos_Update([DataSourceRequest] DataSourceRequest request, GeneroViewModel obj)
        {
            ServicioGenero servicio = new ServicioGenero();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Generos_Destroy([DataSourceRequest] DataSourceRequest request, GeneroViewModel obj)
        {
            ServicioGenero servicio = new ServicioGenero();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region CanalVentas
        [Autorizacion(Permiso = "CanalVentas")]
        public ActionResult CanalVentas()
        {
            return View("CanalVentas");
        }

        public ActionResult CanalVentas_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioCanalVenta servicio = new ServicioCanalVenta();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CanalVentas_Create([DataSourceRequest] DataSourceRequest request, CanalVentaViewModel obj)
        {
            ServicioCanalVenta servicio = new ServicioCanalVenta();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CanalVentas_Update([DataSourceRequest] DataSourceRequest request, CanalVentaViewModel obj)
        {
            ServicioCanalVenta servicio = new ServicioCanalVenta();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult CanalVentas_Destroy([DataSourceRequest] DataSourceRequest request, CanalVentaViewModel obj)
        {
            ServicioCanalVenta servicio = new ServicioCanalVenta();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Monedas
        [Autorizacion(Permiso = "Monedas")]
        public ActionResult Monedas()
        {
            return View("Monedas");
        }

        public ActionResult Monedas_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioMoneda servicio = new ServicioMoneda();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Monedas_Create([DataSourceRequest] DataSourceRequest request, MonedaViewModel obj)
        {
            ServicioMoneda servicio = new ServicioMoneda();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Monedas_Update([DataSourceRequest] DataSourceRequest request, MonedaViewModel obj)
        {
            ServicioMoneda servicio = new ServicioMoneda();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Monedas_Destroy([DataSourceRequest] DataSourceRequest request, MonedaViewModel obj)
        {
            ServicioMoneda servicio = new ServicioMoneda();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Tipo Persona
        [Autorizacion(Permiso = "TipoPersonas")]
        public ActionResult TipoPersonas()
        {
            return View("TipoPersonas");
        }

        public ActionResult TipoPersonas_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioTipoPersona servicio = new ServicioTipoPersona();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoPersonas_Create([DataSourceRequest] DataSourceRequest request, TipoPersonaViewModel obj)
        {
            ServicioTipoPersona servicio = new ServicioTipoPersona();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoPersonas_Update([DataSourceRequest] DataSourceRequest request, TipoPersonaViewModel obj)
        {
            ServicioTipoPersona servicio = new ServicioTipoPersona();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoPersonas_Destroy([DataSourceRequest] DataSourceRequest request, TipoPersonaViewModel obj)
        {
            ServicioTipoPersona servicio = new ServicioTipoPersona();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Tipo Documento
        [Autorizacion(Permiso = "TipoDocumentos")]
        public ActionResult TipoDocumentos()
        {
            return View("TipoDocumentos");
        }

        public ActionResult TipoDocumentos_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioTipoDocumento servicio = new ServicioTipoDocumento();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoDocumentos_Create([DataSourceRequest] DataSourceRequest request, TipoDocumentoViewModel obj)
        {
            ServicioTipoDocumento servicio = new ServicioTipoDocumento();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoDocumentos_Update([DataSourceRequest] DataSourceRequest request, TipoDocumentoViewModel obj)
        {
            ServicioTipoDocumento servicio = new ServicioTipoDocumento();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoDocumentos_Destroy([DataSourceRequest] DataSourceRequest request, TipoDocumentoViewModel obj)
        {
            ServicioTipoDocumento servicio = new ServicioTipoDocumento();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Tipo Empresa
        [Autorizacion(Permiso = "TipoEmpresas")]
        public ActionResult TipoEmpresas()
        {
            return View("TipoEmpresas");
        }

        public ActionResult TipoEmpresas_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioTipoEmpresa servicio = new ServicioTipoEmpresa();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoEmpresas_Create([DataSourceRequest] DataSourceRequest request, TipoEmpresaViewModel obj)
        {
            ServicioTipoEmpresa servicio = new ServicioTipoEmpresa();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoEmpresas_Update([DataSourceRequest] DataSourceRequest request, TipoEmpresaViewModel obj)
        {
            ServicioTipoEmpresa servicio = new ServicioTipoEmpresa();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoEmpresas_Destroy([DataSourceRequest] DataSourceRequest request, TipoEmpresaViewModel obj)
        {
            ServicioTipoEmpresa servicio = new ServicioTipoEmpresa();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Tipo Pago
        [Autorizacion(Permiso = "TipoPagos")]
        public ActionResult TipoPagos()
        {
            return View("TipoPagos");
        }

        public ActionResult TipoPagos_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioTipoPago servicio = new ServicioTipoPago();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoPagos_Create([DataSourceRequest] DataSourceRequest request, TipoPagoViewModel obj)
        {
            ServicioTipoPago servicio = new ServicioTipoPago();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoPagos_Update([DataSourceRequest] DataSourceRequest request, TipoPagoViewModel obj)
        {
            ServicioTipoPago servicio = new ServicioTipoPago();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoPagos_Destroy([DataSourceRequest] DataSourceRequest request, TipoPagoViewModel obj)
        {
            ServicioTipoPago servicio = new ServicioTipoPago();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Pais
        [Autorizacion(Permiso = "Paises")]
        public ActionResult Paises()
        {
            return View("Paises");
        }

        public JsonResult ReadPais(string text)
        {
            return Json(new ServicioPais().ReadByName(text), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Paises_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioPais servicio = new ServicioPais();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Paises_Create([DataSourceRequest] DataSourceRequest request, PaisViewModel obj)
        {
            ServicioPais servicio = new ServicioPais();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Paises_Update([DataSourceRequest] DataSourceRequest request, PaisViewModel obj)
        {
            ServicioPais servicio = new ServicioPais();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Paises_Destroy([DataSourceRequest] DataSourceRequest request, PaisViewModel obj)
        {
            ServicioPais servicio = new ServicioPais();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Departamentos
        [Autorizacion(Permiso = "Departamentos")]
        public ActionResult Departamentos()
        {
            return View("Departamentos");
        }

        //idDepartamento es la variable que trae el texto
        public JsonResult ReadDepartamentos(string idPais, string idDepartamento)
        {
            return Json(new ServicioDepartamento().ReadByNameAndPais(int.Parse(idPais), idDepartamento), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Departamentos_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioDepartamento servicio = new ServicioDepartamento();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Departamentos_Create([DataSourceRequest] DataSourceRequest request, DepartamentoViewModel obj)
        {
            ServicioDepartamento servicio = new ServicioDepartamento();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Departamentos_Update([DataSourceRequest] DataSourceRequest request, DepartamentoViewModel obj)
        {
            ServicioDepartamento servicio = new ServicioDepartamento();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Departamentos_Destroy([DataSourceRequest] DataSourceRequest request, DepartamentoViewModel obj)
        {
            ServicioDepartamento servicio = new ServicioDepartamento();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        public JsonResult ReadDepartamento(string text)
        {
            ServicioDepartamento servicio = new ServicioDepartamento();
            var resultado = servicio.ReadDepartamento(text);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Ciudades
        [Autorizacion(Permiso = "Ciudades")]
        public ActionResult Ciudades()
        {
            return View("Ciudades");
        }

        //idMunicipio es la variable que trae el texto
        public JsonResult ReadCiudad(string idDepartamento, string idMunicipio)
        {
            return Json(new ServicioCiudad().ReadByNameAndDepar(idMunicipio, int.Parse(idDepartamento)), JsonRequestBehavior.AllowGet);
        }

        public ActionResult Ciudades_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioCiudad servicio = new ServicioCiudad();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ciudades_Create([DataSourceRequest] DataSourceRequest request, CiudadViewModel obj)
        {
            ServicioCiudad servicio = new ServicioCiudad();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ciudades_Update([DataSourceRequest] DataSourceRequest request, CiudadViewModel obj)
        {
            ServicioCiudad servicio = new ServicioCiudad();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Ciudades_Destroy([DataSourceRequest] DataSourceRequest request, CiudadViewModel obj)
        {
            ServicioCiudad servicio = new ServicioCiudad();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }


        public JsonResult ReadCiudades(string text)
        {
            ServicioCiudad servicio = new ServicioCiudad();
            var resultado = servicio.ReadCiudades(text);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult get_CiudadByName(string text)
        {
            ServicioCiudad servicio = new ServicioCiudad();
            var resultado = servicio.ReadCiudades(text);
            if (resultado.Count > 0)
            {
                return Json(new { id = resultado.FirstOrDefault().Id }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { id = 0 }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region TipoUsuario
        [Autorizacion(Permiso = "TipoUsuario")]
        public ActionResult TipoUsuario()
        {
            return View("TipoUsuario");
        }
        public ActionResult TipoUsuario_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(new ServicioTipoUsuario().Read().ToDataSourceResult(request));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoUsuario_Create([DataSourceRequest] DataSourceRequest request, TipoUsuarioViewModel obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                new ServicioTipoUsuario().Save(obj);
            }
            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoUsuario_Update([DataSourceRequest] DataSourceRequest request, TipoUsuarioViewModel obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                new ServicioTipoUsuario().Save(obj);
            }
            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }
        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoUsuario_Destroy([DataSourceRequest] DataSourceRequest request, TipoUsuarioViewModel obj)
        {
            if (obj != null)
            {
                new ServicioTipoUsuario().Destroy(obj);
            }
            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }
        #endregion

        #region TipoRecaudos
        [Autorizacion(Permiso = "TipoRecaudos")]
        public ActionResult TipoRecaudos()
        {
            return View("TipoRecaudos");
        }

        public ActionResult TipoRecaudos_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(new ServicioTipoRecaudo().Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoRecaudos_Create([DataSourceRequest] DataSourceRequest request, TipoRecaudoViewModel obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                new ServicioTipoRecaudo().Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoRecaudos_Update([DataSourceRequest] DataSourceRequest request, TipoRecaudoViewModel obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                new ServicioTipoRecaudo().Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoRecaudos_Destroy([DataSourceRequest] DataSourceRequest request, TipoRecaudoViewModel obj)
        {
            if (obj != null)
            {
                new ServicioTipoRecaudo().Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Categorias
        [Autorizacion(Permiso = "Categorias")]
        public ActionResult Categorias()
        {
            return View();
        }

        public ActionResult Categorias_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(new ServicioCategoria().Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Categorias_Create([DataSourceRequest] DataSourceRequest request, CategoriaViewModel obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                new ServicioCategoria().Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Categorias_Update([DataSourceRequest] DataSourceRequest request, CategoriaViewModel obj)
        {
            if (obj != null && ModelState.IsValid)
            {
                new ServicioCategoria().Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Categorias_Destroy([DataSourceRequest] DataSourceRequest request, CategoriaViewModel obj)
        {
            if (obj != null)
            {
                new ServicioCategoria().Destroy(obj);
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
        public ActionResult DireccionesCliente_Destroy([DataSourceRequest] DataSourceRequest request, DireccionViewModel obj)
        {

            if (obj != null)
            {
                var item = DireccionesCliente.FirstOrDefault(c => c.Id == obj.Id);
                if (item != null)
                {

                    if (item.IdCliente > 0 && item.Id > 0)
                    {
                        new ServicioDireccion().EliminarDireccio(obj);
                    }
                    DireccionesCliente.Remove(item);
                }
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Clientes 
        [Autorizacion(Permiso = "Clientes")]
        public ActionResult Clientes()
        {
            return View();
        }

        public ActionResult Clientes_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(new ServicioCliente().ReadClientes().ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Clientes_Create([DataSourceRequest] DataSourceRequest request, ClienteViewModel obj)
        {
            if (obj != null)
            {
                obj.Identificacion = obj.Identificacion.TrimStart('0');
                obj.PrimerNombres = !string.IsNullOrWhiteSpace(obj.PrimerNombres) ? obj.PrimerNombres.Trim() : null;
                obj.SegundoNombres = !string.IsNullOrWhiteSpace(obj.SegundoNombres) ? obj.SegundoNombres.Trim() : null;
                obj.PrimerApellido = !string.IsNullOrWhiteSpace(obj.PrimerApellido) ? obj.PrimerApellido.Trim() : null;
                obj.SegundoApellido = !string.IsNullOrWhiteSpace(obj.SegundoApellido) ? obj.SegundoApellido.Trim() : null;
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
                if (obj.TipoDocumento_id == 2)
                {
                    obj.EsEmpresa = true;
                }
                obj.Auditoria.IdCreatorby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateCreated = DateTime.Now;
                obj.Direcciones = DireccionesCliente;
                new ServicioCliente().Save(obj);

            }
            if (obj.Id == 0)
            {
                string error = "El tipo de Documento y Numero de Identificacion ya existe";
                ModelState.AddModelError("ServerError", error);
            }
            return Json(new[] { obj }.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Clientes_Update([DataSourceRequest] DataSourceRequest request, ClienteViewModel obj)
        {
            if (obj != null)
            {
                if (obj.strImagen != null)
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
                obj.Auditoria.IdChangedby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateChanged = DateTime.Now;
                new ServicioCliente().Update(obj);
            }
            return Json(new[] { obj }.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Clientes_Destroy([DataSourceRequest] DataSourceRequest request, ClienteViewModel obj)
        {
            if (obj != null)
            {
                new ServicioCliente().Destroy(obj);
            }
            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Empresarios

        [Autorizacion(Permiso = "EmpresarioClientes")]
        public ActionResult EmpresarioClientes()
        {
            return View();
        }

        //Cargar las lista de empresarios
        public ActionResult Empresarios_Read([DataSourceRequest] DataSourceRequest request)
        {
            return Json(new ServicioEmpresarios().ReadEmpresarios().ToDataSourceResult(request));
        }

        //Consultar cliente de la plataforma
        [HttpPost]
        public ActionResult GetInfoPersonaUserbyIden([DataSourceRequest] DataSourceRequest request, int tipoID, string iden)
        {
            ServicioEmpresarios servicio = new ServicioEmpresarios();
            var client = servicio.ReadByIdenCliente(tipoID, iden);
            //return Json(user.ToDataSourceResult(request));
            return Json(new { objeto = client }, JsonRequestBehavior.AllowGet);
        }

        //Crear Empresario
        [HttpPost]
        public ActionResult Empresarios_Create([DataSourceRequest] DataSourceRequest request, EmpresarioViewModel obj)
        {
            if (obj != null)
            {
                if (obj.PasswordEmpresaio == obj.ConfriPassEmpresario)
                {
                    if (obj.ClienteId == 0)
                    {
                        obj.Identificacion = obj.Identificacion.TrimStart('0');
                        obj.PrimerNombres = !string.IsNullOrWhiteSpace(obj.PrimerNombres) ? obj.PrimerNombres.Trim() : null;
                        obj.SegundoNombres = !string.IsNullOrWhiteSpace(obj.SegundoNombres) ? obj.SegundoNombres.Trim() : null;
                        obj.PrimerApellido = !string.IsNullOrWhiteSpace(obj.PrimerApellido) ? obj.PrimerApellido.Trim() : null;
                        obj.SegundoApellido = !string.IsNullOrWhiteSpace(obj.SegundoApellido) ? obj.SegundoApellido.Trim() : null;
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
                        if (obj.TipoDocumento_id == 2)
                        {
                            obj.EsEmpresa = true;
                        }
                        else
                        {
                            obj.EmpresarioTipoDocumento_idRL = obj.TipoDocumento_id;
                            obj.EmpresarioIdentificacionRL = obj.Identificacion;
                            obj.EmpresarioPrimerNombreRL = obj.PrimerNombres;
                            obj.EmpresarioSegundoNombreRL = obj.SegundoNombres;
                            obj.EmpresarioPrimerApellidosRL = obj.PrimerApellido;
                            obj.EmpresarioSegundoApellidosRL = obj.SegundoApellido;
                            obj.EmpresarioCelularRL = obj.Celular;
                            obj.EmpresarioEmailRL = obj.Email;
                            obj.EmpresarioGenero_idRL = obj.Genero_id;
                            obj.EmpresarioFechaNacimientoRL = obj.FechaNacimiento;
                        }

                    }
                    else
                    {
                        var Cliente = new ServicioEmpresarios().ReadByIdCliente(obj.ClienteId);
                        if (Cliente.EsEmpresa == false)
                        {
                            obj.EmpresarioTipoDocumento_idRL = Cliente.TipoDocumento_id;
                            obj.EmpresarioIdentificacionRL = Cliente.Identificacion;
                            obj.EmpresarioPrimerNombreRL = Cliente.PrimerNombres;
                            obj.EmpresarioSegundoNombreRL = Cliente.SegundoNombres;
                            obj.EmpresarioPrimerApellidosRL = Cliente.PrimerApellido;
                            obj.EmpresarioSegundoApellidosRL = Cliente.SegundoApellido;
                            obj.EmpresarioCelularRL = Cliente.Celular;
                            obj.EmpresarioEmailRL = Cliente.Email;
                            obj.EmpresarioGenero_idRL = Cliente.Genero_id;
                            obj.EmpresarioFechaNacimientoRL = Cliente.FechaNacimiento;
                        }

                    }

                    obj.Auditoria.IdCreatorby = Utilidades.GetCurrentUser().Id;
                    obj.Auditoria.DateCreated = DateTime.Now;
                    obj.Direcciones = DireccionesCliente;
                    new ServicioEmpresarios().SaveEmpresario(obj);

                }
                else
                {
                    string error = "Las contraseñas no coinciden";
                    ModelState.AddModelError("ServerError", error);
                }

            }
            return Json(new[] { obj }.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Empresarios_Update([DataSourceRequest] DataSourceRequest request, EmpresarioViewModel obj)
        {
            if (obj != null)
            {
                if (obj.TipoDocumento_id != 2)
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
                }
                obj.Auditoria.IdChangedby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateChanged = DateTime.Now;
                obj.Direcciones = DireccionesCliente;
                new ServicioEmpresarios().Update(obj);

            }
            return Json(new[] { obj }.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult Empresarios_Destroy([DataSourceRequest] DataSourceRequest request, EmpresarioViewModel obj)
        {
            if (obj != null)
            {
                obj.Auditoria.IdRetiredby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateRetired = DateTime.Now;
                new ServicioEmpresarios().Destroy(obj);
            }
            return Json(new[] { obj }.ToDataSourceResult(request));
        }

        #endregion

        #region Empresas Eventos
        [Autorizacion(Permiso = "EmpresasEvento")]
        public ActionResult EmpresasEvento()
        {
            return View("EmpresasEvento");
        }

        public JsonResult EmpresabyText(string text)
        {
            ServicioEmpresaEvento servicio = new ServicioEmpresaEvento();
            var resultado = servicio.ReadEmpresasByText(text);
            return Json(resultado, JsonRequestBehavior.AllowGet);
        }

        public ActionResult EmpresasEvento_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioEmpresaEvento servicio = new ServicioEmpresaEvento();
            return Json(servicio.Read().ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmpresasEvento_Create([DataSourceRequest] DataSourceRequest request, EmpresaEventosViewModel obj)
        {
            ServicioEmpresaEvento servicio = new ServicioEmpresaEvento();
            if (obj != null && ModelState.IsValid)
            {
                obj.Auditoria.IdCreatorby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateCreated = DateTime.Now;
                obj.Auditoria.activo = true;
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmpresasEvento_Update([DataSourceRequest] DataSourceRequest request, EmpresaEventosViewModel obj)
        {
            ServicioEmpresaEvento servicio = new ServicioEmpresaEvento();
            if (obj != null)
            {
                obj.Auditoria.IdChangedby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateChanged = DateTime.Now;
                obj.Auditoria.activo = true;
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EmpresasEvento_Destroy([DataSourceRequest] DataSourceRequest request, EmpresaEventosViewModel obj)
        {
            ServicioEmpresaEvento servicio = new ServicioEmpresaEvento();
            if (obj != null)
            {
                obj.Auditoria.IdRetiredby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateRetired = DateTime.Now;
                obj.Auditoria.activo = false;
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region Tipo Boleta Papel

        [Autorizacion(Permiso = "TipoBoletosPapel")]
        public ActionResult TipoBoletosPapel()
        {
            return View("TipoBoletosPapel");
        }

        public ActionResult TipoBoletosPapel_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioTipoBoletoPapel servicio = new ServicioTipoBoletoPapel();
            return Json(servicio.Read().ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult TipoBoletosPapel_Create([DataSourceRequest] DataSourceRequest request, TipoBoletosPapelViewModel obj)
        {
            ServicioTipoBoletoPapel servicio = new ServicioTipoBoletoPapel();
            if (obj != null && ModelState.IsValid)
            {
                obj.Auditoria.IdCreatorby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateCreated = DateTime.Now;
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [HttpPost]
        public ActionResult TipoBoletosPapel_Update([DataSourceRequest] DataSourceRequest request, TipoBoletosPapelViewModel obj)
        {
            ServicioTipoBoletoPapel servicio = new ServicioTipoBoletoPapel();
            if (obj != null)
            {
                obj.Auditoria.IdChangedby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateChanged = DateTime.Now;
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult TipoBoletosPapel_Destroy([DataSourceRequest] DataSourceRequest request, TipoBoletosPapelViewModel obj)
        {
            ServicioTipoBoletoPapel servicio = new ServicioTipoBoletoPapel();
            if (obj != null)
            {
                obj.Auditoria.IdRetiredby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateRetired = DateTime.Now;
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request));
        }
        #endregion

        #region Diseñador Plantillas Boletos

        [Autorizacion(Permiso = "DiseñadorBoletosPapel")]
        public ActionResult DiseñadorBoletosPapel()
        {
            return View("DiseñadorBoletosPapel");
        }

        public ActionResult DiseñadorBoletosPapel_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioDiseñdorPlatnillasBoletos servicio = new ServicioDiseñdorPlatnillasBoletos();
            return Json(servicio.Read().ToDataSourceResult(request));
        }



        [HttpPost]
        public ActionResult DiseñadorBoletosPapel_Create([DataSourceRequest] DataSourceRequest request, PlantillasBoletosPapelViewModel obj)
        {
            ServicioDiseñdorPlatnillasBoletos servicio = new ServicioDiseñdorPlatnillasBoletos();
            if (obj != null)
            {
                obj.Auditoria.IdCreatorby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateCreated = DateTime.Now;
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult DiseñadorBoletosPapel_Update([DataSourceRequest] DataSourceRequest request, PlantillasBoletosPapelViewModel obj)
        {
            ServicioDiseñdorPlatnillasBoletos servicio = new ServicioDiseñdorPlatnillasBoletos();
            if (obj != null)
            {
                obj.Auditoria.IdChangedby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateChanged = DateTime.Now;
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request));
        }

        [HttpPost]
        public ActionResult DiseñadorBoletosPapel_Destroy([DataSourceRequest] DataSourceRequest request, PlantillasBoletosPapelViewModel obj)
        {
            ServicioDiseñdorPlatnillasBoletos servicio = new ServicioDiseñdorPlatnillasBoletos();
            if (obj != null)
            {
                obj.Auditoria.IdRetiredby = Utilidades.GetCurrentUser().Id;
                obj.Auditoria.DateRetired = DateTime.Now;
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request));
        }

        public ActionResult Plantillas_GenerarImagenCodigoBarras(string codigo, int idTipoCodigoBarras, int maxHeight)
        {
            try
            {
                var objImagen = Utilidades.GenerarImagenCodigoBarras(codigo, idTipoCodigoBarras, maxHeight);

                if (objImagen != null)
                {
                    return Json(
                        new
                        {
                            Error = false,
                            Imagen = Convert.ToBase64String(objImagen),
                            message = "OK"
                        }, JsonRequestBehavior.AllowGet);
                }
                return Json(
                    new
                    {
                        Error = true,
                        message = "Error generando la imagen"
                    }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception err)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = err.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult Plantillas_GenerarImagenCodigoQR(string codigo, int idTipoQr, int maxHeight)
        {
            try
            {
                var objImagen = Utilidades.GenerarImagenCodigoQR(codigo, idTipoQr, maxHeight);

                if (objImagen != null)
                {
                    return Json(
                        new
                        {
                            Error = false,
                            Imagen = Convert.ToBase64String(objImagen),
                            message = "OK"
                        }, JsonRequestBehavior.AllowGet);
                }
                return Json(
                    new
                    {
                        Error = true,
                        message = "Error generando la imagen"
                    }, JsonRequestBehavior.AllowGet);

            }
            catch (Exception err)
            {
                return Json(new
                {
                    Error = true,
                    Mensaje = err.Message
                }, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion

        #region Diseñador

        public ActionResult Diseñador()
        {
            return View("Diseñador");
        }

        public ActionResult Diseñador_CreateUpdate(string json, int? idPlantilla)
        {
            try
            {
                if (json != "" && idPlantilla !=null)
                {
                    ServicioDiseñdorPlatnillasBoletos servicio = new ServicioDiseñdorPlatnillasBoletos();
                    servicio.CreateUpdateCanvasDiseño(json,idPlantilla.Value);
                    return Json(new
                    {
                        Message = string.Format("Diseño creado")
                    }, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception err)
            {
                return Json(new
                {
                    Message = string.Format(err.Message)
                }, JsonRequestBehavior.AllowGet);
            }
            
            return null;
        }

        #endregion

        public ActionResult AccesoNoAutorizado()
        {
            return View("AccesoNoAutorizado");
        }

        [HttpPost]
        public ActionResult GetInfoByUserEmail(string user, string email)
        {
            ServicioEmpresarios servicio = new ServicioEmpresarios();
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

        [HttpGet]
        public ActionResult ReadImgbyId(int idCliente)
        {
            var imgStr = new ServicioCliente().ReadImgbyId(idCliente);
            return Json(new { imagen = imgStr }, JsonRequestBehavior.AllowGet);
        }

    }
}
