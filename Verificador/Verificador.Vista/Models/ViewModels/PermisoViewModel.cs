using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class PermisoViewModel
    {

        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Nombre Vista")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { set; get; }

        [Display(Name = "Nombre Link")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Descripcion { set; get; }

        [Display(Name = "Controlador")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Controlador { set; get; }

        [Display(Name = "Menu")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Menu { set; get; }

        [Editable(false)]
        [Display(Name = "Seleccionado")]
        public bool Seleccionado { set; get; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }

        [Display(Name = "IdPermiso")]
        public int IdPermiso { set; get; }

        [Display(Name = "IdPermiso")]
        public string Icono { set; get; }
    }
}