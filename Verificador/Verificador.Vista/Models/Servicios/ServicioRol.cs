
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
    public class ServicioRol : IDisposable
    {
        private Entities entities;

        public ServicioRol()
        {
            this.entities = new Entities();
        }


        public List<RoleViewModel> ReadUsuario(int idUsuario)
        {
            return entities.Roles.Where(c=> c.AplicaMatipos== true && c.AplicaEmpresa==false).Select(c => new RoleViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,                
                Seleccionado = entities.Roles_usuarios.Count(p => p.Idrol == c.Id && p.Idusuario == idUsuario) > 0
            }).ToList();
        }

        public void GuardarRolesUsuario(int idUsuario, string roles)
        {
            List<int> actuales = (from c in roles.Split(",".ToCharArray())
                                  where !string.IsNullOrEmpty(c)
                                  select int.Parse(c)).ToList();

            var enbd = (from c in entities.Roles_usuarios
                        where c.Idusuario == idUsuario
                        select c).ToList();

            foreach (var item in actuales)
            {
                if (enbd.FirstOrDefault(c => c.Idrol == item) == null)
                {
                    entities.Add(new Roles_usuario { Idusuario = idUsuario, Idrol = item, Fechacreacion = DateTime.Now });
                }
            }

            foreach (var item in enbd)
            {
                if (!actuales.Contains(item.Idrol))
                {
                    entities.Delete(item);
                }
            }
            entities.SaveChanges();

        }


        public IEnumerable<RoleViewModel> Read()
        {
            return entities.Roles.Select(c => new RoleViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                FechaCreacion = c.Fechacreacion,
                AplicaEmpresario = c.AplicaEmpresa,
                AplicaMatipos = c.AplicaMatipos
            });
        }

        public void Create(RoleViewModel obj)
        {
            var entity = new Role();
            entity.Nombre = obj.Nombre;
            entity.Fechacreacion = DateTime.Now;
            entity.AplicaMatipos = obj.AplicaMatipos;
            entity.AplicaEmpresa = obj.AplicaEmpresario;
            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(RoleViewModel obj)
        {
            var entity = entities.Roles.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                entity.AplicaMatipos = obj.AplicaMatipos;
                entity.AplicaEmpresa = obj.AplicaEmpresario;
                entities.SaveChanges();
            }
        }

        public void Destroy(RoleViewModel obj)
        {
            var entity = entities.Roles.FirstOrDefault(c => c.Id == obj.Id);
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