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
    public partial class ReporteCierreCajaUsuario : Telerik.Reporting.Report
    {
        public ReporteCierreCajaUsuario()
        {
            //
            // Required for telerik Reporting designer support
            //            
            InitializeComponent();

            System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("es-ES");

            try
            {
                DateTime fechaActual = new DateTime(2015, 8, 7);



            }
            catch (Exception err)
            {
            }
            //
            // TODO: Add any constructor code after InitializeComponent call
            //
        }


        private void textBox53_ItemDataBound(object sender, EventArgs e)
        {

            Telerik.Reporting.Processing.TextBox txt = sender as Telerik.Reporting.Processing.TextBox;
            var valor = (decimal)txt.Value;
            if (valor > 0)
            {
                txt.Style.BackgroundColor = Color.Green;
            }
            else if (valor < 0)
            {
                txt.Style.BackgroundColor = Color.Red;
            }
            else
            {
                txt.Style.BackgroundColor = Color.Transparent;
            }



        }
    }
}