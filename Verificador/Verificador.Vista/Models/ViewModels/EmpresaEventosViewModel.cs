using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class EmpresaEventosViewModel
    {
        public EmpresaEventosViewModel()
        {
            Auditoria = new Auditoria();
        }

        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Tipo de persona")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo tipo de persona es requerido")]
        public int TipoPersona_id{ get; set; }
        public string NombreTipoPersona{ get; set; }

        [Display(Name = "Tipo de documento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo tipo de documento es requerido")]
        public int TipoDocumento_id{ get; set; }
        public string NombreTipoDocumento{ get; set; }

        [Display(Name = "Identificación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo identificación es requerido")]
        public string Identificacion { get; set; }

        [Display(Name = "Dígito")]
        public char? Digito { get; set; }

        [Display(Name = "Nombre / Razón social")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo primer nombre es requerido")]
        public string NombreRazonSocial { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo email es requerido")]
        public string Email { get; set; }

        [Display(Name = "Celular")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo celular es requerido")]
        public string Celular { get; set; }

        [Display(Name = "Telefóno principal")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo telefóno principal es requerido")]
        public string Telefono { get; set; }

        public Auditoria Auditoria{ get; set; }

        public string Descripcion{ get; set; }

        }
}
