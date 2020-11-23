using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Verificador.OpenAccess;
using Verificador.Vista.Models.ViewModels;

namespace Verificador.Vista.Models.Servicios
{
    public class ServicioDiseñdorPlatnillasBoletos : IDisposable
    {
        private Entities entities;

        public ServicioDiseñdorPlatnillasBoletos()
        {
            this.entities = new Entities();
        }

        public List<PlantillasBoletosPapelViewModel> Read()
        {
            return entities.PlantillaBoletosPapels.Where(c => c.Activo == true).Select(c => new PlantillasBoletosPapelViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                Descripcion = c.Descripcion,
                IdTipoBoletoPapel = c.IdTipoBoletoPapel,
                NombreTipoBoletoPapel = c.TipoBoletosPapel.Nombre,
                CanvasJson = c.CanvasJson,
                TipoBoletaPapel = new TipoBoletosPapelViewModel
                {
                    AnchoCmm = c.TipoBoletosPapel.AnchoDefectoCentimetros,
                    AltoCmm = c.TipoBoletosPapel.AltoDefectocCentimetros,
                    Imagen = c.TipoBoletosPapel.ImagenPapel
                },
            }).ToList();
        }

        public void Create(PlantillasBoletosPapelViewModel obj)
        {
            var entity = new PlantillaBoletosPapel();
            try
            {
                entity.Nombre = obj.Nombre;
                entity.Descripcion = obj.Descripcion;
                entity.IdTipoBoletoPapel = obj.IdTipoBoletoPapel;
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

        public void Update(PlantillasBoletosPapelViewModel obj)
        {
            var entity = entities.PlantillaBoletosPapels.Where(c => c.Id == obj.Id).FirstOrDefault();
            if (entity != null)
            {
                try
                {
                    entity.Nombre = obj.Nombre;
                    entity.Descripcion = obj.Descripcion;
                    entity.IdTipoBoletoPapel = obj.IdTipoBoletoPapel;
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

        public void Destroy(PlantillasBoletosPapelViewModel obj)
        {
            var entity = entities.PlantillaBoletosPapels.Where(c => c.Id == obj.Id).FirstOrDefault();
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

        public void CreateUpdateCanvasDiseño(string canvasJson, int idPlantilla)
        {
            var entity = entities.PlantillaBoletosPapels.Where(c => c.Id == idPlantilla).FirstOrDefault();
            if (entity != null)
            {
                try
                {
                    entity.CanvasJson = canvasJson;
                    entities.Add(entity);
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