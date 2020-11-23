using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.Net.Mail;
using System.Net;
using Verificador.OpenAccess;
using System.Runtime.InteropServices;
using System.IO;
using System.Web.Hosting;
using System.Data;
using System.Data.SqlClient;
using Verificador.Vista.Models.Servicios;
using Verificador.Vista.Models.ViewModels;
using Zen.Barcode;
using System.Drawing;
using System.Drawing.Imaging;
using com.google.zxing;
using com.google.zxing.qrcode;


namespace Verificador.Vista.Models
{
    public static class Utilidades
    {

        private static string UsuarioActual = "UsuarioActual";

        public static void SetCurrentUser(BaseUsuarioViewModel usuario)
        {
            HttpContext.Current.Session[Utilidades.UsuarioActual] = usuario;
            Usuario = usuario;
        }

        public static BaseUsuarioViewModel Usuario
        {
            get { return HttpContext.Current.Session["Usuario"] as BaseUsuarioViewModel; }
            set { HttpContext.Current.Session["Usuario"] = value; }
        }

        public static BaseUsuarioViewModel GetCurrentUser()
        {
            try
            {
                return HttpContext.Current.Session[Utilidades.UsuarioActual] as BaseUsuarioViewModel;
            }
            catch
            {
                return null;
            }
        }

        public static bool TienePermiso(string permiso)
        {
            var usuario = GetCurrentUser();
            if (usuario != null)
            {
                return usuario.TienePermiso(permiso);
            }
            return false;
        }

        [DllImport("Iphlpapi.dll")]
        private static extern int SendARP(Int32 dest, Int32 host, ref Int64 mac, ref Int32 length);
        [DllImport("Ws2_32.dll")]
        private static extern Int32 inet_addr(string ip);

        public static string GetClientIP(HttpRequestBase request)
        {
            return request.UserHostAddress; //.ServerVariables["REMOTE_ADDR"];
        }

        public static string GetClientMac(HttpRequestBase request)
        {
            string userip = request.UserHostAddress;
            string strClientIP = request.UserHostAddress.ToString().Trim();
            Int32 ldest = inet_addr(strClientIP);
            Int32 lhost = inet_addr("");
            Int64 macinfo = new Int64();
            Int32 len = 6;
            int res = SendARP(ldest, 0, ref macinfo, ref len);
            string mac_src = macinfo.ToString("X");

            while (mac_src.Length < 12)
            {
                mac_src = mac_src.Insert(0, "0");
            }

            string mac_dest = string.Empty;

            for (int i = 0; i < 11; i++)
            {
                if (0 == (i % 2))
                {
                    if (i == 10)
                    {
                        mac_dest = mac_dest.Insert(0, mac_src.Substring(i, 2));
                    }
                    else
                    {
                        mac_dest = string.Format("{0}", mac_dest.Insert(0, mac_src.Substring(i, 2)));
                    }
                }
            }
            return mac_dest;
        }

        public static void EnviarCorreoConAdjuntos(string asunto, string cuerpo, string correoNotificaciones, List<string> adjuntos)
        {
            MailMessage email;

            Entities _contexto = new Entities();
            var correoEnvio = ConfigurationManager.AppSettings["CorreoEnvio"];

            var correos = correoNotificaciones.Split(";".ToCharArray());

            email = new MailMessage();
            email.From = new MailAddress(correoEnvio);

            foreach (var item in correos)
            {
                if (!string.IsNullOrEmpty(item))
                {
                    email.To.Add(new MailAddress(item.Trim()));
                }
            }



            email.Subject = asunto;
            email.Body = cuerpo;

            foreach (var item in adjuntos)
            {
                email.Attachments.Add(new Attachment(item));
            }

            EnviarCorreo(email);

        }

        public static void EnviarCorreo(MailMessage mensaje)
        {
            string mensajeNoResponder = "<p align='justify'><i>Este correo es informativo, favor no responder a este correo, ya que no se encuentra habilitado para recibir mensajes.</i></p>";
            mensaje.Body = string.Format("{0}{1}", mensaje.Body, mensajeNoResponder);
            var mailserver = "smtp.gmail.com";
            var correoEnvio = "devsebastianvz@gmail.com";
            var pw = "Lavida52!";
            var puerto = int.Parse("465");
            var ssl = "S";
            var tipoSeguridad = "3";
            var componenteEnvio = "2";

            //if (componenteEnvio == "2")
            //{
            //    EASendMail.SmtpMail oMail = new EASendMail.SmtpMail("ES-C1407722592-00334-C3V1A797A3C4TF23-484C1FU6B7D4654U");
            //    EASendMail.SmtpClient oSmtp = new EASendMail.SmtpClient();

            //    // Set sender email address, please change it to yours
            //    foreach (var item in mensaje.To)
            //    {
            //        oMail.To.Add(item.Address);
            //    }
            //    foreach (var item in mensaje.CC)
            //    {
            //        oMail.Cc.Add(item.Address);
            //    }
            //    foreach (var item in mensaje.Bcc)
            //    {
            //        oMail.Bcc.Add(item.Address);
            //    }
            //    byte[] array;
            //    foreach (var item in mensaje.Attachments)
            //    {
            //        array = new byte[item.ContentStream.Length];
            //        item.ContentStream.Read(array, 0, (int)item.ContentStream.Length);
            //        oMail.AddAttachment(item.Name, array);
            //    }

            //    oMail.From = correoEnvio;
            //    // Set email subject
            //    oMail.Subject = mensaje.Subject;

            //    // Set email body
            //    oMail.HtmlBody = mensaje.Body;

            //    // Your SMTP server address
            //    EASendMail.SmtpServer oServer = new EASendMail.SmtpServer(mailserver);

            //    // User and password for ESMTP authentication, if your server doesn't require
            //    // User authentication, please remove the following codes.
            //    oServer.User = correoEnvio;
            //    oServer.Password = pw;

            //    // Set 25 port
            //    oServer.Port = puerto;

            //    // detect TLS connection automatically

            //    switch (tipoSeguridad)
            //    {
            //        case "1":
            //            oServer.ConnectType = EASendMail.SmtpConnectType.ConnectDirectSSL;
            //            break;
            //        case "2":
            //            oServer.ConnectType = EASendMail.SmtpConnectType.ConnectNormal;
            //            break;
            //        case "3":
            //            oServer.ConnectType = EASendMail.SmtpConnectType.ConnectSSLAuto;
            //            break;
            //        case "4":
            //            oServer.ConnectType = EASendMail.SmtpConnectType.ConnectSTARTTLS;
            //            break;
            //        case "5":
            //            oServer.ConnectType = EASendMail.SmtpConnectType.ConnectTryTLS;
            //            break;
            //    }
            //    oSmtp.SendMail(oServer, oMail);
            //}

        }

        public static string GetPrograma()
        {
            var bytes = Convert.FromBase64String(ConfigurationManager.AppSettings["programa"]);
            var programa = Encoding.UTF8.GetString(bytes);
            return Decrypt(programa);
        }

        public static int GetClientes()
        {
            var bytes = Convert.FromBase64String(ConfigurationManager.AppSettings["clientes"]);
            var programa = Encoding.UTF8.GetString(bytes);
            return int.Parse(Decrypt(programa));
        }

        public static bool GetStatusUsuarioEmpresario(int idUsuario)
        {
            ServicioUsuariosEmpresarios servicio = new ServicioUsuariosEmpresarios();
            var Status = servicio.ReadUser(idUsuario);
            return Status;
        }

        public static string ObtenerEstadoLicencia()
        {

            string programa = GetPrograma();
            var serial = Decrypt(ConfigurationManager.AppSettings["serial"]);

            SKGL.SerialKeyConfiguration skc = new SKGL.SerialKeyConfiguration();
            SKGL.Validate ValidateAKey = new SKGL.Validate(skc);

            ValidateAKey.secretPhase = programa;
            ValidateAKey.Key = serial;


            if (ValidateAKey.IsValid)
            {

                if (ValidateAKey.Features[0] == false)
                {
                    if (Math.Abs(ValidateAKey.DaysLeft) <= 30)
                    {
                        return string.Format("La versión del programa finaliza en {0} días . Por favor actualice su licencia.", Math.Abs(ValidateAKey.DaysLeft));
                    }
                }

                if (ValidateAKey.Features[0] == true)
                {

                    if (ValidateAKey.Features[1] == false)
                    {
                        if (Math.Abs(ValidateAKey.DaysLeft) <= 30)
                        {
                            return string.Format("La versión del programa finaliza en {0} días . Por favor actualice su licencia.", Math.Abs(ValidateAKey.DaysLeft));
                            //return "La versión del programa finalizo. Por favor ingrese una licencia valida.";
                        }
                    }
                    else if (ValidateAKey.Features[1] == true)
                    {

                    }

                }
            }
            else
            {
                return "El serial ingresado no es válido.";
            }

            return string.Empty;
        }

        public static string ValidarIngreso()
        {

            string programa = GetPrograma();
            var serial = Decrypt(ConfigurationManager.AppSettings["serial"]);

            SKGL.SerialKeyConfiguration skc = new SKGL.SerialKeyConfiguration();
            SKGL.Validate ValidateAKey = new SKGL.Validate(skc);

            ValidateAKey.secretPhase = programa;
            ValidateAKey.Key = serial;


            if (ValidateAKey.IsValid)
            {

                if (ValidateAKey.Features[0] == false)
                {
                    if (ValidateAKey.DaysLeft < 0)
                    {
                        return "La versión trial del programa finalizo. Por favor ingrese una licencia valida.";
                    }
                }

                if (ValidateAKey.Features[0] == true)
                {

                    if (ValidateAKey.Features[1] == false)
                    {
                        if (ValidateAKey.DaysLeft < 0)
                        {
                            return "La versión del programa finalizo. Por favor ingrese una licencia valida.";
                        }
                    }
                    else if (ValidateAKey.Features[1] == true)
                    {

                    }

                }
            }
            else
            {
                return "El serial ingresado no es válido.";
            }

            return string.Empty;
        }

        private static string password = "Pas5pr@se";
        private static string saltValue = "s@1tValue";
        private static string algoritmo = "SHA1";

        public static string Encrypt(string texto)
        {
            return RijndaelEncryptDecrypt.EncryptDecryptUtils.Encrypt(texto, password, saltValue, algoritmo);
        }

        public static string Decrypt(string texto)
        {
            return RijndaelEncryptDecrypt.EncryptDecryptUtils.Decrypt(texto, password, saltValue, algoritmo);
        }

        public static int ObtenerDiaSemana(DateTime fecha)
        {
            switch (fecha.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return 1;
                case DayOfWeek.Tuesday:
                    return 2;
                case DayOfWeek.Wednesday:
                    return 3;
                case DayOfWeek.Thursday:
                    return 4;
                case DayOfWeek.Friday:
                    return 5;
                case DayOfWeek.Saturday:
                    return 6;
                case DayOfWeek.Sunday:
                    return 7;

            }
            return -1;
        }

        public static void RegistrarError(Exception error)
        {
            var ruta = HostingEnvironment.MapPath("~/log.txt");
            File.WriteAllText(ruta, error.Message + " " + error.StackTrace);
            error = null;
        }

        public static void ExcuteProcedure(SqlParameter[] Parameters, string Procedure)
        {
            using (SqlConnection cnx = new SqlConnection(ConfigurationManager.ConnectionStrings["conexion"].ConnectionString))
            {
                SqlCommand oCom = new SqlCommand();
                oCom.Connection = cnx;
                oCom.CommandType = CommandType.StoredProcedure;
                oCom.CommandText = Procedure;
                oCom.Parameters.AddRange(Parameters);
                try
                {
                    cnx.Open();
                    oCom.ExecuteNonQuery();
                }
                catch (Exception)
                {
                    cnx.Close();
                }
            }
        }

        public static byte[] GenerarImagenCodigoBarras(string codigo, int idTipoCodigoBarras, decimal altoMaximo)
        {
            if (idTipoCodigoBarras <= 0 || string.IsNullOrWhiteSpace(codigo))
            {
                return null;
            }


            BarcodeDraw barcodeDraw = BarcodeDrawFactory.Code128WithChecksum;

            switch (idTipoCodigoBarras)
            {
                case 1:
                    barcodeDraw = BarcodeDrawFactory.Code11WithChecksum;
                    break;
                case 2:
                    barcodeDraw = BarcodeDrawFactory.Code25StandardWithChecksum;
                    break;
                case 3:
                    barcodeDraw = BarcodeDrawFactory.Code39WithChecksum;
                    break;
                case 4:
                    barcodeDraw = BarcodeDrawFactory.Code93WithChecksum;
                    break;
                case 5:
                    barcodeDraw = BarcodeDrawFactory.Code128WithChecksum;
                    break;
                case 6:
                    barcodeDraw = BarcodeDrawFactory.CodePdf417;
                    break;
            }

            var bitmap = new Bitmap(barcodeDraw.Draw(codigo, (int)altoMaximo));

            var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Png);
            var bitmapBytes = memoryStream.ToArray();
            return bitmapBytes;
        }

        public static byte[] GenerarImagenCodigoQR(string codigo, int idTipoQr, decimal maxHeight)
        {

            if (idTipoQr <= 0 || string.IsNullOrWhiteSpace(codigo))
            {
                return null;
            }

            switch (idTipoQr)
            {
                case 2:
                    codigo = "BEGIN:VCARD" + "\n" +
                    "VERSION: 3.0" + "\n" +
                    "N: David Vivas" + "\n" +
                    "ORG: Matipos" + "\n" +
                    "TITLE:Gerente" + "\n" +
                    "TEL:3006116312" + "\n" +
                    "URL: www.manillasparaeventos.com" + "\n" +
                    "EMAIL:david.vivas @manillasparaeventos.com" + "\n" +
                    "ADR:cr 79 49 78" + "\n" +
                    "END:VCARD";
                    break;
                default:
                    break;
            }

            var bitmap = new Bitmap(QRCode(codigo, (int)maxHeight));

            var memoryStream = new MemoryStream();
            bitmap.Save(memoryStream, ImageFormat.Png);
            var bitmapBytes = memoryStream.ToArray();

            return bitmapBytes;
        }

        private static Bitmap QRCode(string codeMessage, int size)
        {

            var writer = new QRCodeWriter();

            var matrix = writer.encode(codeMessage, BarcodeFormat.QR_CODE, size, size, null);

            var img = new Bitmap(size, size);

            for (var y = 0; y < matrix.Height; ++y)
                for (var x = 0; x < matrix.Width; ++x)
                {
                    //Find the colour of the dot
                    img.SetPixel(x, y, matrix.get_Renamed(x, y) == -1 ? System.Drawing.Color.White : System.Drawing.Color.Black);
                }

            return img;
        }
    }
}