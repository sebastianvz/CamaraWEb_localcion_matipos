var Step3 = {
    //props
    CurrentStage: null,
    AforoTotal: EventStorage.getProp('AforoTotal'),
    AforoAcumulado: EventStorage.getProp('AforoAcmulado'),
    AforoSuma: 0,

    //funciones
    Init: function () {
        Step3.Config();
        Step3.LoadAforo();
        const importStages = EventStorage.getProp('ImportStages');
        Step3.AforoTotal = EventStorage.getProp('AforoTotal');
        Step3.AforoAcumulado = EventStorage.getProp('AforoAcmulado');
        if (Evento.State.localidades === Evento.CurrentState() && importStages) {
            Step3.ImportStages();
        }

        const mode = EventStorage.getProp('StagesByMap');
        if (mode) {
            $("#form-stages").hide();
            $("#btnRedirectToMap").show();
            $(".glyphicon.glyphicon-pencil").parent().hide();
            $("div#div-switch").parent().hide();
        } else if (mode == false) {
            $("#form-stages").show();
            $("#btnRedirectToMap").hide();
            $(".glyphicon.glyphicon-pencil").parent().show();
            $("div#div-switch").parent().show();      
        }
    },
    Config: function () {
        $("#btnAddLocalidad").off('click');
        $("#AforoCapacidad").off('blur')
        $("#fmColumnas").off('blur')
        $("#msCantidadSillas").off('blur')
        $("#PalcomsCantidadSillas").off('blur')
        $("#formLocalidades .Next").off('click');
        $("#formLocalidades .Previous").off('click');
        $("#btnRedirectToMap").off('click');
        $('#StegesByMap').off('click');

        $("#btnAddLocalidad").on('click', Step3.AddLocalidad);
        $("#AforoCapacidad").on('blur', function () { Step3.AcumularAforo(1) })
        $("#fmColumnas").on('blur', function () { Step3.AcumularAforo(2) })
        $("#msCantidadSillas").on('blur', function () { Step3.AcumularAforo(3) })
        $("#PalcomsCantidadSillas").on('blur', function () { Step3.AcumularAforo(4) })
        $("#formLocalidades .Next").on('click', function () {
            const localidades = EventStorage.getProp('Localidades');
            if (localidades.length === 0) {
                alert('Debe de crear al menos una localidad');
                return;
            }
            EventStorage.setProp('Progress', 60);
            Evento.SetState(Evento.State.funciones);
        });
        $("#formLocalidades .Previous").on('click', function () { Evento.OpenTab('tab-3') });
        $("#btnRedirectToMap").on('click', function () {
            let url = urlBase + 'Event/StagesCreator';
            window.location.href = url;
        });

        $('#StegesByMap').on('click', function () {
            if (this.checked) {
                $("#form-stages").hide();
                $("#btnRedirectToMap").show();
            } else {
                $("#form-stages").show();
                $("#btnRedirectToMap").hide();
            }
            EventStorage.setProp('StagesByMap', this.checked)
        });
    },
    AddLocalidad: function () {
        ddTipoLocalidad = $("#TipoLocalidad").data("kendoDropDownList");
        tipoLocalidad = parseInt(ddTipoLocalidad.value());
        if (Step3.ValidateLocacion(tipoLocalidad)) {
            return;
        }
        if (Step3.AforoAcumulado > Step3.AforoTotal) {
            alert("El aforo acumulado supera al aforo del evento");
            return;
        }
        let localidades = EventStorage.getProp('Localidades') || []
        let newId = _Array.min(localidades.map(function (e) { return e.Id }));
        if (Step3.CurrentStage != null) {
            for (var i = 0; i < localidades.length; i++) {
                if (localidades[i].Id === Step3.CurrentStage.Id) {
                    localidades[i] = Step3.getLocalidadData(Step3.CurrentStage.Id, tipoLocalidad);
                    break;
                }
            }
        } else {
            newId = newId || 0;
            localidades.push(Step3.getLocalidadData(newId - 1, tipoLocalidad));
        }
        EventStorage.setProp('Localidades', localidades);
        Step3.ReloadLocalidades();
        Step3.LimpiarLocalidad();
    },
    AcumularAforo: function (tipo) {
        const AforoSuma = Step3.getAforo(tipo);
        $("#Aforo-utilizado").parent().css("color", "black");
        const suma = Step3.AforoAcumulado + AforoSuma;
        $("#Aforo-utilizado").html(suma + '/' + Step3.AforoTotal);
        if (suma > Step3.AforoTotal) {
            $("#Aforo-utilizado").parent().css("color", "red");
            alert("El aforo acumulado supera al aforo del evento");
        }
        return true;
    },
    ConfigStagesByType: function () {

    },
    EditarLocalidad: function (id) {
        if (Step3.CurrentStage != null) {
            alert('Tiene cambios sin guardar');
        }
        Step3.LimpiarLocalidad();

        const locations = EventStorage.getProp('Localidades');
        Step3.CurrentStage = locations.filter(function (e) { return e.Id === id })[0];

        if (Step3.CurrentStage.RangoTickets) {
            $("#section-range").removeClass("hide")
            document.getElementById("checkShowRange").checked = true;
            $("#NumeroTicketInicial").val(Step3.CurrentStage.NumeroTicketInicial);
            $("#NumeroTicketFinal").val(Step3.CurrentStage.NumeroTicketFinal);
        }
        $("#NombreLocalidad").val(Step3.CurrentStage.Nombre);
        $("#TipoLocalidad").data("kendoDropDownList").value(Step3.CurrentStage.IdTipoLocalidad);
        $("#ValorConsumo").val(Step3.CurrentStage.ValorConsumo);
        $("#DescripcionConsumo").val(Step3.CurrentStage.Consumo);
        switch (Step3.CurrentStage.IdTipoLocalidad) {
            case 1://Aforo
                $("#AforoCapacidad").val(Step3.CurrentStage.Aforo);
                break;
            case 2://Filas Sillas
                $("#fmFilas").val(Step3.CurrentStage.Filas);
                $("#fmColumnas").val(Step3.CurrentStage.Sillas);
                $("#fmOrientacionFilas").data("kendoDropDownList").value(Step3.CurrentStage.OrientacionFilas);
                $("#fmOrientacionSillas").data("kendoDropDownList").value(Step3.CurrentStage.OrientacionSillas);
                $("#fmTipoNumeracion").data("kendoDropDownList").value(Step3.CurrentStage.IdTipoNumeracion);
                break;
            case 3://Mesas Sillas
                $("#msCantidadMesas").val(Step3.CurrentStage.Filas);
                $("#msCantidadSillas").val(Step3.CurrentStage.Sillas);
                $("#msTipoNumeracion").data("kendoDropDownList").value(Step3.CurrentStage.IdTipoNumeracion);
                break;
            case 4://Palco
                $("#PalcomsCantidadMesas").val(Step3.CurrentStage.Filas);
                $("#PalcomsCantidadSillas").val(Step3.CurrentStage.Sillas);
                $("#PalcomsTipoNumeracion").data("kendoDropDownList").value(Step3.CurrentStage.IdTipoNumeracion);
                break;
        }
        Step3.AforoAcumulado -= Step3.CurrentStage.Aforo;
        EventStorage.setProp('AforoAcmulado', Step3.AforoAcumulado)
        Step3.ShowTipoLocation();
        $("html, body").animate({ scrollTop: 0 }, "slow");
        Loading(false);
    },
    getAforo: function (tipo) {
        var filas = 0;
        var sillas = 0;
        switch (tipo) {
            case 1:
                filas = $("#AforoCapacidad").val();
                sillas = 1;
                break;
            case 2:
                filas = $("#fmFilas").val();
                sillas = $("#fmColumnas").val();
                break;
            case 3:
                filas = $("#msCantidadMesas").val();
                sillas = $("#msCantidadSillas").val();
                break;
            case 4:
                filas = $("#PalcomsCantidadMesas").val();
                sillas = $("#PalcomsCantidadSillas").val();
                break;
        }
        filas = filas == '' ? 0 : parseInt(filas);
        sillas = sillas == '' ? 0 : parseInt(sillas);
        return filas * sillas;
    },
    getLocalidadData: function (newId, tipoLocalidad) {
        let filas = 0,
            sillas = 0,
            numeracion = null,
            numeracionType = null,
            orientacionFilas = null,
            orientacionSillas = null;

        switch (tipoLocalidad) {
            case 1:
                filas = 0;
                sillas = 0;
                break
            case 2:
                filas = $("#fmFilas").val();
                sillas = $("#fmColumnas").val();
                numeracion = $("#fmTipoNumeracion").data("kendoDropDownList").value();
                numeracionType = $("#fmTipoNumeracion").data("kendoDropDownList").text();
                orientacionFilas = $("#fmOrientacionFilas").data("kendoDropDownList").value();
                orientacionSillas = $("#fmOrientacionSillas").data("kendoDropDownList").value();
                break;
            case 3:
                filas = $("#msCantidadMesas").val();
                sillas = $("#msCantidadSillas").val();
                numeracion = $("#msTipoNumeracion").data("kendoDropDownList").value();
                numeracionType = $("#msTipoNumeracion").data("kendoDropDownList").text();
                break;
            case 4:
                filas = $("#PalcomsCantidadMesas").val();
                sillas = $("#PalcomsCantidadSillas").val();
                numeracion = $("#PalcomsTipoNumeracion").data("kendoDropDownList").value();
                numeracionType = $("#PalcomsTipoNumeracion").data("kendoDropDownList").text();
                break;
        }

        const localidad = {
            Id: newId,
            Nombre: $("#NombreLocalidad").val(),
            Aforo: Step3.getAforo(tipoLocalidad),
            Filas: filas,
            Sillas: sillas,
            IdTipoLocalidad: tipoLocalidad,
            TicketNumeroInicio: $("#NumeroTicketInicial").val(),
            TicketNumeroFinal: $("#NumeroTicketFinal").val(),
            RangoTickets: document.getElementById("checkShowRange").checked,
            IdTipoNumeracion: numeracion,
            Numeracion: numeracionType,
            OrientacionFilas: orientacionFilas,
            OrientacionSillas: orientacionSillas,
            Consumo: $("#DescripcionConsumo").val(),
            ValorConsumo: $("#ValorConsumo").val(),
            Tipo: Step3.getStageTypeName(tipoLocalidad),
            AforoAcumulado: 0,
        }

        return localidad;
    },
    getStageIcon: function (tipo) {
        switch (tipo) {
            case 1:
                return 'stop';
            case 2:
                return 'align-justify';
            case 3:
                return 'bed';
            case 4:
                return 'glass';
        }
    },
    getStageTypeName: function (tipo) {
        switch (tipo) {
            case 1:
                return 'Aforo';
            case 2:
                return 'Filas y Sillas';
            case 3:
                return 'Mesas y Sillas';
            case 4:
                return 'Palco';
        }
    },
    ImportStages: function () {
        const venueID = EventStorage.getProp('VenueID');
        $.get(urlBase + 'Event/getLocalidadesByVenue', { venueId: venueID }, function (localidades) {
            let k = -1;
            localidades.forEach(function (item, i) {
                item.Id = k--;
                item.Tipo = Step3.getStageTypeName(item.IdTipoLocalidad);
            });
            const suma = _Array.sum(localidades.map(function (e) { return e.Aforo }));
            if (suma > parseInt(EventStorage.getProp('AforoTotal'))) {
                alert('La suma del aforo de las localidaddes importadas superan el aforo de este evento');
            }
            EventStorage.setProp('Localidades', localidades);
            Step3.ReloadLocalidades();
        })
    },
    LimpiarLocalidad: function () {
        Step3.CurrentStage = null;
        $(".Localidad .form-control").val("");
        $("#TipoLocalidad").data("kendoDropDownList").value("");
        $("#fmOrientacionFilas").data("kendoDropDownList").value("");
        $("#fmOrientacionSillas").data("kendoDropDownList").value("");
        $("#fmTipoNumeracion").data("kendoDropDownList").value("");
        $("#fmTipoNumeracion").data("kendoDropDownList").value("");
        if (document.getElementById("checkShowRange").checked) {
            $("#section-range").addClass("hide")
            document.getElementById("checkShowRange").checked = false;
        }
        $(".Localidad .panel").addClass("hide");
    },
    LoadAforo: function () {
        $("#Aforo-utilizado").html('' + Step3.AforoAcumulado + '/' + Step3.AforoTotal);
    },
    ReloadLocalidades: function () {
        const localidades = EventStorage.getProp('Localidades');
        const localidadesHTML = localidades.map(function (item) {
            const tipo = item.IdTipoLocalidad;
            return ('<dv class="col-xs-12 col-sm-12 col-md-12"><div class="alert alert-info alert-dismissable fade in" style="padding:10px 10px 10px 16px" role="alert">'
                + '<a href="javascript:void(0)" class="btn btn-link" style="position:relative; float:right" onclick="Step3.RemoveLocalidad(' + item.Id + ')"><span class="glyphicon glyphicon-remove" style="font-size: x-large" aria-hidden="true"></span></a>'
                + '<a href="javascript:void(0)" class="btn btn-link" style="position:relative; float:right" onclick="Step3.EditarLocalidad(' + item.Id + ')"><span class="glyphicon glyphicon-pencil" style="font-size: x-large" aria-hidden="true"></span></a>'
                + '<div class="row"><div class="col-xs-1 col-sm-1 col-md-1"><span class="glyphicon glyphicon-' + Step3.getStageIcon(tipo) + '" style="font-size: -webkit-xxx-large;" aria-hidden="true"></span></div><div class="col-xs-9 col-sm-9 col-md-9">'
                + '<h4 class="alert-heading">' + item.Nombre + ' <small>' + Step3.getStageTypeName(tipo) + '</small></h4>'
                + Step3.StageContentBuilder(tipo, item)
                + '</div></div></div></div>');
        });
        const acumulado = _Array.sum(localidades.map(function (e) { return e.Aforo }));
        EventStorage.setProp('AforoAcmulado', acumulado);
        Step3.AforoAcumulado = acumulado;
        $("#Aforo-utilizado").html(acumulado + '/' + Step3.AforoTotal);
        $("#divLocalidades").html(localidadesHTML);
    },
    RemoveLocalidad: (function (id) {
        if (confirm('¿Esta seguro de que quiere eliminar esta Localidad?')) {
            Loading(true);
            let localidades = EventStorage.getProp('Localidades')
            localidades = localidades.filter(function (e) { return e.Id !== id });
            EventStorage.setProp('Localidades', localidades);
            Step3.ReloadLocalidades();
            Loading(false);
        }
    }),
    ShowTipoLocation: function () {
        ddlTipoLocalidad = $("#TipoLocalidad").data("kendoDropDownList");
        $(".Localidad .panel").addClass("hide");
        var idPanel = "#" + ddlTipoLocalidad.text().replace(' y ', '');
        $(idPanel).removeClass("hide");
    },
    StageContentBuilder: function (tipo, item) {
        let sHtml = '<table class="table"><tr>'
        switch (tipo) {
            case 1:
                sHtml += '<td><b>Capacidad: </b>' + item.Aforo + '</td>'
                       + '<td><b>Descripción del Consumo: </b>' + item.Consumo + '</td>'
                       + '<td><b>Valor del Consumo: </b>' + item.ValorConsumo + '</td>';
                break;
            case 2:
                sHtml += '<td><b>Cantidad de Filas: </b>' + item.Filas + '</td>'
                        + '<td><b>Cantidad de Columnas: </b>' + item.Sillas + '</td>'
                        + '<td><b>Orientación: </b>' + item.OrientacionFilas + '/' + item.OrientacionSillas + '</td></tr>'
                        + '<tr><td><b>Tipo de Numeración: </b>' + item.numeracion + '</td>'
                        + '<td><b>Descripción del Consumo: </b>' + item.Consumo + '</td>'
                        + '<td><b>Valor del Consumo: </b>' + item.ValorConsumo + '</td>';
                break;
            case 3:
                sHtml += '<td><b>Cantidad de Mesas: </b>' + item.Filas + '</td>'
                        + '<td><b>Cantidad de Sillas por Mesa: </b>' + item.Sillas + '</td>'
                        + '<tr><td><b>Tipo de Numeración: </b>' + item.numeracion + '</td></tr><tr>'
                        + '<td colspan="3"><table style="width:100%"><tr>'
                        + '<td><b>Descripción del Consumo: </b>' + item.Consumo + '</td>'
                        + '<td><b>Valor del Consumo: </b>' + item.ValorConsumo + '</td>'
                        + '</tr></table></td>';
                break;
            case 4:
                sHtml += '<td><b>Cantidad de Palcos: </b>' + item.Filas + '</td>'
                        + '<td><b>Cantidad de personas por palco: </b>' + item.Sillas + '</td>'
                        + '<td><b>Tipo de Numeración: </b>' + item.numeracion + '</td></tr><tr>'
                        + '<td colspan="3"><table style="width:100%"><tr>'
                        + '<td><b>Descripción del Consumo: </b>' + item.Consumo + '</td>'
                        + '<td><b>Valor del Consumo: </b>' + item.ValorConsumo + '</td>'
                        + '</tr></table></td>';
                break;
        }
        sHtml += '</tr>';
        if (item.RangoTickets) {
            sHtml += '<tr><td colspan="3"><h5>Rango de tiquetes <small><b>Desde: </b>' + item.TicketNumeroInicio + '  <b>Hasta: </b>' + item.TicketNumeroFinal + '</small></h5></td></tr>'
        }
        sHtml += '</table>';
        return sHtml;
    },
    ValidateLocacion: function (e) {
        $(".form-invalid").removeClass("form-invalid");
        var invalid = false;
        if ($("#NombreLocalidad").val() == "") {
            $("#NombreLocalidad").addClass("form-invalid");
            invalid = true;
        }
        if ($("#TipoLocalidad").data("kendoDropDownList").value() == "") {
            $("#TipoLocalidad").addClass("form-invalid");
            invalid = true;
        }
        switch (e) {
            case 1: if ($("#AforoCapacidad").val() == "") {
                $("#AforoCapacidad").addClass("form-invalid");
                invalid = true;
            }
                break;
            case 2: if ($("#fmFilas").val() == "") {
                $("#fmFilas").addClass("form-invalid");
                invalid = true;
            } if ($("#fmColumnas").val() == "") {
                $("#fmColumnas").addClass("form-invalid");
                invalid = true;
            } if ($("#fmOrientacionSillas").data("kendoDropDownList").value() == "") {
                $("#fmOrientacionSillas").addClass("form-invalid");
                invalid = true;
            } if ($("#fmOrientacionFilas").data("kendoDropDownList").value() == "") {
                $("#fmOrientacionFilas").addClass("form-invalid");
                invalid = true;
            } if ($("#fmTipoNumeracion").data("kendoDropDownList").value() == "") {
                $("#fmTipoNumeracion").addClass("form-invalid");
                invalid = true;
            }
                break;
            case 3: if ($("#msCantidadMesas").val() == "") {
                $("#msCantidadMesas").addClass("form-invalid");
                invalid = true;
            } if ($("#msCantidadSillas").val() == "") {
                $("#msCantidadSillas").addClass("form-invalid");
                invalid = true;
            } if ($("#msTipoNumeracion").data("kendoDropDownList").value() == "") {
                $("#msTipoNumeracion").addClass("form-invalid");
                invalid = true;
            }
                break;
            case 4:
                if ($("#PalcomsCantidadMesas").val() == "") {
                    $("#PalcomsCantidadMesas").addClass("form-invalid");
                    invalid = true;
                } if ($("#PalcomsCantidadSillas").val() == "") {
                    $("#PalcomsCantidadSillas").addClass("form-invalid");
                    invalid = true;
                } if ($("#PalcomsTipoNumeracion").data("kendoDropDownList").value() == "") {
                    $("#PalcomsTipoNumeracion").addClass("form-invalid");
                    invalid = true;
                }
                break;
        }
        return invalid;
    },
}
//$(document).ready(function () {
//    Step3.Init();
//})
