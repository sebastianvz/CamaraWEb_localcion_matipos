using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;
using Kendo.Mvc.Extensions;
using Verificador.Vista.Models;

namespace Verificador.Vista.Controllers
{
    public class ImagenesController : Controller
    {
        [HttpPost]
        public ActionResult CargarImagen()
        {
            if (Request.Files.Count > 0)
            {
                var file = Request.Files[0];

                if (file != null && file.ContentLength > 0)
                {
                    string id = Request.Form["iditem"];
                    byte[] imagen = new byte[file.ContentLength];
                    file.InputStream.Read(imagen, 0, file.ContentLength);

                    if (string.IsNullOrEmpty(id))
                    {
                        Session[Comun.ImagenMemoria] = imagen;
                    }
                    else
                    {
                        Session[string.Format("{0}_{1}", Comun.ImagenMemoria, id)] = imagen;
                    }
                }
            }
            return Content("");
        }
    }
}
