
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
    public class ServicioTipoNumeracion : IDisposable
    {
        private Entities entities;

        public ServicioTipoNumeracion()
        {
            this.entities = new Entities();
        }

        public IEnumerable<TipoNumeracionViewModel> Read()
        {
            return entities.TipoNumeracions.Select(c => new TipoNumeracionViewModel
            {
                Id = c.Id,
                Numeracion = c.Numeracion,
            });
        }

        public TipoNumeracionViewModel Read(int id)
        {
            return entities.TipoNumeracions.Where(e => e.Id == id).Select(c => new TipoNumeracionViewModel
            {
                Id = c.Id,
                Numeracion = c.Numeracion,
            }).FirstOrDefault();
        }

        public void Create(TipoNumeracionViewModel obj)
        {
            var entity = new TipoNumeracion();
            entity.Numeracion = obj.Numeracion;

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(TipoNumeracionViewModel obj)
        {
            var entity = entities.TipoNumeracions.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Numeracion = obj.Numeracion;
                entities.SaveChanges();
            }
        }

        public void Destroy(TipoNumeracionViewModel obj)
        {
            var entity = entities.TipoNumeracions.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public static List<ListItem> getOptions()
        {
            return new Entities().TipoNumeracions.Select(c => new ListItem
            {
                Value = c.Id,
                Text = c.Numeracion
            }).ToList();
        }

        public void Dispose()
        {
            entities.Dispose();
        }

    }
}