$(document).ready(function () {
    PrinterTickets.Init();
})
var canvas;
var PrinterTickets = {
    isPendingSave: false,
    CurrentLocation: 0,
    CurrentStage: 0,
    State: {
        IMPRESO: 2,
        DISPONIBLE: 3,
        IMPRESO_DEFECTUOSO: 4,
        REIMPRESO: 5,
        ENCOLA: 6,
    },

    Init: function () {
        PrinterTickets.Config();
    },
    ActionsConfig: function () {
        $(".disponible").on("click", function () {
            $(this).toggleClass("active");
        });
    },
    Config: function () {
        $(".menuitem").on("click", function (e) {
            $("#sidebarRight .btn").removeClass("disabled")
            $(".menuitem").removeClass("active")
            $this = $(this);
            $this.addClass("active")
            if (!PrinterTickets.isPendingSave) {
                PrinterTickets.LoadTickets($this.attr("data-entity"))
            }
        });
        $("#SelectAll").on("click", function () {
            $(".disponible").addClass("active")
        })
        $("#DeselectedAll").on("click", function () {
            $(".disponible").removeClass("active")
        })
        $("#Print").on("click", function () {
            PrinterTickets.GenerateTransaction(PrinterTickets.State.ENCOLA);
        })
        $("#Delete").on("click", function () {
            PrinterTickets.GenerateTransaction(PrinterTickets.State.REIMPRESO);
        })
    },
    ErrorCallBack:function(e){
        console.log(e);
    },
    GetSelectedTickets: function () {
        selecttedTickets = [];
        $("#t-tickets .active").each(function (i, t) {
            selecttedTickets.push({
                Id: $(this).attr("data-entity"),
                Numero: $(this).attr("data-number"),
            });
        })
        return selecttedTickets;
    },
    LoadTickets: function (dataEntity) {
        let ids = dataEntity.split('-');
        let url = urlBase + 'TicketsManagement/getTicketsByLocation'
        PrinterTickets.CurrentStage = ids[0];
        PrinterTickets.CurrentLocation = ids[1];
        $.post(url, { TicketsEtapaId: ids[0], LocalidadId: ids[1] }, PrinterTickets.SuccesCallBack);
    },
    GenerateTransaction: function (state) {
        let url = urlBase + 'TicketsManagement/GenerateTransaction?TicketsEtapaId=' + PrinterTickets.CurrentStage + '&LocalidadId=' + PrinterTickets.CurrentLocation + '&stateId=' + state;       
        var _data = new FormData();
        _data.append("Tickets", JSON.stringify(PrinterTickets.GetSelectedTickets()))

        PrinterTickets.RequestPOST(url, _data, PrinterTickets.SuccesCallBack);
    },
    RequestPOST: function (url, data, successCallBack) {
        $.ajax({
            url: url,
            contentType: false,
            processData: false,
            type: "POST",
            cache: false,
            async: true,
            data: data,
            success: successCallBack,
            error: PrinterTickets.ErrorCallBack
        });
    },
    SuccesCallBack: function (e) {
        if (e.msg != null) {
            alert(e.msg);
        }
        if (!e.status) {            
            return;
        }
        PrinterTickets.TicketsBuilder(e.modelData);
    },
    TicketsBuilder: function (tickets) {
        let rows = ''
        $.each(tickets, function (i, t) {
            rows += '<div class="item-ticket ' + t.Estado + '" data-entity="' + t.Id + '" data-number="' + t.Numero + '"><p>'
            rows += t.Numero
            rows += '</p></div>'
        });
        $("#t-tickets").html(rows);
        PrinterTickets.ActionsConfig();
    }
}