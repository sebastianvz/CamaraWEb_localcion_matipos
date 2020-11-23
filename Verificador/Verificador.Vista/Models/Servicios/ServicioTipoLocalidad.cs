
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
    public class ServicioTipoLocalidad: IDisposable
    {
        private Entities entities;

        public ServicioTipoLocalidad()
        {
            this.entities = new Entities();
        }

        public IEnumerable<TipoLocalidadViewModel> Read()
        {
            return entities.TipoLocalidads.Select(c => new TipoLocalidadViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                FechaCreacion = (DateTime)c.FechaCreacion

            });
        }

        public TipoLocalidadViewModel Read(int id)
        {
            return entities.TipoLocalidads.Where(e=>e.Id == id).Select(c => new TipoLocalidadViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                FechaCreacion = (DateTime)c.FechaCreacion

            }).FirstOrDefault();
        }

        public void Create(TipoLocalidadViewModel obj)
        {
            var entity = new TipoLocalidad();
            entity.Nombre = obj.Nombre;
            entity.Descripcion = obj.Descripcion;
            entity.FechaCreacion = DateTime.Now;            

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(TipoLocalidadViewModel obj)
        {
            var entity = entities.TipoLocalidads.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                entity.Descripcion = obj.Descripcion;

                entities.SaveChanges();
            }
        }

        public void Destroy(TipoLocalidadViewModel obj)
        {
            var entity = entities.TipoLocalidads.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public static List<ListItem> getOptions()
        {
            return new Entities().TipoLocalidads.Select(c => new ListItem
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