
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
    public class ServicioVenue : IDisposable
    {
        private Entities entities;

        public ServicioVenue()
        {
            this.entities = new Entities();
        }

        public List<VenueViewModel> Read(string name)
        {
            return entities.Venues.Where(e => e.Nombre.Contains(name)).Select(c => new VenueViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Direccion = c.Direccion,
                Ciudad_id = (int)(c.Ciudad_id != null ? c.Ciudad_id : 0),
                Departamneto_id = c.Ciudade != null ? c.Ciudade.Iddepartamento : 0,
                Pais_id = c.Ciudade != null ? c.Ciudade.Departamento.Idpais : 0,
                Longitud = c.Longitud,
                Latitud = c.Latitud,
                Telefono = c.Telefono,
                E_mail = c.Email,
                CityName = c.CityName,
                DepartametName = c.DepartametName,
                CountryName = c.CountryName,
                PostalCode = c.PostalCode,
                GeoCode = c.GeoCode,
                Aforo = (int)(c.Aforo == null ? c.Aforo : 0)
            }).ToList();
        }

        public int Save(VenueViewModel obj)
        {
            Venue item = new Venue();
            if (obj.Id > 0)
            {
                item = entities.Venues.FirstOrDefault(e => e.Id == obj.Id);
            }
            item.Nombre = obj.Nombre;
            item.Direccion = obj.Direccion;
            item.Ciudad_id = obj.Ciudad_id;
            item.Telefono = obj.Telefono;
            item.Email = obj.E_mail;
            item.Foto = obj.Foto;
            item.Foto = obj.Foto;
            item.Aforo = obj.Aforo;
            item.Latitud = obj.Latitud;
            item.Longitud = obj.Longitud;
            item.CityName = obj.CityName;
            item.DepartametName = obj.DepartametName;
            item.CountryName = obj.CountryName;
            item.PostalCode = obj.PostalCode;
            item.GeoCode = obj.GeoCode;
            if (obj.Id <= 0)
            {
                obj.FechaCreacion = DateTime.Now;
                entities.Add(item);
            }
            entities.SaveChanges();
            return item.Id;
        }

        public void Dispose()
        {
            entities.Dispose();
        }


        internal VenueViewModel Read(int idEvento)
        {
            return entities.LocalidadesVenueEventos.Where(e => e.IdEvento == idEvento).Select(c => new VenueViewModel
            {
                Id = c.Venue.Id,
                Nombre = c.Venue.Nombre,
                Direccion = c.Venue.Direccion,
                Ciudad_id = (int)(c.Venue.Ciudad_id != null ? c.Venue.Ciudad_id : 0),
                Departamneto_id = c.Venue.Ciudade != null ? c.Venue.Ciudade.Iddepartamento : 0,
                Pais_id = c.Venue.Ciudade != null ? c.Venue.Ciudade.Departamento.Idpais : 0,
                Longitud = c.Venue.Longitud,
                Latitud = c.Venue.Latitud,
                Telefono = c.Venue.Telefono,
                E_mail = c.Venue.Email,
                CityName = c.Venue.CityName,
                DepartametName = c.Venue.DepartametName,
                CountryName = c.Venue.CountryName,
                PostalCode = c.Venue.PostalCode,
                GeoCode = c.Venue.GeoCode,
                Aforo = (int)(c.Venue.Aforo == null ? c.Venue.Aforo : 0)
            }).FirstOrDefault();
        }
    }
}