
//JS Camara
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
var map;
var map2;
function initMap2() {
    map2 = map
    map2.addListener('click', function (e) {
        placeMarkerAndPanTo(e.latLng, map2);
    });
}
var markers = [];
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
///Pantalla Propia
(function ($, kendo) {
    $.extend(true, kendo.ui.validator, {
        rules: { // custom rules
            passwordvalidation: function (input, params) {
                if (input.is("[name='Password']")) {
                    input.attr("data-passwordvalidation-msg", "El campo contraseña es requerido");
                    return (input.val() != null && input.val() != "");
                }
                return true;
            }
        },
        messages: { //custom rules messages
            passwordvalidation: function (input) {
                // return the message text
                return input.attr("data-val-passwordvalidation");
            }
        }
    });
})(jQuery, kendo);

var idUsuarioSeleccionado = null;
function showRoles(e) {
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    idUsuarioSeleccionado = dataItem.Id;
    $("#windowRoles").data("kendoWindow").center().open();
    refrescarGrid();
}

function refrescarGrid() {
    var grid = $("#gridRoles").data("kendoGrid");
    grid.dataSource.page(1);
    grid.dataSource.read();
}

function addUsuarioParameter() {
    return {
        idUsuario: idUsuarioSeleccionado
    }
}

function GuardarRoles() {
    var windowPermisos = $("#windowRoles");
    var checks = windowPermisos.find("input[name='chkSel']")
    var ids = "";
    for (var i = 0; i < checks.length; i++) {
        if (checks[i].checked) {
            if (ids != "") {
                ids += ",";
            }
            ids += $(checks[i]).val();
        }
    }
    var url = '/Seguridad/GuardarRolesUsuario';
    $.get(url, { idUsuario: idUsuarioSeleccionado, roles: ids },
    function (data) {
        if (data != null && data != "") {
            alert(data);
        } else {
            $("#windowRoles").data("kendoWindow").close();
        }
    });

}

var PersonaID = 0;
fotoold = new Image;
function showDetalle(e) {
    var grid = $("#gridUsuarios").data("kendoGrid");
    gridActual = grid;
    PersonaID = e.model.IdClietne;
    if (e.model.Id != 0) {
        var ctx = canvas.getContext("2d");
        fotoold.src = 'data:image/png;base64,' + e.model.strImagen;
        fotoold.onload = function () {
            ctx.drawImage(fotoold, 0, 0, canvas.width, canvas.height);
        }
    } else {
        var url = '/Seguridad/LimpiarDireccion';
        window.$.post(url,
            function (data) {
                console.log(data.Ejecuto);
                var grid = $("#gridDirecciones").data("kendoGrid");
                grid.dataSource.read();
            }
        );
    }
    $("#Password").attr("type", "password");
    var divEdit = $("div[class='k-edit-form-container']");
    if (e.model.Id > 0) {
        $("#Password").val("***");
        $("#ConfriPass").val("***");
        $("#Password")[0].disabled = true;
        $("#Usuario")[0].disabled = true;
        $("#Email")[0].disabled = true;
        $("#ConfriPass")[0].disabled = true;
        PersonaID = e.model.IdClietne;
        var grid = $("#gridDirecciones").data("kendoGrid");
        grid.dataSource.read();
    } else {
        $("#Password")[0].disabled = false;
    }
    e.container.data("kendoWindow").bind("close", function () {
        gridActual.dataSource.read();
    })
}

function showDetalleDireccion(e) {
    var divEdit = $("div[class='k-edit-form-container']");
    var grid = $("#gridDirecciones").data("kendoGrid");
    gridDireccion = grid;
    e.container.data("kendoWindow").bind("close", function () {
        gridDireccion.dataSource.read();
    })
}

//Paises Ciudades
var Pais = 0, Departamento = 0;
var gridDireccion = null;
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

//popout

function VerificarIdentificacion() {
    debugger;
    var Ident = document.getElementById("Identificacion").value;
    var TipoIdent = document.getElementById("TipoDocumento_id").value;
    var url = '/Seguridad/GetInfoUserbyIden';
    $.post(url, { tipoID: TipoIdent, iden: Ident },
    function (data) {
        debugger;
        if (data.Data[0] != null) {
            var ctx = canvas.getContext("2d");
            fotoold.src = 'data:image/png;base64,' + data.Data[0].strImagen;
            fotoold.onload = function () {
                ctx.drawImage(fotoold, 0, 0, canvas.width, canvas.height);
            }
            $("#TipoPersona_id").data("kendoDropDownList").value(data.Data[0].TipoPersona_id);
            $("#TipoDocumento_id").data("kendoDropDownList").value(data.Data[0].TipoDocumento_id);
            $("#Identificacion").val(data.Data[0].Identificacion);
            $("#PrimerNombres").val(data.Data[0].PrimerNombres);
            $("#SegundoNombres").val(data.Data[0].SegundoNombres);
            $("#PrimerApellido").val(data.Data[0].PrimerApellido);
            $("#SegundoApellido").val(data.Data[0].SegundoApellido);
            $("#Genero_id").data("kendoDropDownList").value(data.Data[0].Genero_id);
            $("#FechaNacimiento").data("kendoDatePicker").value(data.Data[0].FechaNacimiento);
            $("#EmailPersona").val(data.Data[0].EmailPersona);
            $("#Celular").val(data.Data[0].Celular);
            $("#TelefonoCasa").val(data.Data[0].TelefonoCasa);
            $("#TelefonoOficina").val(data.Data[0].TelefonoOficina);
            $("#IdClietne").val(data.Data[0].Id);
            PersonaID = data.Data[0].Id;
            var grid = $("#gridDirecciones").data("kendoGrid");
            grid.dataSource.read();
        } else {
            $("#PrimerNombres").val("");
            $("#SegundoNombres").val("");
            $("#PrimerApellido").val("");
            $("#SegundoApellido").val("");
            $("#Genero_id").data("kendoDropDownList").value(null);
            $("#FechaNacimiento").data("kendoDatePicker").value(null);
            $("#EmailPersona").val("");
            $("#Celular").val("");
            $("#TelefonoCasa").val("");
            $("#TelefonoOficina").val("");
            PersonaID = 0;
            var grid = $("#gridDirecciones").data("kendoGrid");
            grid.dataSource.read();
        }
    });
}

function VerificarUserNameEmailSession() {
    var userName = document.getElementById("Usuario").value;
    var emailSession = document.getElementById("Email").value;
    var url = '/Seguridad/GetInfoByUserEmail';
    $.post(url, { user: userName, email: emailSession },
    function (data) {
        if (data.Estado == true) {
            alert(data.Message);
            if (data.flag == "Usuario") {
                $("#Usuario").val("");
            } else {
                $("#Email").val("");
            }
        }
    });
}
