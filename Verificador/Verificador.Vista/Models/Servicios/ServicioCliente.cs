
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
    public class ServicioCliente : IDisposable
    {
        private Entities entities;

        public ServicioCliente()
        {
            this.entities = new Entities();
        }

        public List<ClienteViewModel> ReadClientes()
        {
            List<ClienteViewModel> clientes = (from c in entities.Clientes
                                               where c.Activo == true
                                               select new ClienteViewModel
                                               {
                                                   Id = c.Id,
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
                                                   EsEmpresa = c.IsEmpresa,
                                                   Genero_id = (c.Genero_id != null) ? c.Genero_id.Value : 0,
                                                   Email = c.Email,
                                                   Celular = c.Celular,
                                                   TelefonoCasa = c.TelefonoCasa,
                                                   TelefonoOficina = c.TelefonoOficina,
                                                   JornadaEntrega = c.JornadaEntrega,
                                                   IdentificacionShowGrid = string.Format("{0}", (c.Digito != null) ? string.Format("{0}-{1}", c.Identificacion, c.Digito) : c.Identificacion),
                                                   NombreCompleto = string.Format("{0} {1} {2} {3}", c.PrimerNombreRazonSocial, c.SegundoNombre, c.PrimerApellido, c.SegundoApellido),
                                                   Direcciones = entities.DireccionesClientes.Where(e => e.Cliente_id == c.Id).Select(e => new DireccionViewModel
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
                                               }).ToList();
            return clientes;
        }

        public string ReadImgbyId(int idCLiente)
        {
            byte[] imagen = entities.Clientes.Where(c => c.Id == idCLiente).Select(c => c.Imagen).FirstOrDefault();
            string strimagen = (imagen != null) ? Convert.ToBase64String(imagen) : "";
            return strimagen;
        }

        public ClienteViewModel Save(ClienteViewModel obj)
        {
            var verficadorCliente = (from c in entities.Clientes
                                     where c.TipoDocumento_id == obj.TipoDocumento_id && c.Identificacion == obj.Identificacion
                                     select c).Count();
            if (verficadorCliente <= 0)
            {
                Cliente entity = new Cliente();
                entity.DateCreated = obj.Auditoria.DateCreated;
                entity.IdCreatorby = obj.Auditoria.IdCreatorby;
                bool nuevo = false;
                entity.Id = obj.Id;
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
                            DateCreated = obj.Auditoria.DateCreated,
                            IdCreatorby = obj.Auditoria.IdCreatorby,
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

        public ClienteViewModel Update(ClienteViewModel obj)
        {
            Cliente entity = new Cliente();
            entity = entities.Clientes.Where(e => e.Id == obj.Id).FirstOrDefault();
            entity.DateChanged = obj.Auditoria.DateChanged;
            entity.IdChangedby = obj.Auditoria.IdChangedby;
            entity.Id = obj.Id;
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
                obj.RazonSocial = (obj.RazonSocial == null) ? obj.PrimerNombres : obj.RazonSocial;
                entity.PrimerNombreRazonSocial = obj.RazonSocial;
                entity.Digito = obj.Digito;
            }
            entity.Email = obj.Email;
            entity.Celular = obj.Celular;
            entity.TelefonoCasa = obj.TelefonoCasa;
            entity.TelefonoOficina = obj.TelefonoOficina;
            entity.JornadaEntrega = obj.JornadaEntrega;
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
                    DateCreated = obj.Auditoria.DateCreated,
                    Cliente_id = idCliente,
                    IdCreatorby = obj.Auditoria.IdCreatorby,
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
            return obj;
        }

        public void Destroy(ClienteViewModel obj)
        {
            var entity = entities.Clientes.FirstOrDefault(c => c.Id == obj.Id);
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

        public ClienteViewModel ReadByIdenTipo(int tipoIden, string ident)
        {
            ClienteViewModel User = (from c in entities.Clientes
                                     where (c.Identificacion == ident && c.TipoDocumento_id == tipoIden && c.Activo == true)
                                     select new ClienteViewModel
                                     {
                                         Id = c.Id,
                                         TipoPersona_id = c.TipoPersona_id,
                                         NombreTipoPersona = c.Tipopersona.Nombre,
                                         TipoDocumento_id = c.TipoDocumento_id,
                                         NombreTipoDocumento = c.Tipodocumento.Nombre,
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
                                         Email = c.Email,
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

    }
}