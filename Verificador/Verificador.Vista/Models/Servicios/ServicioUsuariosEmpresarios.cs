using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.Entity;
using Verificador.OpenAccess;
using Verificador.Vista.Models.ViewModels;

namespace Verificador.Vista.Models.Servicios
{
    public class ServicioUsuariosEmpresarios
    {
        private Entities entities;

        public ServicioUsuariosEmpresarios()
        {
            this.entities = new Entities();
        }

        public UsuarioViewModel Login(string usuario, string pw)
        {
            pw = Utilidades.Encrypt(pw);
            var obj = (from c in entities.UsuariosEmpresarios
                       where (c.Nombre == usuario && c.Contrasena == pw && c.Activo == true) || (c.Email == usuario && c.Contrasena == pw && c.Activo == true)
                       select new UsuarioViewModel
                       {
                           Email = c.Email,
                           Id = c.Id,
                           Usuario = c.Nombre,
                           ClienteID = c.IdClientePersonaEmpresa
                       }).FirstOrDefault();
            if (obj != null)
            {
                obj.Roles = (from c in entities.UsuarioEmpresasriosRols
                             where c.IdUsuarioEmpresarios == obj.Id
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
            return obj;
        }

        public void ChangePassword(string username, string password, string passwordnuevo, string passwordnuevorepetir)
        {
            var entity = entities.UsuariosEmpresarios.FirstOrDefault(c => c.Nombre == username);
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
                var user = (entities.UsuariosEmpresarios.Any(e => e.Nombre == userName));
                if (user)
                {
                    return "Usuario";
                }
                else if (!string.IsNullOrWhiteSpace(emailSession))
                {
                    var mail = (entities.UsuariosEmpresarios.Any(e => e.Email == emailSession));
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

        public bool ReadUser(int idUsuario) 
        {
            var usuario = entities.UsuariosEmpresarios.Where(c => c.Id == idUsuario).FirstOrDefault();
            if (usuario != null)
            {
                return true;
            }
            else 
            {
                return false;
            }
        }

        #region Usuarios

        public List<UsuarioViewModel> Read()
        {
            var usuarioEmpresarioConectado = entities.Empresarios.Where(e => e.IdClientePersonaEmpresa == Utilidades.GetCurrentUser().ClienteID).Select(e => e.Id).First();
            List<UsuarioViewModel> lista = entities.UsuarioEmpresasriosRols
                .Where(c => c.IdEmpresario == usuarioEmpresarioConectado)
                .Select(c => new UsuarioViewModel
            {
                Roles = entities.Roles
                .Where(r => r.Id == c.IdRol)
                .Select(r => new RoleViewModel
                {
                    Id = r.Id,
                    Nombre = r.Nombre,
                    Permisos = (from p in r.Permisos_rols
                                select new PermisoViewModel
                                {
                                    Id = p.Id,
                                    Nombre = p.Permiso.Nombre,
                                    Descripcion = p.Permiso.Descripcion,
                                    Controlador = p.Permiso.Controlador
                                }).ToList()
                }).ToList(),

                IdClietne = c.UsuariosEmpresario.Cliente.Id,
                TipoPersona = new TipoPersonaViewModel
                {
                    Id = (int)c.UsuariosEmpresario.Cliente.TipoPersona_id,
                    Nombre = c.UsuariosEmpresario.Cliente.Tipopersona.Nombre
                },
                TipoDocumento = new TipoDocumentoViewModel
                {
                    Id = (int)c.UsuariosEmpresario.Cliente.TipoDocumento_id,
                    Nombre = c.UsuariosEmpresario.Cliente.Tipodocumento.Nombre
                },
                Identificacion = c.UsuariosEmpresario.Cliente.Identificacion,
                PrimerNombres = c.UsuariosEmpresario.Cliente.PrimerNombreRazonSocial,
                PrimerApellido = c.UsuariosEmpresario.Cliente.PrimerApellido,
                SegundoNombres = c.UsuariosEmpresario.Cliente.SegundoNombre,
                SegundoApellido = c.UsuariosEmpresario.Cliente.SegundoApellido,
                FechaNacimiento = c.UsuariosEmpresario.Cliente.FechaNacimiento,
                Genero = (c.UsuariosEmpresario.Cliente.Genero_id != null) ? new GeneroViewModel
                {
                    Id = (int)c.UsuariosEmpresario.Cliente.Genero_id,
                    Nombre = c.UsuariosEmpresario.Cliente.Genero.Nombre
                } : null,
                EmailPersona = c.UsuariosEmpresario.Cliente.Email,
                Celular = c.UsuariosEmpresario.Cliente.Celular,
                TelefonoCasa = c.UsuariosEmpresario.Cliente.TelefonoCasa,
                TelefonoOficina = c.UsuariosEmpresario.Cliente.TelefonoOficina,
                strImagen = (c.UsuariosEmpresario.Cliente.Imagen != null) ? Convert.ToBase64String(c.UsuariosEmpresario.Cliente.Imagen) : "",
                Direcciones = entities.DireccionesClientes.Where(e => e.Cliente_id == c.UsuariosEmpresario.Cliente.Id).Select(e => new DireccionViewModel
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
                PersonaClienteId = c.IdUsuarioEmpresarios,
                Id = c.Id,
                Usuario = c.UsuariosEmpresario.Nombre,
                Email = c.UsuariosEmpresario.Email,
            }).ToList();

            if (lista != null)
            {
                return lista;
            }
            else
            {
                return null;
            }
        }

        public UsuarioViewModel Create(UsuarioViewModel obj)
        {
            var usuarioEmpresarioConectado = entities.Empresarios.Where(e => e.IdClientePersonaEmpresa == Utilidades.GetCurrentUser().ClienteID).Select(e => e.Id).First();
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
                var entityUser = new UsuariosEmpresario();
                entityUser.IdClientePersonaEmpresa = idCliente;
                entityUser.Nombre = obj.Usuario;
                entityUser.Email = obj.Email;
                entityUser.Contrasena = Utilidades.Encrypt(obj.Password);
                entityUser.IdCreatorby = usuarioConectado;
                entityUser.DateCreated = DateTime.Now;
                entityUser.Activo = true;
                entities.Add(entityUser);
                entities.SaveChanges();

                var usuarioEmpresario = entityUser.Id;
                var entityUserEmpresariorRol = new UsuarioEmpresasriosRol();
                entityUserEmpresariorRol.IdEmpresario = usuarioEmpresarioConectado;
                entityUserEmpresariorRol.IdUsuarioEmpresarios = usuarioEmpresario;
                entityUserEmpresariorRol.IdRol = 3;
                entityUserEmpresariorRol.DateCreated = DateTime.Now;
                entityUserEmpresariorRol.IdCreatorby = usuarioEmpresarioConectado;
                entities.Add(entityUserEmpresariorRol);
                entities.SaveChanges();
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
            var usuarioEmpresarioConectado = entities.Empresarios.Where(e => e.IdClientePersonaEmpresa == Utilidades.GetCurrentUser().ClienteID).Select(e => e.Id).First();
            Cliente entity = new Cliente();
            entity = entities.Clientes.Where(e => e.Id == obj.IdClietne).FirstOrDefault();
            entity.Id = obj.IdClietne;
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
                    IdCreatorby = usuarioEmpresarioConectado,
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
            var entityUser = new UsuariosEmpresario();
            entityUser.IdClientePersonaEmpresa = idCliente;
            entityUser.Nombre = obj.Usuario;
            entityUser.Email = obj.Email;
            entityUser.Contrasena = Utilidades.Encrypt(obj.Password);
            entityUser.IdCreatorby = usuarioEmpresarioConectado;
            entityUser.DateCreated = DateTime.Now;
            entityUser.Activo = true;
            entities.Add(entityUser);
            entities.SaveChanges();
            var usuarioEmpresario = entityUser.Id;
            var entityUserEmpresariorRol = new UsuarioEmpresasriosRol();
            entityUserEmpresariorRol.IdEmpresario = usuarioEmpresarioConectado;
            entityUserEmpresariorRol.IdUsuarioEmpresarios = usuarioEmpresario;
            entityUserEmpresariorRol.IdRol = 3;
            entityUserEmpresariorRol.DateCreated = DateTime.Now;
            entityUserEmpresariorRol.IdCreatorby = usuarioEmpresarioConectado;
            entities.Add(entityUserEmpresariorRol);
            entities.SaveChanges();
            return obj;
        }

        public UsuarioViewModel Update(UsuarioViewModel obj)
        {
            var usuarioEmpresarioConectado = entities.Empresarios.Where(e => e.IdClientePersonaEmpresa == Utilidades.GetCurrentUser().ClienteID).Select(e => e.Id).First();
            Cliente entity = new Cliente();
            entity = entities.Clientes.Where(e => e.Id == obj.IdClietne).FirstOrDefault();
            entity.DateChanged = DateTime.Now;
            entity.IdChangedby = usuarioEmpresarioConectado;
            entity.Id = obj.IdClietne;
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
                    IdCreatorby = usuarioEmpresarioConectado,
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

            var entityUser = new UsuariosEmpresario();
            entityUser = entities.UsuariosEmpresarios.Where(e => e.Id == obj.Id).FirstOrDefault();
            entityUser.IdClientePersonaEmpresa = idCliente;
            entityUser.Nombre = obj.Usuario;
            entityUser.Email = obj.Email;
            entityUser.Contrasena = obj.Password == null ? entityUser.Contrasena : Utilidades.Encrypt(obj.Password);
            entityUser.IdChangedby = usuarioEmpresarioConectado;
            entityUser.DateChanged = DateTime.Now;
            entityUser.Activo = true;
            entities.Add(entityUser);
            entities.SaveChanges();
            var usuarioEmpresario = entityUser.Id;
            var entityUserEmpresariorRol = new UsuarioEmpresasriosRol();
            entityUserEmpresariorRol = entities.UsuarioEmpresasriosRols.Where(e => e.Id == obj.Id).FirstOrDefault();
            entityUserEmpresariorRol.IdEmpresario = usuarioEmpresarioConectado;
            entityUserEmpresariorRol.IdUsuarioEmpresarios = usuarioEmpresario;
            entityUserEmpresariorRol.IdRol = 3;
            entityUserEmpresariorRol.DateChanged = DateTime.Now;
            entityUserEmpresariorRol.IdChangedby = usuarioEmpresarioConectado;
            entities.Add(entityUserEmpresariorRol);
            entities.SaveChanges();

            return obj;
        }
        #endregion

        #region Roles
        //Filtrar con ids los roles que no se quieren mostrar
        public List<RoleViewModel> ReadUsuario(int idUsuario)
        {
            return entities.Roles.Where(c=> c.Id != 1).Select(c => new RoleViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Seleccionado = entities.UsuarioEmpresasriosRols.Where(p => p.IdRol == c.Id && p.IdUsuarioEmpresarios == idUsuario).Count() > 0
            }).ToList();
        }

        public IEnumerable<RoleViewModel> ReadRol()
        {
            return entities.Roles.Select(c => new RoleViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                FechaCreacion = c.Fechacreacion
            });
        }


        #endregion

    }
}