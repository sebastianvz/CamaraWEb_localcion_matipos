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
    public partial class ReporteCoberturas : Telerik.Reporting.Report
    {
        public ReporteCoberturas()
        {
            //
            // Required for telerik Reporting designer support
            //            
            InitializeComponent();
            
            System.Threading.Thread.CurrentThread.CurrentUICulture =new System.Globalization.CultureInfo("es-ES");

            try
            {
                Entities entities = new Entities();
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
    }
}