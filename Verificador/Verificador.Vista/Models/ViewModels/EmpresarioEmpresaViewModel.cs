using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class EmpresarioEmpresaViewModel
    {
        
        # region CLienteEmpresa
        //CLienteEmpresa

        [Display(Name = "Id")]
        public int ClienteId { get; set; }

        public TipoDocumentoViewModel ClienteTipoDocumento { get; set; }

        [Display(Name = "Tipo de Documento")]
        public string ClienteNombreTipoDocumento
        {
            get
            {
                if (ClienteTipoDocumento != null)
                {
                    return this.ClienteTipoDocumento.Nombre;
                }
                return string.Empty;
            }
        }

        [Display(Name = "Tipo de Documento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Tipo de Documento es requerido")]
        public int ClienteTipoDocumento_id
        {
            set
            {
                ClienteTipoDocumento = new TipoDocumentoViewModel { Id = value };
            }
            get
            {
                if (ClienteTipoDocumento != null)
                {
                    return ClienteTipoDocumento.Id;
                }
                return 0;
            }
        }

        [Display(Name = "Identificación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Identificación es requerido")]
        public string ClienteIdentificacion { get; set; }

        [Display(Name = "Digito")]
        public char ?ClienteDigito { get; set; }

        [Display(Name = "Razón social")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Primer Nombre es requerido")]
        public string ClientePrimerNombres { get; set; }

        [Display(Name = "Email")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Email es requerido")]
        public string ClienteEmail { get; set; }

        [Display(Name = "Celular")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Celular es requerido")]
        public string ClienteCelular { get; set; }

        [Display(Name = "Telefóno principal")]
        public string ClienteTelefonoCasa { get; set; }

        [Display(Name = "Telefóno secundario")]
        public string ClienteTelefonoOficina { get; set; }

        public bool EsEmpresa { get; set; }

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

#endregion

        #region EmpresarioRL
        [Key]
        [Display(Name = "Id")]
        public int EmpresarioId { get; set; }

        [Display(Name = "Pulet")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido")]
        public string EmpresarioPulet { get; set; }

        [Display(Name = "Nobre Establecimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido")]
        public string NombreEstablecimeinto { get; set; }

        public TipoDocumentoViewModel EmpresarioTipoDocumentoRL { get; set; }

        [Display(Name = "Tipo de Documento")]
        public string EmpresarioNombreTipoDocumentoRL
        {
            get
            {
                if (EmpresarioTipoDocumentoRL != null)
                {
                    return this.EmpresarioTipoDocumentoRL.Nombre;
                }
                return string.Empty;
            }
        }

        [Display(Name = "Tipo de Documento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Tipo de Documento es requerido")]
        public int EmpresarioTipoDocumento_idRL
        {
            set
            {
                EmpresarioTipoDocumentoRL = new TipoDocumentoViewModel { Id = value };
            }
            get
            {
                if (EmpresarioTipoDocumentoRL != null)
                {
                    return EmpresarioTipoDocumentoRL.Id;
                }
                return 0;
            }
        }

        [Display(Name = "Identificación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Identificación es requerido")]
        public string EmpresarioIdentificacionRL { get; set; }

        [Display(Name = "Numero de Registro de Artes las Escenicas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Numero de Registro de Artes las Escenicas es requerido")]
        public string NumeroRegistroArtesEscenicas { get; set; }

        [Display(Name = "Primer Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido")]
        public string EmpresarioPrimerNombreRL { get; set; }

        [Display(Name = "Segundo Nombre")]
        public string EmpresarioSegundoNombreRL { get; set; }

        [Display(Name = "Primer Apellidos")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo  es requerido")]
        public string EmpresarioPrimerApellidosRL { get; set; }

        [Display(Name = "Segungo Apellidos")]
        public string EmpresarioSegundoApellidosRL { get; set; }

        [Display(Name = "Email de Notificación")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Email de Notificación es requerido")]
        public string EmpresarioEmailNotificacion { get; set; }

        [Display(Name = "FechaNacimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo FechaNacimiento es requerido")]
        public DateTime ?EmpresarioFechaNacimientoRL { get; set; }

        public GeneroViewModel EmpresarioGeneroRL { get; set; }

        [Display(Name = "Genero")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Genero es requerido")]
        public int EmpresarioGenero_idRL
        {
            set
            {
                EmpresarioGeneroRL = new GeneroViewModel { Id = value };
            }
            get
            {
                if (EmpresarioGeneroRL != null)
                {
                    return EmpresarioGeneroRL.Id;
                }
                return 0;
            }
        }

        [Display(Name = "Correo Electronico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Correo Electronico es requerido")]
        public string EmpresarioEmailRL { get; set; }

        [Display(Name = "Celular")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Celular es requerido")]
        public string EmpresarioCelularRL { get; set; }

        [Display(Name = "Representante")]
        public string EmpresarioRLInfo {
            get 
            {
                return string.Format("{0} {1} - {2} {3} {4} {5}", this.EmpresarioNombreTipoDocumentoRL, this.EmpresarioIdentificacionRL, this.EmpresarioPrimerNombreRL, this.EmpresarioSegundoNombreRL, this.EmpresarioPrimerApellidosRL, this.EmpresarioSegundoApellidosRL);
            }
             }

        # endregion

        #region UsuarioEmpresario
        //Usuario asocia a la empresa cliente
        [Key]
        [Display(Name = "Id")]
        public int UsuarioEmpresarioId { get; set; }

        [Display(Name = "Usuario")]
        public string UserEmpresario { set; get; }

        [PasswordPropertyText(true)]
        [Display(Name = "Contraseña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido")]
        public string PasswordEmpresaio { set; get; }

        [Display(Name = "Email inicio sesion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo email es requerido")]
        public string UserEmailEmpresario { set; get; }

        [Display(Name = "Confirmar Contraseña")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido")]
        public string ConfriPassEmpresario { set; get; }

        [Key]
        [Display(Name = "Id")]
        public int UsuarioEmpresarioRolId { get; set; }

        #endregion
    }
}