var Step1 = {
    Init: function () {
        Step1.Config();
    },
    Config: function () {
        $("#btnAddImpuesto").on("click", Step1.AddImpueto);
        $("#formDatosAdicionales #Next").on('click', Step1.Save);
        $("#formDatosAdicionales #Previous").on('click', function () { Evento.OpenTab('tab-1') });
    },

    AddImpueto: function () {
        Loading(true);
        ddlImpueto = $("#Impuestos").data("kendoDropDownList");

        let impuestos = EventStorage.getProp('Impuestos');
        impuestos.push({
            Id: parseInt(ddlImpueto.value()), Nombre: ddlImpueto.text(), Valor: $("#ValorIpuesto").val()
        })
        EventStorage.setProp('Impuestos', impuestos);
        Step1.ReloadImpuestos();
    },
    ReloadImpuestos: function(){
        const data = EventStorage.getProp('Impuestos');
        if (data == null) {
            EventStorage.setProp('Impuestos', []);
            return;
        }
        const impuetos = data.map(function (e) {
            const sHtml = '<div class="col-md-3"><div class="alert alert-info alert-dismissible fade in" style="padding:10px 20px 10px 10px" role="alert"><button type="button" class="close" data-dismiss="alert" aria-label="Close" onclick="Step1.RemoveImpuesto('
                + e.Id + ')"><span aria-hidden="true">×</span></button><strong>'
                + e.Nombre + '</strong> ' + e.Valor
                + '</div></div>'
            return sHtml;
        });
        $("#divImpuestos").html(impuetos);
        Loading(false);
    },
    RemoveImpuesto: function (id) {
        Loading(true);
        let impuestos = EventStorage.getProp('Impuestos');
        EventStorage.setProp('Impuestos', impuestos.filter(function (e) { return e.Id !== id }));
        Loading(false);
    },
    Save: function () {
        let data = EventStorage.get();
        data.TipoRecaudo_id = Validate.isNullOrEMptyInt(Getters.dropDown("TipoRecaudo_id"));
        data.CantidadDiasValides = Validate.isNullOrEMptyInt($("#CantidadDiasValides").val());
        data.EntradasxUsuario = Validate.isNullOrEMptyInt($("#EntradasxUsuario").val());
        data.EntradasxPedidoOcompra = Validate.isNullOrEMptyInt($("#EntradasxPedidoOcompra").val());
        data.ValorIpuesto = Validate.isNullOrEMptyInt($("#ValorIpuesto").val());
        data.Progress = 36;
        EventStorage.set(data);
        Evento.SetState(Evento.State.direccionMap);
    },
}
$(document).ready(function () {
    Step1.Init();
})