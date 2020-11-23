
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
    public class ServicioEvento : IDisposable
    {
        private Entities entities;

        public ServicioEvento()
        {
            this.entities = new Entities();
        }

        public int getCantidadEnDB(int id)
        {
            return entities.Eventos.Where(e => e.Id == id).Select(c => (int)c.AforoTotal).FirstOrDefault();
        }

        public List<EventoViewModel> Read()
        {
            return entities.Eventos.Select(c => new EventoViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                FechaInicial = c.FechaInicial == null ? DateTime.MinValue : (DateTime)c.FechaInicial,
                FechaFinal = (DateTime)c.FechaFinal,
                AforoTotal = (int)c.AforoTotal,
                Descripcion = c.Descripcion,
                Venue = entities.LocalidadesVenueEventos.Where(e => e.IdEvento == c.Id).Select(e => new VenueViewModel()
                {
                    Direccion = e.Venue.Direccion,
                }).FirstOrDefault()
            }).OrderByDescending(e=>e.FechaCreacion).ToList();
        }

        public EventoViewModel ReadById(int id)
        {
            return entities.Eventos.Where(e=>e.Id == id).Select(c => new EventoViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                FechaInicial = c.FechaInicial == null ? DateTime.MinValue : (DateTime)c.FechaInicial,
                FechaFinal = (DateTime)c.FechaFinal,
                AforoTotal = (int)c.AforoTotal,
                Empresario_id = (int)c.Empresario_id,
                EdadMinima = (int)c.EdadMinima,
                Funciones = c.FuncionesEventos.Select(e => new FuncionesEventoViewModel
                {
                    Id = e.Id,
                    AperturaPuertas = (DateTime)e.AperturaPuertas
                }).OrderBy(e => e.Id).ToList(),
                //Venue = entities.Venues.Where(e => e.Id == c.Venue_id).Select(e => new VenueViewModel()
                //{
                //    Id = e.Id,
                //    Nombre = e.Nombre,
                //    Direccion = e.Direccion,
                //    Ciudad_id = e.Ciudad_id == null ? 0 : (int)e.Ciudad_id,
                //    Telefono = e.Telefono,
                //    E_mail = e.Email,
                //    Longitud = e.Longitud,
                //    Latitud = e.Latitud
                //}).FirstOrDefault()
            }).FirstOrDefault();
        }

        internal void SaveComplete(EventoViewModel obj, string ticketSVG, string htmlDescription, string mapa) {
            obj.Descripcion = htmlDescription;
            int eventoID = Save(obj);

            ServicioImpuesto impuestoStorage = new ServicioImpuesto();
            impuestoStorage.Save(obj.Impuestos, eventoID);

            ServicioCategoria categoryStorage = new ServicioCategoria();
            categoryStorage.SaveWithEvent(obj.Categoria_id, eventoID);

            ServicioVenue venueStorage = new ServicioVenue();
            obj.Venue_id = venueStorage.Save(obj.Venue);

            ServicioLocalidad localidadStorage = new ServicioLocalidad();
            Dictionary<int, int> stagesIds = localidadStorage.SaveWithEvent(obj.Localidades, eventoID, obj.Venue_id);
            localidadStorage.SaveMapaByEvent(mapa, obj.Id);

            ServicioFuncion functionStorage = new ServicioFuncion();
            functionStorage.Save(obj.Funciones, eventoID);

            ServicioEtapas etapaStorage = new ServicioEtapas();
            etapaStorage.Save(obj.Etapas, eventoID, stagesIds);

            ServicioTicketsEvento ticketStorage = new ServicioTicketsEvento();
            ticketStorage.Save(new TicketsEventoViewModel{ 
                Evento_id = eventoID,
                Cantidad = obj.AforoTotal,
                ContentJson = ticketSVG,
                TicketType = TicketType.grande,
            });
        }

        internal int Save(EventoViewModel obj)
        {
            var entity = new Evento();
            if (obj.Id > 0)
            {
                entity = entities.Eventos.Where(e => e.Id == obj.Id).FirstOrDefault();
            }
            entity.Nombre = obj.Nombre;
            entity.Multifuncion = obj.Funciones.Count > 0;
            entity.CantidadFunciones = obj.Funciones.Count;
            entity.FechaInicial = obj.FechaInicial;
            entity.FechaFinal = obj.FechaFinal;
            entity.Empresario_id = obj.Empresario_id;
            entity.Foto = obj.Foto;
            entity.Descripcion = obj.Descripcion;
            entity.EdadMinima = obj.EdadMinima;
            entity.EventoPublico = obj.EventoPublico == "Sí";
            entity.TipoRecaudo_id = obj.TipoRecaudo_id;
            entity.EntradasxPedidoOcompra = obj.EntradasxPedidoOcompra;
            entity.EntradasxUsuario = obj.EntradasxUsuario;
            entity.AplicaServicio = obj.AplicaServicio == "Sí";
            entity.CantidadDiasValides = obj.CantidadFunciones;
            entity.AforoTotal = obj.AforoTotal;
            entity.Estado = obj.Estado.ToString();
            entity.UrlVideo = obj.UrlVideo;
            entity.StagesByMap = obj.StagesByMap;

            if (obj.Id <= 0)
            {
                entity.FechaCreacion = DateTime.Now;
                entities.Add(entity);
            }
            entities.SaveChanges();
            obj.Id = entity.Id;
            return obj.Id;
        }

        public EventoViewModel Read(int id)
        {
            return entities.Eventos.Where(e => e.Id == id).Select(e => new EventoViewModel
            {
                Id = e.Id,
                Nombre = e.Nombre,
                Empresario_id = e.Empresario_id == null ? 0 : (int)e.Empresario_id,
                TipoEvento_id = e.TipoEvento == null ? 0 : (int)e.TipoEvento,
                Descripcion = e.Descripcion,
                UrlVideo = e.UrlVideo,
                EdadMinima = e.EdadMinima == null ? 0 : (int)e.EdadMinima,
                EventoPublico = e.EventoPublico == true ? "Si" : "No",
                TipoRecaudo_id = e.TipoRecaudo_id == null ? 0 : (int)e.TipoRecaudo_id,
                EntradasxPedidoOcompra = e.EntradasxPedidoOcompra == null ? 0 : (int)e.EntradasxPedidoOcompra,
                EntradasxUsuario = e.EntradasxUsuario == null ? 0 : (int)e.EntradasxUsuario,
                AplicaServicio = e.AplicaServicio == true ? "Si" : "No",
                CantidadDiasValides = e.CantidadDiasValides == null ? 0 : (int)e.CantidadDiasValides,
                FechaCreacion = e.FechaCreacion == null ? DateTime.Now : (DateTime)e.FechaCreacion,
                AforoTotal = e.AforoTotal == null ? 0 : (int)e.AforoTotal,
                Categoria = (from ce in entities.CategoriasEventos
                             join c in entities.Categorias on ce.IdCategoria equals c.Id
                             where ce.IdEvento == e.Id
                             select new CategoriaViewModel
                             {
                                 Id = c.Id,
                                 Nombre = c.Nombre,
                                 VerCodigoArtesEscenicas = (bool)c.VerCodigoArtesEscenicas
                             }).FirstOrDefault(),
                //Venue = entities.Venues.Where(c => c.Id == e.Venue_id).Select(c => new VenueViewModel()
                //{
                //    Id = c.Id,
                //    Nombre = c.Nombre,
                //    Direccion = c.Direccion,
                //    Ciudad_id = c.Ciudad_id == null ? 0 : (int)c.Ciudad_id,
                //    Telefono = c.Telefono,
                //    E_mail = c.Email,
                //    FechaCreacion = (DateTime)c.FechaCreacion,
                //    Longitud = c.Longitud,
                //    Latitud = c.Latitud
                //}).FirstOrDefault(),
                TipoServicios = (from se in entities.ServiciosEventos
                                 join s in entities.TipoServicios on se.Idservicio equals s.Id
                                 where se.Idevento == e.Id
                                 select new TipoServicioViewModel()
                                 {
                                     Id = s.Id,
                                     Nombre = s.Nombre,
                                     Descripcion = s.Descripcion
                                 }).ToList(),
                Funciones = e.FuncionesEventos.Select(o => new FuncionesEventoViewModel()
                {
                    Id = o.Id,
                    FechaInicial = (DateTime)o.FechaIncial,
                    FechaFinal = (DateTime)o.FechaFinal,
                    AperturaPuertas = (DateTime)o.AperturaPuertas,
                    Descripcion = o.Descripcion,
                    Label = o.Label
                }).ToList(),
                Impuestos = (from ie in entities.ImpuestosEventos
                             join i in entities.Impuestos on ie.Idimpuesto equals i.Id
                             where ie.Idevento == e.Id
                             select new ImpuestoViewModel()
                             {
                                 Id = i.Id,
                                 Nombre = i.Nombre,
                                 Valor = i.Valor,
                             }).ToList(),
                Tickets = new ServicioTicketsEvento().ReadByEvent(e.Id),
                StagesByMap = e.StagesByMap     
            }).FirstOrDefault();
        }

        public EventoViewModel SaveLite(EventoViewModel obj)
        {
            var entity = new Evento();
            if (obj.Id > 0)
            {
                entity = entities.Eventos.Where(e => e.Id == obj.Id).FirstOrDefault();
            }
            entity.Nombre = obj.Nombre;
            entity.AforoTotal = obj.AforoTotal;
            entity.Descripcion = obj.Descripcion;
            entity.FechaInicial = obj.FechaInicial;
            entity.FechaFinal = obj.FechaFinal;
            if (obj.Id <= 0)
            {
                entity.FechaCreacion = DateTime.Now;
                entities.Add(entity);
            }
            entities.SaveChanges();
            obj.Id = entity.Id;

            return obj;
        }

        public EventoViewModel InformacionGenral_Save(EventoViewModel obj)
        {
            var entity = new Evento();
            if (obj.Id > 0)
            {
                entity = entities.Eventos.Where(e => e.Id == obj.Id).FirstOrDefault();
            }
            entity.Nombre = obj.Nombre;
            entity.Empresario_id = obj.Empresario_id;
            entity.TipoEvento = obj.TipoEvento_id;
            entity.EventoPublico = obj.EventoPublico == "Si" ? true : false;
            entity.EdadMinima = obj.EdadMinima;
            entity.AforoTotal = obj.AforoTotal;
            entity.Descripcion = obj.Descripcion;
            entity.UrlVideo = obj.UrlVideo;
            if (obj.Id <= 0)
            {
                entity.FechaCreacion = DateTime.Now;
                entities.Add(entity);
            }
            entities.SaveChanges();
            obj.Id = entity.Id;
            if (obj.Categoria_id != 0)
            {
                var data = entities.CategoriasEventos.Where(e => e.IdEvento == obj.Id);
                if (data.Any())
                {
                    data.FirstOrDefault().IdCategoria = obj.Categoria_id;
                }
                else
                {
                    entities.Add(new CategoriasEvento()
                    {
                        IdCategoria = obj.Categoria_id,
                        IdEvento = obj.Id
                    });
                }
                entities.SaveChanges();
            }
            return obj;
        }

        public EventoViewModel InformacionAdicional_Save(EventoViewModel obj)
        {
            Evento entity = entities.Eventos.Where(e => e.Id == obj.Id).FirstOrDefault();
            entity.TipoRecaudo_id = obj.TipoRecaudo_id;
            entity.CantidadDiasValides = obj.CantidadDiasValides;
            entity.AplicaServicio = obj.AplicaServicio == "Si" ? true : false;
            entity.EntradasxUsuario = obj.EntradasxUsuario;
            entity.EntradasxPedidoOcompra = obj.EntradasxPedidoOcompra;
            entity.Multifuncion = obj.Multifuncion;
            entity.CantidadFunciones = obj.CantidadFunciones;
            if (obj.FechaInicial != null)
            {
                entity.FechaInicial = obj.FechaInicial;
            }
            if (obj.FechaFinal != null)
            {
                entity.FechaFinal = obj.FechaFinal;
            }
            entities.SaveChanges();
            return obj;
        }

        public EventoViewModel DireccionMap_save(EventoViewModel obj)
        {
            Venue venue = new Venue();
            if (obj.Venue.Id > 0)
            {
                venue = entities.Venues.Where(e => e.Id == obj.Venue.Id).FirstOrDefault();
            }
            venue.Nombre = obj.Venue.Nombre;
            venue.Telefono = obj.Venue.Telefono;
            venue.Direccion = obj.Venue.Direccion;
            venue.Longitud = obj.Venue.Longitud;
            venue.Latitud = obj.Venue.Latitud;
            venue.Aforo = obj.Venue.Aforo;
            venue.Email = obj.Venue.E_mail;
            if (obj.Venue.Ciudad_id > 0)
            {
                venue.Ciudad_id = obj.Venue.Ciudad_id;
            }
            if (obj.Venue.Id == 0)
            {
                venue.FechaCreacion = DateTime.Now;
                entities.Add(venue);
            }
            entities.SaveChanges();
            obj.Venue.Id = venue.Id;

            Evento entity = entities.Eventos.Where(e => e.Id == obj.Id).FirstOrDefault();
            //entity.Venue_id = obj.Venue.Id;

            entities.SaveChanges();
            return obj;
        }

        public void Destroy(EventoViewModel obj)
        {
            var entity = entities.Eventos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }

        internal List<ImpresionTicketsViewModel> ReadPrintTicketsState()
        {
            try
            {
                List<ImpresionTicketsViewModel> lista = new List<ImpresionTicketsViewModel>();
                var eventos = entities.Eventos;
                if (eventos.Any())
                {                    
                    foreach (var row in eventos)
                    {
                        var item = new ImpresionTicketsViewModel();
                        item.Id = row.Id;
                        item.Nombre = row.Nombre;
                        if (row.TicketsEventos.Count > 0)
                        {
                            //Organizar estados con la tabla de estados
                            item.Impresos = row.TicketsEventos.FirstOrDefault().Tickets.Count(r => r.IdEstado == EstadoTicket.IMPRESO);
                            item.Disponibles = row.TicketsEventos.FirstOrDefault().Tickets.Count(r => r.IdEstado == EstadoTicket.DISPONIBLE || !r.IdEstado.HasValue);
                            item.ImpresosDefectuosos = row.TicketsEventos.FirstOrDefault().Tickets.Count(r => r.IdEstado == EstadoTicket.IMPRESO_DEFECTUOSO);
                        }
                        lista.Add(item);
                    }
                }
                return lista;
                //return entities.Eventos.Select(e => new ImpresionTicketsViewModel()
                //{
                //    Id = e.Id,
                //    Nombre = e.Nombre,
                //    Impresos = e.TicketsEventos.FirstOrDefault().Tickets.Count(r => r.Estado == ("Impreso")),
                //    Disponibles = entities.TicketsEventos.Where(o => o.IdEvento == e.Id).FirstOrDefault().Tickets.Count(r => r.Estado == ("Disponible") || string.IsNullOrEmpty(r.Estado)),
                //    ImpresosDefectuosos = entities.TicketsEventos.Where(o => o.IdEvento == e.Id).FirstOrDefault().Tickets.Count(r => r.Estado == ("Impreso-Defectuoso")),
                //}).ToList();
            }
            catch (Exception)
            {
                return new List<ImpresionTicketsViewModel>();
            }
        }
    }
}