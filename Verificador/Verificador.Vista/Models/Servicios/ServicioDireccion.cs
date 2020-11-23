using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Verificador.OpenAccess;
using Verificador.Vista.Models.ViewModels;

namespace Verificador.Vista.Models.Servicios
{
    public class ServicioDireccion : IDisposable
    {

        private Entities entities;

        public ServicioDireccion()
        {
            this.entities = new Entities();
        }

        public List<DireccionViewModel> getDireccionesByCliente(int cliente_id)
        {
            return entities.DireccionesClientes.Where(e => e.Cliente_id == cliente_id && e.Activo == true).Select(e => new DireccionViewModel
            {
                Id = e.Id,
                IdCliente = (int)e.Cliente_id,
                Direccion = e.Ubicacion,
                CodigoPostal = e.CodigoPostal,
                Ciudad_id = (int)e.Ciudad_id,
                Departamneto_id = e.Ciudade.Iddepartamento,
                Pais_id = e.Ciudade.Departamento.Idpais,
                Latitud = e.Latitud,
                Longitud = e.Longitud,
            }).OrderByDescending(e => e.Id).ToList();
        }

        public void RelacionarDireccion(DireccionViewModel obj, bool nuevo)
        {
            if (nuevo)
            {
                entities.Add(new DireccionesCliente
                {
                    Ubicacion = obj.Direccion,
                    CodigoPostal = obj.CodigoPostal,
                    Ciudad_id = obj.Ciudad_id,
                    Cliente_id = obj.IdCliente,
                    DateCreated = DateTime.Now,
                    IdCreatorby = Utilidades.GetCurrentUser().Id,
                    Latitud = obj.Latitud,
                    Longitud = obj.Longitud,
                    Activo = true
                });
            }
            else
            {
                var records = entities.DireccionesClientes.Where(e => e.Id == obj.Id).FirstOrDefault();
                records.Ubicacion = obj.Direccion;
                records.CodigoPostal = obj.CodigoPostal;
                records.Ciudad_id = obj.Ciudad_id;
                records.Latitud = obj.Latitud;
                records.Longitud = obj.Longitud;
                records.DateChanged = DateTime.Now;
                records.IdChangedby = Utilidades.GetCurrentUser().Id;
            }

            entities.SaveChanges();
        }

        public void EliminarDireccio(DireccionViewModel obj)
        { 
            var records = entities.DireccionesClientes.Where(e => e.Id == obj.Id).FirstOrDefault();
            records.DateRetired= DateTime.Now;
            records.IdRetiredby = Utilidades.GetCurrentUser().Id;
            records.Activo = false;
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }

    }
}