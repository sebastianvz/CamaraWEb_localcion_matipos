using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Verificador.OpenAccess;
using Verificador.Vista.Models.ViewModels;

namespace Verificador.Vista.Models
{
    public class Comun
    {
        public static void WriteErrorLog(Exception ex, int idTrnsaccion = 0)
        {
            RegistrarenDB(ex.Message, EstadoLog.ERROR, idTrnsaccion);
        }

        public static void WriteErrorLog(string ex, int idTrnsaccion = 0)
        {
            RegistrarenDB(ex, EstadoLog.ERROR, idTrnsaccion);
        }

        public static void WriteLog(string Message, EstadoLog estado, int idTrnsaccion = 0)
        {
            RegistrarenDB(Message, estado, idTrnsaccion);
        }

        private static void RegistrarenDB(string descpcion, EstadoLog estado, int idTrnsaccion)
        {
            Entities db = new Entities();
            db.Add(new LogPrintTicket()
            {
                Descripcion = descpcion,
                Estado = estado.ToString(),
                IdTransaccioTicketImpresion = idTrnsaccion == 0 ? null : (int?)idTrnsaccion
            });
            db.SaveChanges();

        }


        public static string FormatoCortoFechaGrid
        {
            get
            {
                return "{0:" + System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern + "}";
            }
        }

        public static string FormatoLargoFechaGrid
        {
            get
            {
                return "{0:" + System.Threading.Thread.CurrentThread.CurrentCulture.DateTimeFormat.LongDatePattern + "}";
            }
        }

        public static string ImagenMemoria = "ImagenMemoria";

        public static string BonosGenerados = "BonosGenerados";

      
    }
}