using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class PortalUsuarioViewModel : BaseUsuarioViewModel
    {

        [Display(Name = "Nombres")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombres es requerido")]
        public string Nombres { set; get; }

        [Display(Name = "Apellidos")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Apellidos es requerido")]
        public string Apellidos { set; get; }

        [Display(Name = "Fecha Nacimiento")]
        public DateTime? FechaNacimiento { set; get; }       
        
    }
}