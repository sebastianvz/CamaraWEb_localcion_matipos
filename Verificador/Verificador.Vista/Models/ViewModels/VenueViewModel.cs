using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class VenueViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { set; get; }

        [Display(Name = "Dirección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Dirección es requerido")]
        public string Direccion { get; set; }

        [Display(Name = "Ciudad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Ciudad es requerido")]
        public int? Ciudad_id { get; set; }

        [Display(Name = "Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Departamento es requerido")]
        public int? Departamneto_id { get; set; }

        [Display(Name = "País")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo País es requerido")]
        public int? Pais_id { get; set; }

        [Display(Name = "Longitud")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Longitud es requerido")]
        public string Longitud { set; get; }

        [Display(Name = "Latitud")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Latitud es requerido")]
        public string Latitud { set; get; }

        [Display(Name = "Telefono")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Telefono es requerido")]
        public string Telefono { set; get; }

        [Display(Name = "E-mail")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo E-mail es requerido")]
        public string E_mail { set; get; }

        [Display(Name = "Foto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Foto es requerido")]
        public string Foto { set; get; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Descripción es requerido")]
        public string Descripcion { set; get; }

        [Display(Name = "Aforo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Edad Minima es requerido")]
        public int Aforo { set; get; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }

        public string CityName { get; set; }
        public string DepartametName { get; set; }
        public string CountryName { get; set; }
        public string PostalCode { get; set; }
        public string GeoCode { get; set; }

        public List<LocalidadViewModel> Localidades { get; set; }
    }
}