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
    public class ServicioPais: IDisposable
    {
        private Entities entities;

        public ServicioPais()
        {
            this.entities = new Entities();
        }

        public IEnumerable<PaisViewModel> Read()
        {
            return entities.Paises.Select(c => new PaisViewModel
            {
                Id = c.Id,
                Codigo = c.Codigo,
                Nombre = c.Nombre,
                FechaCreacion = c.Fechacreacion
            });
        }

        public List<PaisViewModel> ReadByName(string text)
        {
            return entities.Paises.Where(e => e.Nombre.Contains(text)).Select(c => new PaisViewModel
            {
                Id = c.Id,
                Codigo = c.Codigo,
                Nombre = c.Nombre
            }).ToList();
        }

        public void Create(PaisViewModel obj)
        {
            var entity = new Paise();
            entity.Nombre = obj.Nombre;
            entity.Fechacreacion = DateTime.Now;
            entity.Codigo = obj.Codigo;

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(PaisViewModel obj)
        {
            var entity = entities.Paises.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                entity.Codigo = obj.Codigo;
                entities.SaveChanges();
            }
        }

        public void Destroy(PaisViewModel obj)
        {
            var entity = entities.Paises.FirstOrDefault(c => c.Id == obj.Id);
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