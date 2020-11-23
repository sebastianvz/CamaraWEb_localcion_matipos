using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public class EtapasViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Id")]
        public int Evento_id { set; get; }

        [Display(Name = "Nombre de la Etapa")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Codigo Pulep es requerido")]
        public string Nombre { set; get; }

        [Display(Name = "Fecha Inicial")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Fecha Inicial es requerido")]
        public DateTime FechaInicial { set; get; }

        [Display(Name = "Fecha Final")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Fecha Final es requerido")]
        public DateTime FechaFinal { set; get; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Descripción es requerido")]
        public string Descripcion { set; get; }

        //[Display(Name = "Precio del Ticket")]
        //[Required(AllowEmptyStrings = false, ErrorMessage = "El campo Descripción es requerido")]
        //public decimal precio;

        //public decimal Precio
        //{
        //    get
        //    {
        //        if (this.TicketsEtapas != null)
        //        {
        //            if (this.TicketsEtapas.Count() > 0)
        //            {
        //                return this.TicketsEtapas[0].Precio;    
        //            }
        //        } return precio;
        //    }
        //    set { precio = value; }
        //}

        [Display(Name = "Cantidad de Tickets")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre Funsión es requerido")]
        public int cantidad;

        public int Cantidad
        {
            get
            {
                if (TicketsEtapas != null)
                {
                    if (this.TicketsEtapas.Count() > 0)
                    {
                        return TicketsEtapas.Sum(e => e.Aforo);
                    }
                } return cantidad;
            }
            set { cantidad = value; }
        }

        public bool? LlevaCodigo { get; set; }
        public string Codigo { get; set; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }

        public List<TicketsEtapaViewModel> TicketsEtapas { get; set; }
    }
}