using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models.ViewModels
{
    public enum TicketType
    {
        small = 0,
        grande = 1
    }

    public enum EstadoLog
    {
        ERROR = 0,
        EXITOSO = 1,
        ENPROCESO = 2,
        FINALIZADO = 3,
        REINTENTO = 4
    }

    public class PullTicketsViewModel
    {
        public int Evento_id { get; set; }
        public string ContentSVG { get; set; }
        public string ContentJson { get; set; }
        public TicketType TicketType { get; set; }
        public List<TicketViewModel> Tickets { get; set; }
    }

    public class PullTicketsImpresionViewModel
    {
        public int Id { get; set; }
        public int IdPullTickets { get; set; }
        public int IdLocalidad { get; set; }
        public int IdEtapa { get; set; }
        public string ImgTicket { get; set; }
        public TicketType TicketType { get; set; }
        public decimal Precio { get; set; }
        public string Responsable { get; set; }
        public string Ubicacion { get; set; }
        public string NombreEveno { get; set; }
        public string fechaEvento { get; set; }
        public string EdadMinima { get; set; }
        public string HoraApertura { get; set; }
        public string Localidad { get; set; }
        public int Cantidad { get; set; }
        public int Numero { get; set; }
        public string PrinterName { get; set; }
        public string textoPrecio
        {
            get
            {
                if (this.Precio > 0)
                {
                    return this.Precio.ToString("c");
                }
                return "";
            }
        }

        public string textoNumero
        {
            get
            {
                if (this.Numero > 0)
                {
                    return string.Format("TICKET {0}", this.Numero);
                }
                return "";
            }
        }
    }


    public class TicketToPrintViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; }
    }

    public static class EstadoTicket
    {
        public const int IMPRESO = 2;
        public const int DISPONIBLE = 3;
        public const int IMPRESO_DEFECTUOSO = 4;
        public const int REIMPRESO = 5;
        public const int ENCOLA = 6;
        public static string getEstado(int tipo)
        {
            switch (tipo)
            {
                case IMPRESO: return "impreso";
                case DISPONIBLE: return "disponible";
                case IMPRESO_DEFECTUOSO: return "impreso_defectuoso";
                case REIMPRESO: return "reimpreso";
                case ENCOLA: return "en_cola";
                default: return "";
            }
        }
    }

    public class TicketViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        public int idPullTickets { get; set; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { set; get; }

        [Display(Name = "Número")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Número es requerido")]
        public int Numero { get; set; }

        [Display(Name = "Precio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Precio es requerido")]
        public decimal Precio { get; set; }

        public string textoPrecio
        {
            get
            {
                if (this.Precio > 0)
                {
                    return this.Precio.ToString("c");
                }
                return "";
            }
        }

        public string textoNumero
        {
            get
            {
                if (this.Numero > 0)
                {
                    return string.Format("TICKET {0}", this.Numero);
                }
                return "";
            }
        }

        public byte[] BarcodeImage { get; set; }

        public string Barcode { get; set; }

        public string NumericBarcode { get; set; }

        public string NumericBarcodeText { get; set; }

        public string ImageUrl { get; set; }

        public string Responsable { get; set; }

        public string Ubicacion { get; set; }

        public string ContentJson { get; set; }

        public string ContentSVG { get; set; }

        public TicketType ticketType { get; set; }

        public string NombreEveno { get; set; }

        public string fechaEvento { set; get; }

        public string EdadMinima { get; set; }

        public string HoraApertura { get; set; }

        public string Localidad { get; set; }

        public string Estado { get; set; }

        public int? idEstado { get; set; }

        public string BackgorundImgae { get; set; }
    }

    public class TicketsEventoViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        public TicketViewModel TicketPreview { get; set; }
        public int TicketPreview_id
        {
            set
            {
                TicketPreview = new TicketViewModel() { Id = value };
            }
            get
            {
                if (TicketPreview != null)
                {
                    return TicketPreview.Id;
                }
                return 0;
            }
        }

        public int Cantidad { get; set; }

        public decimal Precio { get; set; }

        public int InicioNumeracion { get; set; }

        public int FinNumeracion { get; set; }

        public string ContentJson { get; set; }

        public string ContentSVG { get; set; }

        public string Barcode { get; set; }

        public long NumericBarcode { get; set; }

        public TicketType TicketType { get; set; }

        public int Evento_id { get; set; }

        public List<TicketsEtapaViewModel> TicketsEtapas { get; set; }
    }

    public class TicketsEtapaViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        public EtapasViewModel Etapa { get; set; }
        public int Etapa_id
        {
            set
            {
                Etapa = new EtapasViewModel() { Id = value };
            }
            get
            {
                if (Etapa != null)
                {
                    return Etapa.Id;
                }
                return 0;
            }
        }

        public TipoServicioViewModel TipoServicio { get; set; }
        public int IdTipoServicio
        {
            set
            {
                TipoServicio = new TipoServicioViewModel() { Id = value };
            }
            get
            {
                if (TipoServicio != null)
                {
                    return TipoServicio.Id;
                }
                return 0;
            }
        }

        //public TicketsEventoViewModel pullTickets { get; set; }
        //public int pullTickets_id
        //{
        //    set
        //    {
        //        pullTickets = new TicketsEventoViewModel() { Id = value };
        //    }
        //    get
        //    {
        //        if (pullTickets != null)
        //        {
        //            return pullTickets.Id;
        //        }
        //        return 0;
        //    }
        //}

        public LocalidadViewModel Localidad { get; set; }
        public int IdLocalidad
        {
            set
            {
                Localidad = new LocalidadViewModel() { Id = value };
            }
            get
            {
                if (Localidad != null)
                {
                    return Localidad.Id;
                }
                return 0;
            }
        }


        public int Aforo { get; set; }

        public decimal Precio { get; set; }
    }
}