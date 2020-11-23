
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
    public class ServicioUsuarios : IDisposable
    {
        private Entities entities;

        public ServicioUsuarios()
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
        public UsuarioViewModel ObtenerUsuarioPorEmail(string email)
        {
            var obj = entities.Usuarios.FirstOrDefault(c => c.Email == email);
            if (obj != null)
            {
                return new UsuarioViewModel
                {
                    Id = obj.Id,
                    Usuario = obj.Nombre,
                    Email = obj.Email,
                };
            }
            return null;
        }

        public UsuarioViewModel ReadByIden(int tipoIden, string ident)
        {
            UsuarioViewModel User = (from c in entities.Clientes
                                     where (c.Identificacion == ident && c.TipoDocumento_id == tipoIden)
                                     select new UsuarioViewModel
                                     {
                                         Id = c.Id,
                                         TipoPersona = new TipoPersonaViewModel
                                         {
                                             Id = (int)c.TipoPersona_id,
                                             Nombre = c.Tipopersona.Nombre
                                         },
                                         TipoDocumento = new TipoDocumentoViewModel
                                         {
                                             Id = (int)c.TipoDocumento_id,
                                             Nombre = c.Tipodocumento.Nombre
                                         },
                                         Identificacion = c.Identificacion,
                                         PrimerNombres = c.PrimerNombreRazonSocial,
                                         PrimerApellido = c.PrimerApellido,
                                         SegundoNombres = c.SegundoNombre,
                                         SegundoApellido = c.SegundoApellido,
                                         FechaNacimiento = (DateTime)c.FechaNacimiento,
                                         Genero = new GeneroViewModel
                                         {
                                             Id = (int)c.Genero_id,
                                             Nombre = c.Genero.Nombre
                                         },
                                         EmailPersona = c.Email,
                                         Celular = c.Celular,
                                         TelefonoCasa = c.TelefonoCasa,
                                         TelefonoOficina = c.TelefonoOficina,
                                         JornadaEntrega = c.JornadaEntrega,
                                         EsEmpresa = c.IsEmpresa,
                                         strImagen = (c.Imagen != null) ? Convert.ToBase64String(c.Imagen) : "",
                                         Direcciones =
                                         entities.DireccionesClientes.Where(e => e.Cliente_id == c.Id).Select(e => new DireccionViewModel
                                         {
                                             Id = e.Id,
                                             IdCliente = (int)e.Cliente_id,
                                             Direccion = e.Ubicacion,
                                             CodigoPostal = e.CodigoPostal,
                                             Ciudad_id = (int)e.Ciudad_id,
                                             Departamneto_id = e.Ciudade.Iddepartamento,
                                             Pais_id = e.Ciudade.Departamento.Idpais,
                                             Latitud = e.Latitud,
                                             Longitud = e.Longitud
                                         }).OrderByDescending(e => e.Id).ToList(),
                                     }).FirstOrDefault();
            return User;
        }

        public string ReadUserByuserNameEmail(string userName, string emailSession)
        {
            if (!string.IsNullOrWhiteSpace(userName))
            {
                var user = (entities.Usuarios.Any(e => e.Nombre == userName));
                if (user)
                {
                    return "Usuario";
                }
                else if (!string.IsNullOrWhiteSpace(emailSession))
                {
                    var mail = (entities.Usuarios.Any(e => e.Email == emailSession));
                    if (!mail)
                    {
                        return string.Empty;
                    }
                    return "Email inicio sesion";
                }
                return string.Empty;
            }
            return null;
        }

        public List<UsuarioViewModel> Read()
        {
            List<UsuarioViewModel> lista = entities.Usuarios.Select(c => new UsuarioViewModel
            {
                IdClietne = c.Cliente.Id,
                TipoPersona = new TipoPersonaViewModel
                {
                    Id = (int)c.Cliente.TipoPersona_id,
                    Nombre = c.Cliente.Tipopersona.Nombre
                },
                TipoDocumento = new TipoDocumentoViewModel
                {
                    Id = (int)c.Cliente.TipoDocumento_id,
                    Nombre = c.Cliente.Tipodocumento.Nombre
                },
                Identificacion = c.Cliente.Identificacion,
                PrimerNombres = c.Cliente.PrimerNombreRazonSocial,
                PrimerApellido = c.Cliente.PrimerApellido,
                SegundoNombres = c.Cliente.SegundoNombre,
                SegundoApellido = c.Cliente.SegundoApellido,
                FechaNacimiento = (DateTime)c.Cliente.FechaNacimiento,
                Genero = new GeneroViewModel
                {
                    Id = (int)c.Cliente.Genero_id,
                    Nombre = c.Cliente.Genero.Nombre
                },
                EmailPersona = c.Cliente.Email,
                Celular = c.Cliente.Celular,
                TelefonoCasa = c.Cliente.TelefonoCasa,
                TelefonoOficina = c.Cliente.TelefonoOficina,
                JornadaEntrega = c.Cliente.JornadaEntrega,
                EsEmpresa = c.Cliente.IsEmpresa,
                strImagen = (c.Cliente.Imagen != null) ? Convert.ToBase64String(c.Cliente.Imagen) : "",
                Roles = c.Roles_usuarios.Select
                (r => new RoleViewModel
                     {
                         Id = r.Id,
                         Nombre = r.Role.Nombre,
                         Permisos = (from p in r.Role.Permisos_rols
                                     select new PermisoViewModel
                                     {
                                         Id = p.Id,
                                         Nombre = p.Permiso.Nombre,
                                         Descripcion = p.Permiso.Descripcion,
                                         Controlador = p.Permiso.Controlador
                                     }).ToList()
                     }).ToList(),
                Direcciones = entities.DireccionesClientes.Where(e => e.Cliente_id == c.Cliente.Id).Select(e => new DireccionViewModel
                {
                    Id = e.Id,
                    IdCliente = (int)e.Cliente_id,
                    Direccion = e.Ubicacion,
                    CodigoPostal = e.CodigoPostal,
                    Ciudad_id = (int)e.Ciudad_id,
                    Departamneto_id = e.Ciudade.Iddepartamento,
                    Pais_id = e.Ciudade.Departamento.Idpais,
                    Latitud = e.Latitud,
                    Longitud = e.Longitud
                }).OrderByDescending(e => e.Id).ToList(),
                Activo = c.Activo == null ? false : (bool)c.Activo,
                Id = c.Id,
                Usuario = c.Nombre,
                Email = c.Email,
            }).ToList();

            return lista;
        }

        public UsuarioViewModel Login(string usuario, string pw)
        {
            pw = Utilidades.Encrypt(pw);
            var obj = (from c in entities.Usuarios
                       where (c.Nombre == usuario && c.Contrasena == pw && c.Activo == true) || (c.Email == usuario && c.Contrasena == pw && c.Activo == true)
                       select new UsuarioViewModel
                       {
                           Email = c.Email,
                           Id = c.Id,
                           Usuario = c.Nombre
                       }).FirstOrDefault();
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
                                                 Nombre = p.Permiso.Nombre,
                                                 Descripcion = p.Permiso.Descripcion,
                                                 Controlador = p.Permiso.Controlador,
                                                 IdPermiso = p.Idpermiso,
                                                 Menu = p.Permiso.Menu,
                                                 Icono = p.Permiso.Icono
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

        public UsuarioViewModel Create(UsuarioViewModel obj)
        {
            var verficadorCliente = (from c in entities.Clientes
                                     where c.TipoDocumento_id == obj.TipoDocumento_id && c.Identificacion == obj.Identificacion
                                     select c).Count();
            if (verficadorCliente <= 0)
            {
                Cliente entity = new Cliente();
                entity.DateCreated = DateTime.Now;
                var usuarioConectado = Utilidades.GetCurrentUser().Id;
                entity.IdCreatorby = usuarioConectado;
                bool nuevo = false;
                entity.Id = obj.Id;
                entity.TipoPersona_id = obj.TipoPersona_id;
                entity.TipoDocumento_id = obj.TipoDocumento_id;
                entity.Identificacion = obj.Identificacion;
                entity.PrimerNombreRazonSocial = obj.PrimerNombres;
                entity.SegundoNombre = obj.SegundoNombres;
                entity.PrimerApellido = obj.PrimerApellido;
                entity.SegundoApellido = obj.SegundoApellido;
                entity.FechaNacimiento = obj.FechaNacimiento;
                entity.Genero_id = obj.Genero_id;
                entity.Email = obj.EmailPersona;
                entity.Celular = obj.Celular;
                entity.TelefonoCasa = obj.TelefonoCasa;
                entity.TelefonoOficina = obj.TelefonoOficina;
                entity.Imagen = obj.imagen;
                entity.IsEmpresa = obj.EsEmpresa;
                entity.Activo = true;
                if (obj.Id == 0)
                {
                    nuevo = true;
                    entities.Add(entity);
                }
                entities.SaveChanges();
                obj.Id = entity.Id;
                var idCliente = obj.Id;

                var entityUser = new Usuario();
                entityUser.IdClientePersona = idCliente;
                entityUser.Nombre = obj.Usuario;
                entityUser.Email = obj.Email;
                entityUser.Contrasena = Utilidades.Encrypt(obj.Password);
                entityUser.IdCreatorby = usuarioConectado;
                entityUser.DateCreated = DateTime.Now;
                entityUser.Activo = true;
                entities.Add(entityUser);
                entities.SaveChanges();

                if (nuevo)
                {
                    foreach (var item in obj.Direcciones)
                    {
                        entities.Add(new DireccionesCliente
                        {
                            Ubicacion = item.Direccion,
                            CodigoPostal = item.CodigoPostal,
                            Ciudad_id = item.Ciudad_id,
                            Cliente_id = idCliente,
                            DateCreated = DateTime.Now,
                            IdCreatorby = usuarioConectado,
                            Latitud = item.Latitud,
                            Longitud = item.Longitud,
                            Activo = true
                        });
                        entities.SaveChanges();
                    }
                }
                return obj;
            }
            else
            {
                obj = null;
                return obj;
            }
        }

        public UsuarioViewModel CreateFromCC(UsuarioViewModel obj)
        {
            var usuarioContectado = Utilidades.GetCurrentUser().Id;
            Cliente entity = new Cliente();
            entity = entities.Clientes.Where(e => e.Id == obj.Id).FirstOrDefault();
            entity.DateChanged = DateTime.Now;
            entity.IdChangedby = usuarioContectado;
            entity.Id = obj.Id;
            entity.TipoPersona_id = obj.TipoPersona_id;
            entity.TipoDocumento_id = obj.TipoDocumento_id;
            entity.Identificacion = obj.Identificacion;
            entity.PrimerNombreRazonSocial = obj.PrimerNombres;
            entity.SegundoNombre = obj.SegundoNombres;
            entity.PrimerApellido = obj.PrimerApellido;
            entity.SegundoApellido = obj.SegundoApellido;
            entity.FechaNacimiento = obj.FechaNacimiento;
            entity.Genero_id = obj.Genero_id;
            entity.Email = obj.Email;
            entity.Celular = obj.Celular;
            entity.TelefonoCasa = obj.TelefonoCasa;
            entity.TelefonoOficina = obj.TelefonoOficina;
            entity.JornadaEntrega = obj.JornadaEntrega;
            entity.IsEmpresa = obj.EsEmpresa;
            entity.Imagen = obj.imagen == null ? entity.Imagen : obj.imagen;
            entity.Activo = true;
            entities.SaveChanges();
            obj.Id = entity.Id;
            var idCliente = obj.Id;
            var records = entities.DireccionesClientes.Where(e => e.Cliente_id == obj.Id).ToList();
            var nuevos = obj.Direcciones.Where(c => c.Id < 0).ToList();
            //Borrado Direcciones
            var borrar = (from c in records
                          where obj.Direcciones.FirstOrDefault(r => r.Id == c.Id) == null
                          select c).ToList();

            var match = (from c in records
                         from d in obj.Direcciones
                         where c.Id == d.Id
                         select new
                         {
                             Record = c,
                             Memoria = d
                         }).ToList();

            foreach (var item in nuevos)
            {
                entities.Add(new DireccionesCliente
                {
                    Ubicacion = item.Direccion,
                    CodigoPostal = item.CodigoPostal,
                    Ciudad_id = item.Ciudad_id,
                    DateCreated = DateTime.Now,
                    Cliente_id = idCliente,
                    IdCreatorby = usuarioContectado,
                    Activo = true
                });
            }

            foreach (var item in borrar)
            {
                entities.Delete(item);
            }

            foreach (var item in match)
            {
                item.Record.Ubicacion = item.Memoria.Direccion;
                item.Record.CodigoPostal = item.Memoria.CodigoPostal;
                item.Record.Ciudad_id = item.Memoria.Ciudad_id;
            }
            var entityUser = new Usuario();
            entityUser.IdClientePersona = idCliente;
            entityUser.Nombre = obj.Usuario;
            entityUser.Email = obj.Email;
            entityUser.Contrasena = Utilidades.Encrypt(obj.Password);
            entityUser.IdCreatorby = usuarioContectado;
            entityUser.DateCreated = DateTime.Now;
            entityUser.Activo = true;
            entities.Add(entityUser);
            entities.SaveChanges();
            return obj;
        }

        public UsuarioViewModel Update(UsuarioViewModel obj)
        {
            var usuarioContectado = Utilidades.GetCurrentUser().Id;
            Cliente entity = new Cliente();
            entity = entities.Clientes.Where(e => e.Id == obj.Id).FirstOrDefault();
            entity.DateChanged = DateTime.Now;
            entity.IdChangedby = usuarioContectado;
            entity.Id = obj.Id;
            entity.TipoPersona_id = obj.TipoPersona_id;
            entity.TipoDocumento_id = obj.TipoDocumento_id;
            entity.Identificacion = obj.Identificacion;
            entity.PrimerNombreRazonSocial = obj.PrimerNombres;
            entity.SegundoNombre = obj.SegundoNombres;
            entity.PrimerApellido = obj.PrimerApellido;
            entity.SegundoApellido = obj.SegundoApellido;
            entity.FechaNacimiento = obj.FechaNacimiento;
            entity.Genero_id = obj.Genero_id;
            entity.Email = obj.Email;
            entity.Celular = obj.Celular;
            entity.TelefonoCasa = obj.TelefonoCasa;
            entity.TelefonoOficina = obj.TelefonoOficina;
            entity.JornadaEntrega = obj.JornadaEntrega;
            entity.IsEmpresa = obj.EsEmpresa;
            entity.Imagen = obj.imagen == null ? entity.Imagen : obj.imagen;
            entity.Activo = true;
            entities.SaveChanges();
            obj.Id = entity.Id;
            var idCliente = obj.Id;
            var records = entities.DireccionesClientes.Where(e => e.Cliente_id == obj.Id).ToList();
            var nuevos = obj.Direcciones.Where(c => c.Id < 0).ToList();
            //Borrado Direcciones
            var borrar = (from c in records
                          where obj.Direcciones.FirstOrDefault(r => r.Id == c.Id) == null
                          select c).ToList();

            var match = (from c in records
                         from d in obj.Direcciones
                         where c.Id == d.Id
                         select new
                         {
                             Record = c,
                             Memoria = d
                         }).ToList();

            foreach (var item in nuevos)
            {
                entities.Add(new DireccionesCliente
                {
                    Ubicacion = item.Direccion,
                    CodigoPostal = item.CodigoPostal,
                    Ciudad_id = item.Ciudad_id,
                    DateCreated = DateTime.Now,
                    Cliente_id = idCliente,
                    IdCreatorby = usuarioContectado,
                    Activo = true
                });
            }

            foreach (var item in borrar)
            {
                entities.Delete(item);
            }

            foreach (var item in match)
            {
                item.Record.Ubicacion = item.Memoria.Direccion;
                item.Record.CodigoPostal = item.Memoria.CodigoPostal;
                item.Record.Ciudad_id = item.Memoria.Ciudad_id;
            }

            var entityUser = new Usuario();
            entityUser = entities.Usuarios.Where(e => e.Id == obj.Id).FirstOrDefault();
            entityUser.IdClientePersona = idCliente;
            entityUser.Nombre = obj.Usuario;
            entityUser.Email = obj.Email;
            entityUser.Contrasena = obj.Password == null ? entityUser.Contrasena : Utilidades.Encrypt(obj.Password);
            entityUser.IdChangedby = usuarioContectado;
            entityUser.DateChanged = DateTime.Now;
            entityUser.Activo = true;
            entities.Add(entityUser);
            entities.SaveChanges();
            return obj;
        }

        public void ChangePassword(string username, string password, string passwordnuevo, string passwordnuevorepetir)
        {
            var entity = entities.Usuarios.FirstOrDefault(c => c.Nombre == username);
            if (entity != null)
            {

                if (passwordnuevo != passwordnuevorepetir)
                {
                    throw new Exception("Las contraseñas nuevas no son iguales.");
                }

                if (entity.Contrasena != Utilidades.Encrypt(password))
                {
                    throw new Exception("La contraseña actual no es correcta.");
                }
                entity.Contrasena = Utilidades.Encrypt(passwordnuevo);
                entity.DateChanged = DateTime.Now;
                entity.IdChangedby = Utilidades.GetCurrentUser().Id; 
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
            var entity = entities.Usuarios.FirstOrDefault(c => c.Nombre == username);
            if (entity != null)
            {
                entity.Contrasena = Utilidades.Encrypt(password);
                entities.SaveChanges();
            }
            else
            {
                throw new Exception("El usuario no existe.");
            }
        }

        public void Destroy(UsuarioViewModel obj)
        {
            var entity = entities.Usuarios.FirstOrDefault(c => c.Id == obj.Id);
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