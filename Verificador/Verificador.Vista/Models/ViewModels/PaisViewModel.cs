using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class PaisViewModel
    {   

        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Codigo País")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Codigo País es requerido")]
        public string Codigo { set; get; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { set; get; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }

        [Display(Name = "Nombre País")]
        public string Label { get { return string.Format("{0} - {1}", this.Codigo, this.Nombre); } }        
    }
}