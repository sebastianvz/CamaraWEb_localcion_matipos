using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class ElementosPlantillaBoletosPapelViewModel
    {
        [Editable(false)]
        [Key]
        public int Id { set; get; }

        public int IdPlantilla { set; get; }

        [Display(Name = "Nombre Elemento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string NombreElemento { set; get; }

        [Display(Name = "X")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo X es requerido")]
        public decimal? X { set; get; }

        [Display(Name = "Y")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Y es requerido")]
        public decimal? Y { set; get; }

        [Display(Name = "Ancho")]
        public decimal Ancho { set; get; }

        [Display(Name = "Alto")]
        public decimal Alto { set; get; }

        public bool? IsImage { get; set; }
        public bool? IsText { get; set; }
        public bool? IsQR { get; set; }
        public bool? IsBarcode { get; set; }

        [Display(Name = "Fuente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Fuente es requerido")]
        public string Fuente { set; get; }

        [Display(Name = "TamañoFuente")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Fuente es requerido")]
        public int? TamanoFuente { set; get; }

        [Display(Name = "Texto Elemento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Texto es requerido")]
        public string TextoElemento { set; get; }

        [Display(Name = "Orientación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Orientación es requerido")]
        public int? Orientacion { set; get; }

        public string SRCImagen { set; get; }

        [Display(Name = "Orden")]
        public int? Orden { set; get; }

        [Display(Name = "Tipo")]
        public int? TipoBarcode { set; get; }

        [Display(Name = "Tipo")]
        public int? TipoQr { set; get; }

        [Display(Name = "Elementos por defecto")]
        public int? ElemnetoElementoXDefecto { set; get; }



    }
}