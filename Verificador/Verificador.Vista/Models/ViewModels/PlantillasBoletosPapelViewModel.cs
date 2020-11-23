using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class PlantillasBoletosPapelViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { get; set; }

        [Display(Name = "Tipo Papel")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public int IdTipoBoletoPapel { get; set; }

        [Display(Name = "Tipo Papel")]
        public string NombreTipoBoletoPapel { get; set; }

        [Display(Name = "Descrición")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Descripción es requerido")]
        public string Descripcion { get; set; }

        [Editable(false)]
        [Display(Name = "Canvas")]
        public string CanvasJson { get; set; }

        public PlantillasBoletosPapelViewModel()
        {
            Auditoria = new Auditoria();
            TipoBoletaPapel = new TipoBoletosPapelViewModel();
            ElementosPlantilla = new List<ElementosPlantillaBoletosPapelViewModel>();
        }

        public Auditoria Auditoria { get; set; }

        public TipoBoletosPapelViewModel TipoBoletaPapel { get; set; }

        public List<ElementosPlantillaBoletosPapelViewModel> ElementosPlantilla { get; set; }

    }
}