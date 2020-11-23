
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
    public class ServicioImpuesto: IDisposable
    {
        private Entities entities;

        public ServicioImpuesto()
        {
            this.entities = new Entities();
        }

        public IEnumerable<ImpuestoViewModel> Read()
        {
            return entities.Impuestos.Select(c => new ImpuestoViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Valor = c.Valor,                
                FechaCreacion = c.Fechacreacion
            });
        }

        public List<ImpuestoViewModel> Read(int idevento)
        {
            return (from ie in entities.ImpuestosEventos
                    join i in entities.Impuestos on ie.Idimpuesto equals i.Id
                    where ie.Idevento == idevento
                    select new ImpuestoViewModel
                    {
                        Id = i.Id,
                        Nombre = i.Nombre,
                        Valor = i.Valor,
                        FechaCreacion = i.Fechacreacion
                    }).ToList();
        }

        public ImpuestoViewModel Create(ImpuestoViewModel obj)
        {
            var entity = new Impuesto();
            entity.Nombre = obj.Nombre;
            entity.Valor = obj.Valor;            

            entity.Fechacreacion = DateTime.Now;            

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
            return obj;
        }

        private void AsociarAlEvento(int evento_id, int id)
        {
            if (entities.ImpuestosEventos.FirstOrDefault(c => c.Idimpuesto == id && c.Idevento == evento_id) == null)
            {
                entities.Add(new ImpuestosEvento
                {
                    Idimpuesto = id,
                    Idevento = evento_id
                });
            }
        }

        public void Update(ImpuestoViewModel obj)
        {
            var entity = entities.Impuestos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;
                entity.Valor = obj.Valor;
                

                entities.SaveChanges();
            }
        }

        public void Destroy(ImpuestoViewModel obj)
        {
            var entity = entities.Impuestos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public void Destroy(int id, int evento_id)
        {
            var entity = entities.ImpuestosEventos.FirstOrDefault(c => c.Idimpuesto == id && c.Idevento == evento_id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public void Save(List<ImpuestoViewModel> impuestos, int evento_id) {
            var enDB = entities.ImpuestosEventos.Where(e => e.Idevento == evento_id);
            if (enDB.Count() > 0)
            {
                var arrayImpuestos = impuestos.Select(o => o.Id).ToArray();
                var Removidos = enDB.Where(e => arrayImpuestos.Contains((int)e.Idimpuesto) == false);
                if (Removidos.Count() > 0)
                {
                    foreach (var row in Removidos)
                    {
                        entities.Delete(row);
                    }
                }
            }
            foreach (var impuesto in impuestos)
            {
                AsociarAlEvento(evento_id, impuesto.Id);
            }
            entities.SaveChanges();
        }

        public static List<ListItem> getOptions()
        {
            return new Entities().Impuestos.Select(c => new ListItem
            {
                Value = c.Id,
                Text = c.Nombre,
                Name = c.Valor.ToString()
            }).ToList();
        }

        public void Dispose()
        {
            entities.Dispose();
        }

    }
}