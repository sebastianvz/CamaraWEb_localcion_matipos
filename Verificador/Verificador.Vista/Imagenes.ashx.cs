using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.SessionState;
using Verificador.Vista.Models;
using Verificador.OpenAccess;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;

namespace Verificador.Vista
{
    /// <summary>
    /// Descripción breve de Imagenes
    /// </summary>
    public class Imagenes : IHttpHandler, IReadOnlySessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            byte[] imagenArray = null; ;
            Entities entities = new Entities();
            
            string strid = context.Request["id"];
            string tipo = context.Request["tipo"];
            int id = int.Parse(strid);
            context.Response.ContentType = "image/jpeg";
            if (tipo == "logo")
            {
                string ruta = HttpContext.Current.Server.MapPath(string.Format("~/Images/l{0}.png",id));
                if (File.Exists(ruta))
                {
                    imagenArray = File.ReadAllBytes(ruta);                    
                    context.Response.OutputStream.Write(imagenArray, 0, imagenArray.Length);                    
                }
            }
            else
            {
                /*var registro = entities.Transacciones.FirstOrDefault(c => c.Id == id);
                string codigo = registro.Registro;

                Bitmap imagen = new Bitmap(220, 110);
                //Bitmap imagen = new Bitmap(300 * pancho.Valorint.Value / 96, 300 * palto.Valorint.Value / 96);
                //imagen.SetResolution(300, 300);

                //imagen.SetResolution(300.0F, 300.0F);
                Graphics g = Graphics.FromImage(imagen);
                Zen.Barcode.BarcodeDraw bdf = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                //-if (item.Codigobarras == "Code11")
                    bdf = Zen.Barcode.BarcodeDrawFactory.Code11WithChecksum;
                else if (item.Codigobarras == "Code25")
                    bdf = Zen.Barcode.BarcodeDrawFactory.Code25StandardWithChecksum;
                else if (item.Codigobarras == "Code39")
                    bdf = Zen.Barcode.BarcodeDrawFactory.Code39WithChecksum;
                else if (item.Codigobarras == "Code128")
                    bdf = Zen.Barcode.BarcodeDrawFactory.Code128WithChecksum;
                else if (item.Codigobarras == "CodeEan8")
                    bdf = Zen.Barcode.BarcodeDrawFactory.CodeEan8WithChecksum;
                else if (item.Codigobarras == "CodeEan13")
                    bdf = Zen.Barcode.BarcodeDrawFactory.CodeEan13WithChecksum;
                else if (item.Codigobarras == "CodePdf417")
                    bdf = Zen.Barcode.BarcodeDrawFactory.CodePdf417;-//

                Bitmap temp = new Bitmap(bdf.Draw(codigo, 80));
                //temp.SetResolution(300.0F, 300.0F);
                
                g.DrawImage(temp, 0, 0);

                MemoryStream stream = new MemoryStream();
                temp.Save(stream, ImageFormat.Jpeg);
                stream.Close();
                stream.Dispose();

                var bytes = stream.ToArray();
                context.Response.OutputStream.Write(bytes, 0, bytes.Length);
                */
            }
            
           


            /*if (tipo == "nuevo")
            {
                HttpContext.Current.Session[Comun.ImagenMemoria] = null;
            }
            else if (tipo == "memoria" && string.IsNullOrEmpty(strid) && HttpContext.Current.Session[Comun.ImagenMemoria] != null)
            {
                imagen = HttpContext.Current.Session[Comun.ImagenMemoria] as byte[];                
            }
            else if (tipo == "memoria" && !string.IsNullOrEmpty(strid) && HttpContext.Current.Session[string.Format("{0}_{1}", Comun.ImagenMemoria, strid)] != null)
            {
                imagen = HttpContext.Current.Session[string.Format("{0}_{1}", Comun.ImagenMemoria, strid)] as byte[];
            }
            else if (tipo == "servicio")
            {
                HttpContext.Current.Session[Comun.ImagenMemoria] = null;
                id =  int.Parse(context.Request["id"]);
                var registro = entities.Te_productos.FirstOrDefault(c => c.Id == id);
                if (registro != null)
                {
                    imagen = registro.Logo;    
                }
            }
            else if (tipo == "elemento")
            {
                HttpContext.Current.Session[Comun.ImagenMemoria] = null;
                id = int.Parse(context.Request["id"]);
                var registro = entities.Te_elementos.FirstOrDefault(c => c.Id == id);
                if (registro != null)
                {
                    imagen = registro.Imagen;
                }
            }
            else if (tipo == "logo")
            {
                HttpContext.Current.Session[Comun.ImagenMemoria] = null;
                
                var registro = entities.Te_configs.First();
                if (registro.Logo != null)
                {
                    imagen = registro.Logo;
                }
            }
            else if (tipo == "qr")
            {                   
                imagen = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Images/qr.png"));                
            }
            else if (tipo == "barras")
            {
                imagen = File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/Images/barras.png"));                
            }


            if (imagen != null)
            {
                context.Response.OutputStream.Write(imagen, 0, imagen.Length);
            }*/
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}