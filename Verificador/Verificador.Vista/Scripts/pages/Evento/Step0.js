var Step0 = {
    //Props
    CategoriaSelected: 0,

    //Functions
    Init: function () {
        Step0.Config();
    },
    Config: function () {
        $("#div-categoria .categoria").off("click");
        $("#div-categoria .categoria").on("click", function () {
            const nombre = $(this).data('name');
            const id = $(this).data('id');
            Step0.setCategory(nombre, id);
            Evento.Progress(12);
        })
        $("#SelectedCategory a").off("click")
        $("#SelectedCategory a").on("click", function () {
            $("#SelectedCategory").hide();
            $(".categoria").fadeIn("slow");
            $("#selected-category").html("");
            EventStorage.setProp('Categoria_id', 0);
            Progress(0);
            $("#informacion-general").addClass('hide');
        })
        $("#save-step0").off('click');
        $("#save-step0").on('click', function (e) {
            if (Validate.fields('formDatosGenerales')) {
                Step0.save();
            }
        })
    },
    HideCategories: function (nombre, id) {
        $(".categoria").fadeOut("slow");
        $("#selected-category").html("<strong>" + nombre + "</strong>");
        $("#SelectedCategory").show();
        $("#informacion-general").removeClass('hide');
    },
    save: function () {
        let data = EventStorage.get();
        data.Nombre = $("#Nombre").val();
        data.Empresario_id = $("#Empresario_id").val();
        data.TipoEvento_id = $("#TipoEvento_id").data('kendoDropDownList').value();
        data.UrlVideo = $("#UrlVideo").val();
        data.EventoPublico = $("#EventoPublico").data('kendoDropDownList').value();
        data.EdadMinima = $("#EdadMinima").val();
        data.AforoTotal = parseInt($("#AforoTotal").val());
        data.Descripcion = $("#Descripcion").data('kendoEditor').value();
        EventStorage.set(data);
        EventStorage.setProp('Progress', 24);
        Evento.SetState(Evento.State.informacionAdicional);        
    },
    setCategory: function (nombre, id) {
        Step0.HideCategories(nombre, id);
        EventStorage.setProp('Categoria_id', id);
        EventStorage.setProp('Categoria', {
            Nombre: nombre,
            Id: id
        });
        EventStorage.setProp('Progress', 12);
        Evento.SetState(Evento.State.informacionGenral);
    },
    //validate: function (formName) {
    //    let valid = true;
    //    $('.form-control.required').removeClass('required-field');
    //    $('.form-control.required').each(function (i, v) {
    //        const $v = $(v);
    //        if ($v.val() === '' || $v.val() === '0') {
    //            $v.addClass('required-field');
    //            valid = false;
    //        }
    //    })
    //    return valid;
    //}
}
$(document).ready(function () {
    Step0.Init();
})