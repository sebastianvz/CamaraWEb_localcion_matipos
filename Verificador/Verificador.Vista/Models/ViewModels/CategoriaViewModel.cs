using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class CategoriaViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { set; get; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Descripción es requerido")]
        public string Descripcion { set; get; }

        [Display(Name = "Ver Codigo de Artes Escenicas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo VerCodigoArtesEscenicas es requerido")]
        public bool VerCodigoArtesEscenicas { set; get; }

        public string VerCodigo
        {
            get
            {
                if (this.VerCodigoArtesEscenicas)
                {
                    return "Si";
                }
                return "No";
            }
        }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }

    }
}