
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
    public class ServicioEmpresarios : IDisposable
    {
        private Entities entities;

        public ServicioEmpresarios()
        {
            this.entities = new Entities();
        }

        //Leer todos los empresarios
        public List<EmpresarioViewModel> ReadEmpresarios()
        {
            List<EmpresarioViewModel> resultado = (from c in entities.Empresarios
                                                   where c.Activo == true
                                                   select new EmpresarioViewModel
                                                   {
                                                       EmpresarioId = c.Id,
                                                       EmpresarioPulet = c.Pulet,
                                                       EmpresarioEmailNotificacion = c.EmailNotificacion,
                                                       NumeroRegistroArtesEscenicas = c.NumeroRegistroArtesEscenicas,
                                                       NombreEstablecimeinto = c.NombreEstableciemiento,
                                                       EmpresarioTipoDocumento_idRL = c.TipoDocumentoRL_id.Value,
                                                       EmpresarioNombreTipoDocumentoRL = c.Tipodocumento.Nombre,
                                                       EmpresarioIdentificacionRL = c.IdentificacionRL,
                                                       EmpresarioPrimerNombreRL = c.NombreRL,
                                                       EmpresarioSegundoNombreRL = c.SegundoNombreRl,
                                                       EmpresarioPrimerApellidosRL = c.ApellidosRL,
                                                       EmpresarioSegundoApellidosRL = c.SegindoApellidoRL,
                                                       EmpresarioCelularRL = c.CelularRL,
                                                       EmpresarioEmailRL = c.EmailRL,
                                                       EmpresarioGenero_idRL = c.Sexo_idRL.Value,
                                                       EmpresarioFechaNacimientoRL = c.FechaNacimientoRL,
                                                       ClienteId = c.Cliente.Id,
                                                       TipoPersona_id = c.Cliente.TipoPersona_id,
                                                       NombreTipoPersona = c.Cliente.Tipopersona.Nombre,
                                                       TipoDocumento_id = c.Cliente.TipoDocumento_id,
                                                       NombreTipoDocumento = c.Cliente.Tipodocumento.Nombre,
                                                       Identificacion = c.Cliente.Identificacion,
                                                       Digito = (c.Cliente.Digito == null) ? c.Cliente.Digito : c.Cliente.Digito.Value,
                                                       PrimerNombres = c.Cliente.PrimerNombreRazonSocial,
                                                       PrimerApellido = c.Cliente.PrimerApellido,
                                                       SegundoNombres = c.Cliente.SegundoNombre,
                                                       SegundoApellido = c.Cliente.SegundoApellido,
                                                       RazonSocial = c.Cliente.PrimerNombreRazonSocial,
                                                       FechaNacimiento = c.Cliente.FechaNacimiento,
                                                       EsEmpresa = c.Cliente.IsEmpresa,
                                                       Genero_id = (c.Cliente.Genero_id != null) ? c.Cliente.Genero_id.Value : 0,
                                                       Email = c.Cliente.Email,
                                                       Celular = c.Cliente.Celular,
                                                       TelefonoCasa = c.Cliente.TelefonoCasa,
                                                       TelefonoOficina = c.Cliente.TelefonoOficina,
                                                       IdentificacionShowGrid = string.Format("{0}", (c.Cliente.Digito != null) ? string.Format("{0}-{1}", c.Cliente.Identificacion, c.Cliente.Digito) : c.Cliente.Identificacion),
                                                       NombreCompleto = string.Format("{0} {1} {2} {3}", c.Cliente.PrimerNombreRazonSocial, c.Cliente.SegundoNombre, c.Cliente.PrimerApellido, c.Cliente.SegundoApellido),
                                                       Direcciones = entities.DireccionesClientes.Where(d => d.Cliente_id == c.Cliente.Id).Select(d => new DireccionViewModel
                                                       {
                                                           Id = d.Id,
                                                           IdCliente = (int)d.Cliente_id,
                                                           Direccion = d.Ubicacion,
                                                           CodigoPostal = d.CodigoPostal,
                                                           Ciudad_id = (int)d.Ciudad_id,
                                                           Departamneto_id = d.Ciudade.Iddepartamento,
                                                           Pais_id = d.Ciudade.Departamento.Idpais,
                                                           Latitud = d.Latitud,
                                                           Longitud = d.Longitud
                                                       }).OrderByDescending(d => d.Id).ToList(),
                                                       UsuarioEmpresarioId = (from u in entities.UsuariosEmpresarios where u.IdClientePersonaEmpresa == c.Cliente.Id select u.Id).FirstOrDefault(),
                                                       UserEmpresario = (from u in entities.UsuariosEmpresarios where u.IdClientePersonaEmpresa == c.Cliente.Id select u.Nombre).FirstOrDefault(),
                                                       UserEmailEmpresario = (from u in entities.UsuariosEmpresarios where u.IdClientePersonaEmpresa == c.Cliente.Id select u.Email).FirstOrDefault(),
                                                       //UsuarioEmpresarioRolId = (from u in entities.UsuarioEmpresasriosRols where u.IdEmpresario == c.Id && u.IdUsuarioEmpresarios == u.UsuariosEmpresario.Id select u.Id).FirstOrDefault()
                                                   }).ToList();


            return resultado;
        }

        //Buscar un Cliente 
        public EmpresarioViewModel ReadByIdenCliente(int tipoIden, string ident)
        {
            EmpresarioViewModel cliente = (from c in entities.Clientes
                                           where (c.Identificacion == ident && c.TipoDocumento_id == tipoIden && c.Activo == true)
                                           select new EmpresarioViewModel
                                           {
                                               ClienteId = c.Id,
                                               TipoPersona_id = c.TipoPersona_id,
                                               NombreTipoPersona = c.Tipopersona.Nombre,
                                               TipoDocumento_id = c.TipoDocumento_id,
                                               NombreTipoDocumento = c.Tipodocumento.Nombre,
                                               Identificacion = c.Identificacion,
                                               Digito = (c.Digito == null) ? c.Digito : c.Digito.Value,
                                               PrimerNombres = c.PrimerNombreRazonSocial,
                                               PrimerApellido = c.PrimerApellido,
                                               SegundoNombres = c.SegundoNombre,
                                               SegundoApellido = c.SegundoApellido,
                                               FechaNacimiento = c.FechaNacimiento,
                                               RazonSocial = c.PrimerNombreRazonSocial,
                                               EsEmpresa = c.IsEmpresa,
                                               Genero_id = (c.Genero_id != null) ? c.Genero_id.Value : 0,
                                               Email = c.Email,
                                               Celular = c.Celular,
                                               TelefonoCasa = c.TelefonoCasa,
                                               TelefonoOficina = c.TelefonoOficina,
                                               Direcciones = entities.DireccionesClientes.Where(d => d.Cliente_id == c.Id).Select(d => new DireccionViewModel
                                               {
                                                   Id = d.Id,
                                                   IdCliente = (int)d.Cliente_id,
                                                   Direccion = d.Ubicacion,
                                                   CodigoPostal = d.CodigoPostal,
                                                   Ciudad_id = (int)d.Ciudad_id,
                                                   Departamneto_id = d.Ciudade.Iddepartamento,
                                                   Pais_id = d.Ciudade.Departamento.Idpais,
                                                   Latitud = d.Latitud,
                                                   Longitud = d.Longitud
                                               }).OrderByDescending(d => d.Id).ToList()
                                           }).FirstOrDefault();
            return cliente;
        }

        //Buscar un Cliente
        public EmpresarioViewModel ReadByIdCliente(int idCliente)
        {
            EmpresarioViewModel cliente = (from c in entities.Clientes
                                           where (c.Id == idCliente && c.Activo == true)
                                           select new EmpresarioViewModel
                                           {
                                               ClienteId = c.Id,
                                               TipoPersona_id = c.TipoPersona_id,
                                               NombreTipoPersona = c.Tipopersona.Nombre,
                                               TipoDocumento_id = c.TipoDocumento_id,
                                               NombreTipoDocumento = c.Tipodocumento.Nombre,
                                               Identificacion = c.Identificacion,
                                               Digito = (c.Digito == null) ? c.Digito : c.Digito.Value,
                                               PrimerNombres = c.PrimerNombreRazonSocial,
                                               PrimerApellido = c.PrimerApellido,
                                               SegundoNombres = c.SegundoNombre,
                                               SegundoApellido = c.SegundoApellido,
                                               FechaNacimiento = c.FechaNacimiento,
                                               RazonSocial = c.PrimerNombreRazonSocial,
                                               EsEmpresa = c.IsEmpresa,
                                               Genero_id = (c.Genero_id != null) ? c.Genero_id.Value : 0,
                                               Email = c.Email,
                                               Celular = c.Celular,
                                               TelefonoCasa = c.TelefonoCasa,
                                               TelefonoOficina = c.TelefonoOficina,
                                               Direcciones = entities.DireccionesClientes.Where(d => d.Cliente_id == c.Id).Select(d => new DireccionViewModel
                                               {
                                                   Id = d.Id,
                                                   IdCliente = (int)d.Cliente_id,
                                                   Direccion = d.Ubicacion,
                                                   CodigoPostal = d.CodigoPostal,
                                                   Ciudad_id = (int)d.Ciudad_id,
                                                   Departamneto_id = d.Ciudade.Iddepartamento,
                                                   Pais_id = d.Ciudade.Departamento.Idpais,
                                                   Latitud = d.Latitud,
                                                   Longitud = d.Longitud
                                               }).OrderByDescending(d => d.Id).ToList()
                                           }).FirstOrDefault();
            return cliente;
        }

        //Guardar Empresario
        public EmpresarioViewModel SaveEmpresario(EmpresarioViewModel obj)
        {
            try
            {
                if (obj.ClienteId == 0)
                {
                    Cliente entity = new Cliente();
                    entity.DateCreated = obj.Auditoria.DateCreated;
                    entity.IdCreatorby = obj.Auditoria.IdCreatorby;
                    bool nuevo = false;
                    entity.Id = obj.ClienteId;
                    entity.TipoPersona_id = obj.TipoPersona_id;
                    entity.TipoDocumento_id = obj.TipoDocumento_id;
                    entity.Identificacion = obj.Identificacion;
                    entity.SegundoNombre = obj.SegundoNombres;
                    entity.PrimerApellido = obj.PrimerApellido;
                    entity.SegundoApellido = obj.SegundoApellido;
                    if (obj.TipoDocumento_id != 2)
                    {
                        entity.FechaNacimiento = obj.FechaNacimiento.Value;
                        entity.Genero_id = obj.Genero_id;
                        entity.PrimerNombreRazonSocial = obj.PrimerNombres;
                    }
                    else
                    {
                        entity.PrimerNombreRazonSocial = obj.RazonSocial;
                        entity.Digito = obj.Digito;
                    }
                    entity.Email = obj.Email;
                    entity.Celular = obj.Celular;
                    entity.TelefonoCasa = obj.TelefonoCasa;
                    entity.TelefonoOficina = obj.TelefonoOficina;
                    entity.JornadaEntrega = obj.JornadaEntrega;
                    entity.Imagen = obj.imagen;
                    entity.IsEmpresa = obj.EsEmpresa;
                    entity.Activo = true;
                    if (obj.ClienteId == 0)
                    {
                        nuevo = true;
                        entities.Add(entity);
                    }
                    entities.SaveChanges();
                    obj.ClienteId = entity.Id;
                    var idCliente = obj.ClienteId;
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
                                DateCreated = obj.Auditoria.DateCreated,
                                IdCreatorby = obj.Auditoria.IdCreatorby,
                                Latitud = item.Latitud,
                                Longitud = item.Longitud,
                                Activo = true
                            });
                            entities.SaveChanges();
                        }
                    }
                    else
                    {
                        obj = null;
                    }
                }
                
                Empresario entityEmpresario = new Empresario();
                entityEmpresario.IdCreatorby = obj.Auditoria.IdCreatorby;
                entityEmpresario.DateCreated = obj.Auditoria.DateCreated;
                entityEmpresario.IdClientePersonaEmpresa = obj.ClienteId;
                entityEmpresario.NumeroRegistroArtesEscenicas = obj.NumeroRegistroArtesEscenicas;
                entityEmpresario.Pulet = obj.EmpresarioPulet;
                entityEmpresario.TipoDocumentoRL_id = obj.EmpresarioTipoDocumento_idRL;
                entityEmpresario.IdentificacionRL = obj.EmpresarioIdentificacionRL;
                entityEmpresario.NombreRL = obj.EmpresarioPrimerNombreRL;
                entityEmpresario.SegundoNombreRl = obj.EmpresarioSegundoNombreRL;
                entityEmpresario.ApellidosRL = obj.EmpresarioPrimerApellidosRL;
                entityEmpresario.SegindoApellidoRL = obj.EmpresarioSegundoApellidosRL;
                entityEmpresario.Sexo_idRL = obj.EmpresarioGenero_idRL;
                entityEmpresario.FechaNacimientoRL = obj.EmpresarioFechaNacimientoRL;
                entityEmpresario.EmailRL = obj.EmpresarioEmailRL;
                entityEmpresario.CelularRL = obj.EmpresarioCelularRL;
                entityEmpresario.EmailNotificacion = obj.EmpresarioEmailNotificacion;
                entityEmpresario.NombreEstableciemiento = obj.NombreEstablecimeinto;
                entityEmpresario.Activo = true;
                entities.Add(entityEmpresario);
                entities.SaveChanges();
                obj.EmpresarioId = entityEmpresario.Id;
                UsuariosEmpresario entityUsuario = new UsuariosEmpresario();
                entityUsuario.IdClientePersonaEmpresa = obj.ClienteId;
                entityUsuario.Nombre = obj.UserEmpresario;
                entityUsuario.Email = obj.UserEmailEmpresario;
                entityUsuario.Contrasena = Utilidades.Encrypt(obj.PasswordEmpresaio);
                entityUsuario.IdCreatorby = obj.Auditoria.IdCreatorby;
                entityUsuario.DateCreated = obj.Auditoria.DateCreated;
                entityUsuario.Activo = true;
                entities.Add(entityUsuario);
                entities.SaveChanges();
                obj.UsuarioEmpresarioId = entityUsuario.Id;
                UsuarioEmpresasriosRol entityUsuarioEmpresarioRol = new UsuarioEmpresasriosRol();
                entityUsuarioEmpresarioRol.IdEmpresario = obj.EmpresarioId;
                entityUsuarioEmpresarioRol.IdUsuarioEmpresarios = obj.UsuarioEmpresarioId;
                entityUsuarioEmpresarioRol.IdRol = 2;
                entityUsuarioEmpresarioRol.IdCreatorby = obj.Auditoria.IdCreatorby;
                entityUsuarioEmpresarioRol.DateCreated = obj.Auditoria.DateCreated;
                entityUsuarioEmpresarioRol.Activo = true;
                entities.Add(entityUsuarioEmpresarioRol);
                entities.SaveChanges();
                return obj;
            }
            catch (Exception err)
            {
                var message = err.Message;
                return null;
            }
        }

        //Actualizar Empresario
        public EmpresarioViewModel Update(EmpresarioViewModel obj)
        {
            try
            {
                Cliente entity = new Cliente();
                entity = entities.Clientes.Where(e => e.Id == obj.ClienteId).FirstOrDefault();
                entity.DateChanged = obj.Auditoria.DateChanged;
                entity.IdChangedby = obj.Auditoria.IdChangedby;
                entity.Id = obj.ClienteId;
                entity.TipoPersona_id = obj.TipoPersona_id;
                entity.TipoDocumento_id = obj.TipoDocumento_id;
                entity.Identificacion = obj.Identificacion;
                if (obj.TipoDocumento_id == 2)
                {
                    entity.Digito = obj.Digito;
                    entity.PrimerNombreRazonSocial = obj.RazonSocial;
                }
                else
                {
                    entity.Imagen = obj.imagen !=null? obj.imagen: null;
                }
                
                entity.Email = obj.Email;
                entity.Celular = obj.Celular;
                entity.TelefonoCasa = obj.TelefonoCasa;
                entity.TelefonoOficina = obj.TelefonoOficina;
                entities.SaveChanges();
                obj.ClienteId = entity.Id;
                var records = entities.DireccionesClientes.Where(e => e.Cliente_id == obj.ClienteId).ToList();
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
                        DateCreated = obj.Auditoria.DateChanged,
                        Cliente_id = obj.ClienteId,
                        IdCreatorby = obj.Auditoria.IdChangedby,
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
                entities.SaveChanges();
                Empresario entityEmpresario = new Empresario();
                entityEmpresario = entities.Empresarios.Where(e => e.Id == obj.EmpresarioId).FirstOrDefault();
                entityEmpresario.IdChangedby = obj.Auditoria.IdChangedby;
                entityEmpresario.DateChanged = obj.Auditoria.DateChanged;
                entityEmpresario.IdClientePersonaEmpresa = obj.ClienteId;
                entityEmpresario.NumeroRegistroArtesEscenicas = obj.NumeroRegistroArtesEscenicas;
                entityEmpresario.NombreEstableciemiento = obj.NombreEstablecimeinto;
                entityEmpresario.Pulet = obj.EmpresarioPulet;
                entityEmpresario.TipoDocumentoRL_id = obj.EmpresarioTipoDocumento_idRL;
                entityEmpresario.IdentificacionRL = obj.EmpresarioIdentificacionRL;
                entityEmpresario.NombreRL = obj.EmpresarioPrimerNombreRL;
                entityEmpresario.SegundoNombreRl = obj.EmpresarioSegundoNombreRL;
                entityEmpresario.ApellidosRL = obj.EmpresarioPrimerApellidosRL;
                entityEmpresario.SegindoApellidoRL = obj.EmpresarioSegundoApellidosRL;
                entityEmpresario.Sexo_idRL = obj.EmpresarioGenero_idRL;
                entityEmpresario.FechaNacimientoRL = obj.EmpresarioFechaNacimientoRL;
                entityEmpresario.EmailRL = obj.EmpresarioEmailRL;
                entityEmpresario.CelularRL = obj.EmpresarioCelularRL;
                entityEmpresario.EmailNotificacion = obj.EmpresarioEmailNotificacion;
                entityEmpresario.Activo = true;
                entities.SaveChanges();
                obj.EmpresarioId = entityEmpresario.Id;
                UsuariosEmpresario entityUsuario = new UsuariosEmpresario();
                entityUsuario = entities.UsuariosEmpresarios.Where(e => e.Id == obj.UsuarioEmpresarioId).FirstOrDefault();
                entityUsuario.IdClientePersonaEmpresa = obj.ClienteId;
                entityUsuario.Nombre = obj.UserEmpresario;
                entityUsuario.Email = obj.UserEmailEmpresario;
                entityUsuario.Contrasena = obj.PasswordEmpresaio == null ? entityUsuario.Contrasena : Utilidades.Encrypt(obj.PasswordEmpresaio);
                entityUsuario.IdChangedby = obj.Auditoria.IdChangedby;
                entityUsuario.DateChanged = obj.Auditoria.DateChanged;
                entityUsuario.Activo = true;
                entities.SaveChanges();
                obj.UsuarioEmpresarioId = entityUsuario.Id;
                return obj;
            }
            catch (Exception err)
            {
                var message = err.Message;
                return null;
            }
        }

        //Cambiar estado del empresario
        public void Destroy(EmpresarioViewModel obj)
        {
            try
            {
                var entityEmpresario = entities.Empresarios.FirstOrDefault(c => c.Id == obj.EmpresarioId);
                entityEmpresario.Activo = false;
                entityEmpresario.IdRetiredby = obj.Auditoria.IdRetiredby;
                entityEmpresario.DateRetired = obj.Auditoria.DateRetired;
                var entityUsuarioEmpresario = entities.UsuariosEmpresarios.FirstOrDefault(c => c.IdClientePersonaEmpresa == obj.ClienteId);
                entityUsuarioEmpresario.Activo = false;
                entityUsuarioEmpresario.IdRetiredby = obj.Auditoria.IdRetiredby;
                entityUsuarioEmpresario.DateRetired = obj.Auditoria.DateRetired;
                var entityUsuarioEmpresarioRol = entities.UsuarioEmpresasriosRols.FirstOrDefault(c => c.IdUsuarioEmpresarios == entityUsuarioEmpresario.Id);
                entityUsuarioEmpresarioRol.Activo = false;
                entityUsuarioEmpresarioRol.IdRetiredby = obj.Auditoria.IdRetiredby;
                entityUsuarioEmpresarioRol.DateRetired = obj.Auditoria.DateRetired;
                entities.SaveChanges();
                
            }
            catch (Exception err)
            {
                var message = err.Message;
            }


        }

        public void Dispose()
        {
            entities.Dispose();
        }

        
        public UsuarioViewModel ObtenerUsuarioPorEmail(string email)
        {
            var obj = entities.UsuariosEmpresarios.FirstOrDefault(c => c.Email == email);
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

        public void ChangePassword(string username, string password)
        {
            var entity = entities.UsuariosEmpresarios.FirstOrDefault(c => c.Nombre == username);
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

        public static List<ListItem> getOptions()
        {
            return new Entities().Empresarios.Where(e => e.Activo == true).Select(c => new ListItem
            {
                Value = c.Id,
                Text = c.Cliente.PrimerNombreRazonSocial
            }).ToList();
        }

        public static List<ListItem> getOptonsVentasWeb()
        {
            var options = new List<ListItem>();
            options.Add(new ListItem() { Name = "No", Text = "No" });
            options.Add(new ListItem() { Name = "Si", Text = "Sí" });
            return options;
        }

        //Validar que usuario y email sean unicos
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

        public int ObtenerEmpresarioXClienteId(int clienteId)
        {
            return entities.Empresarios.Where(c => c.IdClientePersonaEmpresa == clienteId).First().Id;
        }


    }
}