﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class TipoDocumentoViewModel
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

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }
        
    }
}