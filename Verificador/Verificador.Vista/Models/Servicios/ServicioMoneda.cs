
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
    public class ServicioMoneda: IDisposable
    {
        private Entities entities;

        public ServicioMoneda()
        {
            this.entities = new Entities();
        }

        public IEnumerable<MonedaViewModel> Read()
        {
            return entities.Tipomonedas.Select(c => new MonedaViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Codigo = c.Codigo,
                Simbolo= c.Simbolo,
                Decimales = c.Decimales,
                FechaCreacion = c.Fechacreacion

            });
        }

        public void Create(MonedaViewModel obj)
        {
            var entity = new Tipomoneda();
            entity.Nombre = obj.Nombre;
            entity.Codigo = obj.Codigo;
            entity.Simbolo = obj.Simbolo;
            entity.Decimales = obj.Decimales;

            entity.Fechacreacion = DateTime.Now;            

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(MonedaViewModel obj)
        {
            var entity = entities.Tipomonedas.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                entity.Codigo = obj.Codigo;
                entity.Simbolo = obj.Simbolo;
                entity.Decimales = obj.Decimales;

                entities.SaveChanges();
            }
        }

        public void Destroy(MonedaViewModel obj)
        {
            var entity = entities.Tipomonedas.FirstOrDefault(c => c.Id == obj.Id);
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