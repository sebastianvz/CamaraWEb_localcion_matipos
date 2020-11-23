using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class PreguntaViewModel
    {
        public int Id { get; set; }
        public int IdEvento { get; set; }
        public string TextoPregunta  { get; set; }
        public int NumeroPregunta { get; set; }
        public int TipoRespuesta { get; set; }
        public List<OpcionesPreguntaViewModel> Opciones { get; set; }

        public PreguntaViewModel()
        {
            Opciones = new List<OpcionesPreguntaViewModel>();

        }

    }

}