
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
    public class ServicioDepartamento : IDisposable
    {
        private Entities entities;

        public ServicioDepartamento()
        {
            this.entities = new Entities();
        }

        public IEnumerable<DepartamentoViewModel> Read()
        {
            return entities.Departamentos.Select(c => new DepartamentoViewModel
            {
                Id = c.Id,
                Codigo = c.Codigo,
                NombreDepartamento = c.Nombre,
                NombrePais = string.Format("{0} - {1}", c.Paise.Codigo, c.Paise.Nombre),
                IdPais = c.Idpais,
                FechaCreacion = c.Fechacreacion
            });
        }

        public void Create(DepartamentoViewModel obj)
        {
            var entity = new Departamento();

            entity.Codigo = obj.Codigo;
            entity.Nombre = obj.NombreDepartamento;
            entity.Idpais = obj.IdPais.Value;
            entity.Fechacreacion = DateTime.Now;

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(DepartamentoViewModel obj)
        {
            var entity = entities.Departamentos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.NombreDepartamento;
                entity.Idpais = obj.IdPais.Value;
                entity.Codigo = obj.Codigo;
                entities.SaveChanges();
            }
        }


        public void Destroy(DepartamentoViewModel obj)
        {
            var entity = entities.Departamentos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public List<DepartamentoViewModel> ReadDepartamento(string texto)
        {

            var query = entities.Departamentos;


            return (from c in query
                    where (
                        c.Nombre.Contains(texto)
                        )
                    select new DepartamentoViewModel
                    {
                        Id = c.Id,
                        NombreDepartamento = string.Format("{0}, {1}", c.Nombre, c.Paise.Nombre)

                    }
            ).ToList();
        }

        public void Dispose()
        {
            entities.Dispose();
        }


        public List<DepartamentoViewModel> ReadByName(string text)
        {
            return entities.Departamentos.Where(e => e.Nombre.Contains(text)).Select(c => new DepartamentoViewModel
            {
                Id = c.Id,
                Codigo = c.Codigo,
                NombreDepartamento = c.Nombre
            }).ToList();
        }

        public List<DepartamentoViewModel> ReadByNameAndPais(int idpais, string text)
        {
            return entities.Departamentos.Where(e => e.Nombre.Contains(text) && e.Idpais == idpais).Select(c => new DepartamentoViewModel
            {
                Id = c.Id,
                Codigo = c.Codigo,
                NombreDepartamento = c.Nombre
            }).ToList();

        }
    }
}