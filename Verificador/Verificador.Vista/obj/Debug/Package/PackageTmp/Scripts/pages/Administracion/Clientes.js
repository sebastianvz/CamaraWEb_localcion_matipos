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

    //JS para operacion de pantallas

    function showDetalleDireccion(e) {
        var divEdit = $("div[class='k-edit-form-container']");
        var gridDireccion = $("#gridDirecciones").data("kendoGrid");
        e.container.data("kendoWindow").bind("close", function () {
            gridDireccion.dataSource.read();
        })
    }

    function showDetalleCliente(e) {
        PersonaID = e.model.Id;
        if (e.model.Id != 0)
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
    function AgregarDigito() {
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
    function RefreshGirdClientes(e)
    {
        if (e.type === "create" || e.type === "update" || e.type === "destroy" || e.type === "edit") {
            var gridCLientes = $("#gridClientes").data("kendoGrid");
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

}