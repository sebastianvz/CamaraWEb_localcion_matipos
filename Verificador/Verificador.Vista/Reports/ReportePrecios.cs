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
    using Despachos.Vista.Models;
    using VentaTiquetes.Dominio.Gestion;

    /// <summary>
    /// Summary description for ReporteDevoluciones.
    /// </summary>
    public partial class ReportePrecios : Telerik.Reporting.Report
    {

        Entities entities = new Entities();


        public ReportePrecios()
        {
            //
            // Required for telerik Reporting designer support
            //            
            InitializeComponent();
            
            System.Threading.Thread.CurrentThread.CurrentUICulture =new System.Globalization.CultureInfo("es-ES");

            try
            {
                
                txtNit.Value = string.Format("Nit:{0}", entities.Te_parametros.First(c => c.Nombre == "Nit").Valorstring);

                var nombreEstablecimiento = entities.Te_parametros.First(c => c.Nombre == "NombreEstablecimiento").Valorstring;
                txtNombre.Value = string.Format("{0} - {1}", entities.Te_configs.First().Nombre, nombreEstablecimiento);
            }
            catch (Exception err)
            {                
            }
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }

        private void textBox8_ItemDataBound(object sender, EventArgs e)
        {
            try
            {
                var text = (sender as Telerik.Reporting.Processing.TextBox);
                var datos = text.Text.Split(",".ToCharArray());
                int idCategoria = int.Parse(datos[0]);
                int id = int.Parse(datos[1]);
                string tipo = datos[2];
                int dia = int.Parse(datos[3]);
                DateTime fechaActual = DateTime.Now;
                decimal result;
                if (tipo == "Entrada")
                {   
                    result = CalcularPreciosPorTipoPersona(idCategoria, id, dia, fechaActual);                    
                }
                else
                {
                    result = CalcularPreciosPorProducto(idCategoria, id, dia, fechaActual);                    
                }
                text.Value = string.Format("$ {0}", result.ToString("N0"));

            }
            catch { }
        }

        public decimal CalcularPreciosPorProducto(int idCategoria, int id, int diaSemana, DateTime fechaActual)
        {
            

            var trabajaPasandoNoche = entities.Te_parametros.First(c => c.Nombre == "TrabajaPasandoNoche");
            var horaInicialTrabajo = entities.Te_parametros.First(c => c.Nombre == "HoraInicialTrabajo");
            var horaFinalTrabajo = entities.Te_parametros.First(c => c.Nombre == "HoraFinalTrabajo");
            var aplicarDescuentos = entities.Te_parametros.First(c => c.Nombre == "AplicarDescuentos");

            

            var resultado = (from c in entities.Te_productos
                             where c.Id==id
                             select new TiqueteCompra
                             {
                                 IdCategoria = idCategoria,
                                 IdProducto = c.Id,
                                 Precio = c.Precio,
                                 Nombre = c.Nombre,
                                 Codigo = c.Codigo
                             }).ToList();



            int idProducto;
            Te_regla precioPorReglaHora;
            foreach (var item in resultado)
            {
                idProducto = item.IdProducto.Value;


                precioPorReglaHora = entities.Te_reglas.FirstOrDefault(c => c.Tipo == "R" && c.Idproducto == idProducto && !c.Idtipoprecio.HasValue &&
                                        fechaActual >= c.Horainicial && fechaActual <= c.Horafinal);


                if (precioPorReglaHora != null)
                {
                    item.Precio = (decimal)precioPorReglaHora.Valor;
                }
                else
                {
                    Te_precio precioPorFecha;

                    if (trabajaPasandoNoche.Valorint == 0)
                    {
                        precioPorFecha = entities.Te_precios.FirstOrDefault(c => c.Idcategoria == idCategoria && fechaActual.Date == c.Fecha && c.Idproducto == idProducto && !c.Idtipoprecio.HasValue);
                    }
                    else
                    {
                        precioPorFecha = entities.Te_precios.FirstOrDefault(c => c.Idcategoria == idCategoria && c.Idproducto == idProducto && !c.Idtipoprecio.HasValue &&
                            fechaActual >= new DateTime(c.Fecha.Value.Year, c.Fecha.Value.Month, c.Fecha.Value.Day, horaInicialTrabajo.Valorint.Value, 0, 0) &&
                            fechaActual <= (new DateTime(c.Fecha.Value.Year, c.Fecha.Value.Month, c.Fecha.Value.Day, 0, 0, 0).AddDays(1).AddHours(horaFinalTrabajo.Valorint.Value)));

                    }

                    if (precioPorFecha != null)
                    {
                        item.Precio = (decimal)precioPorFecha.Valor;
                    }
                    else
                    {
                        Te_precio precioPorDia = null;
                        if (trabajaPasandoNoche.Valorint == 0)
                        {
                            precioPorDia = entities.Te_precios.FirstOrDefault(c => c.Idcategoria == idCategoria && c.Dia == diaSemana && c.Idproducto == idProducto && !c.Idtipoprecio.HasValue);
                        }
                        else
                        {

                            DateTime fechaInicio = DateTime.MinValue;
                            DateTime fechaFin = DateTime.MinValue;
                            bool comparar = false;

                            if (fechaActual.Hour >= horaInicialTrabajo.Valorint.Value && fechaActual.Hour <= 23)
                            {
                                fechaInicio = fechaActual.Date;
                                fechaInicio = fechaInicio.AddHours(horaInicialTrabajo.Valorint.Value);

                                fechaFin = fechaActual.Date.AddDays(1);
                                fechaFin = fechaFin.AddHours(horaFinalTrabajo.Valorint.Value);
                                comparar = true;
                            }
                            else if (fechaActual.Hour >= 0 && fechaActual.Hour <= horaFinalTrabajo.Valorint.Value)
                            {
                                fechaInicio = fechaActual.Date.AddDays(-1);
                                fechaInicio = fechaInicio.AddHours(horaInicialTrabajo.Valorint.Value);

                                fechaFin = fechaActual.Date;
                                fechaFin = fechaFin.AddHours(horaFinalTrabajo.Valorint.Value);
                                comparar = true;
                            }

                            if (comparar == true && fechaActual >= fechaInicio && fechaActual <= fechaFin)
                            {
                                int dia = Utilidades.ObtenerDiaSemana(fechaInicio);
                                precioPorDia = entities.Te_precios.FirstOrDefault(c => c.Idcategoria == idCategoria && c.Dia == dia && c.Idproducto == idProducto && !c.Idtipoprecio.HasValue);
                            }
                        }

                        if (precioPorDia != null)
                        {
                            item.Precio = (decimal)precioPorDia.Valor;
                        }
                    }
                }
            }

            return resultado.Count > 0 ? resultado[0].Precio : 0m;

        }








        public decimal CalcularPreciosPorTipoPersona(int idCategoria, int id, int diaSemana, DateTime fechaActual)
        {
            

            var trabajaPasandoNoche = entities.Te_parametros.First(c => c.Nombre == "TrabajaPasandoNoche");
            var horaInicialTrabajo = entities.Te_parametros.First(c => c.Nombre == "HoraInicialTrabajo");
            var horaFinalTrabajo = entities.Te_parametros.First(c => c.Nombre == "HoraFinalTrabajo");
            var aplicarDescuentos = entities.Te_parametros.First(c => c.Nombre == "AplicarDescuentos");




            var resultado = (from c in entities.Te_conf_tipo_tiquete_categoria
                             where c.Idcategoria == idCategoria && c.Idtipo==id
                             select new TiqueteCompra
                             {
                                 IdCategoria = idCategoria,
                                 IdTipoTiquete = c.Idtipo,
                                 Precio = c.Te_precios_tipo.Precio,
                                 Nombre = c.Te_precios_tipo.Nombre,
                                 Codigo = c.Te_precios_tipo.Codigo
                             }).ToList();

            int idTipo;
            Te_regla precioPorReglaHora;
            foreach (var item in resultado)
            {
                idTipo = item.IdTipoTiquete.Value;


                precioPorReglaHora = entities.Te_reglas.FirstOrDefault(c => c.Tipo == "R" && c.Idtipoprecio == idTipo && !c.Idproducto.HasValue &&
                                        fechaActual >= c.Horainicial && fechaActual <= c.Horafinal);


                if (precioPorReglaHora != null)
                {
                    item.Precio = (decimal)precioPorReglaHora.Valor;
                }
                else
                {
                    Te_precio precioPorFecha;

                    if (trabajaPasandoNoche.Valorint == 0)
                    {
                        precioPorFecha = entities.Te_precios.FirstOrDefault(c => c.Idcategoria == idCategoria && fechaActual.Date == c.Fecha && c.Idtipoprecio == idTipo && !c.Idproducto.HasValue);
                    }
                    else
                    {
                        precioPorFecha = entities.Te_precios.FirstOrDefault(c => c.Idcategoria == idCategoria && c.Idtipoprecio == idTipo && !c.Idproducto.HasValue &&
                            fechaActual >= new DateTime(c.Fecha.Value.Year, c.Fecha.Value.Month, c.Fecha.Value.Day, horaInicialTrabajo.Valorint.Value, 0, 0) &&
                            fechaActual <= (new DateTime(c.Fecha.Value.Year, c.Fecha.Value.Month, c.Fecha.Value.Day, 0, 0, 0).AddDays(1).AddHours(horaFinalTrabajo.Valorint.Value)));

                    }

                    if (precioPorFecha != null)
                    {
                        item.Precio = (decimal)precioPorFecha.Valor;
                    }
                    else
                    {
                        Te_precio precioPorDia = null;
                        if (trabajaPasandoNoche.Valorint == 0)
                        {
                            precioPorDia = entities.Te_precios.FirstOrDefault(c => c.Idcategoria == idCategoria && c.Dia == diaSemana && c.Idtipoprecio == idTipo && !c.Idproducto.HasValue);
                        }
                        else
                        {

                            DateTime fechaInicio = DateTime.MinValue;
                            DateTime fechaFin = DateTime.MinValue;
                            bool comparar = false;

                            if (fechaActual.Hour >= horaInicialTrabajo.Valorint.Value && fechaActual.Hour <= 23)
                            {
                                fechaInicio = fechaActual.Date;
                                fechaInicio = fechaInicio.AddHours(horaInicialTrabajo.Valorint.Value);

                                fechaFin = fechaActual.Date.AddDays(1);
                                fechaFin = fechaFin.AddHours(horaFinalTrabajo.Valorint.Value);
                                comparar = true;
                            }
                            else if (fechaActual.Hour >= 0 && fechaActual.Hour <= horaFinalTrabajo.Valorint.Value)
                            {
                                fechaInicio = fechaActual.Date.AddDays(-1);
                                fechaInicio = fechaInicio.AddHours(horaInicialTrabajo.Valorint.Value);

                                fechaFin = fechaActual.Date;
                                fechaFin = fechaFin.AddHours(horaFinalTrabajo.Valorint.Value);
                                comparar = true;
                            }

                            if (comparar == true && fechaActual >= fechaInicio && fechaActual <= fechaFin)
                            {
                                int dia = Utilidades.ObtenerDiaSemana(fechaInicio);
                                precioPorDia = entities.Te_precios.FirstOrDefault(c => c.Idcategoria == idCategoria && c.Dia == dia && c.Idtipoprecio == idTipo && !c.Idproducto.HasValue);
                            }
                        }

                        if (precioPorDia != null)
                        {
                            item.Precio = (decimal)precioPorDia.Valor;
                        }
                    }
                }
            }

            return resultado.Count > 0 ? resultado[0].Precio : 0m;;

        }
        
    }
}