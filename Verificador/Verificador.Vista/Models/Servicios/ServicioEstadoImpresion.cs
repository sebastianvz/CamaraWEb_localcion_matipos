
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
    public class ServicioEstadoImpresion: IDisposable
    {
        private Entities entities;

        public ServicioEstadoImpresion()
        {
            this.entities = new Entities();
        }

        public IEnumerable<EstadoImpresionViewModel> Read()
        {
            return entities.EstadoImpresions.Select(c => new EstadoImpresionViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                FechaCreacion = c.Fechacreacion

            });
        }

        public void Create(EstadoImpresionViewModel obj)
        {
            var entity = new EstadoImpresion();
            entity.Nombre = obj.Nombre;
            entity.Descripcion = obj.Descripcion;
            entity.Fechacreacion = DateTime.Now;            

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(EstadoImpresionViewModel obj)
        {
            var entity = entities.EstadoImpresions.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                entity.Descripcion = obj.Descripcion;

                entities.SaveChanges();
            }
        }

        public void Destroy(EstadoImpresionViewModel obj)
        {
            var entity = entities.EstadoImpresions.FirstOrDefault(c => c.Id == obj.Id);
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