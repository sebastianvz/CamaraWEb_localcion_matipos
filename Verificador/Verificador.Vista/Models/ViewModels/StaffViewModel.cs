using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class StaffViewModel
    {
        public int Id{ get; set; }

        public int IdEvento { get; set; }

        public int IdEmpresario { get; set; }

        public int IdEmpersa { get; set; }

        public int IdCliente{ get; set; }

        public string CodigoUnico { get; set; }

        public string NombreEmpresa { get; set; }

        public string IdentificacionCliente { get; set; }

        public string NombreCliente { get; set; }

        public Auditoria Auditoria { get; set; }

        public StaffViewModel()
        {
            Auditoria = new Auditoria();
        }

    }
}