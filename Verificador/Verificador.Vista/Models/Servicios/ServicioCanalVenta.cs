
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
    public class ServicioCanalVenta: IDisposable
    {
        private Entities entities;

        public ServicioCanalVenta()
        {
            this.entities = new Entities();
        }

        public IEnumerable<CanalVentaViewModel> Read()
        {
            return entities.CanalesVenta.Select(c => new CanalVentaViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,            
                FechaCreacion = c.Fechacreacion

            });
        }

        public void Create(CanalVentaViewModel obj)
        {
            var entity = new CanalesVentum();
            entity.Nombre = obj.Nombre;
            
            entity.Fechacreacion = DateTime.Now;            

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(CanalVentaViewModel obj)
        {
            var entity = entities.CanalesVenta.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                

                entities.SaveChanges();
            }
        }

        public void Destroy(CanalVentaViewModel obj)
        {
            var entity = entities.CanalesVenta.FirstOrDefault(c => c.Id == obj.Id);
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

    }
}