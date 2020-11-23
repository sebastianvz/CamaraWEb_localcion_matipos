namespace Despachos.Vista.Reports
{
    using System.Linq;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Telerik.Reporting;
    using Telerik.Reporting.Drawing;
    using Despachos.OpenAccess;
    using System.Globalization;

    /// <summary>
    /// Summary description for ReporteDevoluciones.
    /// </summary>
    public partial class ReporteCierreCaja : Telerik.Reporting.Report
    {
        DateTime fechaActual;
        public ReporteCierreCaja()
        {
            //
            // Required for telerik Reporting designer support
            //            
            InitializeComponent();
            
            System.Threading.Thread.CurrentThread.CurrentUICulture =new System.Globalization.CultureInfo("es-ES");



        }

        private void ReporteCierreCaja_NeedDataSource(object sender, EventArgs e)
        {
            fechaActual = (DateTime)((Telerik.Reporting.Processing.Report)sender).Parameters["fecha"].Value;
        }

        private void ReporteCierreCaja_ItemDataBound(object sender, EventArgs e)
        {
            try
            {
                txtFecha.Value = fechaActual.ToString("yyyy-MM-dd");
                Entities entities = new Entities();

                var config = entities.Te_configs.First();

                DateTime fechaInicial;
                DateTime fechaFinal;

                string longFormat = "yyyy-MM-dd HH:mm:ss";
                var trabajaPasandoNoche = entities.Te_parametros.First(c => c.Nombre == "TrabajaPasandoNoche");
                var horaInicialTrabajo = entities.Te_parametros.First(c => c.Nombre == "HoraInicialTrabajo");
                var horaFinalTrabajo = entities.Te_parametros.First(c => c.Nombre == "HoraFinalTrabajo");
                var prefijoFactura = entities.Te_parametros.First(c => c.Nombre == "PrefijoFactura").Valorstring;


                if (trabajaPasandoNoche.Valorint == 0)
                {
                    fechaInicial = fechaActual.Date;
                    fechaFinal = fechaInicial.AddDays(1).AddSeconds(-1);
                }
                else
                {
                    if (fechaActual.Hour >= 0)
                    {
                        fechaActual = fechaActual.AddDays(-1);
                    }
                    fechaInicial = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day, horaInicialTrabajo.Valorint.Value, 0, 0);
                    fechaFinal = new DateTime(fechaActual.Year, fechaActual.Month, fechaActual.Day);
                    fechaFinal = fechaFinal.AddDays(1).AddHours(horaFinalTrabajo.Valorint.Value);
                }

                var nombreEstablecimiento = entities.Te_parametros.First(c => c.Nombre == "NombreEstablecimiento").Valorstring;

                var movimientoInicial = (from c in entities.Te_tiquetes
                                         where c.Fecha >= fechaInicial && c.Fecha <= fechaFinal && c.Cancelada == 0
                                         orderby c.Id ascending
                                         select c).FirstOrDefault();

                var movimientoFinal = (from c in entities.Te_tiquetes
                                       where c.Fecha >= fechaInicial && c.Fecha <= fechaFinal && c.Cancelada == 0
                                       orderby c.Id descending
                                       select c).FirstOrDefault();


                txtNit.Value = string.Format("Nit:{0}", entities.Te_parametros.First(c => c.Nombre == "Nit").Valorstring);
                txtNombre.Value = string.Format("{0} - {1}", config.Nombre, nombreEstablecimiento);

                txtEstablecimiento.Value = nombreEstablecimiento;
                txtRango.Value = string.Format("Desde {0} Hasta {1}", fechaInicial.ToString(longFormat), fechaFinal.ToString(longFormat));
                txtMovimientoRango.Value = string.Format("Movimiento Inicial {0} Movimiento Final {1}", movimientoInicial.Numerofactura, movimientoFinal.Numerofactura);


                /////////////////////////////////////////////////////

                var debito = (from c in entities.Te_mediospagotiquetes
                              where c.Te_tiquete.Fecha >= fechaInicial && c.Te_tiquete.Fecha <= fechaFinal && c.Te_tiquete.Cancelada == 0
                              && c.Idmedio == 2
                              select c.Valorpagado
                                  ).Sum();

                totalDebito.Value = String.Format("$ {0:N0}", debito);

                var credito = (from c in entities.Te_mediospagotiquetes
                               where c.Te_tiquete.Fecha >= fechaInicial && c.Te_tiquete.Fecha <= fechaFinal && c.Te_tiquete.Cancelada == 0
                               && c.Idmedio == 3
                               select c.Valorpagado
                                  ).Sum();

                totalCredito.Value = String.Format("$ {0:N0}", credito);

                var cuentascobrar = (from c in entities.Te_mediospagotiquetes
                                     where c.Te_tiquete.Fecha >= fechaInicial && c.Te_tiquete.Fecha <= fechaFinal && c.Te_tiquete.Cancelada == 0
                                     && c.Idmedio == 4
                                     select c.Valorpagado
                                  ).Sum();

                totalCuentas.Value = String.Format("$ {0:N0}", cuentascobrar);

                var efectivo = (from c in entities.Te_mediospagotiquetes
                                where c.Te_tiquete.Fecha >= fechaInicial && c.Te_tiquete.Fecha <= fechaFinal && c.Te_tiquete.Cancelada == 0
                                && c.Idmedio == 1
                                select c.Valorpagado
                                  ).Sum();

                var devoluciones = (from c in entities.Te_tiquetes
                                    where c.Fecha >= fechaInicial && c.Fecha <= fechaFinal && c.Cancelada == 1
                                    select c.Valorpagado
                                  ).Sum();

                var descuentos = (from c in entities.Te_descuentos_tiquetes
                                  where c.Te_tiquete.Fecha >= fechaInicial && c.Te_tiquete.Fecha <= fechaFinal && c.Te_tiquete.Cancelada == 0
                                  select c.Valor
                                  ).Sum();

                var bonos = (from c in entities.Te_boletas_tiquetes
                             where c.Te_tiquete.Fecha >= fechaInicial && c.Te_tiquete.Fecha <= fechaFinal && c.Te_tiquete.Cancelada == 0
                                && c.Idbono.HasValue
                             select c.Te_bono.Valor).Sum();

                var cortesias = (from c in entities.Te_boletas_tiquetes
                                 where c.Te_tiquete.Fecha >= fechaInicial && c.Te_tiquete.Fecha <= fechaFinal && c.Te_tiquete.Cancelada == 0
                                  && c.Te_tiquete.Escortesia == 1 && c.Idtipoprecio.HasValue
                                 select c).Count();

                totalCantidadCortesias.Value = String.Format("{0:N0}", cortesias);

                totalBonos.Value = String.Format("$ {0:N0}", bonos);

                totalDescuentos.Value = String.Format("$ {0:N0}", descuentos);

                totalDevoluciones.Value = String.Format("$ {0:N0}", devoluciones.HasValue ? devoluciones.Value : 0);

                totalEfectivo.Value = String.Format("$ {0:N0}", efectivo);

                totalDia.Value = String.Format("$ {0:N0}", (efectivo + credito + debito + cuentascobrar + bonos)); // (menos devoluciones donde sea pago ni cuenta por cobrar ni efectivo)

                totalCantidadInventario.Value = String.Format("{0:N0}", config.Cantidadmanillas);

            }
            catch (Exception err)
            {
            }
        }

        
    }
}