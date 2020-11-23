if ($(document).ready) {

    var elements = document.getElementsByClassName("opciones");
    var form = document.getElementById("Encuentas");
    var divRow;
    var divColum;
    var divFrom;
    var label;
    var br;
    var ddlTipo;
    var pregunta;
    var textOpcion;
    var valueOpcion = {};
    var nPreguntas = 0;
    var nOpciones = 0;
    var btnAdd;
    var btnDele;
    var preguntaText;
    var labelOpcion;
    var btnQuestionDele;
    var buscarDiv;
    var loadPregunta = [];
    var loadOpciones = [];
    var validatorEncuentas = window.$("#Encuentas").kendoValidator().data("kendoValidator");

    function Nuevo()
    {
        nPreguntas = 0;
        if (elements.length > 0) {
            for (var i = 0; i < elements.length;) {
                elements[0].remove();
            }
            nOpciones = 0;
        }
        $('#windowsEncuesta').modal('show');
    }

    function Editar(e) {
        Nuevo();
        loadPregunta = [];
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        if (dataItem.Id) {
            var url = "/Event/AddSessionIdEvento";
            $.post(url, { idEvento: dataItem.Id },
                function (data) {
                    if (data.preguntas.length == 0) {
                        Nuevo();
                    } else {
                        nPreguntas = data.preguntas.length;
                        for (var i = 0; i < data.preguntas.length; i++) {
                            loadPregunta.push(new Pregunta(data.preguntas[i].Id, data.preguntas[i].TextoPregunta, data.preguntas[i].NumeroPregunta, data.preguntas[i].TipoRespuesta));
                            for (var j = 0; j < data.preguntas[i].Opciones.length; j++) {
                                loadOpciones.push(new Opciones(data.preguntas[i].Opciones[j].Id, data.preguntas[i].Opciones[j].IdPregunta, data.preguntas[i].Opciones[j].TextoOpcion, data.preguntas[i].Opciones[j].NumeroOpcion));
                            }
                            loadPregunta[i].opciones = loadOpciones;
                            loadOpciones = [];
                        }
                        console.log(loadPregunta);
                        CargarPregunta();
                    }
                });

        }
    }

    //Crear el div contener que tiene linea punteada
    function DivContenedor()
    {
        divRow = document.createElement("div");
        divColum = document.createElement("div");
        divRow.className = "row opciones";
        divColum.className = "col-md-12";
        divRow.style.cssText = "border: dashed 1px black; margin-bottom: 3px;";
        divRow.appendChild(divColum);
    }

    //Crea los div para agergar los elementos
    function CreateDiv() {
        divFrom = document.createElement("div");
        label = document.createElement("label");
        br = document.createElement("br");
        divFrom.className = "form-group";
        label.className = "control-label";

    }

    //Agrega los elementos creados a los DIVs
    function AddChilds() {
        divColum.appendChild(divFrom);
        divFrom.appendChild(label);
        divFrom.appendChild(br);
        form.appendChild(divRow);
    }

    function CargarPregunta() {
        for (var i = 0; i < loadPregunta.length; i++) {
            DivContenedor();
            CreateDiv();
            label.innerHTML = "Pregunta " + (loadPregunta[i].numeroPregunta + 1);
            pregunta = document.createElement("input");
            pregunta.value = loadPregunta[i].textoPregunta;
            preguntaText = "pregunta" + loadPregunta[i].idPregunta;
            pregunta.setAttribute("id", preguntaText);
            pregunta.setAttribute("required", true);
            pregunta.setAttribute("validationmessage", "campo requerido")
            pregunta.className = "k-textbox";
            pregunta.style.cssText = "width:66%";
            btnQuestionDele = document.createElement("button");
            btnQuestionDele.setAttribute("id", "buttonQuestionDele" + preguntaText);
            btnQuestionDele.className = "btn btn-primary";
            btnQuestionDele.style.cssText = "margin-left:15px; background-color:red; color: white;  border-color:red";
            btnQuestionDele.type = "button"
            btnQuestionDele.onclick = function (e)
            {
                buscarDiv =  document.getElementById(e.srcElement.id);
                var form = $(buscarDiv).parent();
                var colum = $(form).parent();
                var div = $(colum).parent().remove();
            }
            var fafaQuestionDele = document.createElement("i");
            fafaQuestionDele.className = "fa fa-trash";
            btnQuestionDele.appendChild(fafaQuestionDele);
            AddChilds();
            divFrom.appendChild(pregunta);
            divFrom.appendChild(btnQuestionDele);
            CreateDiv();
            label.innerHTML = "Tipo de respuesta";
            ddlTipo = document.createElement("input");
            ddlTipo.setAttribute("id", "dllTipo" + loadPregunta[i].numeroPregunta);
            ddlTipo.style.cssText = "width: 66%";
            AddChilds();
            divFrom.appendChild(ddlTipo);
            $("#" + ddlTipo.id).kendoComboBox(
             {
                 placeholder: "Seleccione tipo",
                 dataTextField: "text",
                 dataValueField: "value",
                 index: 1,
                 change: SeleccionTipoRespuesta,
                 dataSource: [
                     { text: "Multiples Respuesta", value: "1" },
                     { text: "Texto", value: "2" },
                     { text: "Unica Respuesta", value: "3" },
                 ],
             });
            $("#" + ddlTipo.id).data("kendoComboBox").value(loadPregunta[i].tipoRespuesta);
            $("#" + ddlTipo.id).data("kendoComboBox").enable(false);
            ///Crear las opciones
            for (var j = 0; j < loadPregunta[i].opciones.length; j++)
            {
                CreateDiv();
                labelOpcion = document.createElement("label");
                labelOpcion.className = "control-label"
                labelOpcion.innerHTML = "Opción " + (loadPregunta[i].opciones[j].numeroOpcion +1);
                textOpcion = document.createElement("input");
                textOpcion.value = loadPregunta[i].opciones[j].textoOpcion;
                textOpcion.setAttribute("id", "texto" + loadPregunta[i].opciones[j].numeroOpcion + preguntaText);
                textOpcion.className = "k-textbox";
                textOpcion.style.cssText = "width:56%";
                textOpcion.setAttribute("required", true);
                textOpcion.setAttribute("validationmessage", "campo requerido")
                divColum.appendChild(divFrom);
                divFrom.appendChild(labelOpcion);
                divFrom.appendChild(br);
                divFrom.appendChild(textOpcion);
            }

        }

    }

    //Agrega pregunta
    function AddPreguntas() {
        if (validatorEncuentas.validate()) {
            if (document.getElementById("_validationMessage") != null) {
                document.getElementById("_validationMessage").remove()
            }
            if (!nPreguntas == 0) {
                btnQuestionDele.remove();
                if (nOpciones != 0) {
                    btnAdd.remove();
                    btnDele.remove();
                }
            }
            nOpciones = 0;
            DivContenedor();
            CreateDiv();
            label.innerHTML = "Pregunta " + (nPreguntas + 1);
            pregunta = document.createElement("input");
            preguntaText = "pregunta" + nPreguntas
            pregunta.setAttribute("id", preguntaText);
            pregunta.setAttribute("required", true);
            pregunta.setAttribute("validationmessage", "campo requerido")
            pregunta.className = "k-textbox";
            pregunta.style.cssText = "width:66%";
            btnQuestionDele = document.createElement("button");
            btnQuestionDele.setAttribute("id", "buttonQuestionDele" + preguntaText);
            btnQuestionDele.className = "btn btn-primary";
            btnQuestionDele.style.cssText = "margin-left:15px; background-color:red; color: white;  border-color:red";
            btnQuestionDele.type = "button"
            btnQuestionDele.onclick = function () {
                divRow.remove();
                nPreguntas--;
            }
            var fafaQuestionDele = document.createElement("i");
            fafaQuestionDele.className = "fa fa-trash";
            btnQuestionDele.appendChild(fafaQuestionDele);
            AddChilds();
            divFrom.appendChild(pregunta);
            divFrom.appendChild(btnQuestionDele);
            CreateDiv();
            label.innerHTML = "Tipo de respuesta";
            ddlTipo = document.createElement("input");
            ddlTipo.setAttribute("id", "dllTipo" + nPreguntas);
            ddlTipo.style.cssText = "width: 66%";
            AddChilds();
            divFrom.appendChild(ddlTipo);
            $("#" + ddlTipo.id).kendoComboBox(
             {
                 placeholder: "Seleccione tipo",
                 dataTextField: "text",
                 dataValueField: "value",
                 index: 1,
                 change: SeleccionTipoRespuesta,
                 dataSource: [
                     { text: "Multiples Respuesta", value: "1" },
                     { text: "Texto", value: "2" },
                     { text: "Unica Respuesta", value: "3" },
                 ],
             });
            nPreguntas++;
            document.getElementById(preguntaText).focus()
        }
    }

    function SeleccionTipoRespuesta() {
        var value = $("#" + ddlTipo.id).data("kendoComboBox").value();
        if (value == 1 || value == 3) {
            AddOptions();
            document.getElementById("texto" + nOpciones + preguntaText).focus();
            $("#" + ddlTipo.id).data("kendoComboBox").enable(false);
        }
    }

    //Agrega las opciones
    function AddOptions() {
        CreateDiv();
        labelOpcion = document.createElement("label");
        labelOpcion.className = "control-label"
        labelOpcion.innerHTML = "Opción " + (nOpciones + 1);
        textOpcion = document.createElement("input");
        textOpcion.setAttribute("id", "texto" + nOpciones + preguntaText);
        textOpcion.className = "k-textbox";
        textOpcion.style.cssText = "width:56%";
        textOpcion.setAttribute("required", true);
        textOpcion.setAttribute("validationmessage", "campo requerido")
        AgregarBotones();
        divColum.appendChild(divFrom);
        divFrom.appendChild(labelOpcion);
        divFrom.appendChild(br);
        divFrom.appendChild(textOpcion);
        divFrom.appendChild(btnAdd);
        divFrom.appendChild(btnDele);

    }

    //Agrega lo botons add y delete la primeera vez
    function AgregarBotones() {
        btnAdd = document.createElement("button");
        btnAdd.setAttribute("id", "buttonAdd" + nOpciones + preguntaText);
        btnAdd.className = "btn btn-primary";
        btnAdd.style.cssText = "margin-left:15px; background-color:darkgreen; color: white;  border-color:darkgreen";
        btnAdd.type = "button"
        btnAdd.onclick = function () {
            if (validatorEncuentas.validate()) {
                if (document.getElementById("_validationMessage") != null) {
                    document.getElementById("_validationMessage").remove();
                }
                this.remove();
                btnDele.remove();
                nOpciones++;
                AddOptions();
                document.getElementById("texto" + nOpciones + preguntaText).focus();
            }
        }
        btnDele = document.createElement("button");
        btnDele.setAttribute("id", "buttonDele" + nOpciones + preguntaText);
        btnDele.className = "btn btn-primary";
        btnDele.style.cssText = "margin-left:15px; background-color:red; color: white;  border-color:red";
        btnDele.type = "button"
        i = 0;
        btnDele.onclick = function () {
            if (nOpciones != 0) {
                if (i == 0) {
                    divFrom.remove();
                    nOpciones--;
                    buscarDiv = "#texto" + (nOpciones) + preguntaText;
                    AgregarBotones();
                    NuevosBotones(buscarDiv);
                } else {
                    $(buscarDiv).parent().remove();
                    nOpciones--;
                    var buscarDivNuevo = "#texto" + (nOpciones) + preguntaText;
                    buscarDiv = buscarDivNuevo
                    AgregarBotones();
                    NuevosBotones(buscarDivNuevo);
                }
                i++;
            }
        }
        var fafaAdd = document.createElement("i");
        fafaAdd.className = "fa fa-plus-circle";
        var fafaDele = document.createElement("i");
        fafaDele.className = "fa fa-trash";
        btnAdd.appendChild(fafaAdd);
        btnDele.appendChild(fafaDele);
    }

    //Agrega los botones add y delete 
    function NuevosBotones(divName) {
        $(divName).parent().append(btnAdd);
        $(divName).parent().append(btnDele)
    }

    function Guardar(e) {
        if (validatorEncuentas) {
            var tempPregunta = [];
            var tempOpciones = [];
            for (var i = 0 ; i < nPreguntas ; i++) {
                tempPregunta.push(new Pregunta(0,document.getElementById("pregunta" + [i]).value, i, document.getElementById("dllTipo" + [i]).value));
                console.log(tempPregunta);
                j = 0;
                while (document.getElementById("texto" + [j] + "pregunta" + [i])) {
                    console.log((document.getElementById("texto" + [j] + "pregunta" + [i])));
                    tempOpciones.push(new Opciones(0,0,document.getElementById("texto" + [j] + "pregunta" + [i]).value, j));
                    j++;
                }
                tempPregunta[i].opciones = tempOpciones
                tempOpciones = [];
            }

            var url = '/Event/Preguntas_Create';
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(tempPregunta),
                datatype: "json",
                url: url,
                success: function (data)
                {
                    if (data.message == "OK") {
                        CloseModal();
                        bootbox.alert({
                            message: "Preguntas guardadas con exito"
                        });
                    } else
                    {
                        CloseModal();
                        bootbox.alert({
                            message: "Se presento un erro al momento de guardar las pregunta"
                        });
                    }
                    console.log(data);
                }
            });
        }
    }

    function CloseModal() {
        $('#windowsEncuesta').modal('toggle');
    }

    //Clases 
    class Pregunta {
        constructor(idPregunta,textoPregunta, numeroPregunta, tipoRespuesta, opciones) {
            this.textoPregunta = textoPregunta;
            this.numeroPregunta = numeroPregunta;
            this.tipoRespuesta = tipoRespuesta;
            this.opciones = opciones;
            this.idPregunta = idPregunta
        }
    }

    class Opciones {
        constructor(idOpcion, idPregunta,textoOpion, numeroOpcion) {
            this.idOpcion = idOpcion;
            this.idPregunta = idPregunta;
            this.textoOpcion = textoOpion;
            this.numeroOpcion = numeroOpcion;
        }
    }

}