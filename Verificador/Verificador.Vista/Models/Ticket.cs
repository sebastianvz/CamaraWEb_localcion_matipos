using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Verificador.Vista.Models
{
    public static class _Ticket
    {
        /// <summary>
        /// Background Image
        /// </summary>
        public static class Background
        {
            private enum BackgroundType { 
                small = 0,
                Medium = 1,
                Large = 2
            }

            /// <summary>
            /// width: 50, Height: 140
            /// </summary>
            public static string Small
            {
                get
                {
                    return ReadImage(BackgroundType.small);
                }
            }

            /// <summary>
            /// width: 63, Height: 188
            /// </summary>
            public static string Large
            {
                get
                {
                    return ReadImage(BackgroundType.Large);
                }
            }

            private static string ReadImage(BackgroundType type) {
                try
                {
                    string url = "";
                    switch (type)
                    {
                        case BackgroundType.small: url = HttpContext.Current.Server.MapPath(@"~\img\bkT1ck3tMD.txt");
                            break;
                        case BackgroundType.Large: url = HttpContext.Current.Server.MapPath(@"~\img\bkT1ck3tXL.txt");
                            break;
                    }
                    string imageText = System.IO.File.ReadAllText(url);
                    return String.Format("data:image/png;base64,{0}", imageText);
                }
                catch (Exception)
                {
                    return string.Empty;
                }
            }

        }
    }
}