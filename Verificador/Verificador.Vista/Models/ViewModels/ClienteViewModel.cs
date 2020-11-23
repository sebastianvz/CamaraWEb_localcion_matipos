using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Verificador.Vista.Models;

namespace Verificador.Vista.Models.ViewModels
{
    public class ClienteViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [Display(Name = "Tipo de Persona")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Tipo de Persona es requerido")]
        public int TipoPersona_id { get; set; }

        [Display(Name = "Tipo de Persona")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Tipo de Persona es requerido")]
        public string NombreTipoPersona { get; set; }

        [Display(Name = "Tipo de Documento")]
        public string NombreTipoDocumento{ get; set; }

        [Display(Name = "Tipo de Documento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Tipo de Documento es requerido")]
        public int TipoDocumento_id{ get; set; }

        [Display(Name = "Identificación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Identificación es requerido")]
        public string Identificacion { get; set; }

        [Display(Name = "Primer Nombre")]
        public string PrimerNombres { get; set; }

        [Display(Name = "Segundo Nombre")]
        public string SegundoNombres { get; set; }

        [Display(Name = "Primer Apellido")]
        public string PrimerApellido { get; set; }

        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Display(Name = "FechaNacimiento")]
        public DateTime? FechaNacimiento { get; set; }

        public GeneroViewModel Genero { get; set; }

        [Display(Name = "Genero")]
        public int Genero_id { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Email es requerido")]
        public string Email { get; set; }

        [Display(Name = "Celular")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Celular es requerido")]
        public string Celular { get; set; }

        [Display(Name = "Telefóno Casa")]
        public string TelefonoCasa { get; set; }

        [Display(Name = "Telefóno de la Oficina")]
        public string TelefonoOficina { get; set; }

        [Display(Name = "Horario de Entrega de Bolteria Preferido")]
        public string JornadaEntrega { get; set; }

        public List<DireccionViewModel> Direcciones { set; get; }

        public DireccionViewModel Direccion
        {
            get
            {
                if (Direcciones != null)
                {
                    return Direcciones.FirstOrDefault();
                }
                return new DireccionViewModel();
            }
        }

        [Display(Name = "Dirección")]
        public string NombreDireccion
        {
            get
            {
                if (Direccion != null)
                {
                    return Direccion.Direccion;
                }
                return string.Empty;
            }
        }

        public byte[] imagen { get; set; }

        public string strImagen { get; set; }

        public string NombreCompleto { get; set; }

        #region Empresa

        public bool EsEmpresa { set; get; }

        [Display(Name = "Digito")]
        public char? Digito { get; set; }

        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }

        public string IdentificacionShowGrid { set; get; }

        #endregion

        public Auditoria Auditoria { get; set; }

        public ClienteViewModel()
        {
            Auditoria = new Auditoria();
        }
    }
}