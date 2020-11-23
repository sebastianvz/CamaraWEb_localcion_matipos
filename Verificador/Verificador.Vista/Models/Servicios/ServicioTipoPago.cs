﻿
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
    public class ServicioTipoPago: IDisposable
    {
        private Entities entities;

        public ServicioTipoPago()
        {
            this.entities = new Entities();
        }

        public IEnumerable<TipoPagoViewModel> Read()
        {
            return entities.Tipopagos.Select(c => new TipoPagoViewModel
            {
                Id = c.Id,
                Nombre = c.Nombre,
                FechaCreacion = c.Fechacreacion

            });
        }

        public void Create(TipoPagoViewModel obj)
        {
            var entity = new Tipopago();
            entity.Nombre = obj.Nombre;
            entity.Fechacreacion = DateTime.Now;            

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(TipoPagoViewModel obj)
        {
            var entity = entities.Tipopagos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.Nombre = obj.Nombre;                

                entities.SaveChanges();
            }
        }

        public void Destroy(TipoPagoViewModel obj)
        {
            var entity = entities.Tipopagos.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entities.Delete(entity);
                entities.SaveChanges();
            }
        }

        public void Dispose()
        {
            entities.Dispose();
        }

    }
}