using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class MenuViewModel
    {
        public string Menu { get; set; }
        public string Accion { get; set; }
        public string Controlador { get; set; }
        public string Icono { get; set; }
        public string LabelMenu{ get; set; }
        public int IdPermiso { get; set; }
    }
}