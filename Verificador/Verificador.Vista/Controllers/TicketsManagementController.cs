using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Verificador.Vista.Models.Servicios;
using Verificador.Vista.Models.ViewModels;
using Verificador.Vista.Models;

namespace Verificador.Vista.Controllers
{
    public class TicketsManagementController : Controller
    {
        //
        // GET: /TicketsManagement/

        public ActionResult Index(int? eventID)
        {
            if (eventID != null)
            {
                ViewBag.Menu = new ServicioEtapas().getStagesByEventWithLocations(eventID, false);
            }
            return View();
        }

        [HttpPost]
        public ActionResult getTicketsByLocation(int TicketsEtapaId, int LocalidadId, string message)
        {
            try
            {
                List<TicketViewModel> tickets = new ServicioLocalidad().getTickets(TicketsEtapaId, LocalidadId);
                if (tickets == null)
                {
                    throw new Exception("Esta localidad no tiene tiquets asociados");
                }
                return Json(new { msg = message, modelData = tickets, status = true });
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message, status = false });
            }
        }

        [HttpPost]
        public ActionResult GenerateTransaction(int TicketsEtapaId, int LocalidadId, int stateId)
        {
            try
            {
                List<TicketViewModel> Tickets = Newtonsoft.Json.JsonConvert.DeserializeObject<List<TicketViewModel>>(HttpContext.Request["Tickets"]);
                string token = Utilidades.GetClientMac(Request);
                new ServicioTickets().CreateTransaction(Tickets, token, stateId);
                string message = "";
                if (stateId == EstadoTicket.ENCOLA)
                {
                    message = "Los tickets se encuentran en cola para ser impresos";
                }
                else
                {
                    message = "Los Tickets fueron eliinados";
                }

                return getTicketsByLocation(TicketsEtapaId, LocalidadId, message);
            }
            catch (Exception ex)
            {
                return Json(new { msg = ex.Message, status = false });
            }
        }

    }
}
