using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Verificador.Vista.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Zen.Barcode;
using IDAutomation.NetAssembly;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Web.Helpers;
using Verificador.Vista.Models.Servicios;
using Verificador.Vista.Models.ViewModels;
using Verificador.Vista.Models;

namespace Verificador.Vista.Controllers
{
    public class EventoController : Controller
    {
        private const int CATEGORIA = 0;
        private const int INFORMACIONGENRAL = 1;
        private const int INFORMACIONADICIONAL = 2;
        private const int DIRECCIONMAP = 3;
        private const int FUNCIONES = 4;
        private const int LOCALIDADES = 5;
        private const int ETAPAS = 6;
        private const int LIQUIDACION = 7;

        byte[] bytes = new byte[1024];
        Socket senderSock;

        public ActionResult TicketPartial()
        {
            var n = DateTime.Now.ToFileTime();
            return View(new TicketViewModel()
            {
                Numero = 99,
                Precio = 25000,
                Barcode = getBarcode(1, n),
                NumericBarcode = n.ToString(),
                Responsable = "RESPONSABLE: Triada Colombia S.A.S NIT: 900136882-9".ToUpper(),
                Ubicacion = "Calle 32 # 35 -30",
                NombreEveno = "Evento de Prueba # 1"
            });
        }

        public ActionResult TicketDesigner()
        {
            var n = DateTime.Now.ToFileTime();
            FontEncoder encoder = new FontEncoder();
            string text = encoder.Code39(n.ToString());

            return View(new TicketViewModel()
            {
                Numero = 99,
                Precio = 25000,
                Barcode = getBarcode(1, n),
                NumericBarcode = n.ToString(),
                NumericBarcodeText = encoder.Code39(n.ToString()),
                ticketType = TicketType.grande,
                fechaEvento = removerAcentos(DateTime.Now.ToString("dddd dd MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("es-CO"))),
                EdadMinima = string.Format("Edad Minima {0}", 18),
                HoraApertura = String.Format("Apertarura de puertas: {0}", DateTime.Now.ToString("HH:mm")),
                Localidad = removerAcentos(string.Format("Localidad: {0}", "Aforo")),
                BackgorundImgae = _Ticket.Background.Large,
                Responsable = "RESPONSABLE: Triada Colombia S.A.S NIT: 900136882-9".ToUpper(),
                Ubicacion = "Calle 32 # 35 -30",
                NombreEveno = "Evento de Prueba # 1"
            });

            //return View(new TicketViewModel()
            //{
            //    Numero = 99,
            //    Precio = 25000,
            //    Barcode = getBarcode(1, n),
            //    NumericBarcode = n,
            //    NumericBarcodeText = encoder.Code39(n.ToString()),
            //    ticketType = TicketType.small,
            //    fechaEvento = removerAcentos(DateTime.Now.ToString("dddd dd MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("es-CO"))),
            //    EdadMinima = string.Format("Edad Minima {0}", 18),
            //    HoraApertura = String.Format("Apertarura de puertas: {0}", DateTime.Now.ToString("HH:mm")),
            //    Localidad = removerAcentos(string.Format("Localidad: {0}", "Aforo")),
            //    BackgorundImgae = _Ticket.Background.Small,
            //    Responsable = "RESPONSABLE: Triada Colombia S.A.S NIT: 900136882-9".ToUpper(),
            //    Ubicacion = "Calle 32 # 35 -30",
            //    NombreEveno = "Evento de Prueba # 1"
            //});
        }

        #region Impresión Tickets
        public ActionResult CustomPrint(int idEvento)
        {
            TicketsEventoViewModel ticketsEvento = new ServicioTicketsEvento().ReadByEvent(idEvento);
            return PartialView("CustomPrint", ticketsEvento);
        }

        public ActionResult getTicketsByEvento([DataSourceRequest] DataSourceRequest request, int idevento, int inicio, int final, string etapa, string localidad)
        {
            return Json(new ServicioTickets().Read(idevento, inicio, final).ToDataSourceResult(request));
        }
        [HttpPost]
        public ActionResult RemoveTickets(int Id, int incio, int final) {
            new ServicioTickets().Destroy(incio, final);
            return Json(new { message = "Tiquetes eliminados satisfactoriamente" }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult RemoveTicket(int Id)
        {
            new ServicioTickets().Destroy(Id);
            return Json(new { message = "Tiquete eliminado satisfactoriamente" }, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ImprimirTickets()
        {
            ViewBag.WCPScript = Neodynamic.SDK.Web.WebClientPrint.CreateScript(Url.Action("ProcessRequest", "WebClientPrintAPI", null, HttpContext.Request.Url.Scheme), "", HttpContext.Session.SessionID);
            return View();
        }

        public ActionResult ImprimirTickets_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioEvento servicio = new ServicioEvento();
            return Json(servicio.ReadPrintTicketsState().ToDataSourceResult(request));
        }

        public ActionResult PrintTicketsPartial(ImpresionTicketsViewModel obj)
        {
            return PartialView("PrintTicketsPartial", obj);
        }

        public JsonResult getEtapasByEvent(int idEvento)
        {
            return Json(new ServicioEtapas().Read(idEvento).Select(e => new ListItem { Value = e.Id, Text = e.Nombre }).ToList(), JsonRequestBehavior.AllowGet);
        }

        public JsonResult getLocalidadesByEtapa(int idEtapa)
        {
            return Json(ServicioLocalidad.getOptions(idEtapa, true), JsonRequestBehavior.AllowGet);
        }
        
        public ActionResult PrepareToPrint(TicketsEventoViewModel obj, int IdTicketsEtapa, int inicio, int final)
        {
            try
            {
                PullTicketsImpresionViewModel PullTicketsImpresion = new PullTicketsImpresionViewModel()
                {
                    IdPullTickets = obj.Id,
                    Cantidad = final - inicio,
                    PrinterName = "Default",
                    ImgTicket = obj.ContentSVG
                };

                int tick = 0;
                string token = string.Empty;
                while (tick <= 3)
                {
                    try
                    {
                        token = Utilidades.GetClientMac(Request);
                        break;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                new ServicioTicketsEvento().SaveTicketToPtint(PullTicketsImpresion, IdTicketsEtapa, token, inicio, final);

                if (string.IsNullOrEmpty(token))
                {
                    Comun.WriteErrorLog("No se pudo obtener la dirección del equipo", PullTicketsImpresion.Id);
                }
                return Json(new { message = "Los tiquetes se encuentran en cola para ser impresos" });
            }
            catch (Exception ex)
            {
                Comun.WriteErrorLog(ex.Message, 0);
            }
            return Json(new { message = "Ocurrio un error al obtener la información"});
        }

        public ActionResult PrintTickets(ImpresionTicketsViewModel obj)
        {
            try
            {
                EventoViewModel evento = new ServicioEvento().ReadById(obj.Id);
                ServicioTicketsEvento TicketsEventoServicio = new ServicioTicketsEvento();
                TicketsEventoViewModel ticketsEvento = TicketsEventoServicio.ReadByEvent(obj.Id);
                TicketsEtapaViewModel ticketsEtapa = new ServicioTicketsEtapa().getByData(obj.idTicketsEtapa);
                //List<TicketViewModel> tickets = new ServicioTickets().ReadByPullTicketsMin(ticketsEvento.Id, obj.Cantidad);
                //LocalidadViewModel localidad = new ServicioLocalidad().ReadById(ticketsEtapa.Localidad_id);

                PullTicketsImpresionViewModel PullTicketsImpresion = new PullTicketsImpresionViewModel()
                {
                    IdPullTickets = ticketsEvento.Id,
                    IdLocalidad = ticketsEtapa.IdLocalidad,
                    IdEtapa = ticketsEtapa.Etapa_id,
                    ImgTicket = ticketsEvento.ContentSVG,
                    TicketType = ticketsEvento.TicketType,
                    Precio = ticketsEtapa.Precio,
                    Numero = ticketsEvento.InicioNumeracion,
                    //Responsable = removerAcentos(evento.Empresario.fullRazonSocial),
                    Ubicacion = removerAcentos(evento.Venue.Direccion),
                    NombreEveno = removerAcentos(evento.Nombre),
                    fechaEvento = removerAcentos(evento.FechaInicial.ToString("dddd dd MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("es-CO"))),
                    EdadMinima = string.Format("Edad Minima {0}", evento.EdadMinima),
                    HoraApertura = String.Format("Apertarura de puertas: {0}", evento.FechaInicial.ToString("HH:mm")),
                    Localidad = removerAcentos(string.Format("Localidad: {0}", ticketsEtapa.Localidad.Nombre)),
                    Cantidad = obj.Cantidad,
                    PrinterName = obj.PrinterName
                };
                int tick = 0;
                string token = string.Empty;
                while (tick <= 3)
                {
                    try
                    {
                        token = Utilidades.GetClientMac(Request);
                        break;
                    }
                    catch (Exception)
                    {
                        continue;
                    }
                }

                PullTicketsImpresion = TicketsEventoServicio.CreatePullTicketsToPrint(PullTicketsImpresion, (int)obj.idTicketsEtapa, token);

                if (string.IsNullOrEmpty(token))
                {
                    Comun.WriteErrorLog("No se pudo obtener la dirección del equipo", PullTicketsImpresion.Id);
                }
                return PartialView("CanvasTicketsPartial", PullTicketsImpresion);
            }
            catch (Exception ex)
            {
                Comun.WriteErrorLog(ex.Message, 0);
            }
            return PartialView("CanvasTicketsPartial", new PullTicketsImpresionViewModel());
        }

        public ActionResult SaveTicketsPrint(PullTicketsImpresionViewModel data)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                //List<TicketToPrintViewModel> ticketToPrint = js.Deserialize<List<TicketToPrintViewModel>>(data);
                //TicketToPrintViewModel ticketToPrint = js.Deserialize<TicketToPrintViewModel>(data);
                new ServicioTicketsEvento().SaveTicketToPtint(data);

                //var macAddress = Utilidades.GetClientMac(Request);

                //Task.Factory.StartNew(() =>
                //{
                //    ConnectClick();
                //    SendClick(macAddress);

                //    Disconnect_Click();
                //});
                //Aquí mandar la peticion al winform para que se encarge de iprimir
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message });
            }
            return Json(new { msg = "Los ticketes se encuentran en cola para ser impresos", OK = true });
        }

        private void ConnectClick()
        {
            try
            {
                // Create one SocketPermission for socket access restrictions 
                SocketPermission permission = new SocketPermission(
                    NetworkAccess.Connect,    // Connection permission 
                    TransportType.Tcp,        // Defines transport types 
                    "",                       // Gets the IP addresses 
                    SocketPermission.AllPorts // All ports 
                    );

                // Ensures the code to have permission to access a Socket 
                permission.Demand();

                // Resolves a host name to an IPHostEntry instance            
                IPHostEntry ipHost = Dns.GetHostEntry("");

                // Gets first IP address associated with a localhost 
                IPAddress ipAddr = ipHost.AddressList[0];

                // Creates a network endpoint 
                IPEndPoint ipEndPoint = new IPEndPoint(ipAddr, 4510);

                // Create one Socket object to setup Tcp connection 
                senderSock = new Socket(
                    ipAddr.AddressFamily,// Specifies the addressing scheme 
                    SocketType.Stream,   // The type of socket  
                    ProtocolType.Tcp     // Specifies the protocols  
                    );

                senderSock.NoDelay = false;   // Using the Nagle algorithm 

                // Establishes a connection to a remote host 
                senderSock.Connect(ipEndPoint);
                //tbStatus.Text = "Socket connected to " + senderSock.RemoteEndPoint.ToString();

                //Connect_Button.IsEnabled = false;
                //Send_Button.IsEnabled = true;
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
            }

        }

        private void SendClick(string message)
        {
            try
            {
                // Sending message 
                //<Client Quit> is the sign for end of data 
                byte[] msg = Encoding.Unicode.GetBytes(message + "<Client Quit>");

                // Sends data to a connected Socket. 
                int bytesSend = senderSock.Send(msg);

                ReceiveDataFromServer();

                //Send_Button.IsEnabled = false;
                //Disconnect_Button.IsEnabled = true;
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
            }
        }

        private void ReceiveDataFromServer()
        {
            try
            {
                // Receives data from a bound Socket. 
                int bytesRec = senderSock.Receive(bytes);

                // Converts byte array to string 
                String theMessageToReceive = Encoding.Unicode.GetString(bytes, 0, bytesRec);

                // Continues to read the data till data isn't available 
                while (senderSock.Available > 0)
                {
                    bytesRec = senderSock.Receive(bytes);
                    theMessageToReceive += Encoding.Unicode.GetString(bytes, 0, bytesRec);
                }

                //tbReceivedMsg.Text = "The server reply: " + theMessageToReceive;
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
            }
        }

        private void Disconnect_Click()
        {
            try
            {
                // Disables sends and receives on a Socket. 
                senderSock.Shutdown(SocketShutdown.Both);

                //Closes the Socket connection and releases all resources 
                senderSock.Close();

                //Disconnect_Button.IsEnabled = false;
            }
            catch (Exception exc)
            {
                //MessageBox.Show(exc.ToString());
            }
        }
        #endregion

        #region Grid-Eventos
        public ActionResult Eventos()
        {
            return View();
        }

        public ActionResult Eventos_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioEvento servicio = new ServicioEvento();
            return Json(servicio.Read().ToDataSourceResult(request));
        }

        /// <summary>
        /// Edicion del evento
        /// </summary>
        /// <param name="id">Id del evento</param>
        /// <returns></returns>
        public ActionResult evento(int id)
        {
            var evento = new ServicioEvento().Read(id);
            //evento.Venue.Localidades = new ServicioLocalidad().Read(evento.Venue_id).ToList();
            evento.Etapas = new ServicioEtapas().Read(id);
            evento.Tickets = new ServicioTicketsEvento().ReadByEvent(id);
            evento.TipoRegisro = Estado.Editando;
            evento.Estado = EstadoEvento.finalizado;
            CurrentEvent = evento;
            return View("Index", evento);
        }
        #endregion

        #region Creacion Evento

        public ActionResult Index()
        {
            CurrentEvent = new EventoViewModel();
            ViewBag.Estado = EstadoEvento.categoria;
            CurrentEvent.Estado = EstadoEvento.categoria;
            CurrentEvent.TipoRegisro = Estado.Nuevo;
            return View();
        }

        #region INFORMACIÓN GENERAL
        [HttpPost]
        public ActionResult Index(EventoViewModel evento, string Next)
        {
            try
            {
                if (Next != null)
                {
                    CurrentEvent.Nombre = evento.Nombre;
                    CurrentEvent.Empresario_id = evento.Empresario_id;
                    CurrentEvent.TipoEvento_id = evento.TipoEvento_id;
                    CurrentEvent.EventoPublico = evento.EventoPublico;
                    CurrentEvent.EdadMinima = evento.EdadMinima;
                    CurrentEvent.AforoTotal = evento.AforoTotal;
                    CurrentEvent.Descripcion = evento.Descripcion;
                    CurrentEvent.Categoria_id = evento.Categoria_id;
                    CurrentEvent.UrlVideo = evento.UrlVideo;
                    if (CurrentEvent.Estado == EstadoEvento.categoria)
                    {
                        CurrentEvent.Estado = EstadoEvento.informacionGenral;
                        ViewBag.Estado = CurrentEvent.Estado;
                    }
                    if (CurrentEvent.TipoRegisro == Estado.Nuevo && (CurrentEvent.Estado <= EstadoEvento.informacionGenral))
                        return View("Step2");
                    else
                        return View("Step2", CurrentEvent);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                //evento.Estado = EstadoEvento.informacionGenral;
            }
            return View();
        }

        public void SaveImage(string fromBase, string toImage)
        {
            byte[] imageBytes = Convert.FromBase64String(fromBase);
            MemoryStream ms = new MemoryStream(imageBytes, 0,
              imageBytes.Length);

            ms.Write(imageBytes, 0, imageBytes.Length);
            System.Drawing.Image image = System.Drawing.Image.FromStream(ms, true);
            image.Save(toImage);
        }

        public ActionResult Save(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    byte[] imgData = new Byte[file.InputStream.Length];
                    long bytesRead = file.InputStream.Read(imgData, 0, (int)file.InputStream.Length);
                    file.InputStream.Close();
                    Session[Comun.ImagenMemoria] = System.Convert.ToBase64String(imgData, 0, imgData.Length);
                }
            }
            return Content("");
        }

        public ActionResult Remove(string[] fileNames)
        {
            if (fileNames != null)
            {
                Session[Comun.ImagenMemoria] = string.Empty;
            }
            return Content("");
        }
        #endregion

        #region INFORMACION ADICIONAL

        #region IMPUESTOS X EVENTO
        [HttpGet]
        public ActionResult getValorImpuesto(int id)
        {
            return Json(new { Valor = new ServicioImpuesto().Read().Where(e => e.Id == id).FirstOrDefault().Valor.ToString() }, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult get_ImpuestosxEvento()
        {
            //return PartialView("ImpuestosPartial", new ServicioImpuesto().Read(evento_id));
            return PartialView("ImpuestosPartial", CurrentEvent.Impuestos);
        }
        [HttpPost]
        public ActionResult AddImpuesto(ImpuestoViewModel impuesto)
        {
            if (CurrentEvent != null)
            {
                bool crear = true;
                if (CurrentEvent.Impuestos != null)
                {
                    if (CurrentEvent.Impuestos.Count() > 0)
                    {
                        crear = false;
                        if (CurrentEvent.Impuestos.Where(e => e.Id == impuesto.Id).Count() == 0)
                        {
                            CurrentEvent.Impuestos.Add(impuesto);
                        }
                    }
                }
                if (crear)
                {
                    CurrentEvent.Impuestos = new List<ImpuestoViewModel>();
                    CurrentEvent.Impuestos.Add(impuesto);
                }
            }
            //ServicioImpuesto servicio = new ServicioImpuesto();
            //servicio.AsociarAlEvento(evento_id, id);
            return PartialView("ImpuestosPartial", CurrentEvent.Impuestos);
        }
        [HttpPost]
        public ActionResult RemoveImpuesto(int id)
        {
            var item = CurrentEvent.Impuestos.Where(e => e.Id == id).FirstOrDefault();
            if (item != null)
            {
                CurrentEvent.Impuestos.Remove(item);
            }
            //ServicioImpuesto servicio = new ServicioImpuesto();
            //servicio.Destroy(id, evento_id);
            return PartialView("ImpuestosPartial", CurrentEvent.Impuestos);
        }
        #endregion

        [HttpPost]
        public ActionResult Step2([Bind(Include = "Id,TipoRecaudo_id,CantidadDiasValides,AplicaServicio,AplicaServicio,EntradasxUsuario,EntradasxPedidoOcompra")] EventoViewModel evento, string Next, string Previous)
        {
            try
            {
                CurrentEvent.TipoRecaudo_id = evento.TipoRecaudo_id;
                CurrentEvent.CantidadDiasValides = evento.CantidadDiasValides;
                CurrentEvent.AplicaServicio = evento.AplicaServicio;
                CurrentEvent.EntradasxUsuario = evento.EntradasxUsuario;
                CurrentEvent.EntradasxPedidoOcompra = evento.EntradasxPedidoOcompra;
                if (CurrentEvent.Estado == EstadoEvento.informacionGenral)
                {
                    CurrentEvent.Estado = EstadoEvento.informacionAdicional;
                    ViewBag.Estado = CurrentEvent.Estado;
                }
                if (Next != null)
                {
                    if (CurrentEvent.TipoRegisro == Estado.Nuevo && (CurrentEvent.Estado <= EstadoEvento.informacionAdicional))
                        return View("Step3");
                    else
                        return View("Step3", CurrentEvent.Venue);
                }
                else if (Previous != null)
                {
                    return View("Index", CurrentEvent);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
        #endregion

        #region DIRECCION MAP
        public int Venue_id
        {
            get
            {
                if (HttpContext.Session["Venue_id"] != null)
                {
                    return Convert.ToInt32(HttpContext.Session["Venue_id"]);
                }
                return 0;
            }
            set
            {
                HttpContext.Session["Venue_id"] = value;
            }
        }

        [HttpPost]
        public ActionResult Step3(VenueViewModel venue, string Next, string Previous)
        {
            try
            {
                if (CurrentEvent != null)
                {
                    if (CurrentEvent.Venue != null)
                    {
                        CurrentEvent.Venue.Nombre = venue.Nombre;
                        CurrentEvent.Venue.Telefono = venue.Telefono;
                        CurrentEvent.Venue.E_mail = venue.E_mail;
                        CurrentEvent.Venue.Direccion = venue.Direccion;
                        CurrentEvent.Venue.Ciudad_id = venue.Ciudad_id;
                        CurrentEvent.Venue.Latitud = venue.Latitud;
                        CurrentEvent.Venue.Longitud = venue.Longitud;
                    }
                    else
                    {
                        CurrentEvent.Venue = venue;
                    }
                    if (CurrentEvent.Estado == EstadoEvento.informacionAdicional)
                    {
                        CurrentEvent.Estado = EstadoEvento.direccionMap;
                        ViewBag.Estado = CurrentEvent.Estado;
                    }
                    if (Next != null)
                    {
                        return View("Step4");
                    }
                    else if (Previous != null)
                    {
                        return View("Step2", CurrentEvent);
                    }
                }
                //evento = new ServicioEvento().DireccionMap_save(evento);
                //evento.Funciones = new List<FuncionesEventoViewModel>();
                //evento.Funciones.Add(new FuncionesEventoViewModel() { FechaFinal = DateTime.Now, FechaInicial = DateTime.Now, AperturaPuertas = DateTime.Now });
                //Venue_id = evento.Venue.Id;
                //evento.Estado = EstadoEvento.funciones;
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }

        [HttpGet]
        public ActionResult getVenueByName(string query) {
            ServicioVenue servicio = new ServicioVenue();
            return Json(servicio.Read(query), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region FUNCIONES
        [HttpPost]
        public JsonResult get_FuncionById(int id)
        {
            return Json(new { data = CurrentEvent.Funciones.Where(e => e.Id == id).FirstOrDefault() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult get_FuncionesxEvento()
        {
            //return PartialView("FuncionesPartial", new ServicioFuncion().Read(evento_id));
            if (CurrentEvent.Funciones == null)
            {
                return PartialView("FuncionesPartial", new List<FuncionesEventoViewModel>());
            }
            return PartialView("FuncionesPartial", CurrentEvent.Funciones.OrderBy(e => e.Id));
        }
        [HttpPost]
        public ActionResult AddFuncion(FuncionesEventoViewModel funcion)
        {
            //ServicioFuncion servicio = new ServicioFuncion();
            //return PartialView("FuncionesPartial", servicio.Read(servicio.Save(funcion).Evento_id));
            if (CurrentEvent != null)
            {
                bool crear = true;
                if (CurrentEvent.Funciones != null)
                {
                    if (CurrentEvent.Funciones.Count() > 0)
                    {
                        var item = CurrentEvent.Funciones.Where(e => e.Id == funcion.Id).FirstOrDefault();
                        if (item != null)
                        {
                            item.Label = funcion.Label;
                            item.AperturaPuertas = funcion.AperturaPuertas;
                            item.FechaFinal = funcion.FechaFinal;
                            item.FechaInicial = funcion.FechaInicial;
                            item.Descripcion = funcion.Descripcion;
                        }
                        else
                        {
                            funcion.Id = CurrentEvent.Funciones.Min(e => e.Id) - 1;
                            CurrentEvent.Funciones.Add(funcion);
                        }
                        crear = false;
                    }
                }
                if (crear)
                {
                    funcion.Id = -1;
                    CurrentEvent.Funciones = new List<FuncionesEventoViewModel>();
                    CurrentEvent.Funciones.Add(funcion);
                }
            }
            return PartialView("FuncionesPartial", CurrentEvent.Funciones.OrderBy(e => e.Id));
        }
        [HttpPost]
        public ActionResult RemoveFuncion(int id)
        {
            //ServicioFuncion servicio = new ServicioFuncion();
            //servicio.Destroy(id);
            //return PartialView("FuncionesPartial", servicio.Read(evento_id));
            var item = CurrentEvent.Funciones.Where(e => e.Id == id).FirstOrDefault();
            if (item != null)
            {
                CurrentEvent.Funciones.Remove(item);
            }
            return PartialView("FuncionesPartial", CurrentEvent.Funciones.OrderBy(e => e.Id));
        }

        public ActionResult Step4(string Next, string Previous)
        {
            if (CurrentEvent.Estado == EstadoEvento.direccionMap)
            {
                CurrentEvent.Estado = EstadoEvento.funciones;
                ViewBag.Estado = CurrentEvent.Estado;
            }
            if (Next != null)
            {
                return View("Step5", CurrentEvent);
            }
            else if (Previous != null)
            {
                return View("Step3", CurrentEvent.Venue);
            }
            return View();
        }
        #endregion

        #region LOCALIDADES
        [HttpPost]
        public ActionResult AddLocalidad(LocalidadViewModel localidad)
        {
            //ServicioLocalidad servicio = new ServicioLocalidad();
            //return PartialView("LocalidadesPartial", servicio.Read(servicio.Save(localidad).Venue_id));
            if (CurrentEvent != null)
            {
                bool crear = true;
                if (CurrentEvent.Venue != null)
                {
                    if (CurrentEvent.Venue.Localidades != null)
                    {
                        if (CurrentEvent.Venue.Localidades.Count() > 0)
                        {
                            var item = CurrentEvent.Venue.Localidades.Where(e => e.Id == localidad.Id).FirstOrDefault();
                            if (item != null)
                            {
                                CurrentEvent.AforoAcmulado -= item.Aforo;
                                CurrentEvent.Venue.Localidades.Remove(item);
                            }
                            if (CurrentEvent.Venue.Localidades.Count > 0)
                            {
                                localidad.Id = CurrentEvent.Venue.Localidades.Min(e => e.Id) - 1;
                            }
                            else
                            {
                                localidad.Id = -1;
                            }
                            CurrentEvent.Venue.Localidades.Add(localidad);
                            crear = false;
                        }
                    }
                }
                if (crear)
                {
                    localidad.Id = -1;
                    CurrentEvent.Venue.Localidades = new List<LocalidadViewModel>();
                    CurrentEvent.Venue.Localidades.Add(localidad);
                }
                CurrentEvent.AforoAcmulado += localidad.Aforo;
            }
            return PartialView("LocalidadesPartial", CurrentEvent.Venue.Localidades.OrderBy(e => e.Id).OrderBy(e => e.Id));
        }

        [HttpPost]
        public ActionResult RemoveLocalidad(int id)
        {
            //ServicioLocalidad servicio = new ServicioLocalidad();
            //servicio.Destroy(id, venue_id);
            //return PartialView("LocalidadesPartial", servicio.Read(venue_id));
            var item = CurrentEvent.Venue.Localidades.Where(e => e.Id == id).FirstOrDefault();
            if (item != null)
            {
                CurrentEvent.Venue.Localidades.Remove(item);
                CurrentEvent.AforoAcmulado -= item.Aforo;
            }
            return PartialView("LocalidadesPartial", CurrentEvent.Venue.Localidades.OrderBy(e => e.Id));
        }

        [HttpPost]
        public ActionResult Get_LocalidadesByVenue()
        {
            //if (venue_id == 0)
            //{
            //    venue_id = Venue_id;
            //}
            //return PartialView("LocalidadesPartial", new ServicioLocalidad().Read(venue_id));
            if (CurrentEvent.Venue.Localidades == null)
            {
                return PartialView("LocalidadesPartial", new List<LocalidadViewModel>());
            }
            return PartialView("LocalidadesPartial", CurrentEvent.Venue.Localidades.OrderBy(e => e.Id));
        }

        [HttpPost]
        public JsonResult get_LocalidadById(int id)
        {
            return Json(new { data = CurrentEvent.Venue.Localidades.Where(e => e.Id == id).FirstOrDefault() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult Step5(string Next, string Previous)
        {
            if (CurrentEvent.Estado == EstadoEvento.funciones)
            {
                CurrentEvent.Estado = EstadoEvento.localidades;
                ViewBag.Estado = CurrentEvent.Estado;
            }
            ViewBag.Localidades = CurrentEvent.Venue.Localidades.Select(e => new
            {
                id = e.Id,
                tipo = e.IdTipoLocalidad,
                aforo = e.Aforo,
                aforoConsumido = 0
            }).ToList();
            if (Next != null)
            {
                return View("Step6");
            }
            else if (Previous != null)
            {
                return View("Step4");
            }
            return View();
        }
        #endregion

        #region ETAPAS
        [HttpPost]
        public JsonResult get_EtapaById(int id)
        {
            return Json(new { data = CurrentEvent.Etapas.Where(e => e.Id == id).FirstOrDefault() }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult get_EtapasxEvento()
        {
            if (CurrentEvent.Etapas == null)
            {
                return PartialView("EtapasPartial", new List<EtapasViewModel>());
            }
            return PartialView("EtapasPartial", CurrentEvent.Etapas.OrderBy(e => e.Id));
        }
        [HttpPost]
        public ActionResult AddEtapa(EtapasViewModel etapa)
        {
            if (CurrentEvent != null)
            {
                bool crear = true;
                if (CurrentEvent.Etapas != null)
                {
                    if (CurrentEvent.Etapas.Count() > 0)
                    {
                        var item = CurrentEvent.Etapas.Where(e => e.Id == etapa.Id).FirstOrDefault();
                        if (item != null)
                        {
                            item.Nombre = etapa.Nombre;
                            item.Cantidad = etapa.Cantidad;
                            //item.Precio = etapa.Precio;
                            item.FechaInicial = etapa.FechaInicial;
                            item.FechaFinal = etapa.FechaFinal;
                            item.Descripcion = etapa.Descripcion;
                            //if (item.TicketsEtapas != null)
                            //{
                            //    item.TicketsEtapas[0] = new TicketsEtapaViewModel()
                            //    {
                            //        Localidad_id = etapa.Localidad_id,
                            //        Etapa_id = etapa.Id,
                            //        Cantidad = etapa.Cantidad,
                            //        Precio = etapa.Precio
                            //    };
                            //}
                            //else
                            //{
                            //    item.TicketsEtapas = new List<TicketsEtapaViewModel>();
                            //    item.TicketsEtapas.Add(new TicketsEtapaViewModel()
                            //    {
                            //        Localidad_id = etapa.Localidad_id,
                            //        Etapa_id = etapa.Id,
                            //        Cantidad = etapa.Cantidad,
                            //        Precio = etapa.Precio
                            //    });
                            //}
                        }
                        else
                        {
                            etapa.Id = CurrentEvent.Etapas.Min(e => e.Id) - 1;
                            etapa.TicketsEtapas = new List<TicketsEtapaViewModel>();
                            //etapa.TicketsEtapas.Add(new TicketsEtapaViewModel()
                            //{
                            //    Localidad_id = etapa.Localidad_id,
                            //    Etapa_id = etapa.Id,
                            //    Cantidad = etapa.Cantidad,
                            //    Precio = etapa.Precio
                            //});
                            CurrentEvent.Etapas.Add(etapa);
                        }
                        crear = false;
                    }
                }
                if (crear)
                {
                    etapa.Id = -1;
                    //etapa.TicketsEtapas = new List<TicketsEtapaViewModel>();
                    //etapa.TicketsEtapas.Add(new TicketsEtapaViewModel()
                    //{
                    //    Localidad_id = etapa.Localidad_id,
                    //    Etapa_id = etapa.Id,
                    //    Cantidad = etapa.Cantidad,
                    //    Precio = etapa.Precio
                    //});
                    CurrentEvent.Etapas = new List<EtapasViewModel>();
                    CurrentEvent.Etapas.Add(etapa);
                }
            }
            return PartialView("EtapasPartial", CurrentEvent.Etapas.OrderBy(e => e.Id));
        }
        [HttpPost]
        public ActionResult RemoveEtapa(int id)
        {
            //ServicioEtapas servicio = new ServicioEtapas();
            //servicio.Destroy(id);
            //return PartialView("EtapasPartial", servicio.Read(evento_id));
            var item = CurrentEvent.Etapas.Where(e => e.Id == id).FirstOrDefault();
            if (item != null)
            {
                CurrentEvent.Etapas.Remove(item);
            }
            return PartialView("EtapasPartial", CurrentEvent.Etapas.OrderBy(e => e.Id));
        }
        [HttpPost]
        public object controlCantidadTicketsEtapas()
        {
            if (CurrentEvent.Etapas != null)
            {
                int cantidadConsumida = CurrentEvent.Etapas.Sum(e => e.Cantidad);
                return new { state = cantidadConsumida.Equals(CurrentEvent.AforoTotal), cantidad = CurrentEvent.AforoTotal = CurrentEvent.AforoTotal - CurrentEvent.AforoTotal };
            }
            return new { state = true };
        }
        [HttpPost]
        public ActionResult Step6(string Next, string Previous)
        {
            if (CurrentEvent.Estado == EstadoEvento.localidades)
            {
                CurrentEvent.Estado = EstadoEvento.etapas;
                ViewBag.Estado = CurrentEvent.Estado;
            }
            if (Next != null)
            {
                if (CurrentEvent.Tickets == null)
                {
                    CurrentEvent.Tickets = new TicketsEventoViewModel()
                    {
                        Cantidad = CurrentEvent.AforoTotal
                    };
                }
                return View("Step7", CurrentEvent);
            }
            else if (Previous != null)
            {
                return View("Step5", CurrentEvent);
            }
            return View();
        }
        public JsonResult getLocalidadesOptions()
        {
            return Json(CurrentEvent.Venue.Localidades.Select(e => new ListItem { Value = e.Id, Text = e.Nombre }).ToList(), JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region TICKETS
        #region SERVICIOS X EVENTO
        [HttpPost]
        public ActionResult get_ServiciosxEvento()
        {
            //return PartialView("ServiciosPartial", );
            return PartialView("ServiciosPartial", CurrentEvent.TipoServicios);
        }
        [HttpPost]
        public ActionResult AddServicio(TipoServicioViewModel servicio)
        {
            if (CurrentEvent != null)
            {
                bool crear = true;
                servicio = new ServicioTipoServicio().ReadById(servicio.Id);
                if (CurrentEvent.TipoServicios != null)
                {
                    if (CurrentEvent.TipoServicios.Count() > 0)
                    {
                        crear = false;
                        if (CurrentEvent.TipoServicios.Where(e => e.Id == servicio.Id).Count() == 0)
                        {
                            CurrentEvent.TipoServicios.Add(servicio);
                        }
                    }
                }
                if (crear)
                {
                    CurrentEvent.TipoServicios = new List<TipoServicioViewModel>();
                    CurrentEvent.TipoServicios.Add(servicio);
                }
            }
            return PartialView("ServiciosPartial", CurrentEvent.TipoServicios);
        }
        [HttpPost]
        public ActionResult RemoveServicio(int evento_id, int id)
        {
            var item = CurrentEvent.TipoServicios.Where(e => e.Id == id).FirstOrDefault();
            if (item != null)
            {
                CurrentEvent.TipoServicios.Remove(item);
            }
            return PartialView("ServiciosPartial", CurrentEvent.TipoServicios);
        }

        #endregion

        [HttpPost]
        public ActionResult generateTicket()
        {
            var n = DateTime.Now.ToFileTime();
            var evento = CurrentEvent;
            return Json(new
            {
                Numero = "TICKET 99",
                Precio = 25000.ToString("c"),
                Barcode = getBarcode(1, n),
                NumericBarcode = n.ToString(),
                Responsable = "RESPONSABLE: Triada Colombia S.A.S NIT: 900136882-9".ToUpper(),
                Ubicacion = "Calle 32 # 35 -30",
                NombreEveno = "Evento de Prueba # 1"
            }, JsonRequestBehavior.AllowGet);
            //return Json(new {
            //    Numero = evento.Tickets.InicioNumeracion,
            //    Precio = evento.Tickets.Precio,
            //    Barcode = getBarcode(1, n),
            //    NumericBarcode = n,
            //    Responsable = evento.Empresario.RazonSocial,
            //    Nit = evento.Empresario.Identificacion,
            //    Ubicacion = evento.Venue.Direccion
            //}, JsonRequestBehavior.AllowGet);
        }

        public string getTicketImage()
        {
            return Session["ImgCanvas"] == null ? string.Empty : Session["ImgCanvas"].ToString();
        }

        public ActionResult SaveCanvas(IEnumerable<HttpPostedFileBase> files)
        {
            if (files != null)
            {
                foreach (var file in files)
                {
                    byte[] imgData = new Byte[file.InputStream.Length];
                    long bytesRead = file.InputStream.Read(imgData, 0, (int)file.InputStream.Length);
                    file.InputStream.Close();
                    Session["ImgCanvas"] = string.Format("data:image/png;base64,{0}", System.Convert.ToBase64String(imgData, 0, imgData.Length));
                }
            }
            return Content("");
        }

        public ActionResult RemoveCanvas(string[] fileNames)
        {
            if (fileNames != null)
            {
                Session["ImgCanvas"] = string.Empty;
            }
            return Content("");
        }

        public string getBarcode(int type, long n)
        {

            //string codigo = n.ToString();

            //Bitmap imagen = new Bitmap(220, 110);
            ////Bitmap imagen = new Bitmap(300 * pancho.Valorint.Value / 96, 300 * palto.Valorint.Value / 96);
            ////imagen.SetResolution(300, 300);

            ////imagen.SetResolution(300.0F, 300.0F);
            //Graphics g = Graphics.FromImage(imagen);
            //Zen.Barcode.BarcodeDraw bdf = Zen.Barcode.BarcodeDrawFactory.Code39WithChecksum;

            //Bitmap temp = new Bitmap(bdf.Draw(codigo, 30));
            ////temp.SetResolution(300.0F, 300.0F);
            ///*if (item.Direccion.HasValue && item.Direccion.Value == 'V')
            //{
            //    temp.RotateFlip(RotateFlipType.Rotate90FlipNone);
            //}*/
            //g.DrawImage(temp, 0, 0);

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    //barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
            //    temp.Save(ms, ImageFormat.Jpeg);
            //    temp.Save(@"C:\test\BarCode(4).jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
            //    byte[] imageBytes = ms.ToArray();
            //    return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(imageBytes));
            //}

            ////var bytes = stream.ToArray();
            ////context.Response.OutputStream.Write(bytes, 0, bytes.Length);

            Zen.Barcode.BarcodeSymbology s = Zen.Barcode.BarcodeSymbology.Code39C;
            BarcodeDraw drawObject = BarcodeDrawFactory.GetSymbology(s);

            var metrics = drawObject.GetDefaultMetrics(100);
            metrics.Scale = 1;
            var barcodeImage = drawObject.Draw(n.ToString(), metrics);

            Bitmap bm = ResizeImage(barcodeImage, 398, 60);

            using (MemoryStream ms = new MemoryStream())
            {
                //barcodeImage.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                bm.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                //bm.Save(@"C:\test\BarCode(3).jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                byte[] imageBytes = ms.ToArray();
                return string.Format("data:image/png;base64,{0}", Convert.ToBase64String(imageBytes));
            }
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(300, 300);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public ActionResult LoadTicketDesigner(TicketType type, int inicioNumeracion)
        {
            var evento = CurrentEvent;
            if (CurrentEvent.Tickets.TicketPreview == null)
            {
                var n = DateTime.Now.ToFileTime();
                CurrentEvent.Tickets.TicketPreview = new TicketViewModel()
                {
                    Barcode = getBarcode(1, n),
                    NumericBarcode = n.ToString()
                };
            }
            CurrentEvent.Tickets.TicketPreview.Numero = inicioNumeracion;
            CurrentEvent.Tickets.TicketPreview.Precio = evento.Tickets.Precio;
            //CurrentEvent.Tickets.TicketPreview.Responsable = removerAcentos(evento.Empresario.fullRazonSocial);
            CurrentEvent.Tickets.TicketPreview.Ubicacion = removerAcentos(evento.Venue.Direccion);
            CurrentEvent.Tickets.TicketPreview.ticketType = type;
            CurrentEvent.Tickets.TicketPreview.NombreEveno = removerAcentos(evento.Nombre);
            CurrentEvent.Tickets.TicketPreview.fechaEvento = removerAcentos(evento.FechaInicial.ToString("dddd dd MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("es-CO")));
            CurrentEvent.Tickets.TicketPreview.EdadMinima = string.Format("Edad Minima {0}", evento.EdadMinima);
            CurrentEvent.Tickets.TicketPreview.HoraApertura = String.Format("Apertarura de puertas: {0}", evento.FechaInicial.ToString("HH:mm"));
            CurrentEvent.Tickets.TicketPreview.Localidad = removerAcentos(string.Format("Localidad: {0}", evento.Venue.Localidades[0].Nombre));
            CurrentEvent.Tickets.TicketPreview.BackgorundImgae = type == TicketType.small ? _Ticket.Background.Small : _Ticket.Background.Large;
            if (!string.IsNullOrEmpty(evento.Tickets.ContentJson))
            {
                CurrentEvent.Tickets.TicketPreview.ContentJson = evento.Tickets.ContentJson;
            }
            return PartialView("TicketDesigner", CurrentEvent.Tickets.TicketPreview);
        }

        private string removerAcentos(string text)
        {
            return text.Replace("&Aacute;", "Á")
            .Replace("&aacute;", "á")
            .Replace("&Eacute;", "É")
            .Replace("&eacute;", "é")
            .Replace("&Iacute;", "Í")
            .Replace("&iacute;", "í")
            .Replace("&Oacute;", "Ó")
            .Replace("&oacute;", "ó")
            .Replace("&Uacute;", "Ú")
            .Replace("&uacute;", "ú")
            .Replace("&Uuml;", "Ü")
            .Replace("&uuml;", "ü")
            .Replace("&Ntilde;", "Ṅ")
            .Replace("&ntilde;", "ñ");
        }

        [HttpPost]
        public bool setJasonCanvas(string json)
        {
            if (CurrentEvent.Tickets != null)
            {
                CurrentEvent.Tickets.ContentJson = @json;
            }
            return true;
        }

        [HttpPost]
        public bool setSVGCanvas(string svg)
        {
            if (CurrentEvent.Tickets != null)
            {
                CurrentEvent.Tickets.ContentSVG = @svg;
            }
            return true;
        }

        [HttpPost]
        public ActionResult Step7([Bind(Include = "Tickets")] EventoViewModel evento, string Next, string Previous)
        {
            CurrentEvent.Tickets.Cantidad = evento.Tickets.Cantidad;
            CurrentEvent.Tickets.InicioNumeracion = evento.Tickets.InicioNumeracion;
            if (CurrentEvent.Estado == EstadoEvento.etapas)
            {
                CurrentEvent.Estado = EstadoEvento.tickets;
                ViewBag.Estado = CurrentEvent.Estado;
            }
            if (Next != null)
            {
                if (CurrentEvent.Tickets == null)
                {
                    CurrentEvent.Tickets = new TicketsEventoViewModel();
                }
                return View("Step8");
            }
            else if (Previous != null)
            {
                return View("Step6", CurrentEvent);
            }
            return View();
        }

        #endregion

        #region FINALIZACION
        #region Almacenar evento en base de datos
        public void SaveEvent()
        {
            ServicioEvento evento = new ServicioEvento();
            CurrentEvent.Id = evento.InformacionGenral_Save(CurrentEvent).Id;
            int eventoID = CurrentEvent.Id;

            new ServicioImpuesto().Save(CurrentEvent.Impuestos, eventoID);

            evento.DireccionMap_save(CurrentEvent);

            new ServicioFuncion().Save(CurrentEvent.Funciones, eventoID);

            evento.InformacionAdicional_Save(CurrentEvent);

            ServicioLocalidad localidad = new ServicioLocalidad();
            foreach (var item in CurrentEvent.Venue.Localidades)
            {
                //if (CurrentEvent.TipoRegisro == Estado.Editando)
                //{
                //    localidad.Destroy(item.Id, CurrentEvent.Venue_id);
                //    localidad.Destroy(item.Id);
                //}

                //item.IdVenue = CurrentEvent.Venue.Id;
                int idLocalidad = item.Id;
                //item.Id = localidad.SaveOptimizado(item).Id;

                foreach (var row in CurrentEvent.Etapas)
                {
                    var localidadID = row.TicketsEtapas.Where(e => e.IdLocalidad == idLocalidad);
                    if (localidadID.Any())
                    {
                        foreach (var fila in localidadID)
                        {
                            fila.IdLocalidad = item.Id;
                        }
                    }
                }
            }

            //Task.Factory.StartNew(() =>
            //{
            //    var x = new EventoViewModel();
            //    x = CurrentEvent;
            //    TicketsEventoViewModel pullTicketsByEvento = TicketsSave(evento.getCantidadEnDB(eventoID), eventoID, x);
            //    new ServicioEtapas().Save(x.Etapas, eventoID, pullTicketsByEvento.Id);
            //});

            //foreach (var item in CurrentEvent.Etapas)
            //{
            //    item.Evento_id = CurrentEvent.Id;
            //    item.Id = etapa.Save(item).Id;

            //    foreach (var row in item.TicketsEtapas)
            //    {
            //        ServicioTicketsEtapa pullTicketsEtapa = new ServicioTicketsEtapa();
            //        pullTicketsEtapa.Save(new TicketsEtapaViewModel()
            //        {
            //            Etapa_id = item.Id,
            //            Localidad_id = row.Localidad_id,
            //            pullTickets_id = pullTicketsByEvento.Id,
            //            Cantidad = row.Cantidad,
            //            Precio = row.Precio
            //        });
            //    }
            //}

            new ServicioTipoServicio().Save(CurrentEvent.TipoServicios, eventoID);

            if (Session[Comun.ImagenMemoria] != null)
            {
                SaveImage(Session[Comun.ImagenMemoria].ToString(),
                    Server.MapPath(string.Format("~/Images/Eventos/{0}.jpg", CurrentEvent.Id)));
                Session[Comun.ImagenMemoria] = null;
            }
        }

        public TicketsEventoViewModel TicketsSave(int aforoEnDB, int evento_id, EventoViewModel newEvent)
        {
            ServicioTickets tickets = null;
            ServicioTicketsEvento ticketsEvento = null;
            int cantidad = newEvent.Venue.Localidades.Sum(e => e.Aforo), inicioNumeracion = newEvent.Tickets.InicioNumeracion;

            TicketsEventoViewModel pullTicketsByEvento = new TicketsEventoViewModel();
            pullTicketsByEvento.Evento_id = newEvent.Id;
            pullTicketsByEvento.Cantidad = cantidad;
            pullTicketsByEvento.TicketType = newEvent.Tickets.TicketPreview.ticketType;
            pullTicketsByEvento.ContentJson = newEvent.Tickets.ContentJson;
            ticketsEvento = new ServicioTicketsEvento();
            //TODO: DEpurar en este punto porque los tickets no estan tomando el id del pull de tickets
            pullTicketsByEvento.Id = ticketsEvento.Save(pullTicketsByEvento).Id;

            if (newEvent.TipoRegisro == Estado.Nuevo)
            {
                pullTicketsByEvento.InicioNumeracion = inicioNumeracion;
                tickets = new ServicioTickets();
                for (int i = 0; i < cantidad; i++)
                {
                    tickets.Save(new TicketViewModel()
                    {
                        Numero = inicioNumeracion,
                        idPullTickets = pullTicketsByEvento.Id,
                        idEstado = EstadoTicket.DISPONIBLE
                    });
                    inicioNumeracion += 1;
                }
                pullTicketsByEvento.FinNumeracion = inicioNumeracion;
            }
            else
            {
                pullTicketsByEvento.InicioNumeracion = newEvent.Tickets.InicioNumeracion;
                int ultimoTicketID = 0, ultimoTicketNumero = newEvent.Tickets.FinNumeracion;
                if (cantidad > aforoEnDB)
                {
                    for (int i = 0; i < cantidad - aforoEnDB; i++)
                    {
                        ultimoTicketNumero += 1;
                        ultimoTicketID = tickets.Save(new TicketViewModel()
                        {
                            Numero = ultimoTicketNumero,
                            idPullTickets = pullTicketsByEvento.Id,
                            idEstado = EstadoTicket.DISPONIBLE
                        }).Id;
                    }
                    pullTicketsByEvento.FinNumeracion = ultimoTicketNumero + 1;
                }
                else if (cantidad < aforoEnDB)
                {
                    int i = 0;
                    var items = tickets.ReadByEvent(pullTicketsByEvento.Id).OrderByDescending(e => e.Id);
                    foreach (var item in items)
                    {
                        if (i >= aforoEnDB - cantidad)
                        {
                            break;
                        }
                        ultimoTicketNumero = item.Numero;
                        tickets.Destroy(item.Id);
                        i++;
                    }
                    pullTicketsByEvento.FinNumeracion = ultimoTicketNumero;
                }
            }
            ticketsEvento.Save(pullTicketsByEvento);
            return pullTicketsByEvento;
        }
        #endregion

        [HttpPost]
        public ActionResult Step8([Bind(Include = "Tickets")] EventoViewModel evento, string Next, string Previous)
        {
            if (CurrentEvent.Estado == EstadoEvento.tickets)
            {
                CurrentEvent.Estado = EstadoEvento.liquidacion;
                ViewBag.Estado = CurrentEvent.Estado;
            }
            if (Next != null)
            {
                try
                {
                    SaveEvent();
                    //Session.Remove("CurrentEvent");
                }
                catch (Exception ex)
                {
                    ViewBag.error = ex.Message;
                    return View();
                }
                return RedirectToAction("Eventos", "Evento");
            }
            else if (Previous != null)
            {
                return View("Step7", CurrentEvent);
            }
            return View();
        }
        #endregion

        public EventoViewModel CurrentEvent
        {
            get
            {
                var items = HttpContext.Session["CurrentEvent"] as EventoViewModel;
                if (items == null)
                {
                    items = new EventoViewModel();
                    HttpContext.Session["CurrentEvent"] = items;
                }
                return items;
            }
            set
            {
                HttpContext.Session["CurrentEvent"] = value;
            }
        }

        #endregion

        #region TipoNumeracion

        public ActionResult TipoNumeracion()
        {
            return View("TipoNumeracion");
        }

        public ActionResult TipoNumeracion_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioTipoNumeracion servicio = new ServicioTipoNumeracion();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoNumeracion_Create([DataSourceRequest] DataSourceRequest request, TipoNumeracionViewModel obj)
        {
            ServicioTipoNumeracion servicio = new ServicioTipoNumeracion();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoNumeracion_Update([DataSourceRequest] DataSourceRequest request, TipoNumeracionViewModel obj)
        {
            ServicioTipoNumeracion servicio = new ServicioTipoNumeracion();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoNumeracion_Destroy([DataSourceRequest] DataSourceRequest request, TipoNumeracionViewModel obj)
        {
            ServicioTipoNumeracion servicio = new ServicioTipoNumeracion();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion

        #region TipoLocalidad

        public ActionResult TipoLocalidad()
        {
            return View("TipoLocalidad");
        }

        public ActionResult TipoLocalidad_Read([DataSourceRequest] DataSourceRequest request)
        {
            ServicioTipoLocalidad servicio = new ServicioTipoLocalidad();
            return Json(servicio.Read().ToDataSourceResult(request));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoLocalidad_Create([DataSourceRequest] DataSourceRequest request, TipoLocalidadViewModel obj)
        {
            ServicioTipoLocalidad servicio = new ServicioTipoLocalidad();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Create(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoLocalidad_Update([DataSourceRequest] DataSourceRequest request, TipoLocalidadViewModel obj)
        {
            ServicioTipoLocalidad servicio = new ServicioTipoLocalidad();
            if (obj != null && ModelState.IsValid)
            {
                servicio.Update(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult TipoLocalidad_Destroy([DataSourceRequest] DataSourceRequest request, TipoLocalidadViewModel obj)
        {
            ServicioTipoLocalidad servicio = new ServicioTipoLocalidad();
            if (obj != null)
            {
                servicio.Destroy(obj);
            }

            return Json(new[] { obj }.ToDataSourceResult(request, ModelState));
        }

        #endregion
    }
}
