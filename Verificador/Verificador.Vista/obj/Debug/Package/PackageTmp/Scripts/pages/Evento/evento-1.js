
var Evento = {
    Init: function () {
        Evento.Config();
    },
    Config: function () {
        Evento.Progress(25);
    },
    Progress: function Progress(value) {
        var progress = $('.progress-bar');
        progress.css('width', value + '%').attr('aria-valuenow', value).html(value + '%');
    }
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

function SelectedCategoria(categoria) {
    $(".categoria").fadeOut("slow");
    $("#text").html("<strong>" + categoria.nombre + "</strong>");
    $("#SelectedCategory").show();
    CategoriaSelected = categoria.id;
    $("#Categoria_id").val(categoria.id);
    Progress(12);
    $("#informacion-general").removeClass('hide');
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
