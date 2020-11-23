
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
    public class ServicioTipoServicio: IDisposable
    {
        private Entities entities;

        public ServicioTipoServicio()
        {
            this.entities = new Entities();
        }

        public IEnumerable<TipoServicioViewModel> Read()
        {
            return entities.TipoServicios.Select(c => new TipoServicioViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                FechaCreacion = c.Fechacreacion
            });
        }

        public List<TipoServicioViewModel> Read(int idevento)
        {
            return (from ie in entities.ServiciosEventos
                    join i in entities.TipoServicios on ie.Idservicio equals i.Id
                    where ie.Idevento == idevento
                    select new TipoServicioViewModel
                    {
                        Id = i.Id,
                        Nombre = i.Nombre,
                        FechaCreacion = i.Fechacreacion
                    }).ToList();
        }

        public TipoServicioViewModel ReadById(int id)
        {
            return entities.TipoServicios.Where(e => e.Id == id).Select(c => new TipoServicioViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                FechaCreacion = c.Fechacreacion
            }).FirstOrDefault();
        }

        public void Create(TipoServicioViewModel obj)
        {
            var entity = new TipoServicio();
            entity.Nombre = obj.Nombre;
            entity.Descripcion = obj.Descripcion;
            entity.Fechacreacion = DateTime.Now;            

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(TipoServicioViewModel obj)
        {
            var entity = entities.TipoServicios.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                entity.Descripcion = obj.Descripcion;

                entities.SaveChanges();
            }
        }

        public void Destroy(TipoServicioViewModel obj)
        {
            var entity = entities.TipoServicios.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public void Destroy(int id, int evento_id)
        {
            var entity = entities.ServiciosEventos.FirstOrDefault(c => c.Idservicio == id && c.Idevento == evento_id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public void AsociarAlEvento(int evento_id, int id)
        {
            if (entities.ServiciosEventos.FirstOrDefault(c => c.Idservicio == id && c.Idevento == evento_id) == null)
            {
                entities.Add(new ServiciosEvento
                {
                    Idservicio = id,
                    Idevento = evento_id
                });    
            }
        }

        public void Save(List<TipoServicioViewModel> servicios, int evento_id)
        {
            var enDB = entities.ServiciosEventos.Where(e => e.Idevento == evento_id);
            if (enDB.Count() > 0)
            {
                var arrayServicios = servicios.Select(o => o.Id).ToArray();
                var Removidos = enDB.Where(e => arrayServicios.Contains((int)e.Idservicio) == false);
                if (Removidos.Count() > 0)
                {
                    foreach (var row in Removidos)
                    {
                        entities.Delete(row);
                    }
                }
            }
            foreach (var servicio in servicios)
            {
                AsociarAlEvento(evento_id, servicio.Id);
            }
            entities.SaveChanges();
        }

        public static List<ListItem> getOptions()
        {
            return new Entities().TipoServicios.Select(c => new ListItem
            {
                Value = c.Id,
                Text = c.Nombre
            }).ToList();
        }

        public void Dispose()
        {
            entities.Dispose();
        }

    }
}