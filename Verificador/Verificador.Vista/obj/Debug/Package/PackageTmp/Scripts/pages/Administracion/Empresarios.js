if ($(document).ready) {

    //Variable
    var tipoIdenticacion = 0;
    var map;
    var map2;
    var markers = [];
    var Pais = 0, Departamento = 0;
    var gridDireccion = null;
    var PersonaID = 0;
    fotoold = new Image;

    //Js Camara - Depende de main.js /Scripts/pages/Camara
    function TomarFoto() {
        canvas.width = video.videoWidth * 0.50;
        canvas.height = video.videoHeight * 0.50;
        canvas.getContext('2d').drawImage(video, 0, 0, canvas.width, canvas.height);
        fotonew = new Image();
        fotonew = canvas.toDataURL();
        document.getElementById("strImagen").value = fotonew;
        $("#strImagen").trigger("change")
    }

    //Js para Operacion de mapas

    function initMap2() {
        map2 = map
        map2.addListener('click', function (e) {
            placeMarkerAndPanTo(e.latLng, map2);
        });
    }

    function placeMarkerAndPanTo(latLng, map2) {
        console.log(latLng.lat());
        console.log(latLng.lng());
        var marker = new google.maps.Marker({
            position: latLng,
            map: map2
        });
        markers.push(marker);
        for (var i = 0; i < markers.length; i++) {
            j = i + 1
            if (markers.length == j) {
                markers[i].setMap(map2);
                console.log(markers[i]);
                document.getElementById("Latitud").value = markers[i].position.lat();
                $("#Latitud").trigger("change")
                document.getElementById("Longitud").value = markers[i].position.lng();
                $("#Longitud").trigger("change")
                map2.panTo(markers[i].position);
            } else {
                markers[i].setMap(null);
            }
        }
    }

    function initMap() {
        var myLatlng
        var uno = document.getElementById("Latitud").value;
        var dos = document.getElementById("Longitud").value;
        if (uno != "" && dos != "") {
            myLatlng = new google.maps.LatLng(uno, dos);
        } else {
            myLatlng = new google.maps.LatLng(6.2530408, -75.56457369999998);
        }

        map = new google.maps.Map(document.getElementById('map'), {
            center: myLatlng,
            zoom: 13,
            streetViewControl: false,
            panControl: false,
            mapTypeControl: true,
            zoomControl: true,
        });

        infoWindow = new google.maps.InfoWindow;

        if (uno == "") {
            if (navigator.geolocation) {
                navigator.geolocation.getCurrentPosition(function (position) {
                    var pos = {
                        lat: position.coords.latitude,
                        lng: position.coords.longitude
                    };

                    infoWindow.setPosition(pos);
                    infoWindow.setContent('Ubicacion Encontrada.');
                    infoWindow.open(map);
                    map.setCenter(pos);
                }, function () {
                    handleLocationError(true, infoWindow, map.getCenter());
                });
            } else {
                // Browser doesn't support Geolocation
                handleLocationError(false, infoWindow, map.getCenter());
            }
        }
        else {
            var marker = new google.maps.Marker({
                position: myLatlng,
                map: map,
                draggable: true,
                title: "Drag me!"
            });
        }

        initMap2();
    }

    function handleLocationError(browserHasGeolocation, infoWindow, pos) {
        infoWindow.setPosition(pos);
        infoWindow.setContent(browserHasGeolocation ?
                              'Error: El servio de Geolocacion Fallo.' :
                              'Error: El navegador NO soporta Geolocaciona.');
        infoWindow.open(map);
    }

    //Operaciones pantall

    function getDepartamentos() {
        var idPais = 0;
        if (Pais != 0 && $("#Pais_id").val() == "") {
            idPais = Pais;
        } else {
            idPais = $("#Pais_id").val()
        }
        Pais_id
        return {
            idPais: idPais,
            idDepartamento: $("#Departamneto_id").data("kendoComboBox").input.val()
        }
    }

    function getMunicipios() {
        var idDepartamento = 0;
        if (idDepartamento != 0 && $("#Departamneto_id").val() == "") {
            idDepartamento = Departamento;
        } else {
            idDepartamento = $("#Departamneto_id").val()
        }
        return {
            idDepartamento: idDepartamento,
            idMunicipio: $("#Ciudad_id").data("kendoComboBox").input.val()
        };
    }


    function showDetalleDireccion(e)
    {
        var divEdit = $("div[class='k-edit-form-container']");
        var gridDireccion = $("#gridDirecciones").data("kendoGrid");
        e.container.data("kendoWindow").bind("close", function () {
            gridDireccion.dataSource.read();
        })
    }

    function showDetalleEmpresario(e)
    {
        PersonaID = e.model.ClienteId;
        if (e.model.EmpresarioId != 0)
        {
            var dropdownlist = $("#TipoDocumento_id").data("kendoDropDownList");
            dropdownlist.enable(false);

            if (e.model.EsEmpresa == false)
            {
                EliminarClassEmnpresa();
                var url = '/Administracion/ReadImgbyId';
                $.get(url, { idCliente: PersonaID },
                function (data) {
                    var ctx = canvas.getContext("2d");
                    fotoold.src = 'data:image/png;base64,' + data.imagen;
                    fotoold.onload = function () {
                        ctx.drawImage(fotoold, 0, 0, canvas.width, canvas.height);
                    }
                });
                RefreshGridDireccion()
            }
            else
            {
                $("#RazonSocial").val(e.model.PrimerNombres);
                EliminarClassCliente();
                RefreshGridDireccion();
            }
        }
        else
        {    
            var url = '/Administracion/LimpiarDireccion';
            window.$.post(url,
                function (data) {
                    console.log(data.Ejecuto, "Clean Address");
                }
            );
            $(".empresa").hide();
            $(".cliente").show();
            RefreshGridDireccion();
        }
    }


    function getID(e) {
        return {
            cliente_id: PersonaID
        };
    }

    function RefreshGridDireccion()
    {
        var gridDirecciones = $("#gridDirecciones").data("kendoGrid");
        gridDirecciones.dataSource.read();
    }

    //Evento remueve los divs don calses cliente o empresa para quitar las validacione y que pase al controlador 
    function ChangeEmpresa(e)
    {
        tipoIdenticacion = e.sender.selectedIndex;
        var dropdownlist = $("#TipoDocumento_id").data("kendoDropDownList");
        dropdownlist.enable(false);
        if (tipoIdenticacion == 2) {
            $(".empresa").show();
            EliminarClassCliente();

        } else
        {
            EliminarClassEmnpresa();
            $("#Digito").val("");
            $("#Digito").trigger("change")
        }
    }

    //Evento para calcular el digito de verificación del NIT
    function AgregarDigito()
    {
        if (tipoIdenticacion == 2) {
            var digito = document.querySelector("#Digito").value;
            if (digito != "") {
                digito = "";
            }
            var vpri,
                x,
                y,
                z;
            var myNit = document.querySelector("#Identificacion").value;
            // Se limpia el Nit
            myNit = myNit.replace(/\s/g, ""); // Espacios
            myNit = myNit.replace(/,/g, ""); // Comas
            myNit = myNit.replace(/\./g, ""); // Puntos
            myNit = myNit.replace(/-/g, ""); // Guiones

            // Se valida el nit
            if (isNaN(myNit)) {
                console.log("El nit/cédula '" + myNit + "' no es válido(a).");
                return "";
            };
            // Procedimiento
            vpri = new Array(16);
            z = myNit.length;
            vpri[1] = 3;
            vpri[2] = 7;
            vpri[3] = 13;
            vpri[4] = 17;
            vpri[5] = 19;
            vpri[6] = 23;
            vpri[7] = 29;
            vpri[8] = 37;
            vpri[9] = 41;
            vpri[10] = 43;
            vpri[11] = 47;
            vpri[12] = 53;
            vpri[13] = 59;
            vpri[14] = 67;
            vpri[15] = 71;
            x = 0;
            y = 0;
            for (var i = 0; i < z; i++) {
                y = (myNit.substr(i, 1));
                // console.log ( y + "x" + vpri[z-i] + ":" ) ;

                x += (y * vpri[z - i]);
                // console.log ( x ) ;
            }
            y = x % 11;
            if (y > 1) {
                digito = 11 - y;
            } else {
                digito = y;
            }
            $("#Digito").val(digito);
            $("#Digito").trigger("change")
        }
    }

    //Refrescar Grid cuando se guardar o elimina
    function RefreshGirdEmpresarios(e)
    {
        if (e.type === "create" || e.type === "update" || e.type === "destroy" || e.type === "edit") {
            var gridCLientes = $("#gridEmpresarios").data("kendoGrid");
            gridCLientes.dataSource.read();
        }
    }

    function EliminarClassEmnpresa()
    {
        var elementosEmpresa = $(".empresa").length;
        for (var i = 0; i < elementosEmpresa ; i++) {
            $(".empresa")[0].remove();
        }
    }
    
    function EliminarClassCliente()
    {
        var elementosCliente = $(".cliente").length;
        for (var i = 0; i < elementosCliente ; i++) {
            $(".cliente")[0].remove();
        }
    }

    //Busqueda de un cliente en la plataforma
    function VerificarIdentificacion()
    {
        var Ident = document.getElementById("Identificacion").value;
        var TipoIdent = document.getElementById("TipoDocumento_id").value;
        var url = '/Administracion/GetInfoPersonaUserbyIden';
        $.post(url, { tipoID: TipoIdent, iden: Ident },
        function (data) {
            if (data.objeto != null)
            {
                if (data.objeto.EsEmpresa == false)
                {
                    EliminarClassEmnpresa();
                    $("#PrimerNombres").val(data.objeto.PrimerNombres);
                    $("#PrimerNombres").trigger("change");
                    $("#PrimerNombres")[0].disabled = true;
                    $("#PrimerApellido").val(data.objeto.PrimerApellido);
                    $("#PrimerApellido").trigger("change");
                    $("#PrimerApellido")[0].disabled = true;
                    $("#SegundoApellido").val(data.objeto.SegundoApellido);
                    $("#SegundoApellido").trigger("change");
                    $("#SegundoApellido")[0].disabled = true;
                    $("#SegundoNombres").val(data.objeto.SegundoNombres);
                    $("#SegundoNombres").trigger("change");
                    $("#SegundoNombres")[0].disabled = true
                    $("#Genero_id").data("kendoDropDownList").enable(false)
                    $("#Genero_id").data("kendoDropDownList").value(data.objeto.Genero_id);
                    $("#FechaNacimiento").data("kendoDatePicker").enable(false)
                    $("#FechaNacimiento").data("kendoDatePicker").value(data.objeto.FechaNacimiento);
                    $("#FechaNacimiento").trigger("change")
                    var url = '/Administracion/ReadImgbyId';
                    $.get(url, { idCliente: data.objeto.ClienteId },
                    function (data)
                    {
                        debugger;
                        var ctx = canvas.getContext("2d");
                        fotoold.src = 'data:image/png;base64,' + data.imagen;
                        fotoold.onload = function () {
                            ctx.drawImage(fotoold, 0, 0, canvas.width, canvas.height);
                        }
                    });
                }
                else
                {
                    EliminarClassCliente();
                    debugger;
                    $("#RazonSocial").val(data.objeto.RazonSocial);
                    $("#RazonSocial").trigger("change")
                    $("#RazonSocial")[0].disabled = true;
                    $("#Digito").val(data.objeto.Digito);
                    $("#Digito").trigger("change")
                    $("#Digito")[0].disabled = true;
                }
                $("#TipoPersona_id").data("kendoDropDownList").enable(false)
                $("#TipoPersona_id").data("kendoDropDownList").value(data.objeto.TipoPersona_id);
                $("#TipoPersona_id").trigger("change")
                $("#TipoDocumento_id").data("kendoDropDownList").enable(false)
                $("#TipoDocumento_id").data("kendoDropDownList").value(data.objeto.TipoDocumento_id);
                $("#Email").val(data.objeto.Email);
                $("#Email").trigger("change");
                $("#Celular").val(data.objeto.Celular);
                $("#Celular").trigger("change");
                $("#TelefonoCasa").val(data.objeto.TelefonoCasa);
                $("#TelefonoCasa").trigger("change");
                $("#TelefonoOficina").val(data.objeto.TelefonoOficina);
                $("#TelefonoOficina").trigger("change");
                $("#ClienteId").val(parseInt(data.objeto.ClienteId));
                $("#ClienteId").trigger("change")
                $("#Identificacion")[0].disabled = true;
                $("#Email")[0].disabled = true;
                $("#Celular")[0].disabled = true;
                $("#TelefonoCasa")[0].disabled = true;
                $("#TelefonoOficina")[0].disabled = true;
                PersonaID = data.objeto.ClienteId;
                var grid = $("#gridDirecciones").data("kendoGrid");
                grid.dataSource.read();
            } else {
                AgregarDigito();
                PersonaID = 0;
                var grid = $("#gridDirecciones").data("kendoGrid");
                grid.dataSource.read();
            }
        });
    }

    function VerificarUserNameEmailSession()
    {
        var userName = document.getElementById("UserEmpresario").value;
        var emailSession = document.getElementById("UserEmailEmpresario").value;
        var url = '/Administracion/GetInfoByUserEmail';
        $.post(url, { user: userName, email: emailSession },
        function (data) {
            if (data.Estado == true) {
                alert(data.Message);
                if (data.flag == "Usuario") {
                    $("#UserEmpresario").val("");
                } else {
                    $("#UserEmailEmpresario").val("");
                }
            }
        });
    }

}