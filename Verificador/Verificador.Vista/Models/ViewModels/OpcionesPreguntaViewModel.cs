using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class OpcionesPreguntaViewModel
    {
        public int Id { get; set; }
        public int IdPregunta { get; set; }
        public string TextoOpcion { get; set; }
        public int NumeroOpcion { get; set; }
     
    }
}