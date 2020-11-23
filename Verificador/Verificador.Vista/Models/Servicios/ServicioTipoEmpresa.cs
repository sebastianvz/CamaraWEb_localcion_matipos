
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
    public class ServicioTipoEmpresa : IDisposable
    {
        private Entities entities;

        public ServicioTipoEmpresa()
        {
            this.entities = new Entities();
        }

        public IEnumerable<TipoEmpresaViewModel> Read()
        {
            return entities.Tipoempresas.Select(c => new TipoEmpresaViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Codigo = c.Codigo,
                FechaCreacion = c.Fechacreacion

            });
        }

        public void Create(TipoEmpresaViewModel obj)
        {
            var entity = new Tipoempresa();
            entity.Nombre = obj.Nombre;
            entity.Codigo = obj.Codigo;
            entity.Fechacreacion = DateTime.Now;

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(TipoEmpresaViewModel obj)
        {
            var entity = entities.Tipoempresas.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                entity.Codigo = obj.Codigo;

                entities.SaveChanges();
            }
        }

        public void Destroy(TipoEmpresaViewModel obj)
        {
            var entity = entities.Tipoempresas.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public static List<ListItem> getOptions()
        {
            return new Entities().Tipoempresas.Select(c => new ListItem
            {
                Value = c.Id,
                Text = String.Format("{0} - {1}", c.Codigo, c.Nombre)
            }).ToList();
        }

        public void Dispose()
        {
            entities.Dispose();
        }

    }
}