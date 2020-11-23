
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
    public class ServicioTipoPersona : IDisposable
    {
        private Entities entities;

        public ServicioTipoPersona()
        {
            this.entities = new Entities();
        }

        public IEnumerable<TipoPersonaViewModel> Read()
        {
            return entities.Tipopersonas.Select(c => new TipoPersonaViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                FechaCreacion = c.Fechacreacion

            });
        }

        public void Create(TipoPersonaViewModel obj)
        {
            var entity = new Tipopersona();
            entity.Nombre = obj.Nombre;
            entity.Fechacreacion = DateTime.Now;            

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(TipoPersonaViewModel obj)
        {
            var entity = entities.Tipopersonas.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;                

                entities.SaveChanges();
            }
        }

        public void Destroy(TipoPersonaViewModel obj)
        {
            var entity = entities.Tipopersonas.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public static List<ListItem> getOptions()
        {
            return new Entities().Tipopersonas.Select(c => new ListItem
            {
                Value = c.Id,
                Text =  c.Nombre
            }).ToList();
        }

        public void Dispose()
        {
            entities.Dispose();
        }

    }
}