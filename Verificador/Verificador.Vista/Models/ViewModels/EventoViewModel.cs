using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Web;
using Verificador.Vista.Models.Servicios;

namespace Verificador.Vista.Models.ViewModels
{
    public enum EstadoEvento
    {
        categoria = 0,
        informacionGenral = 1,
        informacionAdicional = 2,
        direccionMap = 3,
        funciones = 4,
        localidades = 5,
        etapas = 6,
        tickets = 7,
        liquidacion = 8,
        finalizado = 9
    }

    public enum Estado
    {
        Nuevo = 0,
        Editando = 1
    }

    public class ImpresionTicketsViewModel {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        [Display(Name = "Nombre de el evento")]
        public string Nombre { set; get; }

        [Display(Name = "Tickets Disponibles")]
        public int Disponibles { set; get; }

        [Display(Name = "Tickets Impresos")]
        public int Impresos { set; get; }

        [Display(Name = "Tickets Impresos Defectusos")]
        public int ImpresosDefectuosos { set; get; }

        public int idTicketsEtapa { get; set; }

        public int Cantidad { get; set; }

        public string PrinterName { get; set; }
    }

    public class EventoViewModel
    {
        [Editable(false)]
        [Key]
        [Display(Name = "Id")]
        public int Id { set; get; }

        public EstadoEvento Estado { set; get; }

        public Estado TipoRegisro { set; get; }

        [Display(Name = "Nombre")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Nombre es requerido")]
        public string Nombre { set; get; }

        [Display(Name = "Multifunción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Multifunción es requerido")]
        public bool Multifuncion
        {
            get
            {
                if (this.Funciones != null)
                {
                    return this.Funciones.Count() > 1;
                }
                return false;
            }
        }

        [Display(Name = "Cantidad Funciones")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Cantidad Funciones es requerido")]
        public int CantidadFunciones
        {
            get
            {
                if (this.Funciones != null)
                {
                    return this.Funciones.Count();
                }
                return 0;
            }
        }

        [Display(Name = "Fecha Inicial")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Fecha Inicial es requerido")]
        private DateTime fechaInicial;
        public DateTime FechaInicial
        {
            get
            {
                if (this.Funciones != null)
                {
                    return this.Funciones.Min(e => e.FechaInicial);
                }
                return fechaInicial;
            }
            set { fechaInicial = value; }
        }

        [Display(Name = "Fecha Final")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Fecha Final es requerido")]
        private DateTime fechaFinal;

        public DateTime FechaFinal
        {
            get
            {
                if (this.Funciones != null)
                {
                    return this.Funciones.Max(e => e.FechaFinal);
                } 
                return fechaFinal;
            }
            set { fechaFinal = value; }
        }

        public VenueViewModel Venue { set; get; }

        [Display(Name = "Venue")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Venue es requerido")]
        public int Venue_id
        {
            set
            {
                Venue = new VenueViewModel { Id = value };
            }
            get
            {
                if (Venue != null)
                {
                    return Venue.Id;
                }
                return 0;
            }
        }

        public EmpresarioViewModel Empresario { get; set; }

        [Display(Name = "Empresario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Empresario es requerido")]
        public int Empresario_id
        {
            get;
            set;
            //set
            //{
            //    Empresario = new ServicioEmpresarios().ReadByIdCliente(value);
            //}
            //get
            //{
            //    if (Empresario != null)
            //    {
            //        return Empresario.EmpresarioId;
            //    }
            //    return 0;
            //}
        }

        [Display(Name = "Tipo de Evento")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Tipo Evento es requerido")]
        public int TipoEvento_id { set; get; }

        [Display(Name = "Foto")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Foto es requerido")]
        public string Foto { set; get; }

        [Display(Name = "Descripción")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Descripción es requerido")]
        public string Descripcion { set; get; }

        [Display(Name = "Url del Video")]
        public string UrlVideo { set; get; }

        [Display(Name = "Edad Minima")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Edad Minima es requerido")]
        public int EdadMinima { set; get; }

        [Display(Name = "Evento Publico")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Evento Publico es requerido")]
        public string EventoPublico { set; get; }

        [Display(Name = "Tipo de Recaudo")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo TipoRecaudo es requerido")]
        public int TipoRecaudo_id { set; get; }

        [Display(Name = "Cantidad Maxima de Boletas por Compra")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Entradas por Pedido o compra es requerido")]
        public int EntradasxPedidoOcompra { set; get; }

        [Display(Name = "Cantidad Maxima de Ventas Boletas a un Usuario")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Entradas por Usuario es requerido")]
        public int EntradasxUsuario { set; get; }

        [Display(Name = "Aplica Servicio")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Aplica Servicio es requerido")]
        public string AplicaServicio { set; get; }

        [Display(Name = "Cantidad de Dias de Valides")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Cantidad de Dias de Valides es requerido")]
        public int CantidadDiasValides { set; get; }

        [Display(Name = "Aforo Total")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Aforo Total es requerido")]
        public int AforoTotal { set; get; }

        public int AforoAcmulado { set; get; }

        [Display(Name = "Fecha Creación")]
        public DateTime FechaCreacion { set; get; }

        public CategoriaViewModel Categoria { set; get; }

        public bool? StagesByMap { get; set; }      

        [Display(Name = "Categoria")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "El campo Categoria es requerido")]
        public int Categoria_id
        {
            set
            {
                Categoria = new ServicioCategoria().Read(value);  //new CategoriaViewModel { Id = value };
            }
            get
            {
                if (Categoria != null)
                {
                    return Categoria.Id;
                }
                return 0;
            }
        }

        [Display(Name = "Funciones")]
        public List<FuncionesEventoViewModel> Funciones { get; set; }

        public List<LocalidadViewModel> Localidades { get; set; }

        public List<ImpuestoViewModel> Impuestos { get; set; }

        public List<TipoServicioViewModel> TipoServicios { get; set; }

        public List<EtapasViewModel> Etapas { get; set; }

        public TicketsEventoViewModel Tickets { get; set; }

        public int Tickets_id
        {
            set
            {
                Tickets = new TicketsEventoViewModel() { Id = value };
            }
            get
            {
                if (Tickets != null)
                {
                    return Tickets.Id;
                }
                return 0;
            }
        }
    }
    public class barcodecs
    {
        public string generateBarcode()
        {
            try
            {
                string[] charPool = "1-2-3-4-5-6-7-8-9-0".Split('-');
                StringBuilder rs = new StringBuilder();
                int length = 6;
                Random rnd = new Random();
                while (length-- > 0)
                {
                    int index = (int)(rnd.NextDouble() * charPool.Length);
                    if (charPool[index] != "-")
                    {
                        rs.Append(charPool[index]);
                        charPool[index] = "-";
                    }
                    else
                        length++;
                }
                return rs.ToString();
            }
            catch (Exception ex)
            {
                //ErrorLog.WriteErrorLog("Barcode", ex.ToString(), ex.Message);
            }
            return "";
        }
        //public Byte[] getBarcodeImage(string barcode, string file)
        //{
        //    try
        //    {
        //        BarCode39 _barcode = new BarCode39();
        //        int barSize = 16;
        //        string fontFile = HttpContext.Current.Server.MapPath("~/fonts/FREE3OF9.TTF");
        //        return (_barcode.Code39(barcode, barSize, true, file, fontFile));
        //    }
        //    catch (Exception ex)
        //    {
        //        //ErrorLog.WriteErrorLog("Barcode", ex.ToString(), ex.Message);
        //    }
        //    return null;
        //}
    }

}