using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Verificador.OpenAccess;
using Verificador.Vista.Models.ViewModels;

namespace Verificador.Vista.Models.Servicios
{
    public class ServicioEmpresaEvento
    {
        private Entities entities;

        public ServicioEmpresaEvento()
        {
            this.entities = new Entities();
        }

        public IEnumerable<EmpresaEventosViewModel> Read()
        {
            return entities.EmpresasEventos.Where(c=>c.Activo ==true).Select(c => new EmpresaEventosViewModel
            {
                Id = c.Id,
                TipoPersona_id = c.TipoPersona_id,
                NombreTipoPersona = c.Tipopersona.Nombre,
                TipoDocumento_id = c.TipoDocumento_id,
                NombreTipoDocumento = c.Tipodocumento.Nombre,
                Identificacion =c.Identificacion,
                Digito = c.Digito== null ? c.Digito : c.Digito.Value,
                NombreRazonSocial =c.PrimerNombreRazonSocial,
                Email =c.Email,
                Telefono =c.Telefono,
                Celular =c.Celular,
                Descripcion = string.Format("{0} {1} {2}", c.Tipodocumento.Nombre, c.Digito != null ? string.Format("{0}-{1}", c.Identificacion, c.Digito) : c.Identificacion, c.PrimerNombreRazonSocial)
            });
        }

        public void Create(EmpresaEventosViewModel obj)
        {
            var entity = new EmpresasEvento();
            entity.TipoPersona_id = obj.TipoPersona_id;
            entity.TipoDocumento_id = obj.TipoDocumento_id;
            entity.Identificacion = obj.Identificacion;
            entity.Digito = obj.Digito != null ? obj.Digito.Value: obj.Digito ;
            entity.PrimerNombreRazonSocial = obj.NombreRazonSocial;
            entity.Email = obj.Email;
            entity.Telefono = obj.Telefono;
            entity.Celular = obj.Celular;
            entity.DateCreated = obj.Auditoria.DateCreated;
            entity.IdCreatorby = obj.Auditoria.IdCreatorby;
            entity.Activo = obj.Auditoria.activo; ;
            entities.Add(entity);
            entities.SaveChanges();
        }

        public void Update(EmpresaEventosViewModel obj)
        {
            var entity = entities.EmpresasEventos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.TipoPersona_id = obj.TipoPersona_id;
                entity.TipoDocumento_id = obj.TipoDocumento_id;
                entity.Identificacion = obj.Identificacion;
                entity.Digito = obj.Digito;
                entity.PrimerNombreRazonSocial = obj.NombreRazonSocial;
                entity.Email = obj.Email;
                entity.Telefono = obj.Telefono;
                entity.Celular = obj.Celular;
                entity.DateChanged = obj.Auditoria.DateChanged;
                entity.IdChangedby = obj.Auditoria.IdChangedby;
                entity.Activo = true;
                entities.SaveChanges();
            }
        }

        public void Destroy(EmpresaEventosViewModel obj)
        {
            var entity = entities.EmpresasEventos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.DateRetired = obj.Auditoria.DateRetired;
                entity.IdRetiredby = obj.Auditoria.IdRetiredby;
                entity.Activo = obj.Auditoria.activo;
                entities.SaveChanges();
            }
        }

        public List<ListItem> ReadEmpresasByText(string texto)
        {
            return new Entities().EmpresasEventos.Where(c=>c.PrimerNombreRazonSocial.Contains(texto)).Select(c => new ListItem
            {
                Value = c.Id,
                Text  = string.Format("{0} {1} {2}", c.Tipodocumento.Nombre, c.Digito != null ? string.Format("{0}-{1}", c.Identificacion, c.Digito) : c.Identificacion, c.PrimerNombreRazonSocial)
            }).ToList();
        }

        public static List<ListItem> getOptions()
        {
            return new Entities().EmpresasEventos.Select(c => new ListItem
            {
                Value = c.Id,
                Text = string.Format("{0}{1}{2} - {3}", c.Tipodocumento.Nombre, c.Identificacion, c.Digito, c.PrimerNombreRazonSocial)
            }).ToList();
        }

    }
}