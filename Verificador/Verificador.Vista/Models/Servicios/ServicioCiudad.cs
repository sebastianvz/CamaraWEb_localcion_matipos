
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
    public class ServicioCiudad: IDisposable
    {
        private Entities entities;

        public ServicioCiudad()
        {
            this.entities = new Entities();
        }

        

        public IEnumerable<CiudadViewModel> Read()
        {
            return entities.Ciudades.Select(c => new CiudadViewModel
            {
                Id = c.Id,
                Codigo = c.Codigo,
                NombreCiudad=c.Nombre,
                NombreDepartamento = string.Format("{0} - {1}", c.Departamento.Codigo, c.Departamento.Nombre),
                NombrePais = string.Format("{0} - {1}", c.Departamento.Paise.Codigo, c.Departamento.Paise.Nombre),
                IdDepartamento = c.Iddepartamento,
                FechaCreacion = c.Fechacreacion                
            });
        }

        public void Create(CiudadViewModel obj)
        {
            var entity = new Ciudade();

            entity.Codigo = obj.Codigo;
            entity.Nombre = obj.NombreCiudad;
            entity.Iddepartamento = obj.IdDepartamento;
            entity.Fechacreacion = DateTime.Now;


            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(CiudadViewModel obj)
        {
            var entity = entities.Ciudades.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Codigo = obj.Codigo;
                entity.Nombre = obj.NombreCiudad;
                entity.Iddepartamento = obj.IdDepartamento;            

                entities.SaveChanges();
            }
        }


        public void Destroy(CiudadViewModel obj)
        {
            var entity = entities.Ciudades.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public string ReadNombreCompletoById(int id)
        {

            var query = entities.Ciudades;


            return (from c in query
                    where (
                        c.Id==id
                        )
                    select string.Format("{0}, {1}, {2}", c.Nombre, c.Departamento.Nombre, c.Departamento.Paise.Nombre)
                    
            ).FirstOrDefault();
        }

        public List<CiudadViewModel> ReadCiudades(string texto)
        {
            var query = entities.Ciudades;
            return (from c in query
                    where (
                        c.Nombre.Contains(formatText(texto))                        
                        )
                    select new CiudadViewModel
                    {
                        Id = c.Id,
                        NombreCiudad = string.Format("{0}, {1}, {2}", c.Nombre, c.Departamento.Nombre, c.Departamento.Paise.Nombre)
                    }
            ).ToList();
        }

        public string formatText(string texto) {
            return texto.Replace("í", "i");
        }

        public void Dispose()
        {
            entities.Dispose();
        }


        public List<CiudadViewModel> ReadByName(string text)
        {
            return entities.Ciudades.Where(e => e.Nombre.Contains(text)).Select(c => new CiudadViewModel
            {
                Id = c.Id,
                Codigo = c.Codigo,
                NombreCiudad = c.Nombre
            }).ToList();
        }

        internal object ReadByDepartamento(string idDepartamento)
        {
            return entities.Ciudades.Where(e => e.Iddepartamento == Convert.ToInt32(idDepartamento)).Select(c => new CiudadViewModel
            {
                Id = c.Id,
                Codigo = c.Codigo,
                NombreCiudad = c.Nombre
            }).ToList();
        }

        public List<CiudadViewModel> ReadByNameAndDepar(string text, int idDepartamento)
        {
            return entities.Ciudades.Where(e => e.Nombre.Contains(text)&& e.Iddepartamento== idDepartamento).Select(c => new CiudadViewModel
            {
                Id = c.Id,
                Codigo = c.Codigo,
                NombreCiudad = c.Nombre
            }).ToList();
        }
    }
}