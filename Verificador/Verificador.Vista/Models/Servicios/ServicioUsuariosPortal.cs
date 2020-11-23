
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
    public class ServicioUsuariosPortal : IDisposable
    {
        private Entities entities;

        public ServicioUsuariosPortal()
        {
            this.entities = new Entities();
        }


        public void ActualizarPasswords()
        {

            /*var usuarios = entities.Te_usuarios.ToList().Where(c => c.Password.Length <= 5).ToList();
            if (usuarios.Count > 0)
            {
                foreach (var item in usuarios)
                {
                    item.Password = Utilidades.Encrypt(item.Password);
                }
                entities.SaveChanges();
            }*/
        }

        /// <summary>
        /// Obtiene un usuario por email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns></returns>
        public PortalUsuarioViewModel ObtenerUsuarioPorEmail(string email)
        {
            var obj = entities.Usuarios_portals.FirstOrDefault(c => c.Email == email);
            if (obj != null)
            {
                return new PortalUsuarioViewModel
                {
                    Id = obj.Id,                    
                    Email = obj.Email,
                    Nombres = obj.Nombres,
                    Apellidos = obj.Apellidos,                                        
                    FechaNacimiento = obj.Fecha,                    
                    Bloqueado = obj.Bloqueado,
                    FechaCreacion = obj.Fechacreacion                      
                };
            }
            return null;
        }



        public List<PortalUsuarioViewModel> Read()
        {
            return entities.Usuarios_portals.Select(c => new PortalUsuarioViewModel
            {
                Id = c.Id,                
                Email = c.Email,
                Nombres = c.Nombres,
                Apellidos = c.Apellidos,                                
                FechaNacimiento = c.Fecha,              
                Bloqueado = c.Bloqueado,
                FechaCreacion = c.Fechacreacion
            }).ToList();
        }

        public PortalUsuarioViewModel Login(string usuario, string pw)
        {
            //pw = Utilidades.Encrypt(pw);
            var obj = (from c in entities.Usuarios_portals
                       where c.Email == usuario && c.Contrasena == pw
                       select new PortalUsuarioViewModel { Email = c.Email, Id = c.Id}).FirstOrDefault();
            if (obj != null)
            {
                obj.Roles = (from c in entities.Roles_usuarios
                             where c.Idusuario == obj.Id
                             select new RoleViewModel
                             {
                                 Id = c.Id,
                                 Nombre = c.Role.Nombre,                                 
                                 Permisos = (from p in c.Role.Permisos_rols
                                             select new PermisoViewModel
                                             {
                                                 Id = p.Id,
                                                 Nombre = p.Permiso.Nombre
                                             }).ToList()
                             }).ToList();
            }
            //if (obj != null)
            //{
            //    foreach (var item in obj.Roles)
            //    {
            //        obj.NombresPermisos.AddRange(item.Permisos.Select(c => c.Nombre).ToList());
            //    }
            //}
            return obj;
        }

        public void BasicCreate(PortalUsuarioViewModel obj)
        {
            var entity = new Usuarios_portal();            
            entity.Email = obj.Email;
            entity.Contrasena = obj.Password;//Utilidades.Encrypt(obj.Password); 
            entity.Bloqueado = false;
            entity.Fecha = obj.FechaNacimiento;
            entity.Fechacreacion = DateTime.Now;

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Create(PortalUsuarioViewModel obj)
        {
            var entity = new Usuarios_portal();            
            entity.Email = obj.Email;
            entity.Contrasena = obj.Password;//Utilidades.Encrypt(obj.Password); 
            entity.Bloqueado = obj.Bloqueado;
            entity.Nombres = obj.Nombres;
            entity.Apellidos = obj.Apellidos;                        
            entity.Fecha = obj.FechaNacimiento;
            
            entity.Fechacreacion = DateTime.Now;

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(PortalUsuarioViewModel obj)
        {
            var entity = entities.Usuarios_portals.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {                
                entity.Email = obj.Email;                
                entity.Bloqueado = obj.Bloqueado;
                entity.Nombres = obj.Nombres;
                entity.Apellidos = obj.Apellidos;                
                entity.Fecha = obj.FechaNacimiento;
                
                entities.SaveChanges();
            }
        }

        public void ChangePassword(string username, string password, string passwordnuevo, string passwordnuevorepetir)
        {
            //password = Utilidades.Encrypt(password);
            var entity = entities.Usuarios_portals.FirstOrDefault(c => c.Email == username);
            if (entity != null)
            {

                if (passwordnuevo != passwordnuevorepetir)
                {
                    throw new Exception("Las contraseñas nuevas no son iguales.");
                }

                if (entity.Contrasena != password)
                {
                    throw new Exception("La contraseña actual no es correcta.");
                }
                entity.Contrasena = passwordnuevo;//Utilidades.Encrypt(passwordnuevo);
                entities.SaveChanges();
            }
            else
            {
                throw new Exception("No se encontro el usuario.");
            }
        }

        /// <summary>
        /// Cambiar el password de un usuario
        /// </summary>
        /// <param name="username">Usuario</param>
        /// <param name="password">Contraseña</param>
        public void ChangePassword(string username, string password)
        {
            var entity = entities.Usuarios_portals.FirstOrDefault(c => c.Email == username);
            if (entity != null)
            {
                entity.Contrasena = password;//Utilidades.Encrypt(password);
                entities.SaveChanges();
            }
            else
            {
                throw new Exception("El usuario no existe.");
            }
        }

        public void Destroy(UsuarioViewModel obj)
        {
            var entity = entities.Usuarios_portals.FirstOrDefault(c => c.Id == obj.Id);
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