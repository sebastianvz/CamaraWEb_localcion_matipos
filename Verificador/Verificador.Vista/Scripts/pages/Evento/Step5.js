var Step5 = {
    //props
    Localidades: [],
    EtapaID: 0,
    Services: [],
    /////1- VALIDAR QUE LAS FECHAS NO SE TRASLAPEN, 2- IMPORTAR LOCALIDADES

    //functions
    Init: function () {
        Step5.Localidades = EventStorage.getProp('Localidades');
        Step5.Config();
        Step5.LoadLocalidades();
        const currentState = EventStorage.getProp('Estado');
        if (Evento.State.etapas === currentState) {
            Step5.OpenModal();
        }
        //Step5.LockButtonAdd();
        Step5.LoadServices();
        setTimeout(function () { Step5.SetUniqueEtapa(); }, 3000)

    },
    Config: function () {
        $("#formEtapas .Next").off('click');
        $("#Next-1").off('click');
        $("#formEtapas .Previous").off('click');
        $("#formEtapas .add-localidad").off('click');
        $("#btnAddEtapa").off('click');
        $("#dllLocalidades").off('change');
        $("#ConfigCategories").off('click');
        $("#cloce-modal").off('click');

        $("#formEtapas .Next").on('click', function () {
            const etapas = EventStorage.getProp('Etapas');
            if (etapas.length === 0) {
                alert('Debe de crear al menos una etapa');
                return;
            }
            EventStorage.setProp('Localidades', Step5.Localidades);
            EventStorage.setProp('Progress', 84);
            Evento.SetState(Evento.State.tickets);
        });
        $("#Next-1").on('click', Step5.ContinueLite);
        $("#formEtapas .Previous").on('click', function () { Evento.OpenTab('tab-5') });
        $("#formEtapas .add-localidad").on('click', Step5.AddLocalidad);
        $("#btnAddEtapa").on('click', Step5.AddEtapa);
        $("#dllLocalidades").on('change', Step5.ChangeLocalidad);
        $("#ConfigCategories").on('click', function () {
            Step5.ChangeConfigEtapa(this.checked)
        })
        $("#ckbCodigo").on('click', function () {
            if (this.checked) {
                $('#div-codigo').show()
            }
            else {
                $('#div-codigo').hide()
            }
        });
        $("#cloce-modal").on('click', function () {
            EventStorage.setProp('UniqueEtapa', false);
            $("#btnAddEtapa").prop('disabled', false);
            $("#btnAddEtapa").removeClass('disabled');
            $("#NombreEtapa").val('');
            $("#NombreEtapa").prop('disabled', false);
            $("#NombreEtapa").removeClass('disabled');
            
        });
    },
    AddEtapa: function () {
        if (!Step5.Validate()) {
            return;
        }
        Loading(true);
        let etapas = EventStorage.getProp('Etapas') || []
        let newId = _Array.min(etapas.map(function (e) { return e.Id }));
        if (Step5.EtapaID !== 0) {
            for (let i = 0; i < etapas.length; i++) {
                if (etapas[i].Id === Step5.EtapaID) {
                    etapas[i] = Step5.getDataEtapa(Step5.EtapaID);
                    break;
                }
            }
        } else {
            newId = newId || 0;
            etapas.push(Step5.getDataEtapa(newId - 1));
        }
        EventStorage.setProp('Etapas', etapas);
        Step5.ReloadEtapas();
        Step5.NewEtapa();
        Step5.LockButtonAdd();
        Loading(false);
    },
    AddLocalidad: function () {
        const $localidad = $("#dllLocalidades")
        const selectedLocalidad = $localidad.val();
        if (selectedLocalidad === '0') {
            $localidad.addClass('required-field');
            return;
        }
        $("#table-localidades tbody").append(Step5.getLocalidadData(selectedLocalidad));
        $("#dllLocalidades option[value='" + selectedLocalidad + "']").remove();
    },
    ChangeConfigEtapa: function (state) {
        if (state) {
            $("#ConfigEtapasLabel").text("Manejar varias etapas para el evento");
            $("#cloce-modal").show();
            $("#Next-1").hide();
        } else {
            $("#ConfigEtapasLabel").text("Manejar solo una etapa para el evento");
            $("#cloce-modal").hide();
            $("#Next-1").show();
        }
    },
    ChangeLocalidad: function () {
        $("#dllLocalidades").removeClass("required-field");
    },
    ContinueLite: function () {
        EventStorage.setProp('UniqueEtapa', true);
        Step5.SetUniqueEtapa();
        Step5.FillStagesWithFullAforo();
        $('#modalConfigEtapas').modal('hide');
    },
    EtapasBuilder: function (id, item, aforo, precio, tipoServicio) {
        aforo = aforo || '';
        precio = precio || '';
        tipoServicio = tipoServicio || '';
        const isUniqueEtapa = EventStorage.getProp('UniqueEtapa') || false;
        const disabled = (isUniqueEtapa ? 'disabled' : '');
        return ("<tr id='tr" + id + "'><td>" + item.Nombre
           + "</td><td>" + item.Tipo
           + '</td><td><span id="sp-aforo' + id + '">' + (isUniqueEtapa ? item.Aforo : item.AforoAcumulado) + '</span>/' + item.Aforo
           + '</td><td><input type="number" class="form-control required aforo ' + disabled + '" ' + disabled + ' value="' + (isUniqueEtapa ? item.Aforo : aforo) + '" onfocus="Step5.SubstractAforoByStage(this.value, ' + id + ')" onblur="Step5.UpdateAforoByStage(this, ' + id + ')"></td>'
           + "<td><input type='number' class='form-control required precio' value='" + precio + "' ></td>"
           + "<td><select class='form-control servicio'><option value='0'>Seleccione</option>" + Step5.ServicesBuilder(tipoServicio) + "</select></td>"
           + '<td><div class="btn btn-danger" onclick="Step5.RemoveLocalidad(\'' + item.Nombre + '\'   , ' + id + ')"><i class="glyphicon glyphicon-minus"></i></div></td></tr>');
    },
    EditarEtapa: function (id) {
        if (Step5.EtapaID != 0) {
            alert('Tiene cambios sin guardar');
            return;
        }
        Step5.NewEtapa();
        const etapas = EventStorage.getProp('Etapas');
        const currentEtapa = etapas.find(function (e) { return e.Id === id });
        Step5.EtapaID = currentEtapa.Id;
        $("#NombreEtapa").val(currentEtapa.Nombre);
        $("#FechaInicialEtapa").val(currentEtapa.FechaInicial);
        $("#FechaFinalEtapa").val(currentEtapa.FechaFinal);
        $("#DescripcionEtapa").val(currentEtapa.Descripcion);
        if (currentEtapa.LlevaCodigo) {
            document.getElementById('ckbCodigo').checked = true;
            $("#CodigoEtapa").val(currentEtapa.Codigo);
            $('#div-codigo').show();
        }

        currentEtapa.TicketsEtapas.forEach(function (v, i) {
            const localidadID = v.IdLocalidad;
            $("#table-localidades tbody").append(Step5.getLocalidadData(localidadID, v.Aforo, v.Precio, v.IdTipoServicio, v.AforoAcumulado));
            $("#dllLocalidades option[value='" + localidadID + "']").remove();
        });
        $("#btnAddEtapa").prop('disabled', false);
        $("#btnAddEtapa").removeClass("disabled");
        $("html, body").animate({ scrollTop: 0 }, "slow");
    },
    FillStagesWithFullAforo: function myfunction() {
        let sHtml = '';
        item = Step5.Localidades.forEach(function (item, i) {
            sHtml += Step5.EtapasBuilder(item.Id, item);
            $("#dllLocalidades option[value='" + item.Id + "']").remove();
        });
        $("#table-localidades tbody").append(sHtml);
    },
    getDataEtapa: function (newId) {
        const localidades = [];
        $("#table-localidades tbody tr").each(function (i, v) {
            const inputAforo = $(v).find(".aforo"),
                inputPrecio = $(v).find(".precio"),
                inputServicio = $(v).find(".servicio"),
                id = v.id.replace("tr", "");
            const item = Step5.Localidades.find(function (e) { return e.Id + '' == id });

            localidades.push({
                Aforo: inputAforo.val(),
                Precio: inputPrecio.val(),
                IdLocalidad: id,
                IdTipoServicio: inputServicio.val(),
                AforoAcumulado: item.AforoAcumulado
            });
        });
        return {
            Id: newId,
            Nombre: $("#NombreEtapa").val(),
            FechaInicial: $("#FechaInicialEtapa").val(),
            FechaFinal: $("#FechaFinalEtapa").val(),
            Descripcion: $("#DescripcionEtapa").val(),
            LlevaCodigo: document.getElementById('ckbCodigo').checked,
            Codigo: $("#CodigoEtapa").val(),
            TicketsEtapas: localidades,
        };
    },
    getLocalidadData: function (id, aforo, precio, tipoServicio, aforoAcumulado) {
        let item = {};
        id = parseInt(id);
        
        item = Step5.Localidades.find(function (e) { return e.Id === id });
        //item.AforoAcumulado = item.AforoAcumulado == null ? 0 : item.AforoAcumulado;
        item.AforoAcumulado = aforoAcumulado == null ? (item.AforoAcumulado || 0) : aforoAcumulado;
        return Step5.EtapasBuilder(id, item, aforo, precio, tipoServicio);
    },
    LoadLocalidades: function () {
        $("#dllLocalidades").html('<option value="0">Seleccione</option>');
        const LocalidadesMap = Step5.Localidades.map(function (e) {
            return ("<option value='" + e.Id + "'>" + e.Nombre + "</option>");
        });
        $("#dllLocalidades").append(LocalidadesMap);
    },
    LoadServices: function () {
        $.get(urlBase + 'Event/getServices', {}, function (servicios) {
            Step5.Services = servicios;
        });
    },
    LockButtonAdd: function () {
        const uniqueEtapa = EventStorage.getProp('UniqueEtapa') || false;
        $("#btnAddEtapa").prop('disabled', uniqueEtapa);
    },
    NewEtapa: function () {
        Step5.EtapaID = 0;
        $(".Etapa .form-control").val("");
        $("#table-localidades tbody").empty();
        Step5.LoadLocalidades();
        $('#div-codigo').hide();
        document.getElementById('ckbCodigo').checked = false;
    },
    OpenModal: function () {
        $('#modalConfigEtapas').modal({
            backdrop: 'static',
            keyboard: false
        });
    },
    ReloadEtapas: function () {
        let etapas = EventStorage.getProp('Etapas') || []
        const etpasHTML = etapas.map(function (e) {
            return ('<div class="col-xs-12 col-sm-12 col-md-12" id="Localidad-3"><div class="alert alert-info alert-dismissable fade in" style="padding:10px 10px 10px 16px" role="alert">'
                    + '<a href="javascript:void(0)" class="btn btn-link" style="position:relative; float:right" onclick="Step5.RemoveEtapa(' + e.Id + ')">'
                    + '<span class="glyphicon glyphicon-remove" style="font-size: x-large" aria-hidden="true">'
                    + '</span></a><a href="javascript:void(0)" class="btn btn-link" style="position:relative; float:right" onclick="Step5.EditarEtapa(' + e.Id + ')"><span class="glyphicon glyphicon-pencil" style="font-size: x-large" aria-hidden="true"></span></a>'
                    + '<div class="row"><div class="col-xs-1 col-sm-1 col-md-1"><span class="glyphicon glyphicon-calendar" style="font-size: -webkit-xxx-large;" aria-hidden="true">'
                    + '</span></div><div class="col-xs-9 col-sm-9 col-md-9"><h4 class="alert-heading">' + e.Nombre + '</h4><table class="table"><tbody><tr><td><p><b>Desde:</b> ' + e.FechaInicial + '</p></td>'
                    + '<td><p><b>Hasta:</b> ' + e.FechaFinal + '</p></td></tr><tr><td colspan="4"><p>' + e.Descripcion + '</p></td></tr></tbody></table></div></div></div></div>');
        });
        $("#divEtapas").html(etpasHTML);
    },
    RemoveEtapa: function (id) {
        if (confirm('¿Esta seguro de que quiere eliminar esta etapa?')) {
            Loading(true);
            let etapas = EventStorage.getProp('Etapas')
            etapas = etapas.filter(function (e) { return e.Id !== id });
            EventStorage.setProp('Etapas', etapas);
            Step5.ReloadEtapas();
            Loading(false);
        }
    },
    RemoveLocalidad: function (nombre, id) {
        const $tr = $("#tr" + id);
        const descuento = $tr.find('.aforo').val();
        item = Step5.Localidades.find(function (e) { return e.Id === id });
        item.AforoAcumulado -= parseInt(descuento);
        $("#dllLocalidades").append("<option value='" + id + "'>" + nombre + "</option>");
        $tr.remove();
    },
    ServicesBuilder: function (selected) {
        return Step5.Services.map(function (e) {
            return (
                '<option value="' + e.Id + '"' + (selected == e.Id + '' ? ' selected' : '') + '>' + e.Nombre + '</option>'
            );
        });
    },
    SetUniqueEtapa: function () {
        const isUniqueEtapa = EventStorage.getProp('UniqueEtapa') || false;
        if (!isUniqueEtapa) {
            return;
        }
        const functions = EventStorage.getProp('Funciones');
        const fechas = functions.map(function (e) { return moment(e.FechaInicial, _Date.format) });
        const end = moment.min(fechas);
        const now = moment(new Date(), _Date.format).add(2, 'days');

        $("#NombreEtapa").val('Etapa Unica');
        $("#FechaInicialEtapa").val(now.format(_Date.format));
        $("#FechaFinalEtapa").val(end.format(_Date.format));
        $("#NombreEtapa").val('Etapa Unica');
        $("#NombreEtapa").prop('disabled', true);
        //$("#FechaInicialEtapa").data("kendoDateTimePicker").enable(false);
        //$("#FechaFinalEtapa").data("kendoDateTimePicker").enable(false);        
    },
    SubstractAforoByStage: function (value, id) {
        value = parseInt(value);
        if (isNaN(value)) {
            return;
        }
        const item = Step5.Localidades.find(function (e) { return e.Id === id });
        if (item.AforoAcumulado != 0) {
            item.AforoAcumulado -= parseInt(value);
        }
    },
    UpdateAforoByStage: function (input, id) {
        const $input = $(input),
            value = $input.val();
        $input.removeClass('required-field');
        if (value === '') {
            $input.addClass('required-field');
            return;
        }
        const item = Step5.Localidades.find(function (e) { return e.Id === id });
        const v = parseInt(value);
        
        item.AforoAcumulado += parseInt(value);
        if (item.AforoAcumulado > item.Aforo) {
            alert('Sobrepaso el tope de la localidad');
            item.AforoAcumulado -= parseInt(value);
            return;
        }
        $("#sp-aforo" + id).html(item.AforoAcumulado);
    },
    Validate: function () {
        if (!Validate.fields('table-localidades')) {
            return false;
        }

        const inicioEtapa = $("#FechaInicialEtapa").val(),
            finalEtapa = $("#FechaFinalEtapa").val(),
            etapas = EventStorage.getProp('Etapas') || [];

        const obj = etapas.map(function (e) { return { start: e.FechaInicial, end: e.FechaFinal, id: e.Id } });

        const functions = EventStorage.getProp('Funciones');
        const iniciales = functions.map(function (e) { return moment(e.FechaInicial, _Date.format) });
        const inicioEvento = moment.min(iniciales)

        inicioEvento.format(_Date.format)

        let validate = _Date.validateRangeArray(inicioEtapa, finalEtapa, Step5.EtapaID, obj);
        const _finalEtapa = moment(finalEtapa, _Date.format);

        if (!validate) {
            return validate;
        } else if (!_finalEtapa.isSameOrBefore(inicioEvento)) {
            alert("La fecha final de la etatapa, no puede ser mayor a la fecha inicial de la función");
            return false;
        }
        return true;
    },
}
//$(document).ready(function () {
//    Step5.Init();
//})

function getVenue() {
    return { venue: Venue_id };
}



function ControlLocalidad() {
    var id = parseInt($("#Localidad_id").data("kendoDropDownList").value());
    for (var i = 0; i < Localidades.length; i++) {
        var x = Localidades[i];
        if (x.id == id) {
            $(".input-group-addon").html('Cantidad utilizada ' + x.aforoConsumido + '/' + x.aforo);
            switch (x.tipo) {
                case 1: document.getElementById('tipo-localidad').innerHTML = 'Cantidad de Aforos';
                    break;
                case 2: document.getElementById('tipo-localidad').innerHTML = 'Cantidad de Sillas'
                    break;
                case 3: document.getElementById('tipo-localidad').innerHTML = 'Cantidad de Mesas'
                    break;
                case 4: document.getElementById('tipo-localidad').innerHTML = 'Cantida de Palcos'
                    break;
            }
            break;
        }
    }
}

function consumirAforo(txt) {
    var id = parseInt($("#Localidad_id").data("kendoDropDownList").value());
    for (var i = 0; i < Localidades.length; i++) {
        var x = Localidades[i];
        if (x.id == id) {
            $("#btnAddEtapa").removeProp('disabled');
            $("#btnAddEtapa").removeClass("disabled");
            var suma = parseInt(txt.value) + x.aforoConsumido;
            if (suma > x.aforo) {
                $(".input-group-addon").html('Cantidad utilizada <label style="color:red">' + suma + '</label>/' + x.aforo);
                alert("El aforo acumulado para esta localidad a superado el aforo total de la localidad seleccionada");
                $("#btnAddEtapa").prop('disabled', true);
                $("#btnAddEtapa").addClass("disabled");
            } else {
                $(".input-group-addon").html('Cantidad utilizada ' + suma + '/' + x.aforo);
            }
            break;
        }
    }
}

function findAndSet(descontar) {
    var id = parseInt($("#Localidad_id").data("kendoDropDownList").value());
    for (var i = 0; i < Localidades.length; i++) {
        var x = Localidades[i];
        if (x.id == id) {
            if (descontar) {
                Localidades[i].aforoConsumido += parseInt(document.getElementById('Cantidad').value);
            } else {
                Localidades[i].aforoConsumido -= parseInt(document.getElementById('Cantidad').value);
            }
            break;
        }
    }
}

var divEtapas = $("#divEtapas"),
    Etapa_id = 0,
    LimpiarEtapa = function () {

    },


   RemoveEtapa = function (e) {
       if (confirm('¿Esta seguro de que quiere eliminar esta etapa?')) {
           var url = '@Url.Action("RemoveEtapa", "Evento")';
           Loading(true);
           $.post(url, { id: e, evento_id: Evento_id }, function (e) {
               divEtapas.html(e);
               Loading(false);
           });
       }
   },
   DateFormat = function (date) {
       var d = new Date(date);
       return ("0" + d.getDate()).slice(-2) + "-" + ("0" + (d.getMonth() + 1)).slice(-2) + "-" +
            d.getFullYear() + " " + ("0" + d.getHours()).slice(-2) + ":" + ("0" + d.getMinutes()).slice(-2);
   },
   EditarEtapa = function (id) {
       var url = '@Url.Action("get_EtapaById", "Evento")';
       Loading(true);
       $.post(url, { id: id }, function (e) {
           if (e.data) {
               LimpiarEtapa();
               var data = e.data;
               Etapa_id = data.Id;
               $("#Nombre").val(data.Nombre);
               $("#Localidad_id").data("kendoDropDownList").value(data.Localidad_id);
               $("#Cantidad").val(data.Cantidad);
               $("#Precio").val(data.Precio);
               $("#FechaInicial").data("kendoDateTimePicker").value(data.FechaInicial);
               $("#FechaFinal").data("kendoDateTimePicker").value(data.FechaFinal);
               $("#Descripcion").val(data.Descripcion);
               $("html, body").animate({ scrollTop: 0 }, "slow");
               findAndSet(false);
               ControlLocalidad();
               Loading(false);
           }
       });
   };

//var url = '@Url.Action("get_EtapasxEvento", "Evento")';
//$.post(url, { evento_id: Evento_id }, function (e) {
//    divEtapas.html(e);
//});
