
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
    public class ServicioGenero: IDisposable
    {
        private Entities entities;

        public ServicioGenero()
        {
            this.entities = new Entities();
        }

        public IEnumerable<GeneroViewModel> Read()
        {
            return entities.Generos.Select(c => new GeneroViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,                
                FechaCreacion = c.Fechacreacion

            });
        }

        public void Create(GeneroViewModel obj)
        {
            var entity = new Genero();
            entity.Nombre = obj.Nombre;            
            entity.Fechacreacion = DateTime.Now;            

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(GeneroViewModel obj)
        {
            var entity = entities.Generos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;               

                entities.SaveChanges();
            }
        }

        public void Destroy(GeneroViewModel obj)
        {
            var entity = entities.Generos.FirstOrDefault(c => c.Id == obj.Id);
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


        public static List<ListItem> getOptions()
        {
            return new Entities().Generos.Select(c => new ListItem
            {
                Value = c.Id,
                Text = c.Nombre
            }).ToList();
        }
    }
}