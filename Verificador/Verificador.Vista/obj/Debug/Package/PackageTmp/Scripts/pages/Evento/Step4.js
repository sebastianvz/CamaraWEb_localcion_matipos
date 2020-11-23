var Step4 = {
    //Props
    Funcion_id: 0,
    Servicios: 0,

    //Functions
    Init: function () {
        Step4.Config();
        //$.get('', {}, function (servicios) {
        //    Step4.Servicios = servicios
        //})
    },
    Config: function () {
        $("#formFunciones .Next").off('click');
        $("#formFunciones .Previous").off('click');
        $("#formFunciones #btn-add").off('click');

        $("#formFunciones .Next").on('click', function () {
            const functions = EventStorage.getProp('Funciones');
            if (functions.length === 0) {
                alert('Debe de crear al menos una función');
                return;
            }
            EventStorage.setProp('Progress', 72);
            Evento.SetState(Evento.State.etapas);
        });
        $("#formFunciones .Previous").on('click', function () { Evento.OpenTab('tab-3') });
        $("#formFunciones #btn-add").on('click', Step4.AddFuncion);
    },
    AddFuncion: function () {
        if (!Step4.Validate()) {
            return;
        }
        Loading(true);
        let funciones = EventStorage.getProp('Funciones') || [];

        if (Step4.Funcion_id != 0) {
            for (var i = 0; i < funciones.length; i++) {
                if (funciones[i].Id === Step4.Funcion_id) {
                    funciones[i] = Step4.getData();
                    break;
                }
            }
        } else {
            const newId = _Array.min(funciones.map(function (e) { return e.Id }));
            funciones.push(Step4.getData((newId || 0) - 1));
        }
        EventStorage.setProp('Funciones', funciones);
        Step4.ReloadFunctions();
        Step4.NewBehabior();

        Loading(false);
    },
    EditarFuncion: function (id) {
        Loading(true);
        Step4.NewBehabior();
        const functions = EventStorage.getProp('Funciones');
        let data = functions.filter(function (e) { return e.Id === id })[0];
        if (data) {
            Step4.Funcion_id = data.Id;
            $("#SS_FunctionFechaInicial").data("kendoDateTimePicker").value(data.FechaInicial);
            $("#SS_FunctionFechaFinal").data("kendoDateTimePicker").value(data.FechaFinal);
            $("#AperturaPuertas").data("kendoTimePicker").value(data.AperturaPuertas);
            $("#FunctionDescripcion").val(data.Descripcion);
            $("#Label").val(data.Label);
            $("html, body").animate({ scrollTop: 0 }, "slow");
        }
        Loading(false);
    },
    getData: function (newId) {
        return {
            Id: newId || Step4.Funcion_id,
            FechaInicial: $("#SS_FunctionFechaInicial").val(),
            FechaFinal: $("#SS_FunctionFechaFinal").val(),
            AperturaPuertas: $("#AperturaPuertas").val(),
            Descripcion: $("#FunctionDescripcion").val(),
            Label: $("#Label").val()
        };
    },
    NewBehabior: function () {
        Step4.Funcion_id = 0;
        $("#SS_FunctionFechaInicial").data("kendoDateTimePicker").value("");
        $("#SS_FunctionFechaFinal").data("kendoDateTimePicker").value("");
        $(".Funcion .form-control").val("");
    },
    ReloadFunctions: function () {/////AGREGAR LOS COBOS DE SERICIOS A LA TABLA, PONER A FUNCIONAR EL DISEÑADOR DE BOLETAS
        const functions = EventStorage.getProp('Funciones');
        if (functions == null) {
            return;
        }
        const data = functions.map(function (item) {
            return ('<div class="col-xs-12 col-sm-12 col-md-12" id="Localidad-3"><div class="alert alert-info alert-dismissable fade in" style="padding:10px 10px 10px 16px" role="alert">'
                + '<a href="javascript:void(0)" class="btn btn-link" style="position:relative; float:right" onclick="Step4.RemoveFuncion(' + item.Id + ')"><span class="glyphicon glyphicon-remove" style="font-size: x-large" aria-hidden="true"></span></a>'
                + '<a href="javascript:void(0)" class="btn btn-link" style="position:relative; float:right" onclick="Step4.EditarFuncion(' + item.Id + ')"><span class="glyphicon glyphicon-pencil" style="font-size: x-large" aria-hidden="true"></span></a>'
                + '<div class="row"><div class="col-xs-1 col-sm-1 col-md-1"><span class="glyphicon glyphicon-music" style="font-size: -webkit-xxx-large;" aria-hidden="true"></span></div>'
                + '<div class="col-xs-9 col-sm-9 col-md-9"><h4 class="alert-heading">' + item.Label + '</h4><table class="table"><tbody><tr>'
                + '<td><p><b>Desde:</b> ' + item.FechaInicial + '</p></td>'
                + '<td><p><b>Hasta:</b> ' + item.FechaFinal + '</p></td>'
                + '<td><p><b>Hora de apertura:</b> ' + item.AperturaPuertas + '</p></td></tr><tr><td colspan="5">'
                + '<p>' + item.Descripcion + '</p></td></tr></tbody></table></div></div></div></div>')
        });
        $("#divFunciones").html(data);
    },
    RemoveFuncion: function (id) {
        if (confirm('¿Esta seguro de que quiere eliminar esta función?')) {
            Loading(true);
            let funciones = EventStorage.getProp('Funciones');
            EventStorage.setProp('Funciones', funciones.filter(function (e) { return e.Id !== id }));
            Step4.ReloadFunctions();
            Loading(false);
        }
    },
    Validate: function () {
        const inicio = $("#SS_FunctionFechaInicial").val(),
            final = $("#SS_FunctionFechaFinal").val(),
            functions = EventStorage.getProp('Funciones') || [];

        const obj = functions.map(function (e) { return { start: e.FechaInicial, end: e.FechaFinal, id: e.Id } });

        return _Date.validateRangeArray(inicio, final, Step4.Funcion_id, obj);
    },
}
$(document).ready(function () {
    Step4.Init();
})