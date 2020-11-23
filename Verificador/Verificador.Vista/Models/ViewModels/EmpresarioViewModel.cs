using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Verificador.Vista.Models;

namespace Verificador.Vista.Models.ViewModels
{
    public class EmpresarioViewModel
    {

        #region Cliente

        
        [Key]
        [Display(Name = "ClienteId")]
        public int ClienteId { get; set; }

        [Display(Name = "Tipo de Persona")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Tipo de Persona es requerido")]
        public int TipoPersona_id { get; set; }

        [Display(Name = "Tipo de Persona")]
        public string NombreTipoPersona { get; set; }

        [Display(Name = "Tipo de Documento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Tipo de Documento es requerido")]
        public int TipoDocumento_id { get; set; }

        [Display(Name = "Tipo de Documento")]
        public string NombreTipoDocumento{ get; set; }

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

        #endregion

        #region Empresa

        public bool EsEmpresa { set; get; }

        [Display(Name = "Digito")]
        public char? Digito { get; set; }

        [Display(Name = "Razón Social")]
        public string RazonSocial { get; set; }

        public string IdentificacionShowGrid { set; get; }

        #endregion

        #region EmpresarioRL
        [Key]
        [Display(Name = "Id")]
        public int EmpresarioId { get; set; }

        [Display(Name = "Nombre Establecimiento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido")]
        public string NombreEstablecimeinto { get; set; }

        [Display(Name = "Pulet")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo es requerido")]
        public string EmpresarioPulet { get; set; }

 
        [Display(Name = "Tipo de Documento")]
        public int EmpresarioTipoDocumento_idRL { get; set; }

        [Display(Name = "Tipo de Documento")]
        public string EmpresarioNombreTipoDocumentoRL { get; set; }

        [Display(Name = "Identificación")]
        public string EmpresarioIdentificacionRL { get; set; }

        [Display(Name = "Numero de Registro de Artes las Escenicas")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Numero de Registro de Artes las Escenicas es requerido")]
        public string NumeroRegistroArtesEscenicas { get; set; }

        [Display(Name = "Primer Nombre")]
        public string EmpresarioPrimerNombreRL { get; set; }

        [Display(Name = "Segundo Nombre")]
        public string EmpresarioSegundoNombreRL { get; set; }

        [Display(Name = "Primer Apellidos")]
        public string EmpresarioPrimerApellidosRL { get; set; }

        [Display(Name = "Segungo Apellidos")]
        public string EmpresarioSegundoApellidosRL { get; set; }

        [Display(Name = "Email de Notificación")]
        public string EmpresarioEmailNotificacion { get; set; }

        [Display(Name = "FechaNacimiento")]
        public DateTime ?EmpresarioFechaNacimientoRL { get; set; }

        public GeneroViewModel EmpresarioGeneroRL { get; set; }

        [Display(Name = "Genero")]
        public int EmpresarioGenero_idRL { get; set; }

        [Display(Name = "Correo Electronico")]
        public string EmpresarioEmailRL { get; set; }

        [Display(Name = "Celular")]
        public string EmpresarioCelularRL { get; set; }

        [Display(Name = "Representante")]
        public string EmpresarioRLInfo { get; set; }

        #endregion

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

        public Auditoria Auditoria { get; set; }

        public EmpresarioViewModel()
        {
            Auditoria = new Auditoria();
        }
    }
}