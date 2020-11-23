using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Verificador.OpenAccess;
using Verificador.Vista.Models.ViewModels;

namespace Verificador.Vista.Models.Servicios
{
    public class ServicioStaff : IDisposable
    {
        private Entities entities;

        public ServicioStaff()
        {
            this.entities = new Entities();
        }

        //Muestra todos o los propios del IdEmpresario
        public List<EventoViewModel> ReadEventos(int? idEmpresario)
        {
            if (idEmpresario.HasValue) 
            {
                return entities.Eventos.Where(c => c.Empresario_id == idEmpresario).Select(c => new EventoViewModel
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    FechaInicial = c.FechaInicial == null ? DateTime.MinValue : (DateTime)c.FechaInicial,
                    FechaFinal = (DateTime)c.FechaFinal,
                    AforoTotal = (int)c.AforoTotal,
                    Descripcion = c.Descripcion,
                    Empresario_id = c.Empresario_id,
                    Venue = entities.LocalidadesVenueEventos.Where(e => e.IdEvento == c.Id).Select(e => new VenueViewModel()
                    {
                        Direccion = e.Venue.Direccion,
                    }).FirstOrDefault()
                }).OrderByDescending(e => e.FechaCreacion).ToList();
            }
            else
            {
                return entities.Eventos.Select(c => new EventoViewModel
                {
                    Id = c.Id,
                    Nombre = c.Nombre,
                    FechaInicial = c.FechaInicial == null ? DateTime.MinValue : (DateTime)c.FechaInicial,
                    FechaFinal = (DateTime)c.FechaFinal,
                    AforoTotal = (int)c.AforoTotal,
                    Descripcion = c.Descripcion,
                    Venue = entities.LocalidadesVenueEventos.Where(e => e.IdEvento == c.Id).Select(e => new VenueViewModel()
                    {
                        Direccion = e.Venue.Direccion,
                    }).FirstOrDefault()
                }).OrderByDescending(e => e.FechaCreacion).ToList();
            }
        }

        public void AsociarStaff(StaffViewModel obj)
        {
            var entity = new StaffEventoEmpresaEmpresario();
            entity.IdEvento = obj.IdEvento;
            entity.IdEmpresario = obj.IdEmpresario;
            entity.IdEmpresa = obj.IdEmpersa;
            entity.IdCliente = obj.IdCliente;
            entity.IdCreatorby = obj.Auditoria.IdCreatorby;
            entity.CodigoUnico = obj.CodigoUnico;
            entity.DateCreated = obj.Auditoria.DateCreated;
            entity.Activo = true;
            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public List<StaffViewModel> ReadStaffEventoEmprearioEmpresa(int idEvento, int idEmpresario, int? idEmpresa)
        {
            List<StaffViewModel> objStaff = new List<StaffViewModel>();
            if (idEmpresa.HasValue)
            {
                objStaff = entities.StaffEventoEmpresaEmpresarios
                    .Where(c => c.IdEvento == idEvento && c.IdEmpresario == idEmpresario && c.IdEmpresa == idEmpresa && c.Activo == true)
                    .Select(c => new StaffViewModel
                    {
                        Id = c.Id,
                        IdCliente = c.IdCliente,
                        IdEmpersa = c.IdEmpresa,
                        IdEmpresario = c.IdEmpresario,
                        IdEvento = c.IdEvento,
                        NombreEmpresa  = c.EmpresasEvento.PrimerNombreRazonSocial,
                        CodigoUnico = c.CodigoUnico,
                        IdentificacionCliente = string.Format("{0} - {1}",c.Cliente.Tipodocumento.Nombre, c.Cliente.Identificacion),
                        NombreCliente = string.Format("{0} {1} {2} {3}", c.Cliente.PrimerNombreRazonSocial, c.Cliente.SegundoNombre, c.Cliente.PrimerApellido, c.Cliente.SegundoApellido)
                    }).OrderBy(c=> c.IdEmpersa).ToList();
            }
            else 
            {
                objStaff = entities.StaffEventoEmpresaEmpresarios
                    .Where(c => c.IdEvento == idEvento && c.IdEmpresario == idEmpresario)
                    .Select(c => new StaffViewModel
                    {
                        Id = c.Id,
                        IdCliente = c.IdCliente,
                        IdEmpersa = c.IdEmpresa,
                        IdEmpresario = c.IdEmpresario,
                        IdEvento = c.IdEvento,
                        NombreEmpresa  = c.EmpresasEvento.PrimerNombreRazonSocial,
                        CodigoUnico = c.CodigoUnico,
                        IdentificacionCliente = string.Format("{0} - {1}",c.Cliente.Tipodocumento.Nombre, c.Cliente.Identificacion),
                        NombreCliente = string.Format("{0}{1} {2}{3}", c.Cliente.PrimerNombreRazonSocial, c.Cliente.SegundoNombre, c.Cliente.PrimerApellido, c.Cliente.SegundoApellido)
                    }).OrderBy(c => c.IdEmpersa).ToList();
            }
            
            return objStaff;
        }

        public void Dispose()
        {
            entities.Dispose();
        }

        public void Destroy(StaffViewModel obj)
        {
            var entity = entities.StaffEventoEmpresaEmpresarios.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.IdRetiredby = Utilidades.GetCurrentUser().Id;
                entity.DateRetired = DateTime.Now;
                entity.Activo = false;
                entities.SaveChanges();
            }
        }
    }
}