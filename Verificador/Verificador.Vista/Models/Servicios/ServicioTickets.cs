
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using Verificador.OpenAccess;
using Verificador.Vista.Models.ViewModels;

namespace Verificador.Vista.Models.Servicios
{
    public class ServicioTickets : IDisposable
    {
        private Entities entities;

        public ServicioTickets()
        {
            this.entities = new Entities();
        }

        public List<TicketViewModel> Read(int idEvento, int inicio, int final)
        {
            return entities.Tickets.Where(e => e.TicketsEvento.IdEvento == idEvento
                && e.Numero >= inicio && e.Numero <= final
            ).Select(e => new TicketViewModel()
            {
                Id = e.Id,
                Numero = (int)e.Numero,
                Estado = e.EstadoImpresion.Nombre
            }).ToList();
        }

        public List<TicketViewModel> ReadByEvent(int id)
        {
            return entities.Tickets.Where(e => e.IdTicketsEvento == id).Select(e => new TicketViewModel()
            {
                Id = e.Id,
                NumericBarcode = e.NumericBarcode,
                Numero = (int)e.Numero
            }).ToList();
        }

        public List<TicketViewModel> ReadByPullTicketsMin(int id, int cantidad)
        {
            return entities.Tickets.Where(e => e.IdTicketsEvento == id && !e.IdEstado.HasValue).Select(e => new TicketViewModel()
            {
                Id = e.Id,
                NumericBarcode = e.NumericBarcode,
                Numero = (int)e.Numero,
                Barcode = e.Barcode,
                idEstado = e.IdEstado
            }).OrderBy(e => e.Id).Take(cantidad).ToList();
        }

        public void Destroy(int id)
        {
            var entity = entities.Tickets.FirstOrDefault(c => c.Id == id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public void Destroy(int inicio, int final)
        {
            var items = entities.Tickets.Where(c => c.Numero >= inicio && c.Numero <= final);
            if (items != null)
            {
                entities.Delete(items);
                entities.SaveChanges();
            }
        }

        public TicketViewModel Save(TicketViewModel obj)
        {
            Ticket table = new Ticket();
            table.Numero = obj.Numero;
            table.IdTicketsEvento = obj.idPullTickets;
            table.IdEstado = obj.idEstado;
            entities.Add(table);
            entities.SaveChanges();
            obj.Id = table.Id;
            return obj;
        }

        public void Dispose()
        {
            entities.Dispose();
        }


        internal string getCanvasByEvent(int evento_id)
        {
            return entities.TicketsEventos.FirstOrDefault(e => e.IdEvento == evento_id).JsonCanvas;
        }

        internal void CreateTransaction(List<TicketViewModel> _tickets, string token, int estadoTicket)
        {
            int[] ticketsIds = _tickets.Select(e => e.Id).ToArray();
            var tickets = entities.Tickets.Where(e => ticketsIds.Contains(e.Id));
            foreach (Ticket ticket in tickets)
            {
                ticket.IdEstado = estadoTicket;
                ticket.Numero = ticket.Numero;
                TransaccionTicketsImpresion transaction = entities.TransaccionTicketsImpresions.FirstOrDefault(e => e.IdTicket == ticket.Id && e.IdEstado == estadoTicket);
                if (transaction == null)
                {
                    transaction = new TransaccionTicketsImpresion();
                    transaction.IdTicket = ticket.Id;
                    transaction.IdEstado = estadoTicket;
                }
                transaction.Token = token;
                entities.Add(transaction);
            }
            entities.SaveChanges();
        }
    }

    public class ServicioTicketsEvento : IDisposable
    {
        private Entities entities;

        public ServicioTicketsEvento()
        {
            this.entities = new Entities();
        }

        public TicketsEventoViewModel ReadByEvent(int Evento_id)
        {
            return entities.TicketsEventos.Where(e => e.IdEvento == Evento_id).Select(e => new TicketsEventoViewModel()
            {
                Id = e.Id,
                Evento_id = (int)e.IdEvento,
                Cantidad = (int)e.Cantidad,
                InicioNumeracion = (int)e.InicioNumeracion,
                FinNumeracion = (int)e.FinNumeracion,
                ContentSVG = e.JsonCanvas,
                TicketType = e.TicketType == "small" ? TicketType.small : TicketType.grande
            }).FirstOrDefault();
        }

        public TicketsEventoViewModel Save(TicketsEventoViewModel obj)
        {
            TicketsEvento table = new TicketsEvento();
            if (obj.Id > 0)
            {
                table = entities.TicketsEventos.FirstOrDefault(e => e.Id == obj.Id);
            }
            table.IdEvento = obj.Evento_id;
            table.Cantidad = obj.Cantidad;
            table.InicioNumeracion = obj.InicioNumeracion;
            table.FinNumeracion = obj.FinNumeracion;
            table.JsonCanvas = obj.ContentJson;
            table.TicketType = obj.TicketType.ToString();
            if (obj.Id == 0)
            {
                entities.Add(table);
            }
            entities.SaveChanges();
            obj.Id = table.Id;
            return obj;
        }

        public void SaveTicketsToPtint(List<TicketToPrintViewModel> ticketsToPrit)
        {
            if (ticketsToPrit.Any())
            {
                foreach (TicketToPrintViewModel item in ticketsToPrit)
                {
                    entities.Add(new TransaccionTicketsImpresion()
                    {
                        IdTicket = item.Id,
                        Token = "dkodsj"
                    });
                    var ticket = entities.Tickets.Where(e => e.Id == item.Id).FirstOrDefault();
                    if (ticket != null)
                    {
                        ticket.IdEstado = EstadoTicket.IMPRESO;
                    }
                }
                entities.SaveChanges();
            }
        }

        public void SaveTicketToPtint(PullTicketsImpresionViewModel ticketToPrit)
        {
            var ticket = entities.TransaccionPullTicketsImpresions.Where(e => e.Id == ticketToPrit.Id).FirstOrDefault();

            if (ticket != null)
            {
                //ticket.NumeroInicial = entities.TicketsEventos.FirstOrDefault(e => e.Id == ticketToPrit.IdPullTickets).Tickets.Where(r => r.IdEstado == EstadoTicket.DISPONIBLE).Min(r => (int)r.Numero); ;
                ticket.ImagenTicket = ticketToPrit.ImgTicket;//.Replace("data:image/svg+xml;base64,", "");
                ticket.Fecha = DateTime.Now;
                //ticket.IdEstado = EstadoTicket.DISPONIBLE;
            }
            entities.SaveChanges();
        }

        public void SaveTicketToPtint(PullTicketsImpresionViewModel ticketToPrit, int idTicketsEtapa, string token, int inicio, int final)
        {
            var data = new TransaccionPullTicketsImpresion()
            {
                //IdTicketsEvento = ticketToPrit.IdPullTickets,
                ImagenTicket = ticketToPrit.ImgTicket,
                Fecha = DateTime.Now,
                //Cantidad = ticketToPrit.Cantidad,
                Token = token,
                //IdTicketsEtapa = idTicketsEtapa,
                //PrinterName = ticketToPrit.PrinterName,
                //NumeroInicial = inicio,
                //NumeroFinal = final,
                //IdEstado = EstadoTicket.DISPONIBLE
            };
            entities.Add(data);
            entities.SaveChanges();
            ticketToPrit.Id = data.Id;
        }

        public void Dispose()
        {
            entities.Dispose();
        }

        internal PullTicketsImpresionViewModel CreatePullTicketsToPrint(PullTicketsImpresionViewModel p, int idTicketsEtapa, string token)
        {

            //Guardar eñ id de  los tickets Etaa
            var data = new TransaccionPullTicketsImpresion()
            {
                //IdTicketsEvento = p.IdPullTickets,
                //ImagenTicket = p.ImgTicket,
                Fecha = DateTime.Now,
                //Cantidad = p.Cantidad,
                Token = token,//"7530A39A-169A-4B83-B476-9AF84A7CECA2",//MAC address,
                //IdTicketsEtapa = idTicketsEtapa,
                //PrinterName = p.PrinterName
            };
            entities.Add(data);
            entities.SaveChanges();
            p.Id = data.Id;
            return p;
        }
    }

    public class ServicioTicketsEtapa : IDisposable
    {
        private Entities entities;

        public ServicioTicketsEtapa()
        {
            this.entities = new Entities();
        }

        public TicketsEtapaViewModel getByData(int idTicketsEtapa)
        {
            return entities.TicketsEtapas.Where(e => e.Id == idTicketsEtapa).Select(e => new TicketsEtapaViewModel()
            {
                Id = e.Id,
                Aforo = (int)e.Aforo,
                Precio = (decimal)e.Precio,
                Localidad = new LocalidadViewModel()
                {
                    Id = e.IdLocalidad,
                    Nombre = e.Localidad.Nombre
                },
                Etapa = new EtapasViewModel()
                {
                    Id = e.IdEtapa,
                    Nombre = e.EtapasVentaEvento.Nombre
                }
            }).FirstOrDefault();
        }

        public TicketsEtapaViewModel Save(TicketsEtapaViewModel obj)
        {
            TicketsEtapa table = new TicketsEtapa();
            if (obj.Id > 0)
            {
                table = entities.TicketsEtapas.FirstOrDefault(e => e.Id == obj.Id);
            }
            table.IdLocalidad = obj.IdLocalidad;
            table.IdEtapa = obj.Etapa_id;
            table.IdTipoServicio = obj.IdTipoServicio;
            table.Aforo = obj.Aforo;
            table.Precio = obj.Precio;
            if (obj.Id <= 0)
            {
                entities.Add(table);
            }
            entities.SaveChanges();
            obj.Id = table.Id;
            return obj;
        }

        public void Dispose()
        {
            entities.Dispose();
        }

        internal void SaveWithEtapa(List<TicketsEtapaViewModel> etapas, int etapa_id, Dictionary<int, int> stagesIds)
        {
            foreach (var item in etapas)
            {
                item.Etapa_id = etapa_id;
                if (item.IdLocalidad <= 0)
                {
                    item.IdLocalidad = stagesIds[item.IdLocalidad];
                }
                Save(item);
            }
            var actuales = etapas.Where(c => c.Id > 0).ToList();
            foreach (var item in entities.TicketsEtapas.Where(c => c.IdEtapa == etapa_id).ToList())
            {
                var updates = actuales.FirstOrDefault(c => c.Id == item.Id);
                if (updates == null)
                {
                    entities.Delete(item);
                }
            }
            entities.SaveChanges();
        }
    }
}