
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
    public class ServicioTipoEvento: IDisposable
    {
        private Entities entities;

        public ServicioTipoEvento()
        {
            this.entities = new Entities();
        }

        public IEnumerable<TipoEventoViewModel> Read()
        {
            return entities.TipoEventos.Select(c => new TipoEventoViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                FechaCreacion = c.Fechacreacion

            });
        }

        public void Create(TipoEventoViewModel obj)
        {
            var entity = new TipoEvento();
            entity.Nombre = obj.Nombre;
            entity.Descripcion = obj.Descripcion;
            entity.Fechacreacion = DateTime.Now;            

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(TipoEventoViewModel obj)
        {
            var entity = entities.TipoEventos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                entity.Descripcion = obj.Descripcion;

                entities.SaveChanges();
            }
        }

        public void Destroy(TipoEventoViewModel obj)
        {
            var entity = entities.TipoEventos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

       

        public static List<ListItem> getOptions()
        {
            return new Entities().TipoEventos.Select(c => new ListItem
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