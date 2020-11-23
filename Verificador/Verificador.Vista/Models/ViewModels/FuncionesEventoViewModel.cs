using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class FuncionesEventoViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Id")]
        public int Evento_id { set; get; }

        [Display(Name = "Fecha Inicial")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Fecha Inicial es requerido")]
        public DateTime FechaInicial { set; get; }

        [Display(Name = "Fecha Final")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Fecha Final es requerido")]
        public DateTime FechaFinal { set; get; }

        [Display(Name = "Apertura Puertas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Apertura Puertas es requerido")]
        public DateTime AperturaPuertas { set; get; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Descripción es requerido")]
        public string Descripcion { set; get; }

        [Display(Name = "Nombre de la Funsión")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre Funsión es requerido")]
        public string Label { set; get; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }

    }
}