
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
    public class ServicioEtapas : IDisposable
    {
        private Entities entities;

        public ServicioEtapas()
        {
            this.entities = new Entities();
        }

        public List<EtapasViewModel> Read(int idevento)
        {
            return (from o in entities.EtapasVentaEventos
                    where o.Idevento == idevento
                    select new EtapasViewModel
                    {
                        Id = o.Id,
                        Evento_id = (int)o.Idevento,
                        Nombre = o.Nombre,
                        Descripcion = o.Descripcion,
                        //Precio = (decimal)o.Precio,
                        //Cantidad = (int)o.Cantidad,
                        FechaFinal = (DateTime)o.FechaFinal,
                        FechaInicial = (DateTime)o.FechaInicial,
                        TicketsEtapas = entities.TicketsEtapas.Where(e => e.IdEtapa == o.Id).Select(e => new TicketsEtapaViewModel()
                        {
                            Id = e.Id,
                            Etapa_id = e.IdEtapa,
                            IdLocalidad = (int)e.IdLocalidad,
                            //pullTickets_id = e.IdTicketsEvento,
                            Aforo = (int)e.Aforo,
                            Precio = (decimal)e.Precio,
                        }).ToList()
                    }).ToList();
        }

        public void Destroy(int id)
        {
            var entity = entities.EtapasVentaEventos.FirstOrDefault(c => c.Id == id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public EtapasViewModel Guardar(EtapasViewModel obj)
        {
            EtapasVentaEvento table = new EtapasVentaEvento();
            if (obj.Id > 0)
            {
                table = entities.EtapasVentaEventos.Where(e => e.Id == obj.Id).FirstOrDefault();
            }
            table.Idevento = obj.Evento_id;
            table.Nombre = obj.Nombre;
            table.Descripcion = obj.Descripcion;
            table.FechaFinal = obj.FechaFinal;
            table.FechaInicial = obj.FechaInicial;
            table.LlevaCodigo = obj.LlevaCodigo;
            table.Codigo = obj.Codigo;
            if (obj.Id <= 0)
            {
                entities.Add(table);
            }
            entities.SaveChanges();
            obj.Id = table.Id;

            return obj;
        }


        public void Save(List<EtapasViewModel> etapas, int evento_id, Dictionary<int, int> stagesIds)
        {
            ServicioTicketsEtapa ticketsEtapaStorage = new ServicioTicketsEtapa();
            foreach (var item in etapas)
            {
                item.Evento_id = evento_id;
                Guardar(item);
                ticketsEtapaStorage.SaveWithEtapa(item.TicketsEtapas, item.Id, stagesIds);
            }
            var actuales = etapas.Where(c => c.Id > 0).ToList();
            foreach (var item in entities.EtapasVentaEventos.Where(c => c.Idevento == evento_id).ToList())
            {
                var updates = actuales.FirstOrDefault(c => c.Id == item.Id);
                if (updates == null)
                {
                    entities.Delete(item.TicketsEtapas);
                    entities.Delete(item);
                }
            }
            entities.SaveChanges();
        }

        public static List<ListItem> getOptions()
        {
            return new Entities().EtapasVentaEventos.Select(c => new ListItem
            {
                Value = c.Id,
                Text = c.Nombre
            }).ToList();
        }

        public static List<ListItem> getOptions(int idEvento)
        {
            return new Entities().EtapasVentaEventos.Where(e => e.Idevento == idEvento && DateTime.Now >= e.FechaInicial && DateTime.Now <= e.FechaFinal).Select(c => new ListItem
            {
                Value = c.Id,
                Text = c.Nombre
            }).ToList();
        }

        public void Dispose()
        {
            entities.Dispose();
        }


        internal List<EtapasViewModel> getStagesByEventWithLocations(int? eventID, bool getOnlyStagesActive)
        {
            return (from e in entities.EtapasVentaEventos
                    where e.Idevento == eventID && ((getOnlyStagesActive && e.FechaFinal >= DateTime.Now)
                     || !getOnlyStagesActive)
                    select new EtapasViewModel()
                    {
                        Id = e.Id,
                        Evento_id = (int)e.Idevento,
                        Nombre = e.Nombre,
                        //Localidades = entities.TicketsEtapas.Where(x => x.IdEtapa == e.Id).Select(x => new LocalidadViewModel()
                        //{
                        //    Id = x.IdLocalidad,
                        //    Nombre = x.Localidad.Nombre,
                        //    IdTicketEtapa = x.Id,
                        //    TicketNumeroInicio = x.Tickets.Count(k => k.IdEstado == EstadoTicket.IMPRESO),
                        //    TicketNumeroFinal = x.Tickets.Count(),
                        //}).ToList()
                    }).ToList();
        }
    }
}