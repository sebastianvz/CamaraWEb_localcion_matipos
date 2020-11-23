using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class CiudadViewModel
    {

        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Nombre Ciudad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre Ciudad es requerido")]
        public string NombreCiudad { set; get; }

        [Display(Name = "Codigo Ciudad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Codigo Ciudad es requerido")]
        public string Codigo { set; get; }

        [Display(Name = "Nombre Departamento")]
        public string NombreDepartamento { set; get; }

        [Display(Name = "Nombre País")]
        public string NombrePais { set; get; }

        [Display(Name = "País - Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo País - Departamento es requerido")]
        public int IdDepartamento { set; get; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }

        [Display(Name = "Nombre Ciudad")]
        public string Label { get { return string.Format("{0} - {1}", this.Codigo, this.NombreCiudad); } }

    }
}