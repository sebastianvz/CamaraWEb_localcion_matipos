var Evento = {
    //propiedades
    CategoriaSelected: null,
    State: {
        categoria: 0,
        informacionGenral: 1,
        informacionAdicional: 2,
        direccionMap: 3,
        localidades: 4,
        funciones: 5,
        etapas: 6,
        tickets: 7,
        liquidacion: 8,
        finalizado: 9
    },
    TipoRegisro: {
        Nuevo: 0,
        Editando: 1
    },
    //functions
    Init: function () {
        
        Evento.Config();
        Evento.SetForm();
        const stateID = EventStorage.getProp('Estado');
        if (stateID > 0) {
            EventStorage.setProp('TipoRegisro', this.TipoRegisro.Editando)
        }
        Evento.SetState(stateID);

        var url_string = window.location.href
        var url = new URL(url_string);
        var parameter = url.searchParams.get("reload");
        if (parameter == null || parameter == '') {
            return false;
        } else {
            $("a[href='#step-4']").click()
        }

    },
    InitAll: function () {
        Step0.Init();
        Step1.Init();
        Step2.Init();
        Step3.Init();
        Step4.Init();
        Step5.Init();
        Step6.Init();
    },
    Config: function () {

    },
    CurrentState: function () {
        return EventStorage.getProp('Estado');
    },
    ManageState: function (state) {
        Evento.ShowTabs();
        Evento.OpenTab('tab-' + state);
        if (state >= Evento.State.informacionGenral) {
            const categoria = EventStorage.getProp('Categoria');
            Step0.HideCategories(categoria.Nombre, categoria.Id);
        }
        if (state >= Evento.State.informacionAdicional) {
            Step1.ReloadImpuestos();
        }        
        if (state >= Evento.State.localidades) {
            Step3.ReloadLocalidades();
        }
        if (state >= Evento.State.funciones) {
            Step4.ReloadFunctions();
        }
        if (state >= Evento.State.etapas) {
            Step5.ReloadEtapas();
        }
    },
    ErrorCallBack: function () {
        showMessage(e.Message, 'ERROR');
    },
    getCurrentProgress: function(){
        const currentProgress = parseInt($(".progress-bar").attr("aria-valuenow"));
        return isNaN(currentProgress) ? 0 : currentProgress;
    },
    OpenTab: function (tabName) {
        $('#' + tabName + ' a').trigger('click');
        $(document).scrollTop(0);
    },
    Progress: function Progress(value) {
        var progress = $('.progress-bar');
        progress.css('width', value + '%').attr('aria-valuenow', value).html(value + '%');
    },
    SetForm: function () {
        let data = EventStorage.get();
        Object.keys(data).forEach(function (e) {
            if ($("#" + e)) {
                $("#" + e).val(data[e]);
            }
        })
    },
    SetState: function (state) {
        
        if (state > EventStorage.getProp('Estado')) {
            EventStorage.setProp('Estado', state);
        }
        const newProgress = EventStorage.getProp('Progress');
        if (newProgress > this.getCurrentProgress()) {
            Evento.Progress(newProgress)
        } 
        this.ManageState(state);
        switch (state) {
            case Evento.State.categoria:
                break;
            case Evento.State.informacionGenral:
                break;
            case Evento.State.informacionAdicional:
                break;
            case Evento.State.direccionMap:
                break;
            case Evento.State.localidades:
                Step3.Init();
                break;
            case Evento.State.funciones:
                break;
            case Evento.State.etapas:
                Step5.Init();
                break;
            case Evento.State.tickets:
                Step6.Init();
                break;
            case Evento.State.liquidacion:
                break;
            case Evento.State.finalizado:
                this.InitAll();
                break;
            default:
        }
    },
    ShowTabs: function () {
        const currentState = EventStorage.getProp('Estado');
        for (var i = 0; i < Evento.State.finalizado; i++) {
            $("#tab-" + i).show();
            if (currentState <= i) {
                break;
            }
        }
    },

}
$(document).ready(function () {
    Evento.Init();
})





var CategoriaSelected = 0;
var Evento_id = '@Model.Id';
var Venue_id = '@Model.Venue_id';


function ShowTipoPalco() {
    ddlTipoPalco = $("#TipoPalco").data("kendoDropDownList");
    $(".palco").addClass("hide");
    var idPanel = "#Palco #" + ddlTipoPalco.text().replace(' y ', '');
    $(idPanel).removeClass("hide");
}

function ShowTipoLocation() {
    ddlTipoLocalidad = $("#TipoLocalidad").data("kendoDropDownList");
    $(".Localidad .panel").addClass("hide");
    var idPanel = "#" + ddlTipoLocalidad.text().replace(' y ', '');
    $(idPanel).removeClass("hide");
}

function showValorIpuesto() {
    ddlImpueto = $("#Impuestos").data("kendoDropDownList");
    if (ddlImpueto.value() == '0' || ddlImpueto.value() == '') {
        return false;
    }
    var url = urlBase + "Evento/getValorImpuesto";
    $.get(url, { id: ddlImpueto.value() }, function (e) {
        $("#ValorIpuesto").val(e.Valor);
    });
}

function showServicios() {
    var estado = $("#AplicaServicio").val();
    if (estado == "Si") {
        $("#divServicio").removeClass("hide");
    } else {
        $("#divServicio").addClass("hide");
    }
}

function isItemRepeat(array, e) {
    for (var i = 0; i < array.length; i++) {
        if (array[i].Id == e) {
            return true;
        }
    }
    return false;
}

function deselectedAlert() {
    $("#SelectedCategory").hide();
    $(".categoria").fadeIn("slow");
    $("#text").html("");
    CategoriaSelected = 0;
    $("#Categoria_id").val();
    Progress(0);
    $("#informacion-general").addClass('hide');
}

function Progress(value) {
    var progress = $('.progress-bar');
    progress.css('width', value + '%').attr('aria-valuenow', value).html(value + '%');
}

$('form').on('keyup keypress', function (e) {
    var keyCode = e.keyCode || e.which;
    if (keyCode === 13) {
        e.preventDefault();
        return false;
    }
});

function showButtonNext(estado) {
    if (!estado) {
        $("#Next").addClass("hide");
    } else {
        $("#Next").removeClass("hide");
    }
}

function RemoverAcentos(tx) {
    var text = String(tx);
    text = text.replace(/&Aacute;/g, "Á")
            .replace(/&aacute;/g, "á")
            .replace(/&Eacute;/g, "É")
            .replace(/&eacute;/g, "é")
            .replace(/&Iacute;/g, "Í")
            .replace(/&iacute;/g, "í")
            .replace(/&Oacute;/g, "Ó")
            .replace(/&oacute;/g, "ó")
            .replace(/&Uacute;/g, "Ú")
            .replace(/&uacute;/g, "ú")
            .replace(/&Uuml;/g, "Ü")
            .replace(/&uuml;/g, "ü")
            .replace(/&Ntilde;/g, "Ṅ")
            .replace(/&ntilde;/g, "ñ");
    text = text.replace(/&#193;/g, "Á")
           .replace(/&#225;/g, "á")
           .replace(/&#201;/g, "É")
           .replace(/&#233;/g, "é")
           .replace(/&#205;/g, "Í")
           .replace(/&#237;/g, "í")
           .replace(/&#211;/g, "Ó")
           .replace(/&#243;/g, "ó")
           .replace(/&#218;/g, "Ú")
           .replace(/&#250;/g, "ú")
           .replace(/&#220;/g, "Ü")
           .replace(/&#252;/g, "ü")
           .replace(/&#209;/g, "ñ");
    return text;
}
