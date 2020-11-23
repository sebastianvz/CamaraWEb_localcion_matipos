namespace Despachos.Vista.Reports
{
    using Despachos.Vista.Models;
    using System.Linq;
    using System.Linq.Expressions;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Despachos.OpenAccess;
    using System.Globalization;
    using System.Web;
    using System.Collections.Generic;
    using VentaTiquetes.Dominio.Gestion;

    /// <summary>
    /// Summary description for ReporteDevoluciones.
    /// </summary>
    public partial class ReporteVerFactura : Telerik.Reporting.Report
    {
        string idTiquete;
        public ReporteVerFactura()
        {
            //
            // Required for telerik Reporting designer support
            //            
            InitializeComponent();
            
            System.Threading.Thread.CurrentThread.CurrentUICulture =new System.Globalization.CultureInfo("es-ES");

            try
            {
                Entities entities = new Entities();
                
            }
            catch (Exception err)
            {                
            }
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        

        

        
        private void factura_ItemDataBinding(object sender, EventArgs e)
        {            
            int id = int.Parse(idTiquete);


            Entities _contexto = new Entities();

            var configuracion = _contexto.Te_configs.First();
            var prefijoFactura = _contexto.Te_parametros.First(c => c.Nombre == "PrefijoFactura");
            Te_parametro pancho = _contexto.Te_parametros.First(c => c.Nombre == "AnchoBoleta");
            Te_parametro palto = _contexto.Te_parametros.First(c => c.Nombre == "AltoBoleta");
            List<Te_elemento> elementos = _contexto.Te_elementos.ToList();

            string nombreFactura = (from c in _contexto.Te_parametros where c.Nombre == "NombreDocumentoFactura" select c.Valorstring).First();
            bool imprimirFactura = _contexto.Te_parametros.First(c => c.Nombre == "ImprimirFactura").Valorint.Value == 1;
            bool facturacionCaja = _contexto.Te_parametros.First(c => c.Nombre == "FacturacionPorCaja").Valorint.Value == 1;
            var config = _contexto.Te_configs.First();
            string formato = config.Formatoimpresion;
            string formatoFinal = config.Formatoimpresionfinal;
            var regstroVenta = _contexto.Te_tiquetes.FirstOrDefault(c => c.Id == id);

            Te_caja confCaja = null;
            if (facturacionCaja)
            {
                confCaja = _contexto.Te_cajas.FirstOrDefault(c => c.Numero_caja == regstroVenta.Caja);
                formato = confCaja.Formatoimpresion;
                formatoFinal = confCaja.Formatoimpresionfinal;
            }




            Venta venta = new Venta
            {
                Fecha = regstroVenta.Fecha,
                Estado = regstroVenta.Cancelada == 1 ? "Anulada" : "Original",
                Caja = regstroVenta.Caja,
                PrefijoFactura = prefijoFactura.Valorstring,
                NumeroFactura = regstroVenta.Numerofactura,
                TextoFactura = formato,
                TextoFacturaFinal = formatoFinal,
                SubTotal = regstroVenta.Subtotal,
                Total = regstroVenta.Total,

                Impuestos = (from c in regstroVenta.Te_impuestos_tiquetes
                             select new ImpuestoVenta
                             {
                                 IdImpuesto = c.Idimpuesto,
                                 Nombre = c.Te_impuesto.Nombre,
                                 TotalImpuesto = c.Valorcobrado,
                                 Valor = c.Valor,
                                 PrecioBase = c.Preciobase
                             }).ToList(),
                Boletas = (from c in regstroVenta.Te_boletas_tiquetes
                           select new Boleta
                           {
                               CodigoBoleta = c.Codigo,
                               IdBono = c.Idbono,
                               IdTipoTiquete = c.Idtipoprecio,
                               IdProducto = c.Idproducto,
                               AplicaImpuesto = c.Idtipoprecio.HasValue ?
                                    _contexto.Te_impuestos_tiposprecios.Where(r => r.Idtipo == c.Idtipoprecio.Value).Count() > 0 :
                                    c.Idproducto.HasValue ? _contexto.Te_impuestos_productos.Where(r => r.Idproducto == c.Idproducto.Value).Count() > 0 : true,
                               Precio = c.Preciopagado 
                           }).ToList(),
                Descuentos = (
                                from c in regstroVenta.Te_descuentos_tiquetes
                                select new Descuento
                                {
                                    Tipo = c.Tipodescuento,
                                    Valor = c.Valor,
                                    Nombre = c.Tipodescuento == 'C' ? "Desc. Cumpl" : c.Tipodescuento == 'V' ? "Desc. VIP" : c.Tipodescuento == 'D' ? "Desc. Nombr" : c.Tipodescuento == 'P' ? "Desc. Promo" : c.Tipodescuento == 'S' ? "Desc. Promoción" : ""
                                }
                                ).ToList(),
                ImprimirFactura = imprimirFactura,
                MedioPagos = (from c in regstroVenta.Te_mediospagotiquetes
                              select new MedioPago
                              {
                                  Valor = c.Valorpagado,
                                  //Nombre = c.Mediopago,
                                  Descripcion = c.Te_mediospago.Nombre,
                              }).ToList(),
                TiquetesCompra = (from c in regstroVenta.Te_tipotiquete_tiquetes
                                  select new TiqueteCompra
                                  {
                                      Nombre = (c.Idtipoprecio.HasValue) ? c.Te_precios_tipo.Nombre : (c.Idproducto.HasValue) ? c.Te_producto.Nombre : "Estandar",
                                      Cantidad = c.Cantidad,
                                      IdProducto = c.Idproducto,
                                      IdTipoTiquete = c.Idtipoprecio,
                                      Precio = c.Precioboleta,
                                  }
                               ).ToList()
            };

            var boletas = venta.Boletas;
            //GenerarImagenesBoleta(boletas, pancho, palto, elementos, configuracion, venta.Fecha, _contexto);


            boletas = boletas.DistinctBy(c => c.CodigoBoleta).ToList();
            venta.Boletas = boletas;


            if (venta.Estado == "Anulada")
            {
                foreach (var item in venta.Boletas)
                {
                    item.Precio = 0;
                }

                foreach (var item in venta.Descuentos)
                {
                    item.Valor = 0;
                }

                foreach (var item in venta.MedioPagos)
                {
                    item.Valor = 0;
                }

                foreach (var item in venta.Impuestos)
                {
                    item.Valor = 0;
                    item.TotalImpuesto = 0;
                    item.PrecioBase = 0;
                }

                venta.SubTotal = 0;
                venta.Total = 0;

            }
            
            
            Bitmap imagen = Despachos.Vista.ImageUtilities.ImprimirReciboComoImagen(venta, nombreFactura, 600);
            //Bitmap logo =new Bitmap(HttpContext.Current.Server.MapPath("~/Images/logo_t.png"));
            factura.Size=new SizeU(Unit.Pixel(imagen.Width),Unit.Pixel(imagen.Height));
            factura.Value = imagen;

            
        }

        private void ReporteVerFactura_NeedDataSource(object sender, EventArgs e)
        {
            idTiquete = ((Telerik.Reporting.Processing.Report)sender).Parameters["Id"].Value.ToString();
        }
    }
}