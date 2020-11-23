using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class RoleViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { set; get; }        

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }

        [Display(Name = "Aplica para Matipos")]
        public bool AplicaMatipos { set; get; }

        [Display(Name = "Aplica para Empresario")]
        public bool AplicaEmpresario { set; get; }

        [Editable(false)]
        [Display(Name = "Seleccionado")]
        public bool Seleccionado { set; get; }

        public List<PermisoViewModel> Permisos { set; get; }

        public String ParaMatipos 
        {
            get
            {if(this.AplicaMatipos ==true)
            {return "SI";}
            else { return "NO"; }
            }
        }

        public String ParaEmpresa
        {
            get
            {
                if (this.AplicaEmpresario == true)
                { return "SI"; }
                else { return "NO"; }
            }
        }

    }
}