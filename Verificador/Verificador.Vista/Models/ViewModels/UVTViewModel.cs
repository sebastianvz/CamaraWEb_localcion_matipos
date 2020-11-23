using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class UVTViewModel
    {   

        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Año")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Año es requerido")]
        [Range(0, 9999, ErrorMessage = "El campo Valor no puede ser negativo")]
        public int Anio { set; get; }

        [Display(Name = "Valor")]
        [Range(0, double.MaxValue, ErrorMessage = "El campo Valor no puede ser negativo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Valor es requerido")]
        public decimal Valor { set; get; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }
        
    }
}