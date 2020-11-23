using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class UsuarioViewModel : BaseUsuarioViewModel
    {

        [Display(Name = "Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo usuario es requerido")]
        public string Usuario { set; get; }

        [Display(Name = "Confirmar Contraseña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido")]
        public string ConfriPass { set; get; }

        public int PersonaClienteId { set; get; }

        //Persona
        public bool EsEmpresa { set; get; }

        public int IdClietne { set; get; }

        public TipoPersonaViewModel TipoPersona { get; set; }

        [Display(Name = "Tipo de Persona")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Tipo de Persona es requerido")]
        public int TipoPersona_id
        {
            set
            {
                TipoPersona = new TipoPersonaViewModel { Id = value };
            }
            get
            {
                if (TipoPersona != null)
                {
                    return TipoPersona.Id;
                }
                return 0;
            }
        }

        public TipoDocumentoViewModel TipoDocumento { get; set; }
        [Display(Name = "Tipo de Documento")]
        public string NombreTipoDocumento
        {
            get
            {
                if (TipoDocumento != null)
                {
                    return this.TipoDocumento.Nombre;
                }
                return string.Empty;
            }
        }

        [Display(Name = "Tipo de Documento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Tipo de Documento es requerido")]
        public int TipoDocumento_id
        {
            set
            {
                TipoDocumento = new TipoDocumentoViewModel { Id = value };
            }
            get
            {
                if (TipoDocumento != null)
                {
                    return TipoDocumento.Id;
                }
                return 0;
            }
        }

        [Display(Name = "Identificación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Identificación es requerido")]
        public string Identificacion { get; set; }

        [Display(Name = "Primer Nombre o Razón Social")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Primer Nombre es requerido")]
        public string PrimerNombres { get; set; }

        [Display(Name = "Segundo Nombre")]
        public string SegundoNombres { get; set; }

        [Display(Name = "Primer Apellido")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo orimner apellido es requerido")]
        public string PrimerApellido { get; set; }

        [Display(Name = "Segundo Apellido")]
        public string SegundoApellido { get; set; }

        [Display(Name = "FechaNacimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo fecha nacimiento es requerido")]
        public DateTime? FechaNacimiento { get; set; }

        public GeneroViewModel Genero { get; set; }

        [Display(Name = "Genero")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo genero es requerido")]

        public int Genero_id
        {
            set
            {
                Genero = new GeneroViewModel { Id = value };
            }
            get
            {
                if (Genero != null)
                {
                    return Genero.Id;
                }
                return 0;
            }
        }

        [Display(Name = "Genero")]
        public string NombreGenero
        {
            get
            {
                if (Genero != null)
                {
                    return this.Genero.Nombre;
                }
                return string.Empty;
            }
        }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Email es requerido")]
        public string EmailPersona { get; set; }

        [Display(Name = "Celular")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Celular es requerido")]
        public string Celular { get; set; }

        [Display(Name = "Telefóno Casa")]
        public string TelefonoCasa { get; set; }

        [Display(Name = "Telefóno de la Oficina")]
        public string TelefonoOficina { get; set; }

        [Display(Name = "Horario de Entrega de Bolteria Preferido")]
        public string JornadaEntrega { get; set; }

        [Display(Name = "Activo")]
        public bool Activo { get; set; }

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

        public string NombreCompleto
        {
            get
            {
                if (this.Id != null)
                {
                    return string.Format("{0} {1} {2} {3}", this.PrimerNombres, this.SegundoNombres, this.PrimerApellido, this.SegundoApellido);
                }
                else
                {
                    return string.Empty;
                }

            }
        }

    }
}