using IDAutomation.NetAssembly;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Verificador.Vista.Models;
using Zen.Barcode;
using Verificador.Vista.Models.Servicios;
using Verificador.Vista.Models.ViewModels;
using Verificador.Vista.Models;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;

namespace Verificador.Vista.Controllers
{
    public class EventController : Controller
    {
        //
        // GET: /Event/

        public ActionResult Index()     
        {
            return View();
        }

        [HttpGet]
        public ActionResult getLocalidadesByVenue(int venueId)
        {
            ServicioLocalidad servicio = new ServicioLocalidad();
            return Json(new { Localidades = servicio.ReadByVenue(venueId) , Mapa = ""}, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult getServices()
        {
            ServicioTipoServicio servicio = new ServicioTipoServicio();
            return Json(servicio.Read(), JsonRequestBehavior.AllowGet);
        }



        #region Guardado del evento
        [HttpPost]
        public ActionResult EventSaveLite(EventoViewModel obj)
        {
            try
            {
                ServicioEvento servicio = new ServicioEvento();
                servicio.SaveLite(obj);
                return Json(new { status = 200, message = "Evento Guardado Satisfactoriamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = 200, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult EventSave()
        {
            try
            {
                //ESto es una puta prueba que no me quiere funcionar sabra el putas poir que
                HttpRequest request = HttpContext.ApplicationInstance.Context.Request;

                int empresarioId = JsonConvert.DeserializeObject<int>(request["Empresario"]);
                EventoViewModel evento = JsonConvert.DeserializeObject<EventoViewModel>(request["EventData"]);
                VenueViewModel venue = JsonConvert.DeserializeObject<VenueViewModel>(request["VenueData"]);
                List<LocalidadViewModel> stages = JsonConvert.DeserializeObject<List<LocalidadViewModel>>(request["StagesData"]);
                List<FuncionesEventoViewModel> functions = JsonConvert.DeserializeObject<List<FuncionesEventoViewModel>>(request["FunctionsData"]);
                List<EtapasViewModel> steps = JsonConvert.DeserializeObject<List<EtapasViewModel>>(request["StepsData"]);
                string mapa = request["__Mapa"].ToString();
                string ticket = request["__TicketData"].ToString();
                string htmlDescription = request["__EventDescription"].ToString();

                evento.Empresario_id = empresarioId;
                evento.Venue = venue;
                evento.Localidades = stages;
                evento.Funciones = functions;
                evento.Etapas = steps;

                ServicioEvento eventoStorage = new ServicioEvento();
                eventoStorage.SaveComplete(evento, ticket, htmlDescription, mapa);



                return Json(new { status = HttpStatusCode.OK, message = "Evento almacenado correctamente" }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { status = HttpStatusCode.InternalServerError, message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion

        #region EditarEvento
        [HttpGet]
        public ActionResult getEventById(int id) {
            ServicioLocalidad LocalidadServicio = new ServicioLocalidad();
            var evento = new ServicioEvento().Read(id);
            evento.Venue = new ServicioVenue().Read(id);       
            evento.Localidades = LocalidadServicio.ReadByVenue(evento.Venue_id, id).ToList();
            evento.Etapas = new ServicioEtapas().Read(id);
            var Ticket = new ServicioTicketsEvento().ReadByEvent(id);
            string mapa = LocalidadServicio.getMapByevent(id);
            evento.TipoRegisro = Estado.Editando;
            evento.Estado = EstadoEvento.finalizado;
            return Json(new { evento = evento, ticketData = Ticket.ContentSVG, Mapa = mapa }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region TicketDesigner
        public ActionResult TicketDesigner(TicketType type)
        {
            TicketViewModel ticket = new TicketViewModel();

            var n = DateTime.Now.ToFileTime();
            FontEncoder encoder = new FontEncoder();
            string text = encoder.Code39(n.ToString());

            return View(new TicketViewModel()
            {
                Numero = 99,
                Precio = 25000,
                Barcode = getBarcode(1, n),
                NumericBarcode = n.ToString(),
                //NumericBarcodeText = encoder.Code39(n.ToString()),
                ticketType = TicketType.grande,
                fechaEvento = removerAcentos(DateTime.Now.ToString("dddd dd MMMM", System.Globalization.CultureInfo.CreateSpecificCulture("es-CO"))),
                //EdadMinima = string.Format("Edad Minima {0}", 18),
                HoraApertura = String.Format("Apertarura de puertas: {0}", DateTime.Now.ToString("HH:mm")),
                //Localidad = removerAcentos(string.Format("Localidad: {0}", "Aforo")),
                BackgorundImgae = type == TicketType.small ? _Ticket.Background.Small : _Ticket.Background.Large,
                Responsable = "RESPONSABLE: Triada Colombia S.A.S NIT: 900136882-9".ToUpper(),
                //Ubicacion = "Calle 32 # 35 -30",
                //NombreEveno = "Evento de Prueba # 1"
            });
        }


        public string getBarcode(int type, long n)
        {
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
        #endregion

        #region Stages Creator
        public ActionResult StagesCreator()
        {
            return View();
        }
        #endregion

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

        #region Encuentas
        [Autorizacion(Permiso = "EncuestasEventosxEmpresario")]
        public ActionResult EncuestasEventosxEmpresario()
        {
            return View("EncuestasEventosxEmpresario");
        }

        public ActionResult EncuestasEventos_Read([DataSourceRequest] DataSourceRequest request)
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

        [HttpPost]
        public ActionResult Preguntas_Create([DataSourceRequest] DataSourceRequest request,  List<PreguntaViewModel> obj)
        {
            //TempData sirve para pasar variables entre metodos o controladores semenja a una variable de session
            var idEvento =TempData["IdEvento"].ToString();
            ServicioPreguntasEvento servicio = new ServicioPreguntasEvento();
            var mensaje = servicio.Create(obj, int.Parse(idEvento));
            return Json(new
            {message= mensaje }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult AddSessionIdEvento(int idEvento)
        {
            ServicioPreguntasEvento servicio = new ServicioPreguntasEvento();
            var Preguntas = servicio.ReadbyiD(idEvento);
            TempData["IdEvento"]= idEvento;

            return Json(new
            { preguntas = Preguntas }, JsonRequestBehavior.AllowGet);
        }

        #endregion

    }
}
