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
    public class ServicioTipoBoletoPapel : IDisposable
    {
        private Entities entities;

        public ServicioTipoBoletoPapel()
        {
            this.entities = new Entities();
        }

        public static List<ListItem> getOptions()
        {
            return new Entities().TipoBoletosPapels.Select(c => new ListItem
            {
                Value = c.Id,
                Text = string.Format("{0}x{1} - {2}",c.AnchoDefectoCentimetros, c.AltoDefectocCentimetros,c.Nombre) 
            }).ToList();
        }

        public IEnumerable<TipoBoletosPapelViewModel> Read()
        {
            return entities.TipoBoletosPapels.Where(c=> c.Activo == true).Select(c => new TipoBoletosPapelViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                AnchoCmm = c.AnchoDefectoCentimetros,
                AltoCmm = c.AltoDefectocCentimetros,
                Imagen = c.ImagenPapel
            });
        }

        public void Create(TipoBoletosPapelViewModel obj)
        {
            var entity = new TipoBoletosPapel();
            try
            {
                entity.Nombre = obj.Nombre;
                entity.AltoDefectocCentimetros = obj.AltoCmm;
                entity.AnchoDefectoCentimetros = obj.AnchoCmm;
                entity.ImagenPapel = obj.Imagen;
                entity.IdCreatorby = obj.Auditoria.IdCreatorby;
                entity.DateCreated = obj.Auditoria.DateCreated;
                entity.Activo = true;
                entities.Add(entity);
                entities.SaveChanges();
            }
            catch (Exception err)
            {

                err.Message.ToString();
            }
            
        }

        public void Update(TipoBoletosPapelViewModel obj)
        {
            var entity = entities.TipoBoletosPapels.Where(c => c.Id == obj.Id).FirstOrDefault();
            if (entity != null)
            {
                try
                {
                    entity.Nombre = obj.Nombre;
                    entity.AltoDefectocCentimetros = obj.AltoCmm;
                    entity.AnchoDefectoCentimetros = obj.AnchoCmm;
                    entity.ImagenPapel = obj.Imagen;
                    entity.IdChangedby = obj.Auditoria.IdChangedby;
                    entity.DateChanged = obj.Auditoria.DateChanged;
                    entities.Add(entity);
                    entities.SaveChanges();
                }
                catch (Exception err)
                {

                    err.Message.ToString(); 
                }

            }
        }

        public void Destroy(TipoBoletosPapelViewModel obj)
        {
            var entity = entities.TipoBoletosPapels.Where(c => c.Id == obj.Id).FirstOrDefault();
            if (entity != null)
            {
                try
                {
                    entity.Activo = false;
                    entity.IdRetiredby = obj.Auditoria.IdRetiredby;
                    entity.DateRetired = obj.Auditoria.DateRetired;
                    entities.SaveChanges();
                }
                catch (Exception err)
                {

                    err.Message.ToString();
                }

            }
        }


        public void Dispose()
        {
            entities.Dispose();
        }
    }
}