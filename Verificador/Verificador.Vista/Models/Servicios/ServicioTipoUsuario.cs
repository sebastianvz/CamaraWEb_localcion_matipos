
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
    public class ServicioTipoUsuario: IDisposable
    {
        private Entities entities;

        public ServicioTipoUsuario()
        {
            this.entities = new Entities();
        }

        public IEnumerable<TipoUsuarioViewModel> Read()
        {
            return entities.TipoUsuarios.Select(c => new TipoUsuarioViewModel
            {
                Id = c.Id,
                Code = c.Code,
                Description = c.Description,  
                FechaCreacion = (DateTime)c.CreateDate,
            });
        }

        public TipoUsuarioViewModel Save(TipoUsuarioViewModel obj)
        {
            var entity = new TipoUsuario();
            if (obj.Id > 0)
            {
                entity = entities.TipoUsuarios.Where(e => e.Id == obj.Id).FirstOrDefault();
            }
            entity.Code = obj.Code;
            entity.Description = obj.Description;
            if (obj.Id == 0)
            {
                entity.CreateDate = DateTime.Now;
                entities.Add(entity);    
            }
            entities.SaveChanges();
            obj.Id = entity.Id;
            return obj;
        }

        public void Destroy(TipoUsuarioViewModel obj)
        {
            var entity = entities.TipoUsuarios.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public static List<ListItem> getOptions()
        {
            return new Entities().TipoUsuarios.Select(c => new ListItem
            {
                Value = c.Id,
                Text = String.Format("{0} - {1}", c.Code, c.Description)
            }).ToList();
        }


        public void Dispose()
        {
            entities.Dispose();
        }

    }
}