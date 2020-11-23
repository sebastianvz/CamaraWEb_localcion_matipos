var Step6 = {
    Init: function () {
        Step6.Config();
        $("#btnOpenTicketDesigner").show();
    },
    Config: function () {
        $("#btnOpenTicketDesigner").on("click", function () {
            Step6.OpenTicketDesigner();
        });
        $("#formTicketCreator .Finish").on('click', Step6.SaveAll);        
    },
    OpenTicketDesigner: function () {
        var ticketType = $("#TicketType").data("kendoDropDownList").value();
        if (ticketType == '' || ticketType == '0') {
            alert('Primero debe de seleccionar el tipo de boleto que esea diseñar');
            return;
        }
        var url = urlBase + 'Event/TicketDesigner?Type=' + ticketType;
        window.location.href = url;
    },
    SaveAll: function () {
        const ticketData = TicketStorage.get();
        if (ticketData == null || ticketData == "") {
            alert('Primero debe de crear un tiqute');
            return;
        }
        const functions = EventStorage.getProp('Funciones');
        const iniciales = functions.map(function (e) { return moment(e.FechaInicial, _Date.format) });
        const finales = functions.map(function (e) { return moment(e.FechaFinal, _Date.format) });
        const end = moment.max(finales);
        const start = moment.min(iniciales)

        EventStorage.setProp('FechaInicial', start.format(_Date.format));
        EventStorage.setProp('FechaFinal', end.format(_Date.format));

        Loading(true);

        const desc = EventStorage.getProp('Descripcion');
        EventStorage.setProp("Descripcion", '');

        const venue = EventStorage.getProp('Venue');
        const stages = EventStorage.getProp('Localidades');
        const steps = EventStorage.getProp('Etapas');

        EventStorage.setProp('Venue', {});
        EventStorage.setProp('Localidades', []);
        EventStorage.setProp('Funciones', []);
        EventStorage.setProp('Etapas', []);

        var eventData = new FormData();
        eventData.append("EventData", JSON.stringify(EventStorage.get()));
        eventData.append("Empresario", EventStorage.getProp('Empresario_id'));
        eventData.append("VenueData", JSON.stringify(venue));
        eventData.append("StagesData", JSON.stringify(stages));
        eventData.append("FunctionsData", JSON.stringify(functions));
        eventData.append("StepsData", JSON.stringify(steps));
        eventData.append("__TicketData", JSON.stringify(ticketData));
        eventData.append("__EventDescription", JSON.stringify(desc));
        eventData.append("__Mapa", MapStorage.getJSON());

        EventStorage.setProp("Descripcion", desc);
        EventStorage.setProp('Venue', venue);
        EventStorage.setProp('Localidades', stages);
        EventStorage.setProp('Funciones', functions);
        EventStorage.setProp('Etapas', steps);

        const url = urlBase + 'Event/EventSave';
        Request.Generate(url, eventData, "POST", function (e) {
            Loading(false);
            if (e.status != 200) {
                alert(e.message)
                return;
            }
            window.location.href = urlBase + 'Evento/Eventos';
        });
    },
}
//$(document).ready(function () {
//    Step6.Init();
//})

function showTicketDesigner() {
    var isvalid = true;
    $(".form-invalid").removeClass("form-invalid");
    if ($("#Tickets_Cantidad").val() == "") {
        $("#Tickets_Cantidad").addClass("form-invalid");
        isvalid = false;
    } if ($("#Tickets_InicioNumeracion").val() == "") {
        $("#Tickets_InicioNumeracion").addClass("form-invalid");
        isvalid = false;
    } if (!isvalid) {
        return;
    }
    var ticketType = $("#Tickets_TicketType").data("kendoDropDownList").value();
    if (ticketType == '' || ticketType == '0') {
        alert('Primero debe de seleccionar el tipo de boleto que esea diseñar');
        return;
    }
    var url = '@Url.Action("LoadTicketDesigner", "Evento", new { type = "p1", inicioNumeracion = "p2" })';
    url = url.replace("p1", ticketType).replace("p2", $("#Tickets_InicioNumeracion").val()).replace('&amp;', '&');
    var frame = document.getElementById("iframe");
    frame.src = url;
    Loading(true);
    $("#modalTicket").modal("show");
    setTimeout(function () { Loading(false); }, 5000);
}

var divServicios = $("#divServicios"),
   AddServicio = function () {
       ddlServicio = $("#Servicio").data("kendoDropDownList");
       var url = '@Url.Action("AddServicio", "Evento")';
       $.post(url, { id: parseInt(ddlServicio.value()) }, function (e) {
           divServicios.html(e);
       });
   },
   RemoveServicio = function (e) {
       var url = '@Url.Action("RemoveServicio", "Evento")';
       $.post(url, { id: e }, function (e) {
           divServicios.html(e);
       });
   };



//url = '@Url.Action("get_ServiciosxEvento", "Evento")';
//$.post(url, {}, function (e) {
//    divServicios.html(e);
//});

function SaveCanvas() {
    window.frames[0].SaveCanvasToJson();
    showButtonNext(true);
    $("#modalTicket").modal("hide");
}

//Progress(84);