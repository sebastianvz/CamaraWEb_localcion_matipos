
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using Verificador.OpenAccess;
using System.Data.SqlClient;
using Verificador.Vista.Models.ViewModels;

namespace Verificador.Vista.Models.Servicios
{
    public class ServicioLocalidad : IDisposable
    {
        private Entities entities;

        public ServicioLocalidad()
        {
            this.entities = new Entities();
        }

        public LocalidadViewModel ReadById(int id)
        {
            return entities.Localidads.Where(e => e.Id == id).Select(e => new LocalidadViewModel()
            {
                Id = e.Id,
                Nombre = e.Nombre
            }).FirstOrDefault();
        }

        public List<LocalidadViewModel> ReadByVenue(int venueId)
        {
            return (from ve in entities.LocalidadesVenueEventos
                    join l in entities.Localidads on ve.IdLocalidad equals l.Id
                    where ve.IdVenue == venueId
                    select new LocalidadViewModel()
                    {
                        Id = l.Id,
                        Nombre = l.Nombre,
                        IdTipoLocalidad = (l.IdTipoLocalidad == null ? 0 : (int)l.IdTipoLocalidad),
                        Aforo = (l.Aforo == null ? 0 : (int)l.Aforo),
                        Filas = (l.Filas == null ? 0 : (int)l.Filas),
                        Sillas = (l.Sillas == null ? 0 : (int)l.Sillas),
                        TicketNumeroInicio = (l.TicketNumeroInicio == null ? 0 : (int)l.TicketNumeroInicio),
                        TicketNumeroFinal = (l.TicketNumeroFinal == null ? 0 : (int)l.TicketNumeroFinal),
                        RangoTickets = (l.RangoTickets == null ? false : (bool)l.RangoTickets),
                        IdTipoNumeracion = (l.IdTipoNumeracion == null ? 0 : (int)l.IdTipoNumeracion),
                        OrientacionFilas = l.OrientacionFilas,
                        OrientacionSillas = l.OrientacionSillas,
                        Consumo = l.Consumo,
                        ValorConsumo = (l.ValorConsumo == null ? 0 : (decimal)l.ValorConsumo),
                        Asientos = l.AsientosLocalidads.Select(e => new AsisentoViewModel()
                        {
                            Id = e.Id,
                            IdLocalidad = e.IdLocalidad,
                            GroupName = e.GroupName,
                            GroupOrder = e.GroupOrder,
                            Order = e.Order,
                            Label = e.Label,
                            Visible = e.Visible
                        }).ToList(),
                    }).ToList();
        }

        public List<LocalidadViewModel> ReadByVenue(int venueId, int idEvento)
        {
            return (from ve in entities.LocalidadesVenueEventos
                    join l in entities.Localidads on ve.IdLocalidad equals l.Id
                    where ve.IdVenue == venueId && ve.IdEvento == idEvento
                    select new LocalidadViewModel()
                    {
                        Id = l.Id,
                        Nombre = l.Nombre,
                        IdTipoLocalidad = (l.IdTipoLocalidad == null ? 0 : (int)l.IdTipoLocalidad),
                        Aforo = (l.Aforo == null ? 0 : (int)l.Aforo),
                        Filas = (l.Filas == null ? 0 : (int)l.Filas),
                        Sillas = (l.Sillas == null ? 0 : (int)l.Sillas),
                        TicketNumeroInicio = (l.TicketNumeroInicio == null ? 0 : (int)l.TicketNumeroInicio),
                        TicketNumeroFinal = (l.TicketNumeroFinal == null ? 0 : (int)l.TicketNumeroFinal),
                        RangoTickets = (l.RangoTickets == null ? false : (bool)l.RangoTickets),
                        IdTipoNumeracion = (l.IdTipoNumeracion == null ? 0 : (int)l.IdTipoNumeracion),
                        OrientacionFilas = l.OrientacionFilas,
                        OrientacionSillas = l.OrientacionSillas,
                        Consumo = l.Consumo,
                        Token = l.Token,
                        ValorConsumo = (l.ValorConsumo == null ? 0 : (decimal)l.ValorConsumo),
                        Asientos = l.AsientosLocalidads.Select(e => new AsisentoViewModel()
                        {
                            Id = e.Id,
                            IdLocalidad = e.IdLocalidad,
                            GroupName = e.GroupName,
                            GroupOrder = e.GroupOrder,
                            Order = e.Order,
                            Label = e.Label,
                            Visible = e.Visible,
                            Token = e.Token,
                        }).ToList(),
                    }).ToList();
        }

        public List<LocalidadViewModel> ReadByEvent(int eventId)
        {
            return (from ve in entities.LocalidadesVenueEventos
                    join l in entities.Localidads on ve.IdLocalidad equals l.Id
                    where ve.IdEvento == eventId
                    select new LocalidadViewModel()
                    {
                        Id = l.Id,
                        Nombre = l.Nombre,
                        IdTipoLocalidad = (l.IdTipoLocalidad == null ? 0 : (int)l.IdTipoLocalidad),
                        Aforo = (l.Aforo == null ? 0 : (int)l.Aforo),
                        Filas = (l.Filas == null ? 0 : (int)l.Filas),
                        Sillas = (l.Sillas == null ? 0 : (int)l.Sillas),
                        TicketNumeroInicio = (l.TicketNumeroInicio == null ? 0 : (int)l.TicketNumeroInicio),
                        TicketNumeroFinal = (l.TicketNumeroFinal == null ? 0 : (int)l.TicketNumeroFinal),
                        RangoTickets = (l.RangoTickets == null ? false : (bool)l.RangoTickets),
                        IdTipoNumeracion = (l.IdTipoNumeracion == null ? 0 : (int)l.IdTipoNumeracion),
                        OrientacionFilas = l.OrientacionFilas,
                        OrientacionSillas = l.OrientacionSillas,
                        Consumo = l.Consumo,
                        ValorConsumo = (l.ValorConsumo == null ? 0 : (decimal)l.ValorConsumo),
                    }).ToList();
        }

        public Dictionary<int, int> SaveWithEvent(List<LocalidadViewModel> localidades, int eventoId, int venueId)
        {
            var itemsOnDB = entities.LocalidadesVenueEventos.Where(e => e.IdEvento == eventoId);

            if (localidades.Any(e => e.Id > 0))
            {
                int[] arrayIds = localidades.Select(e => e.Id).ToArray();
                var itemsToDelete = itemsOnDB.Where(e => !arrayIds.Contains((int)e.IdLocalidad));

                if (itemsToDelete.Any())
                {
                    foreach (var item in itemsToDelete)
                    {
                        entities.Delete(item);
                    }
                }
            }

            Dictionary<int, int> dataIds = Save(localidades);

            foreach (var item in localidades)
            {
                if (entities.LocalidadesVenueEventos.Any(e => e.IdEvento == eventoId && e.IdVenue == venueId && e.IdLocalidad == item.Id))
                {
                    continue;
                }

                LocalidadesVenueEvento data = new LocalidadesVenueEvento()
                {
                    IdLocalidad = item.Id,
                    IdEvento = eventoId,
                    IdVenue = venueId
                };
                if (item.Id <= 0)
                {
                    data.IdLocalidad = dataIds[item.Id];
                }
                entities.Add(data);
            }
            entities.SaveChanges();

            return dataIds;
        }

        public Dictionary<int, int> Save(List<LocalidadViewModel> localidades)
        {
            Localidad localidad = null;
            Dictionary<int, int> dict = new Dictionary<int, int>();
            foreach (var item in localidades)
            {
                localidad = new Localidad();
                if (item.Id > 0)
                {
                    localidad = entities.Localidads.FirstOrDefault(e => e.Id == item.Id);
                }
                localidad.Nombre = item.Nombre;
                localidad.Aforo = item.Aforo;
                localidad.IdTipoLocalidad = item.IdTipoLocalidad;
                localidad.RangoTickets = item.RangoTickets;
                if (item.RangoTickets)
                {
                    localidad.TicketNumeroInicio = item.TicketNumeroInicio;
                    localidad.TicketNumeroFinal = localidad.TicketNumeroFinal;
                }
                localidad.IdTipoNumeracion = item.IdTipoNumeracion;
                localidad.Consumo = item.Consumo;
                localidad.ValorConsumo = item.ValorConsumo;
                localidad.Filas = item.Filas;
                localidad.Sillas = item.Sillas;
                localidad.OrientacionFilas = item.OrientacionFilas;
                localidad.OrientacionSillas = item.OrientacionSillas;
                localidad.Token = item.Token;
                if (item.Id <= 0)
                {
                    localidad.FechaCreacion = DateTime.Now;
                    entities.Add(localidad);
                }
                entities.SaveChanges();
                SeatsSave(item.Asientos, localidad.Id);
                dict.Add(item.Id, localidad.Id);
                item.Id = localidad.Id;
            }
            return dict;
        }

        private void SeatsSave(List<AsisentoViewModel> seats, int idLocalidad)
        {
            if (seats == null)
            {
                return;
            }
            var enDB = entities.AsientosLocalidads.Where(e => e.IdLocalidad == idLocalidad);
            var arSeats = seats.Where(e => e.Id > 0).Select(e => e.Id).ToArray();
            var toDelete = enDB.Where(e => !arSeats.Contains(e.Id));

            foreach (var item in toDelete)
            {
                entities.Delete(item);
            }

            foreach (var item in seats)
            {
                AsientosLocalidad seat = new AsientosLocalidad();
                if (item.Id > 0)
                {
                    seat = entities.AsientosLocalidads.FirstOrDefault(e => e.Id == item.Id);
                }
                seat.IdLocalidad = idLocalidad;
                seat.GroupOrder = item.GroupOrder;
                seat.GroupName = item.GroupName;
                seat.Order = item.Order;
                seat.Label = item.Label;
                seat.Visible = item.Visible;
                seat.Token = item.Token;
                
                if (item.Id <= 0)
                {
                    entities.Add(seat);    
                }
            }
            entities.SaveChanges();
        }

        //public IEnumerable<LocalidadViewModel> Read(int Venue_id)
        //{
        //    List<LocalidadViewModel> Localidades = new List<LocalidadViewModel>();

        //    foreach (var item in entities.LocalidadesVenues.Where(e => e.IdVenue == Venue_id))
        //    {
        //        LocalidadViewModel localidad = entities.Localidads.Where(e => e.Id == item.IdLocalidad).Select(e => new LocalidadViewModel
        //        {
        //            Id = e.Id,
        //            Label = e.Label,
        //            Curve = e.Curve == null ? 0 : (decimal)e.Curve,
        //            Skew = e.Skew == null ? 0 : (decimal)e.Skew,
        //            _X = e.X == null ? 0 : (decimal)e.X,
        //            _Y = e.Y == null ? 0 : (decimal)e.Y,
        //            Angle = e.Angle == null ? 0 : (decimal)e.Angle,
        //            Width = e.Width == null ? 0 : (int)e.Width,
        //            Height = e.Height == null ? 0 : (int)e.Height,
        //            Aforo = e.Aforo == null ? 0 : (int)e.Aforo,
        //            TipoLocalidad = new TipoLocalidadViewModel()
        //            {
        //                Id = e.TipoLocalidad.Id,
        //                Nombre = e.TipoLocalidad.Nombre
        //            },
        //            FechaCreacion = (DateTime)e.FechaCreacion,
        //            TipoSubLocalidad = entities.TipoLocalidads.Where(c => c.Id == e.IdTipoSubLocalidad).Select(c => new TipoLocalidadViewModel()
        //            {
        //                Id = c.Id,
        //                Nombre = c.Nombre
        //            }).FirstOrDefault(),
        //            Venue_id = Venue_id
        //        }).FirstOrDefault();

        //        localidad.LocalidadMesasFilasSillas = new List<LocalidadMesasFilasSillasViewModel>();

        //        foreach (var row in entities.LocalidadMesasFilasSillas.Where(e => e.IdLocalidad == item.IdLocalidad))
        //        {
        //            ConsumoViewModel consumo = entities.Consumos.Where(e => e.Id == row.IdConsumo).Select(e => new ConsumoViewModel
        //            {
        //                Id = e.Id,
        //                Valor = e.Valor == null ? 0 : (decimal)e.Valor,
        //                descripcion = e.Descripcion
        //            }).FirstOrDefault();

        //            MesasFilasViewModel mesaFila = entities.MesasFilas.Where(e => e.Id == row.IdMesasFilas).Select(e => new MesasFilasViewModel
        //            {
        //                Id = e.Id,
        //                Label = e.Label,
        //                Orientacion = e.Orientacion,
        //                InicioNumeracion = e.InicioNumeracion,
        //                Forma = e.Forma,
        //                capacidad = e.Capacidad == null ? 0 : (int)e.Capacidad,
        //                TipoNumeracion = entities.TipoNumeracions.Where(c => c.Id == e.IdTipoNumracion).Select(c => new TipoNumeracionViewModel()
        //                {
        //                    Id = c.Id,
        //                    Nombre = c.Nombre
        //                }).FirstOrDefault()
        //            }).FirstOrDefault();

        //            mesaFila.Sillas = new List<SillasViewModel>();

        //            foreach (var fila in entities.MesasFilasSillas.Where(e => e.IdMesasFilas == row.IdMesasFilas))
        //            {
        //                SillasViewModel silla = entities.Sillas.Where(e => e.Id == fila.IdSilla).Select(e => new SillasViewModel
        //                {
        //                    Id = e.Id,
        //                    Label = e.Label,
        //                    Orientacion = e.Orientacion,
        //                    InicioNumeracion = e.InicioNumeracion,
        //                    TipoNumeracion = entities.TipoNumeracions.Where(c => c.Id == e.IdTipoNumracion).Select(c => new TipoNumeracionViewModel()
        //                    {
        //                        Id = c.Id,
        //                        Nombre = c.Nombre
        //                    }).FirstOrDefault()
        //                }).FirstOrDefault();

        //                mesaFila.Sillas.Add(silla);
        //            }

        //            localidad.LocalidadMesasFilasSillas.Add(new LocalidadMesasFilasSillasViewModel
        //            {
        //                Consumo = consumo,
        //                MesasFilas = mesaFila
        //            });
        //        }

        //        Localidades.Add(localidad);
        //    }
        //    return Localidades;
        //}

        //public LocalidadViewModel Save(LocalidadViewModel obj)
        //{
        //    var localidad = new Localidad();
        //    if (obj.Id > 0)
        //    {
        //        localidad = entities.Localidads.Where(e => e.Id == obj.Id).FirstOrDefault();
        //    }
        //    localidad.Label = obj.Label;
        //    localidad.Curve = obj.Curve;
        //    localidad.Skew = obj.Skew;
        //    localidad.X = obj._X;
        //    localidad.Y = obj._Y;
        //    localidad.Angle = obj.Angle;
        //    localidad.Width = obj.Width;
        //    localidad.Height = obj.Height;
        //    localidad.Aforo = obj.Aforo;
        //    localidad.IdTipoLocalidad = obj.TipoLocalidad_id;
        //    localidad.IdTipoSubLocalidad = obj.TipoSubLocalidad_id;
        //    if (obj.Id <= 0)
        //    {
        //        localidad.FechaCreacion = DateTime.Now;
        //        entities.Add(localidad);
        //    }
        //    entities.SaveChanges();
        //    obj.Id = localidad.Id;

        //    entities.Add(new LocalidadesVenue
        //    {
        //        IdVenue = obj.Venue_id,
        //        IdLocalidad = localidad.Id
        //    });

        //    if (obj.LocalidadMesasFilasSillas != null)
        //    {
        //        if (obj.LocalidadMesasFilasSillas.Count > 0)
        //        {
        //            var consumo = new Consumo();
        //            if (obj.LocalidadMesasFilasSillas.First().Consumo.Valor > 0 || !string.IsNullOrEmpty(obj.LocalidadMesasFilasSillas.First().Consumo.descripcion))
        //            {
        //                consumo = new Consumo()
        //                {
        //                    Valor = obj.LocalidadMesasFilasSillas.First().Consumo.Valor,
        //                    Descripcion = obj.LocalidadMesasFilasSillas.First().Consumo.descripcion
        //                };
        //                entities.Add(consumo);
        //            }

        //            foreach (var mesasFilas in obj.LocalidadMesasFilasSillas.Select(e => e.MesasFilas))
        //            {
        //                var MesasFilas = new MesasFila()
        //                {
        //                    Label = mesasFilas.Label,
        //                    Orientacion = mesasFilas.Orientacion,
        //                    InicioNumeracion = mesasFilas.InicioNumeracion,
        //                    Forma = mesasFilas.Forma,
        //                    Capacidad = mesasFilas.capacidad,
        //                    IdTipoNumracion = mesasFilas.TipoNumeracion_id
        //                };
        //                entities.Add(MesasFilas);
        //                entities.SaveChanges();

        //                LocalidadMesasFilasSilla localidadMesasFilasSilla = new LocalidadMesasFilasSilla
        //                {
        //                    IdLocalidad = obj.Id,
        //                    IdMesasFilas = MesasFilas.Id,
        //                };
        //                if (consumo.Id > 0)
        //                {
        //                    localidadMesasFilasSilla.IdConsumo = consumo.Id;
        //                }
        //                entities.Add(localidadMesasFilasSilla);

        //                if (mesasFilas.Sillas != null)
        //                {
        //                    if (mesasFilas.Sillas.Count > 0)
        //                    {
        //                        foreach (var silla in mesasFilas.Sillas)
        //                        {
        //                            var Silla = new Silla()
        //                            {
        //                                //CREAR SILLA, Y CREAR ASOCIACION DE SILLA CON LA FILAMESA
        //                                //Label = mesasFilas.Label,
        //                                //Orientacion = mesasFilas.Orientacion,
        //                                //InicioNumeracion = mesasFilas.InicioNumeracion,
        //                                //IdTipoNumracion = mesasFilas.TipoNumeracion_id
        //                                Label = silla.Label,
        //                                Orientacion = silla.Orientacion,
        //                                InicioNumeracion = silla.InicioNumeracion,
        //                                IdTipoNumracion = silla.TipoNumeracion_id
        //                            };
        //                            entities.Add(Silla);
        //                            entities.SaveChanges();

        //                            entities.Add(new MesasFilasSilla
        //                            {
        //                                IdMesasFilas = MesasFilas.Id,
        //                                IdSilla = Silla.Id
        //                            });

        //                        }
        //                    }
        //                }

        //            }
        //        }
        //    }
        //    entities.SaveChanges();
        //    return obj;
        //}

        //public LocalidadViewModel SaveOptimizado(LocalidadViewModel obj)
        //{
        //    var localidad = new Localidad();
        //    if (obj.Id > 0)
        //    {
        //        localidad = entities.Localidads.Where(e => e.Id == obj.Id).FirstOrDefault();
        //    }
        //    localidad.Label = obj.Label;
        //    localidad.Curve = obj.Curve;
        //    localidad.Skew = obj.Skew;
        //    localidad.X = obj._X;
        //    localidad.Y = obj._Y;
        //    localidad.Angle = obj.Angle;
        //    localidad.Width = obj.Width;
        //    localidad.Height = obj.Height;
        //    localidad.Aforo = obj.Aforo;
        //    localidad.IdTipoLocalidad = obj.TipoLocalidad_id;
        //    localidad.IdTipoSubLocalidad = obj.TipoSubLocalidad_id;
        //    localidad.TicketNumeroInicio = obj.NumeroTicketInicial;
        //    localidad.TicketNumeroFinal = obj.NumeroTicketFinal;
        //    localidad.RangoTickets = obj.RangoTickets;
        //    if (obj.Id <= 0)
        //    {
        //        localidad.FechaCreacion = DateTime.Now;
        //        entities.Add(localidad);
        //    }
        //    entities.SaveChanges();

        //    var localidadVEnue = entities.LocalidadesVenues.Where(e => e.IdLocalidad == obj.Id).FirstOrDefault();
        //    if (localidadVEnue != null)
        //    {
        //        localidadVEnue.IdLocalidad = localidad.Id;
        //    }
        //    else
        //    {
        //        entities.Add(new LocalidadesVenue
        //        {
        //            IdVenue = obj.Venue_id,
        //            IdLocalidad = localidad.Id
        //        });
        //    }
        //    entities.SaveChanges();

        //    if (obj.Id > 0)
        //    {
        //        Utilidades.ExcuteProcedure(new SqlParameter[]{
        //                new SqlParameter(){
        //                    ParameterName = "@LocalidadID",
        //                    SqlDbType = SqlDbType.Int,
        //                    SqlValue = localidad.Id
        //                }
        //        }, "sp_ResetLocalidad");
        //    }

        //    if (obj.LocalidadMesasFilasSillas != null)
        //    {
        //        if (obj.LocalidadMesasFilasSillas.Count > 0)
        //        {
        //            var consumo = new Consumo();
        //            if (obj.LocalidadMesasFilasSillas.First().Consumo.Valor > 0 || !string.IsNullOrEmpty(obj.LocalidadMesasFilasSillas.First().Consumo.descripcion))
        //            {
        //                consumo = new Consumo()
        //                {
        //                    Valor = obj.LocalidadMesasFilasSillas.First().Consumo.Valor,
        //                    Descripcion = obj.LocalidadMesasFilasSillas.First().Consumo.descripcion
        //                };
        //                entities.Add(consumo);
        //            }
        //            entities.SaveChanges();

        //            Utilidades.ExcuteProcedure(new SqlParameter[]{
        //                new SqlParameter(){
        //                    ParameterName = "@LocalidadID",
        //                    SqlDbType = SqlDbType.Int,
        //                    SqlValue = localidad.Id
        //                }, new SqlParameter(){
        //                     ParameterName = "@ConsumoID",
        //                     SqlDbType = SqlDbType.Int,
        //                     SqlValue = consumo.Id
        //                }, new SqlParameter(){
        //                     ParameterName = "@Orientacion",
        //                     SqlDbType = SqlDbType.VarChar,
        //                     SqlValue = String.IsNullOrEmpty(obj.LocalidadMesasFilasSillas.Select(e => e.MesasFilas).First().Orientacion) ? "" : obj.LocalidadMesasFilasSillas.Select(e => e.MesasFilas).First().Orientacion
        //                }, new SqlParameter(){
        //                     ParameterName = "@capacidad",
        //                     SqlDbType = SqlDbType.Int,
        //                     SqlValue = obj.Aforo
        //                }, new SqlParameter(){
        //                     ParameterName = "@TipoNumeracion_id",
        //                     SqlDbType = SqlDbType.Int,
        //                     SqlValue = obj.LocalidadMesasFilasSillas.Select(e => e.MesasFilas).First().TipoNumeracion_id
        //                }, new SqlParameter(){
        //                    ParameterName = "@cFilas",
        //                    SqlDbType = SqlDbType.Int,
        //                    SqlValue = obj.LocalidadMesasFilasSillas.Select(e => e.MesasFilas).First().cFilas
        //                }, new SqlParameter(){
        //                    ParameterName = "@cSillas",
        //                    SqlDbType = SqlDbType.Int,
        //                    SqlValue = obj.LocalidadMesasFilasSillas.Select(e => e.MesasFilas).First().cSillas
        //                }, new SqlParameter(){
        //                    ParameterName = "@OrientacionSilla",
        //                    SqlDbType = SqlDbType.VarChar,
        //                    SqlValue = string.IsNullOrEmpty(obj.LocalidadMesasFilasSillas.Select(e => e.MesasFilas).First().OrientacionSilla) ? "" : obj.LocalidadMesasFilasSillas.Select(e => e.MesasFilas).First().OrientacionSilla
        //                }, new SqlParameter(){
        //                    ParameterName = "@TipoNumeracionSilla_id",
        //                    SqlDbType = SqlDbType.Int,
        //                    SqlValue = obj.LocalidadMesasFilasSillas.Select(e => e.MesasFilas).First().TipoNumeracionSilla_id
        //                }, new SqlParameter(){
        //                    ParameterName = "@TipoLocalidad",
        //                    SqlDbType = SqlDbType.Int,
        //                    SqlValue = obj.TipoLocalidad_id
        //                }
        //            }, "sp_SaveLocalidadData");
        //        }
        //    }

        //    obj.Id = localidad.Id;
        //    return obj;
        //}

        //public void Destroy(int id, int Venue_id)
        //{
        //    var entity = entities.LocalidadesVenues.FirstOrDefault(c => c.IdLocalidad == id && c.IdVenue == Venue_id);
        //    if (entity != null)
        //    {
        //        entities.Delete(entity);
        //        entities.SaveChanges();
        //    }
        //}

        public void Destroy(int id)
        {
            var entity = entities.Localidads.FirstOrDefault(c => c.Id == id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public static List<ListItem> getOptions(int Venue_id)
        {
            Entities entities = new Entities();
            return (from o in entities.Localidads
                    where o.LocalidadesVenueEventos.Where(e => e.IdVenue == Venue_id).Select(e => e.IdLocalidad).Contains(o.Id)
                    select new ListItem
                    {
                        Value = o.Id,
                        Text = o.Nombre
                    }).ToList();
        }

        public static List<ListItem> getOptions(int idEtapa, bool ByEtapa)
        {
            Entities entities = new Entities();
            return (from t in entities.TicketsEtapas
                    where t.IdEtapa == idEtapa
                    select new ListItem
                    {
                        Value = t.Id,
                        Text = t.Localidad.Nombre
                    }).ToList();
        }

        public void Dispose()
        {
            entities.Dispose();
        }


        internal List<TicketViewModel> getTickets(int ticketEtapaId, int idlocalidad)
        {
            List<TicketViewModel> tickets = new List<TicketViewModel>();
            int numeroTicket = (int)entities.Localidads.FirstOrDefault(x => x.Id == idlocalidad).TicketNumeroInicio;

            (from e in entities.EtapasVentaEventos
             join te in entities.TicketsEtapas.Where(x => x.Id == ticketEtapaId) on e.Id equals te.IdEtapa
             join t in entities.Tickets on te.Id equals t.IdTicketsEtapa
             select t).ToList().ForEach(item =>
             {
                 tickets.Add(new TicketViewModel
                 {
                     Id = item.Id,
                     Estado = EstadoTicket.getEstado((int)item.IdEstado),
                     Numero = (numeroTicket++)
                 });
             });

            return tickets;
        }

        internal string getMapByevent(int idEvento)
        {
            var mapa = entities.Mapas.FirstOrDefault(e => e.IdEvento == idEvento);
            if (mapa != null)
            {
                return mapa.Mapa1;
            }
            return string.Empty;
        }

        internal void SaveMapaByEvent(string mapa, int idEvento)
        {
            var item = entities.Mapas.FirstOrDefault(e => e.IdEvento == idEvento);
            if (item != null)
            {
                item.Mapa1 = mapa;
            }
            else
            {
                entities.Add(new Mapa()
                {
                    IdEvento = idEvento,
                    FechaCreacion = DateTime.Now,
                    Mapa1 = mapa
                });
            }
            entities.SaveChanges();
        }
    }
}