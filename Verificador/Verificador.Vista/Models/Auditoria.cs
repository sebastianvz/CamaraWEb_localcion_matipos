using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models
{
    public class Auditoria
    {
        public int IdCreatorby { get; set; }
        public DateTime DateCreated { get; set; }
        public int IdRetiredby { get; set; }
        public DateTime DateRetired { get; set; }
        public int IdChangedby { get; set; }
        public DateTime DateChanged { get; set; }
        public bool activo { get; set; }
    }
}