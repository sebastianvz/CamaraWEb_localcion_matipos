using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class BaseUsuarioViewModel
    {

        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Usuario")]
        public string Usuario { set; get; }

        [PasswordPropertyText(true)]
        [Display(Name = "Contraseña")]
        public string Password { set; get; }

        [Display(Name = "Email inicio sesion")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo email es requerido")]
        public string Email { set; get; }

        [Display(Name = "Seleccionado")]
        public bool Seleccionado { set; get; }

        [Display(Name = "Bloqueado")]
        public bool Bloqueado { set; get; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }

        public List<RoleViewModel> Roles { set; get; }

        public List<MenuViewModel> Menus 
        {
            get 
            {
                if (this.Roles != null)
                {
                    var AllPermisos = new List<MenuViewModel>();
                    foreach (var item in this.Roles)
                    {
                        foreach (var itemPermisos in item.Permisos)
                        {
                            if (!AllPermisos.Any(c => c.IdPermiso == itemPermisos.IdPermiso))
                            {
                                AllPermisos.Add(new MenuViewModel
                                {
                                    Menu = itemPermisos.Menu,
                                    Accion = itemPermisos.Nombre,
                                    Controlador = itemPermisos.Controlador,
                                    Icono = itemPermisos.Icono,
                                    LabelMenu = itemPermisos.Descripcion,
                                    IdPermiso = itemPermisos.IdPermiso
                                });
                            }
                        }
                    }
                    return AllPermisos;
                }
                else 
                {
                    return null;
                }
            }
        }

        public bool TienePermiso(string permiso)
        {
            return this.Menus.Exists(c => c.Accion == permiso);
        }

        public int ClienteID { set; get; }

    }
}