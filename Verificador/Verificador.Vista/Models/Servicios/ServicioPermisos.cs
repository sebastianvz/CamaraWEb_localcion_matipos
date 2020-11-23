
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
    public class ServicioPermisos : IDisposable
    {
        private Entities entities;

        public ServicioPermisos()
        {
            this.entities = new Entities();
        }


        public IEnumerable<PermisoViewModel> Read()
        {
            return from c in entities.Permisos
                   orderby c.Nombre ascending
                   select new PermisoViewModel
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                        Descripcion = c.Descripcion,
                        Controlador = c.Controlador,
                        FechaCreacion = c.Fechacreacion

                    };
        }

        public List<PermisoViewModel> ReadRol(int idRol)
        {
            return (from c in entities.Permisos
                    orderby c.Nombre ascending
                    select new PermisoViewModel
                    {
                        Id = c.Id,
                        Nombre = c.Nombre,
                        Seleccionado = entities.Permisos_rols.Count(p => p.Idpermiso == c.Id && p.Idrol == idRol) > 0
                    }).ToList();
        }

        public void GuardarPermisosRol(int idRol, string permisos)
        {


            List<int> actuales = (from c in permisos.Split(",".ToCharArray())
                                  where !string.IsNullOrEmpty(c)
                                  select int.Parse(c)).ToList();

            var enbd = (from c in entities.Permisos_rols
                        where c.Idrol == idRol
                        select c).ToList();

            foreach (var item in actuales)
            {
                if (enbd.FirstOrDefault(c => c.Idpermiso == item) == null)
                {
                    entities.Add(new Permisos_rol { Idrol = idRol, Idpermiso = item, Fechacreacion = DateTime.Now });
                }
            }

            foreach (var item in enbd)
            {
                if (!actuales.Contains(item.Idpermiso))
                {
                    entities.Delete(item);
                }
            }
            entities.SaveChanges();

        }

        public void Create(PermisoViewModel obj)
        {
            var entity = new Permiso();
            entity.Nombre = obj.Nombre;
            entity.Fechacreacion = DateTime.Now;

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(PermisoViewModel obj)
        {
            var entity = entities.Permisos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                entities.SaveChanges();
            }
        }

        public void Destroy(PermisoViewModel obj)
        {
            var entity = entities.Permisos.FirstOrDefault(c => c.Id == obj.Id);
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