
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
    public class ServicioFuncion: IDisposable
    {
        private Entities entities;

        public ServicioFuncion()
        {
            this.entities = new Entities();
        }

        public List<FuncionesEventoViewModel> Read(int evento_id)
        {
            return entities.FuncionesEventos.Where(e => e.Idevento == evento_id).Select(e => new FuncionesEventoViewModel
            {
                Id = e.Id,
                Evento_id = (int)e.Idevento,
                FechaInicial = (DateTime)e.FechaIncial,
                FechaFinal = (DateTime)e.FechaFinal,
                Descripcion = e.Descripcion,
                Label = e.Label,
                FechaCreacion = (DateTime)e.FechaCreacion,
                AperturaPuertas = (DateTime)e.AperturaPuertas
            }).ToList();
        }

        public IEnumerable<FuncionesEventoViewModel> Read()
        {
            return entities.FuncionesEventos.Select(e => new FuncionesEventoViewModel
            {
                Id = e.Id,
                Evento_id = (int)e.Idevento,
                FechaInicial = (DateTime)e.FechaIncial,
                FechaFinal = (DateTime)e.FechaFinal,
                Descripcion = e.Descripcion,
                Label = e.Label,
                FechaCreacion = (DateTime)e.FechaCreacion,
                AperturaPuertas = (DateTime)e.AperturaPuertas
            });
        }

        public FuncionesEventoViewModel Guardar(FuncionesEventoViewModel obj)
        {
            var entity = new FuncionesEvento();
            if (obj.Id > 0)
            {
                entity = entities.FuncionesEventos.Where(e => e.Id == obj.Id).FirstOrDefault();
            }
            entity.Label = obj.Label;
            entity.FechaIncial = obj.FechaInicial;
            entity.FechaFinal = obj.FechaFinal;
            entity.Descripcion = obj.Descripcion;
            entity.AperturaPuertas = obj.AperturaPuertas;
            if (obj.Id <= 0)
            {
                entity.FechaCreacion = DateTime.Now;
                entity.Idevento = obj.Evento_id;
                entities.Add(entity);
            }
            entities.SaveChanges();
            obj.Id = entity.Id;
            return obj;
        }

        public void Destroy(int id)
        {
            var entity = entities.FuncionesEventos.FirstOrDefault(c => c.Id == id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public static List<ListItem> getOptions(int evento_id)
        {
            return new Entities().FuncionesEventos.Where(e=>e.Idevento == evento_id).Select(c => new ListItem
            {
                Value = c.Id,
                Text = c.Label
            }).ToList();
        }

        public void Save(List<FuncionesEventoViewModel> funciones, int evento_id)
        {   
            foreach (var item in funciones)
            {
                item.Evento_id = evento_id;
                Guardar(item);
            }
            var actuales = funciones.Where(c => c.Id > 0).ToList();
            foreach (var item in entities.FuncionesEventos.Where(c => c.Idevento == evento_id).ToList())
            {
                var updates = actuales.FirstOrDefault(c => c.Id == item.Id);
                if (updates == null)
                {
                    entities.Delete(item);
                }
            }
            entities.SaveChanges();
        }

        public void Dispose()
        {
            entities.Dispose();
        }

    }
}