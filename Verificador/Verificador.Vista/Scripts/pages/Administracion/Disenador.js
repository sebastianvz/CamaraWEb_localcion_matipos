if ($(document).ready) {
    var tipo; //identifica si es texto = t, qr = q , barcode = b, image = im, elementoxdefecto = ed
    var canvas = new fabric.Canvas('Canvas', {
        backgroundColor: 'rgb(238, 238, 238)',
    });
    var disenador = JSON.parse(localStorage.getItem("disenador"));
    var archivo;
    var idTipoCodigoBarras;
    var idTipoQr;
    var maxHeight;
    var codigo;
    var url;
    var ImagenDefecto;
    document.querySelector("#NombrePlantilla").innerHTML = disenador.NombrePlantilla;
    // Sentencia que habilita el evento change del control de Upload
    document.getElementById("CargarArchivo").addEventListener("change", Cambio, false);
    //Deselecciona el elemento con el cambio de pestaña
    $('a[data-toggle="tab"]').on('click', function (e) { LimniarTodosLosCampos(); });
    //Cargar el diseño de la boleta o empezar desde 0
    if (disenador.canvas == null) {
        MostrarPapel();
    } else {
        canvas.clear();
        canvas.loadFromJSON(disenador.canvas, function ()
        {
            MostrarPapel();
            canvas.renderAll();
        });
    }

    var validatorFormTexto = window.$("#FormTexto").kendoValidator().data("kendoValidator");
    var validatorFormCodigoBarras = window.$("#FormCodigoBarras").kendoValidator().data("kendoValidator");
    var validatorFormQrCode = window.$("#FormQrCode").kendoValidator().data("kendoValidator");
    var validatorFormImagen = window.$("#FormImagen").kendoValidator().data("kendoValidator");
    var validatorFormDefault = window.$("#FormDefault").kendoValidator().data("kendoValidator");

    $("#FormTexto").submit(
   function (e) {
       e.preventDefault();
       if (validatorFormTexto.validate()) {
           tipo = "t";
           AgregarElemento();
       }
   });

    $("#FormCodigoBarras").submit(
    function (e) {
        e.preventDefault();
        if (validatorFormCodigoBarras.validate()) {
            tipo = "b";
            AgregarElemento();
        }
    });

    $("#FormQrCode").submit(
    function (e) {
        e.preventDefault();
        if (validatorFormQrCode.validate()) {
            tipo = "q";
            AgregarElemento();
        }
    });

    $("#FormImagen").submit(
    function (e) {
        e.preventDefault();
        if (validatorFormImagen.validate()) {
            tipo = "im";
            AgregarElemento();
        }
    });

    $("#FormDefault").submit(
    function (e) {
        e.preventDefault();
        if (validatorFormDefault.validate()) {
            tipo = "ed";
            AgregarElemento();
        }
    });

    //Crea el primer elemento del cnvas que seria el tipo de papel 
    function MostrarPapel() {
        if (disenador.imagenBoelto != null)
        {
            imagenBoleta = new Image();
            imagenBoleta.onload = function ()
            {
                var imgbc = new fabric.Image(imagenBoleta,
                    {
                        left: 0,
                        top: 0,
                        scaleX: 1.45,
                        scaleY: 1.33,
                        selectable: false
                    });
                canvas.add(imgbc);
                canvas.sendToBack(imgbc);
            }
            imagenBoleta.src = disenador.imagenBoelto;
        }
        else {
            var boleta = new fabric.Rect({
                left: 0,
                top: 0,
                fill: 'white',
                width: disenador["Ancho"] * 37.795276,
                height: disenador["Alto"] * 37.795276,
                strokeWidth: 1,
                stroke: 'rgba(0,0,0)',
                selectable: false
            });
            canvas.add(boleta);
            canvas.sendToBack(boleta);
        }
    }

    //Agregar Elementos
    function AgregarElemento() {
        var obj = canvas.getActiveObject();
        if (obj == undefined || obj == null) {
            Dibujar(true);
        } else {
            switch (obj.ownType) {
                case "t":
                    obj.text = document.getElementById('TextoElemento').value;
                    obj.fontFamily = $("#FuenteTexto").data("kendoComboBox").value();
                    obj.fontSize = $("#TamanoTexto").data("kendoNumericTextBox").value();
                    break;
                case "b":
                    obj.ddlValue = $("#TipoBarcode").data("kendoComboBox").value();
                    obj.height = $("#AltoBarcode").data("kendoNumericTextBox").value();
                    break;
                case "q":
                    obj.ddlValue = $("#TipoQr").data("kendoComboBox").value();
                    obj.height = $("#AltoQrcode").data("kendoNumericTextBox").value();
                    break;
                case "im":
                    break;
                case "ed":
                    obj.name = $("#ElemnetoElementoXDefecto").data("kendoComboBox").value();
                    obj.fontFamily = $("#FuenteElementoXDefaulTexto").data("kendoComboBox").value();
                    obj.fontSize = $("#TamanoElementoXDefaulTexto").data("kendoNumericTextBox").value();
                    break;
                default:

            }
            Dibujar(false);
            LimpiarCampos(obj.ownType);
        }
    }

    //Dibujar en el canvas
    function Dibujar(nuevo) {
        if (!nuevo) { canvas.remove(canvas.getActiveObject()); }
        switch (tipo) {
            case "t":
                var text = new fabric.Text(document.getElementById('TextoElemento').value,
                    {
                        left: 10,
                        top: 10,
                        fontFamily: $("#FuenteTexto").data("kendoComboBox").value(),
                        fontSize: $("#TamanoTexto").data("kendoNumericTextBox").value(),
                    });

                text.toObject =
                (function (toObject) {
                    return function () {
                        return fabric.util.object.extend(toObject.call(this),
                        {
                            name: this.name,
                            ownType: this.ownType
                        });
                    };

                })(text.toObject);
                text.name = null;
                text.ownType = tipo;

                canvas.add(text);
                LimpiarCampos(tipo);
                break;

            case "b":
                idTipoCodigoBarras = $("#TipoBarcode").data("kendoComboBox").value();
                maxHeight = $("#AltoBarcode").data("kendoNumericTextBox").value();
                codigo = "1234567890123456";
                url = '/Administracion/Plantillas_GenerarImagenCodigoBarras';
                $.post(url,
                    { codigo: codigo, idTipoCodigoBarras: idTipoCodigoBarras, maxHeight: maxHeight },
                    function (data) {
                        if (!data.Error) {
                            imagen = new Image();
                            imagen.onload = function () {
                                var imgbc = new fabric.Image(imagen,
                                    {
                                        left: 100,
                                        top: 100,
                                    });
                                imgbc.lockScalingX = true;
                                imgbc.toObject =
                                (function (toObject) {
                                    return function () {
                                        return fabric.util.object.extend(toObject.call(this),
                                        {
                                            name: this.name,
                                            ownType: this.ownType,
                                            ddlValue: this.ddlValue,
                                            lockScalingX: true
                                        });
                                    };
                                })(imgbc.toObject);
                                imgbc.name = null;
                                imgbc.ownType = tipo;
                                imgbc.ddlValue = idTipoCodigoBarras;
                                canvas.add(imgbc);
                            };
                            imagen.src = 'data:image/png;base64,' + data.Imagen;
                            LimpiarCampos(tipo);
                        }
                    });
                break;

            case "q":
                idTipoQr = $("#TipoQr").data("kendoComboBox").value();
                maxHeight = $("#AltoQrcode").data("kendoNumericTextBox").value();
                codigo = "1234567890123456";
                url = '/Administracion/Plantillas_GenerarImagenCodigoQR';
                $.post(url,
                    { codigo: codigo, idTipoQr: idTipoQr, maxHeight: maxHeight },
                    function (data) {
                        if (!data.Error) {
                            imagen = new Image();
                            imagen.onload = function () {
                                var imgqr = new fabric.Image(imagen,
                                    {
                                        left: 100,
                                        top: 100,
                                    });
                                imgqr.lockScalingX = true;
                                imgqr.toObject =
                                (function (toObject) {
                                    return function () {
                                        return fabric.util.object.extend(toObject.call(this),
                                        {
                                            name: this.name,
                                            ownType: this.ownType,
                                            ddlValue: this.ddlValue
                                        });
                                    };
                                })(imgqr.toObject);
                                imgqr.name = null;
                                imgqr.ownType = tipo;
                                imgqr.ddlValue = idTipoQr;
                                canvas.add(imgqr);
                            };
                            imagen.src = 'data:image/png;base64,' + data.Imagen;
                            LimpiarCampos(tipo);
                        }
                    });
                break;
            case "im":
                if (archivo != null) {
                    imagen = new Image();
                    reader = new FileReader();
                    reader.onloadend = function () {
                        imagen.src = reader.result;
                    };
                    imagen.onload = function () {
                        var img = new fabric.Image(imagen,
                        {
                            left: 100,
                            top: 100,
                        });

                        var widtch3 = img["width"];
                        if (widtch3 > 300 && widtch3 < 1000) {
                            img.scale(0.7);
                            canvas.renderAll();
                        } else if (widtch3 >= 1000 && widtch3 < 2000) {
                            img.scale(0.5);
                            canvas.renderAll();
                        } else if (widtch3 >= 2000 && widtch3 < 3000) {
                            img.scale(0.3);
                            canvas.renderAll();
                        } else if (widtch3 >= 3000) {
                            img.scale(0.1);
                            canvas.renderAll();
                        }

                        img.toObject =
                                (function (toObject) {
                                    return function () {
                                        return fabric.util.object.extend(toObject.call(this),
                                        {
                                            name: this.name,
                                            ownType: this.ownType
                                        });
                                    };
                                })(img.toObject);
                        img.name = null;
                        img.ownType = tipo;

                        canvas.add(img);
                    };
                    reader.readAsDataURL(archivo);
                    imagen.src = reader.result;
                    LimpiarCampos(tipo);
                }
                break;
            case "ed":
                if (!ImagenDefecto) {
                    var edtext = new fabric.Text($("#ElemnetoElementoXDefecto").data("kendoComboBox").text(),
                    {
                        left: 10,
                        top: 10,
                        fontFamily: $("#FuenteElementoXDefaulTexto").data("kendoComboBox").value(),
                        fontSize: $("#TamanoElementoXDefaulTexto").data("kendoNumericTextBox").value(),
                    });

                    edtext.toObject =
                    (function (toObject) {
                        return function () {
                            return fabric.util.object.extend(toObject.call(this),
                            {
                                name: this.name,
                                ownType: this.ownType,
                                ddlValue: this.ddlValue,
                            });
                        };

                    })(edtext.toObject);
                    edtext.name = $("#ElemnetoElementoXDefecto").data("kendoComboBox").text();
                    edtext.ddlValue = $("#ElemnetoElementoXDefecto").data("kendoComboBox").value();
                    edtext.ownType = tipo;

                    canvas.add(edtext);
                    LimpiarCampos(tipo);
                } else {
                    imagenxDefecto = new Image();

                    if ($("#ElemnetoElementoXDefecto").data("kendoComboBox").value() == 17) {
                        imagenxDefecto.src = "/Images/Eventos/logo.jpg";
                    } else {
                        imagenxDefecto.src = "/Images/Eventos/ImgenEvento.jpg";
                    }

                    imagenxDefecto.onload = function () {
                        var imgxdefecto = new fabric.Image(imagenxDefecto,
                        {
                            left: 100,
                            top: 100,
                        });

                        imgxdefecto.toObject =
                                (function (toObject) {
                                    return function () {
                                        return fabric.util.object.extend(toObject.call(this),
                                        {
                                            name: this.name,
                                            ownType: this.ownType,
                                            ddlValue: this.ddlValue,
                                        });
                                    };
                                })(imgxdefecto.toObject);
                        imgxdefecto.name = $("#ElemnetoElementoXDefecto").data("kendoComboBox").text();
                        imgxdefecto.ownType = tipo;
                        imgxdefecto.ddlValue = $("#ElemnetoElementoXDefecto").data("kendoComboBox").value();
                        canvas.add(imgxdefecto);
                    }
                }

                break;
            default:
                // code block
        }
    }

    canvas.on({
        'mouse:up': Selected,
    });

    //Cargar las propiedades del elemento en el formulario
    function Selected(options) {
        if (canvas.getActiveObject() != null) {
            switch (canvas.getActiveObject().ownType) {
                case "t":
                    $('.nav-tabs a[href="#Textos"]').tab('show');
                    $("#FuenteTexto").data("kendoComboBox").value(canvas.getActiveObject().fontFamily);
                    $("#TamanoTexto").data("kendoNumericTextBox").value(canvas.getActiveObject().fontSize);
                    document.getElementById('TextoElemento').value = canvas.getActiveObject().text;
                    break;
                case "b":
                    $('.nav-tabs a[href="#Barcode"]').tab('show');
                    $("#TipoBarcode").data("kendoComboBox").value(canvas.getActiveObject().ddlValue);
                    $("#AltoBarcode").data("kendoNumericTextBox").value(canvas.getActiveObject().height);
                    break;
                case "q":
                    $('.nav-tabs a[href="#QrCode"]').tab('show');
                    $("#TipoQr").data("kendoComboBox").value(canvas.getActiveObject().ddlValue);
                    $("#AltoQrcode").data("kendoNumericTextBox").value(canvas.getActiveObject().height);
                    break;
                case "im":
                    $('.nav-tabs a[href="#Image"]').tab('show');
                    break;
                case "ed":
                    $('.nav-tabs a[href="#Default"]').tab('show');
                    $("#ElemnetoElementoXDefecto").data("kendoComboBox").value(canvas.getActiveObject().name);
                    $("#FuenteElementoXDefaulTexto").data("kendoComboBox").value(canvas.getActiveObject().fontFamily);
                    $("#TamanoElementoXDefaulTexto").data("kendoNumericTextBox").value(canvas.getActiveObject().fontSize);
                    break;
                default:
                    $('.nav-tabs a[href="#Textos"]').tab('show')
                    break;
            }
        }
        else {
            $('.nav-tabs a[href="#Textos"]').tab('show')
            LimniarTodosLosCampos();
        }

    }

    // Elimina el Objeto seleccionado
    function Eliminar() {
        canvas.remove(canvas.getActiveObject());
        LimpiarCampos(tipo)
    }

    // Función para limpiar el ComboBox de Fuente de los Textos en caso de que se ingrese un elemento no existente en la lista predefinida
    function CambioFuenteTexto() {
        var comboFuenteTexto = window.$("#FuenteTexto").data("kendoComboBox");
        if (comboFuenteTexto.selectedIndex === -1) {
            comboFuenteTexto.value(null);
        }
    }

    function LimpiarCampos(tipo) {
        switch (tipo) {
            case "t":
                $("#FuenteTexto").data("kendoComboBox").value('');
                $("#TamanoTexto").data("kendoNumericTextBox").value('');
                document.getElementById('TextoElemento').value = '';
                var validatorFormTexto = false;

                break;
            case "b":
                $("#TipoBarcode").data("kendoComboBox").value('');
                $("#AltoBarcode").data("kendoNumericTextBox").value('');
                break;
            case "q":
                $("#TipoQr").data("kendoComboBox").value('');
                $("#AltoQrcode").data("kendoNumericTextBox").value('');
                break;
            case "im":
                document.getElementById("CargarArchivo").value = '';
                break;
            case "ed":
                $("#ElemnetoElementoXDefecto").data("kendoComboBox").value('');
                $("#FuenteElementoXDefaulTexto").data("kendoComboBox").value('');
                $("#TamanoElementoXDefaulTexto").data("kendoNumericTextBox").value('');
                break;
            default:
                // code block
        }
    }

    function LimniarTodosLosCampos() {
        $("#FuenteTexto").data("kendoComboBox").value('');
        $("#TamanoTexto").data("kendoNumericTextBox").value('');
        document.getElementById('TextoElemento').value = '';
        $("#TipoBarcode").data("kendoComboBox").value('');
        $("#AltoBarcode").data("kendoNumericTextBox").value('');
        $("#TipoQr").data("kendoComboBox").value('');
        $("#AltoQrcode").data("kendoNumericTextBox").value('');
        document.getElementById("CargarArchivo").value = '';
        $("#ElemnetoElementoXDefecto").data("kendoComboBox").value('');
        $("#FuenteElementoXDefaulTexto").data("kendoComboBox").value('');
        $("#TamanoElementoXDefaulTexto").data("kendoNumericTextBox").value('');
        canvas.discardActiveObject();
    }

    function TraerAdelante() {
        canvas.bringForward(canvas.getActiveObject());
    }

    function EnviarAtras() {
        canvas.sendBackwards(canvas.getActiveObject());
    }

    // Función que se ejecuta cuando se carga un archivo en control de Upload
    function Cambio() {
        archivo = document.getElementById("CargarArchivo").files[0];
    };

    function Volver() {
        window.history.back();
    }

    //Detecta cuando se suelta la tecla Supr
    $(document).keyup(function (e) {
        if (e.keyCode == 46) {
            Eliminar();
        }
    });

    //Setea el valor minimo que permite el Qr en la caja de texto alto
    function SeleccionTipoQr() {
        var value = $("#TipoQr").data("kendoComboBox").value();
        var alto = $("#AltoQrcode").data("kendoNumericTextBox");
        if (value == 1) {
            alto.value(80);
            alto.min(80);
        } else {
            alto.value(150);
            alto.min(150);
        }
    }

    //Setea el valor por defecto en la creacion del barcode
    function SeleccionTipoBarcode() {
        var value = $("#TipoBarcode").data("kendoComboBox").value();
        var alto = $("#AltoBarcode").data("kendoNumericTextBox");
        if (value > 0) {
            alto.value(15);
        }
    }

    //Identifica cuales son imagenes 
    function SeleccionElementoxDefecto() {
        var value = $("#ElemnetoElementoXDefecto").data("kendoComboBox").value();
        if (value == 17) {
            DeshabilitarPropiedadElementosxDefecto();
            ImagenDefecto = true;

        } else if (value == 19) {
            DeshabilitarPropiedadElementosxDefecto();
            ImagenDefecto = true;
        }
        else {
            var test = document.getElementById("FuenteElementoXDefaulTexto");
            if (!test.getAttribute("required")) {
                $(".elementoTexto").show();
                test.required = true
                var test2 = document.getElementById("TamanoElementoXDefaulTexto");
                test2.required = true
            }
            ImagenDefecto = false;
        }
    }

    //Elimina los atibutos de requeridos de las propiedades
    function DeshabilitarPropiedadElementosxDefecto() {
        var test = document.getElementById("FuenteElementoXDefaulTexto");
        test.removeAttribute("required");
        test.removeAttribute("data-val-required");
        var test2 = document.getElementById("TamanoElementoXDefaulTexto");
        test2.removeAttribute("required");
        test2.removeAttribute("data-val-required");
        $(".elementoTexto").hide();
    }

    function GuardarCanvas() {
        var url = '/Administracion/Diseñador_CreateUpdate';
        var papel = canvas.getObjects()[0];
        canvas.remove(papel);
        var json = canvas.toJSON(['selectable', 'name', 'ownType', 'ddlValue', 'lockScalingX']);
        var canvasJson = JSON.stringify(json);
        var disenador = JSON.parse(localStorage.getItem("disenador"));
        var idPlantilla = disenador.IdPlantilla
        if (canvas != null) {
            $.post(url, { json: canvasJson, idPlantilla: idPlantilla }, function (data) {
                if (data) {
                    bootbox.alert({
                        size: "small",
                        title: "Diseñador",
                        message: "Diseño guradado",
                        callback: function () {
                            localStorage.clear();
                            Volver();
                        }
                    });
                } else {
                    bootbox.alert("No fue posible guardar el diseño" + data.Message);
                }
            })
        }
    }

    function Print() {
        var dataURL = canvas.toDataURL({
            format: 'jpeg',
            quality: 1
        });
        var html = "<html><head><script>function step1(){\n" +
            "setTimeout('step2()', 10);}\n" +
            "function step2(){window.print();window.close()}\n" +
            "</scri" + "pt></head><body onload='step1()'>\n" +
            "<img src='" + dataURL + "' /></body></html>"
        var printWindow = window.open("", "PDF");
        printWindow.document.open();
        printWindow.document.write(html);
        printWindow.document.close();

    }


}
