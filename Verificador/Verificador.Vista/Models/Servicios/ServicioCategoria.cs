
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
    public class ServicioCategoria : IDisposable
    {
        private Entities entities;

        public ServicioCategoria()
        {
            this.entities = new Entities();
        }

        public IEnumerable<CategoriaViewModel> Read()
        {
            return entities.Categorias.Select(c => new CategoriaViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                VerCodigoArtesEscenicas = (bool)c.VerCodigoArtesEscenicas,
                FechaCreacion = (DateTime)c.FechaCreacion

            });
        }


        public CategoriaViewModel Read(int id)
        {
            return entities.Categorias.Where(e => e.Id == id).Select(c => new CategoriaViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                VerCodigoArtesEscenicas = (bool)c.VerCodigoArtesEscenicas,
                FechaCreacion = (DateTime)c.FechaCreacion

            }).FirstOrDefault();
        }

        public void Create(CategoriaViewModel obj)
        {
            var entity = new Categoria();
            entity.Nombre = obj.Nombre;
            entity.Descripcion = obj.Descripcion;
            entity.FechaCreacion = DateTime.Now;
            entity.VerCodigoArtesEscenicas = obj.VerCodigoArtesEscenicas;

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(CategoriaViewModel obj)
        {
            var entity = entities.Categorias.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                entity.Descripcion = obj.Descripcion;
                entity.VerCodigoArtesEscenicas = obj.VerCodigoArtesEscenicas;
                entities.SaveChanges();
            }
        }

        public void Destroy(CategoriaViewModel obj)
        {
            var entity = entities.Categorias.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public void SaveWithEvent(int categoryId, int eventoId)
        {
            CategoriasEvento entity = entities.CategoriasEventos.FirstOrDefault(e => e.IdEvento == eventoId);
            if (entity == null)
            {
                entities.Add(new CategoriasEvento()
                {
                    IdCategoria = categoryId,
                    IdEvento = eventoId,
                });
            }
            else
            {
                entity.IdCategoria = categoryId;
            }
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }

    }
}