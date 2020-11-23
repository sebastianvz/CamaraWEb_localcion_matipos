
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
    public class ServicioTipoDocumento: IDisposable
    {
        private Entities entities;

        public ServicioTipoDocumento()
        {
            this.entities = new Entities();
        }

        public IEnumerable<TipoDocumentoViewModel> Read()
        {
            return entities.Tipodocumentos.Select(c => new TipoDocumentoViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Codigo = c.Codigo,
                FechaCreacion = c.Fechacreacion

            });
        }

        public void Create(TipoDocumentoViewModel obj)
        {
            var entity = new Tipodocumento();
            entity.Nombre = obj.Nombre;
            entity.Codigo = obj.Codigo;
            entity.Fechacreacion = DateTime.Now;            

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(TipoDocumentoViewModel obj)
        {
            var entity = entities.Tipodocumentos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                entity.Codigo = obj.Codigo;

                entities.SaveChanges();
            }
        }

        public void Destroy(TipoDocumentoViewModel obj)
        {
            var entity = entities.Tipodocumentos.FirstOrDefault(c => c.Id == obj.Id);
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
            return new Entities().Tipodocumentos.Select(c => new ListItem
            {
                Value = c.Id,
                Text = String.Format("{0} - {1}", c.Codigo, c.Nombre)
            }).ToList();
        }

    }
}