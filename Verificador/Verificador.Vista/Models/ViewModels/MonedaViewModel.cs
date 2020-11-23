using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class MonedaViewModel
    {   

        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { set; get; }

        [Display(Name = "Código")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Código es requerido")]
        public string Codigo { set; get; }

        [Display(Name = "Simbolo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Simbolo es requerido")]
        public string Simbolo { set; get; }

        [Display(Name = "Decimales")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Valor no puede ser negativo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Decimales es requerido")]
        public int Decimales { set; get; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }
        
    }
}