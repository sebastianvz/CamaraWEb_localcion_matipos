
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
    public class ServicioUVT: IDisposable
    {
        private Entities entities;

        public ServicioUVT()
        {
            this.entities = new Entities();
        }

        public IEnumerable<UVTViewModel> Read()
        {
            return entities.UVTs.Select(c => new UVTViewModel
            {
                Id = c.Id,
                Anio = c.Anio,
                Valor = c.Valor,                
                FechaCreacion = c.Fechacreacion
            });
        }

        public void Create(UVTViewModel obj)
        {
            var entity = new UVT();
            entity.Anio = obj.Anio;
            entity.Valor = obj.Valor;            

            entity.Fechacreacion = DateTime.Now;            

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(UVTViewModel obj)
        {
            var entity = entities.UVTs.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Anio = obj.Anio;
                entity.Valor = obj.Valor;
                

                entities.SaveChanges();
            }
        }

        public void Destroy(UVTViewModel obj)
        {
            var entity = entities.UVTs.FirstOrDefault(c => c.Id == obj.Id);
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