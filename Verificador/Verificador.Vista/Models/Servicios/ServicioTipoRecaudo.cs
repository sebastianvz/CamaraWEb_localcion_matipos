
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
    public class ServicioTipoRecaudo: IDisposable
    {
        private Entities entities;

        public ServicioTipoRecaudo()
        {
            this.entities = new Entities();
        }

        public IEnumerable<TipoRecaudoViewModel> Read()
        {
            return entities.TipoRecaudos.Select(c => new TipoRecaudoViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                FechaCreacion = c.Fechacreacion

            });
        }

        public void Create(TipoRecaudoViewModel obj)
        {
            var entity = new TipoRecaudo();
            entity.Nombre = obj.Nombre;
            entity.Descripcion = obj.Descripcion;
            entity.Fechacreacion = DateTime.Now;            

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(TipoRecaudoViewModel obj)
        {
            var entity = entities.TipoRecaudos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                entity.Descripcion = obj.Descripcion;

                entities.SaveChanges();
            }
        }

        public void Destroy(TipoRecaudoViewModel obj)
        {
            var entity = entities.TipoRecaudos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public static List<ListItem> getOptions()
        {
            return new Entities().TipoRecaudos.Select(c => new ListItem
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