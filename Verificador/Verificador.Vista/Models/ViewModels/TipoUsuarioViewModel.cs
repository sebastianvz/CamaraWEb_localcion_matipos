using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class TipoUsuarioViewModel
    {   

        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Codigo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Codigo es requerido")]
        public string Code { set; get; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Description { set; get; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }
        
    }

    public class ListItem
    {
        public int Value { get; set; }
        public string Name { get; set; }
        public string Text { get; set; }
    }
}