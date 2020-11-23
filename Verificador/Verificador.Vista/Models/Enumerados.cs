using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;
using Verificador.Vista.Models.ViewModels;

namespace Verificador.Vista.Models
{
    public class Enumerados
    {
        public static string ObtenerDescripcionEnumerado(Enum valor)
        {
            var campo = valor.GetType().GetField(valor.ToString());

            var atributo = Attribute.GetCustomAttribute(campo, typeof(DescriptionAttribute)) as DescriptionAttribute;

            return atributo == null ? valor.ToString() : atributo.Description;
        }


        [Flags]
        public enum OrientacionElementoPlantilla
        {
            [Description("Horizontal")]
            Horizontal = 1,

            [Description("Vertical Hacia Abajo")]
            VerticalHaciaAbajo = 2,

            [Description("Vertical Hacia Arriba")]
            VerticalHaciaArriba = 3
        }

        public static List<EnumeradoViewModel> OrientacionesElementosPlantilla()
        {
            var codigos = Enum.GetNames(typeof(OrientacionElementoPlantilla));
            var valores = Enum.GetValues(typeof(OrientacionElementoPlantilla));

            return codigos.Select((t, i) => new EnumeradoViewModel
            {
                Id = Convert.ToInt32(valores.GetValue(i)),
                Codigo = t,
                Descripcion = ObtenerDescripcionEnumerado((OrientacionElementoPlantilla)Convert.ToInt32(valores.GetValue(i)))
            }
            ).ToList();
        }

        [Flags]
        public enum TipoCodigoBarras
        {
            [Description("Code11")]
            Code11 = 1,

            [Description("Code25")]
            Code25 = 2,

            [Description("Code39")]
            Code39 = 3,

            [Description("Code93")]
            Code93 = 4,

            [Description("Code128")]
            Code128 = 5,

            [Description("CodePdf417")]
            CodePdf417 = 6
        }

        public static List<EnumeradoViewModel> TiposCodigoBarras()
        {
            var codigos = Enum.GetNames(typeof(TipoCodigoBarras));
            var valores = Enum.GetValues(typeof(TipoCodigoBarras));

            return codigos.Select((t, i) => new EnumeradoViewModel
            {
                Id = Convert.ToInt32(valores.GetValue(i)),
                Codigo = t,
                Descripcion = ObtenerDescripcionEnumerado((TipoCodigoBarras)Convert.ToInt32(valores.GetValue(i)))
            }
            ).ToList();
        }

        public enum TipoQr
        {
            [Description("QR Code")]
            QrCode = 1,

            [Description("Qr Vcard")]
            QrVcard = 2,

        }

        public static List<EnumeradoViewModel> TiposQr()
        {
            var codigos = Enum.GetNames(typeof(TipoQr));
            var valores = Enum.GetValues(typeof(TipoQr));

            return codigos.Select((t, i) => new EnumeradoViewModel
            {
                Id = Convert.ToInt32(valores.GetValue(i)),
                Codigo = t,
                Descripcion = ObtenerDescripcionEnumerado((TipoQr)Convert.ToInt32(valores.GetValue(i)))
            }
            ).ToList();
        }

        public enum ElementoXDefecto
        {
            [Description("Nombre del evento")]
            NombreEvento = 1,
            [Description("Fecha del evento")]
            FechaEvento = 2,
            [Description("Lugar del evento")]
            LugarEvento = 3,
            [Description("Organizador del evento")]
            OrganizadorEvento = 4,
            [Description("Hora del evento")]
            HoraEvento = 5,
            [Description("Precio")]
            Precio = 6,
            [Description("Etapa")]
            Etapa = 7,
            [Description("Hora de Apertura")]
            HoraApertura = 8,
            [Description("Edad minima")]
            EdadMinima = 9,
            [Description("Localida")]
            Localidad = 10,
            [Description("Nombre completo persona")]
            NombreCompleto= 11,
            [Description("Tipo documento y numero")]
            TipoDocumentoNumero = 12,
            [Description("Empresa")]
            Empresa = 13,
            [Description("Servicio tiquete")]
            ServicioTiquete = 14,
            [Description("Valido hasta ?")]
            ValidoHasta = 15,
            [Description("Codigo Boleto")]
            CdogioBoleto = 16, 
            [Description("Logo")]
            Logo = 17,
            [Description("Numero Boleto")]
            NumeroBoleto = 18,
            [Description("Imagen Evento")]
            ImagenEvento= 19,
            [Description("Texto responsable")]
            TextoResponsable = 20,

        }

        public static List<EnumeradoViewModel> ElementosXDefecto()
        {
            var codigos = Enum.GetNames(typeof(ElementoXDefecto));
            var valores = Enum.GetValues(typeof(ElementoXDefecto));

            return codigos.Select((t, i) => new EnumeradoViewModel
            {
                Id = Convert.ToInt32(valores.GetValue(i)),
                Codigo = t,
                Descripcion = ObtenerDescripcionEnumerado((ElementoXDefecto)Convert.ToInt32(valores.GetValue(i)))
            }
            ).OrderBy(c => c.Descripcion).ToList();
        }
    }
}