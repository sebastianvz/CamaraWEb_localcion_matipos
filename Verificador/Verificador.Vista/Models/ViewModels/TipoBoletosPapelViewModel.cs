using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class TipoBoletosPapelViewModel
    {
        public TipoBoletosPapelViewModel()
        {
            Auditoria = new Auditoria();
        }

        public Auditoria Auditoria { get; set; }

        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Ancho")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Ancho es requerido")]
        public decimal? AnchoCmm { get; set; }

        [Display(Name = "Alto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Alto es requerido")]
        public decimal? AltoCmm { get; set; }

        [Display(Name = "Imagen papel")]
        public string Imagen { get; set; }
    }
}