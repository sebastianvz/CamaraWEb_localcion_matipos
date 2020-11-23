using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class PrintersByComputersViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Dirección MAC")]
        public string MacAddress { set; get; }

        [Display(Name = "Seleccione una impresora")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Debe de seleccinar una impresora")]
        public string PrinterName { set; get; }

       

    }
}