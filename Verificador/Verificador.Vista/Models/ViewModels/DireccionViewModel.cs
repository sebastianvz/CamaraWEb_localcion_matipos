using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Verificador.Vista.Models.Servicios;

namespace Verificador.Vista.Models.ViewModels
{
    public class DireccionViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Id Cliente")]
        public int IdCliente { set; get; }

        [Display(Name = "Dirección")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Dirección es requerido")]
        public string Direccion { set; get; }

        [Display(Name = "Codigo Postal")]
        public string CodigoPostal { set; get; }

        public string FullAdress
        {
            get
            {
                return string.Format("{0}: {1} Codigo Postal: {2}", new ServicioCiudad().ReadNombreCompletoById(this.Ciudad_id), this.Direccion, this.CodigoPostal);
            }
        }

        [Display(Name = "Ciudad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Ciudad es requerido")]
        public int Ciudad_id { set; get; }

        [Display(Name = "Departamento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido")]
        public int Departamneto_id { get; set; }

        [Display(Name = "País")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido")]
        public int? Pais_id { get; set; }

        [Display(Name = "Latitud")]
        public string Latitud { get; set; }

        [Display(Name = "Longitud")]
        public string Longitud { get; set; }


    }
}