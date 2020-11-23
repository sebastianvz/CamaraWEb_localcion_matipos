using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Verificador.Vista.Models.Servicios;

namespace Verificador.Vista.Models.ViewModels
{
    public class LocalidadViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        public int IdTicketEtapa { set; get; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { set; get; }

        [Display(Name = "Aforo")]
        public int Aforo { set; get; }

        [Display(Name = "Numero de ticket inicial")]
        public int? TicketNumeroInicio { set; get; }

        [Display(Name = "Numero de ticket final")]
        public int? TicketNumeroFinal { set; get; }

        [Display(Name = "Definir rango de tiquetes por localidad")]
        public bool RangoTickets { set; get; }

        public TipoLocalidadViewModel TipoLocalidad { set; get; }

        [Display(Name = "Tipo de localidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Venue es requerido")]
        public int IdTipoLocalidad
        {
            set
            {
                TipoLocalidad = new ServicioTipoLocalidad().Read(value);
            }
            get
            {
                if (TipoLocalidad != null)
                {
                    return TipoLocalidad.Id;
                }
                return 0;
            }
        }

        public TipoNumeracionViewModel TipoNumracion { set; get; }

        [Display(Name = "Tipo de localidad")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Venue es requerido")]
        public int? IdTipoNumeracion
        {
            set
            {
                TipoNumracion = new TipoNumeracionViewModel();
            }
            get
            {
                if (TipoNumracion != null)
                {
                    return TipoNumracion.Id;
                }
                return 0;
            }
        }

        [Display(Name = "Consumo")]
        public string Consumo { get; set; }

        [Display(Name = "Valor del Consumo")]
        public decimal? ValorConsumo { get; set; }

        public int Filas { get; set; }
        public int Sillas { get; set; }
        public string OrientacionFilas { get; set; }
        public string OrientacionSillas { get; set; }
        public string Token { get; set; }

        public List<AsisentoViewModel> Asientos { get; set; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }
    }

    public class AsisentoViewModel
    {
        public int Id { get; set; }
        public int IdLocalidad { get; set; }
        public string GroupName { get; set; }
        public int GroupOrder { get; set; }
        public int Order { get; set; }
        public string Label { get; set; }
        public string Token { get; set; }
        public bool Visible { get; set; }

    }

    public class LocalidadMesasFilasSillasViewModel
    {
        public MesasFilasViewModel MesasFilas { set; get; }
        public int MesasFilas_id
        {
            set
            {
                MesasFilas = new MesasFilasViewModel { Id = value };
            }
            get
            {
                if (MesasFilas != null)
                {
                    return MesasFilas.Id;
                }
                return 0;
            }
        }
        public ConsumoViewModel Consumo { set; get; }
        public int Consumo_id
        {
            set
            {
                Consumo = new ConsumoViewModel { Id = value };
            }
            get
            {
                if (Consumo != null)
                {
                    return Consumo.Id;
                }
                return 0;
            }
        }
    }

    public class ConsumoViewModel
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string descripcion { get; set; }
    }

    public class MesasFilasViewModel
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Orientacion { get; set; }
        public string InicioNumeracion { get; set; }
        public string Forma { get; set; }
        public int capacidad { get; set; }
        public TipoNumeracionViewModel TipoNumeracion { set; get; }
        public int TipoNumeracion_id
        {
            set
            {
                TipoNumeracion = new ServicioTipoNumeracion().Read(value);
            }
            get
            {
                if (TipoNumeracion != null)
                {
                    return TipoNumeracion.Id;
                }
                return 0;
            }
        }
        public List<SillasViewModel> Sillas { get; set; }

        ///Optimización
        ///
        public int cFilas { get; set; }
        public int cSillas { get; set; }
        public string OrientacionSilla { get; set; }
        public TipoNumeracionViewModel TipoNumeracionSilla { set; get; }
        public int TipoNumeracionSilla_id
        {
            set
            {
                TipoNumeracionSilla = new ServicioTipoNumeracion().Read(value);
            }
            get
            {
                if (TipoNumeracionSilla != null)
                {
                    return TipoNumeracionSilla.Id;
                }
                return 0;
            }
        }
        public TipoLocalidadViewModel TipoLocalidad { set; get; }
        public int TipoLocalidad_id
        {
            set
            {
                TipoLocalidad = new ServicioTipoLocalidad().Read(value);
            }
            get
            {
                if (TipoLocalidad != null)
                {
                    return TipoLocalidad.Id;
                }
                return 0;
            }
        }
        ///FIN:Optimización
    }

    public class SillasViewModel
    {
        public int Id { get; set; }
        public string Label { get; set; }
        public string Orientacion { get; set; }
        public string InicioNumeracion { get; set; }
        public TipoNumeracionViewModel TipoNumeracion { set; get; }
        public int TipoNumeracion_id
        {
            set
            {
                TipoNumeracion = new ServicioTipoNumeracion().Read(value);
            }
            get
            {
                if (TipoNumeracion != null)
                {
                    return TipoNumeracion.Id;
                }
                return 0;
            }
        }
    }

    public enum FormaMesa
    {
        REDONDA = 0,
        CUADRAD = 1
    }


}