var span = document.getElementsByClassName("close")[0];
var modal = document.getElementById('windowstaff');
var imagen = document.getElementById('imagen');
var validatorBuscarCliente = $("#buscarClienteForm").kendoValidator().data("kendoValidator");
var validatorCliente = $("#clientesForm").kendoValidator().data("kendoValidator");
var DivPesrona = document.querySelector("#FotoPersonaCargada");
var DivNewPersona = document.querySelector("#TomarNewFoto");
var DivBtnPersona = document.querySelector("#btnPersonaCargada");
var DivNewBtnPersona = document.querySelector("#btnNewPersona");
var DivDireccionPersona = document.querySelector("#DireccionPersona");

if ($(document).ready) {
    var evento = JSON.parse(localStorage.getItem("evento"));
    document.querySelector("#NombreEvento").innerHTML = evento.NombreEvento;

    //Buscar CLientes Ya inscritos en la plataforma
    function BuscarCliente() {
        event.preventDefault();
        var identificacion = document.querySelector("#numeroIdentificacion").value;
        var tipoId = $("#TipoIdentificacionId").data("kendoDropDownList").value();
        if (validatorBuscarCliente.validate()) {
            if (identificacion != "") {
                var url = '/Staff/Staff_ReadByIdentificacion';
                Loading(true);
                $.get(url, { tipoId: tipoId, identificacion: identificacion },
                function (data) {
                    if (data.objeto != null) {
                        DivNewPersona.style.display = "none";
                        DivNewBtnPersona.style.display = "none";
                        DivPesrona.style.display = "block";
                        DivBtnPersona.style.display = "block";
                        DivDireccionPersona.style.display = "block";
                        window.$('#windowstaff').modal('show');
                        CargarPersona(data.objeto);
                        Loading(false);
                    } else {
                        alert("No se encontro ningun registro")
                        Loading(false);
                    }
                });
            }
        }
    }

    //Cagar la informacion del cliente 
    function CargarPersona(objeto) {
        imagen.src = 'data:image/png;base64,' + objeto.strImagen;
        $("#tipoIdentificacion").data("kendoDropDownList").value(objeto.TipoDocumento_id);
        $("#tipoIdentificacion").data("kendoDropDownList").enable(false)
        $("#identificacion").val(objeto.Identificacion);
        $("#identificacion").attr("disabled", true)
        $("#primerNombre").val(objeto.PrimerNombres);
        $("#primerNombre").attr("disabled", true)
        $("#segundoNombre").val(objeto.SegundoNombres);
        $("#segundoNombre").attr("disabled", true)
        $("#primerApellido").val(objeto.PrimerApellido);
        $("#primerApellido").attr("disabled", true)
        $("#segundoApellido").val(objeto.SegundoApellido);
        $("#segundoApellido").attr("disabled", true)
        $("#genero").data("kendoDropDownList").value(objeto.Genero_id);
        $("#genero").data("kendoDropDownList").enable(false)
        $("#fechaNacimiento").data("kendoDatePicker").value(objeto.FechaNacimiento);
        $("#fechaNacimiento").data("kendoDatePicker").enable(false)
        $("#email").val(objeto.Email);
        $("#email").attr("disabled", true)
        $("#celular").val(objeto.Celular);
        $("#celular").attr("disabled", true)
        $("#casa").val(objeto.TelefonoCasa);
        $("#casa").attr("disabled", true)
        $("#oficina").val(objeto.TelefonoOficina);
        $("#oficina").attr("disabled", true)
        $("#direccion").val(objeto.Direcciones[0].FullAdress);
        $("#direccion").attr("disabled", true)
        var storage = JSON.parse(localStorage.getItem("evento"));
        storage["IdCliente"] = objeto.Id;
        localStorage.setItem("evento", JSON.stringify(storage));
    }

    //Guardar un Cliente ya inscrito
    function AsociarStaff() {
        event.preventDefault();
        var clienteStaff = JSON.parse(localStorage.getItem("evento"));
        Loading(true);
        if (clienteStaff.IdEmpersa == 0 || typeof (clienteStaff.IdEmpersa) == "undefined") {
            alert("Debe seleccionar una empresa");
            CloseModal();
            Loading(false);
        } else {
            var url = '/Staff/Staff_Save';
            $.ajax({
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: JSON.stringify(clienteStaff),
                datatype: "json",
                url: url,
                success: function (data) {
                    if (data.execute == true) {
                        alert(data.messagge);
                        CloseModal();
                        var gridStaff = $("#gridStaff").data("kendoGrid").dataSource.read();
                    } else {
                        alert("No fue posible asosiar al usuario")
                    }
                }
            });
            Loading(false);
        }
    }

    //Cerrar la ventana
    function CloseModal() {
        $("#numeroIdentificacion").val("");
        $("#TipoIdentificacionId").data("kendoDropDownList").value("");
        $('#windowstaff').modal('toggle');
        Loading(false);
    }

    //Evento de cambio de empresa
    function ChangeEmpresa(e) {
        if (e.item) {
            var dataItem = this.dataItem(e.item.index());
            var storage = JSON.parse(localStorage.getItem("evento"));
            storage["IdEmpersa"] = dataItem.Value;
            localStorage.setItem("evento", JSON.stringify(storage));
            var gridStaff = $("#gridStaff").data("kendoGrid").dataSource.read();
        }
    }

    //Cargar el grid segun la empresa seleccionada
    function DatosGrid() {
        var storage = JSON.parse(localStorage.getItem("evento"));
        var evento = storage.IdEvento;
        var empresario = storage.IdEmpresario;
        var empresa = storage.IdEmpersa;
        if (typeof (empresa) == "undefined" || (empresa) == 0)
        { return { idEvento: evento, idEmpresario: empresario }; }
        else
        {
            {
                return { idEvento: evento, idEmpresario: empresario, idEmpresa: empresa };
            }
        }

    }

    //Abrir la ventana para crear una nueva
    //Se ocultan unos div y se muestran otros , campos y botones
    function OpenNewPsersona() {
        Loading(true);
        LimpiarCampos();
        DivNewPersona.style.display = "block";
        DivNewBtnPersona.style.display = "block";
        DivPesrona.style.display = "none";
        DivBtnPersona.style.display = "none";
        DivDireccionPersona.style.display = "none";
        Loading(false);
        window.$('#windowstaff').modal('show');
        var context = canvas.getContext('2d');
        context.clearRect(0, 0, 320, 240);
        return false;
    }

    //Limpiar los campos
    function LimpiarCampos() {
        $("#tipoIdentificacion").data("kendoDropDownList").value("");
        $("#identificacion").val("");
        $("#primerNombre").val("");
        $("#segundoNombre").val("");
        $("#primerApellido").val("");
        $("#segundoApellido").val("");
        $("#genero").data("kendoDropDownList").value("");
        $("#fechaNacimiento").data("kendoDatePicker").value("");
        $("#email").val("");
        $("#celular").val("");
        $("#casa").val("");
        $("#oficina").val("");
        $("#tipoIdentificacion").data("kendoDropDownList").enable(true)
        $("#identificacion").attr("disabled", false);
        $("#primerNombre").attr("disabled", false);
        $("#segundoNombre").attr("disabled", false);
        $("#primerApellido").attr("disabled", false);
        $("#segundoApellido").attr("disabled", false);
        $("#genero").data("kendoDropDownList").enable(true)
        $("#fechaNacimiento").data("kendoDatePicker").enable(true)
        $("#email").attr("disabled", false);
        $("#celular").attr("disabled", false);
        $("#casa").attr("disabled", false);
        $("#oficina").attr("disabled", false);
        var storage = JSON.parse(localStorage.getItem("evento"));
        storage["IdCliente"] = 0;
        localStorage.setItem("evento", JSON.stringify(storage));

    }

    //Js Camara - Depende de main.js /Scripts/pages/Camara
    function TomarFoto() {
        event.preventDefault();
        canvas.width = video.videoWidth * 0.50;
        canvas.height = video.videoHeight * 0.50;
        var context = canvas.getContext('2d');
        context.drawImage(video, 0, 0, canvas.width, canvas.height);
        fotonew = new Image();
        fotonew = canvas.toDataURL();
        document.getElementById("strImagen").value = fotonew;
        $("#strImagen").trigger("change")
        return false
    }

    //Guardar y asociar un cliente que no existe (Nuevo)
    function GuardarCLiente() {
        event.preventDefault();
        if (validatorCliente.validate())
        {
            Loading(true);
            var clienteStaff = JSON.parse(localStorage.getItem("evento"));
            if (clienteStaff.IdEmpersa == 0 || typeof (clienteStaff.IdEmpersa) == "undefined")
            {
                alert("Debe seleccionar una empresa");
                CloseModal();
                Loading(false);
            } else {
                var entidad =
                {
                    TipoDocumento_id: $("#tipoIdentificacion").data("kendoDropDownList").value(),
                    Identificacion: $("#identificacion").val(),
                    PrimerNombres: $("#primerNombre").val(),
                    SegundoNombres: $("#segundoNombre").val(),
                    PrimerApellido: $("#primerApellido").val(),
                    SegundoApellido: $("#segundoApellido").val(),
                    Genero_id: $("#genero").data("kendoDropDownList").value(),
                    FechaNacimiento: $("#fechaNacimiento").data("kendoDatePicker").value(),
                    Email: $("#email").val(),
                    Celular: $("#celular").val(),
                    TelefonoCasa: $("#casa").val(),
                    TelefonoOficina: $("#casa").val(),
                    strImagen: $("#strImagen").val()
                }
                var newClienteStyaff =
                    {
                        Cliente: entidad,
                        Staff: clienteStaff
                    }
                var url = '/Staff/StaffNewPersonSave';
                $.ajax({
                    type: 'POST',
                    data: JSON.stringify(newClienteStyaff),
                    contentType: "application/json; charset=utf-8",
                    datatype: "json",
                    url: url,
                    success: function (data) {
                        if (data.execute == true) {
                            alert(data.messagge);
                            CloseModal();
                            Loading(false);
                            var gridStaff = $("#gridStaff").data("kendoGrid").dataSource.read();
                        } else {
                            alert("No fue posible asosiar al usuario" + data.messagge)
                            Loading(false);
                            CloseModal();
                            var gridStaff = $("#gridStaff").data("kendoGrid").dataSource.read();
                        }
                    }
                });
            }

        }
    }

}
