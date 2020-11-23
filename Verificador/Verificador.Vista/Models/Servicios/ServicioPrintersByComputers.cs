
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
    public class ServicioPrintersByComputers : IDisposable
    {
        private Entities entities;

        public ServicioPrintersByComputers()
        {
            this.entities = new Entities();
        }

        

        public IEnumerable<PrintersByComputersViewModel> Read()
        {
            return entities.PrintersByComputers.Select(c => new PrintersByComputersViewModel
            {
                Id = c.Id,
                MacAddress = c.MacAddress,
                PrinterName = c.PrinterName
            });
        }

        public void Create(PrintersByComputersViewModel obj)
        {
            var entity = new PrintersByComputer();

            entity.MacAddress = obj.MacAddress;
            entity.PrinterName = obj.PrinterName;

            entities.Add(entity);
            entities.SaveChanges();
            obj.Id = entity.Id;
        }

        public void Update(PrintersByComputersViewModel obj)
        {
            var entity = entities.PrintersByComputers.FirstOrDefault(c => c.Id == obj.Id);
            if (entity != null)
            {
                entity.MacAddress = obj.MacAddress;
                entity.PrinterName = obj.PrinterName;       

                entities.SaveChanges();
            }
        }


        public void Destroy(PrintersByComputersViewModel obj)
        {
            var entity = entities.PrintersByComputers.FirstOrDefault(c => c.Id == obj.Id);
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