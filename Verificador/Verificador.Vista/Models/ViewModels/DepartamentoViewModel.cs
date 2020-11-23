using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class DepartamentoViewModel
    {   

        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Codigo Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Codigo Departamento es requerido")]
        public string Codigo { set; get; }        

        [Display(Name = "Nombre Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre Departamento es requerido")]
        public string NombreDepartamento { set; get; }

        [Display(Name = "Nombre País")]
        public string NombrePais { set; get; }

        [Display(Name = "País")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo País es requerido")]
        public int?  IdPais { set; get; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }

        [Display(Name = "Nombre Departamento")]
        public string Label { get { return string.Format("{0} - {1}", this.Codigo, this.NombreDepartamento); } }
        
    }
}