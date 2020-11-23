using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class ImpuestoViewModel
    {

        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Id del Evento")]
        public int Evento_id { set; get; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { set; get; }

        [Display(Name = "Valor")]
        [Range(0, double.MaxValue, ErrorMessage = "El campo Valor no puede ser negativo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Valor es requerido")]
        public decimal Valor { set; get; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }

    }
}