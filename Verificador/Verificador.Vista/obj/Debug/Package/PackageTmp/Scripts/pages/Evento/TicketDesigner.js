var Creator = {
    //props
    Ticket: null,
    
    //functions
    Init: function () {
        Creator.Config();
        $("#creator-header .event-title").text(EventStorage.getProp('Nombre'));


    },
    Config: function () {      
        $("#creator-header .Back").on("click", Creator.goBack);
        $("#creator-header .Save").on("click", Creator.Save);
    },
    goBack: function () {
        window.history.back();
    },
    Save:function() {
        //const ticketSVG = canvas.toSVG();
        //TicketStorage.set(ticketSVG);

        const ticketJSON = canvas.toJSON();
        TicketStorage.set(ticketJSON);

        const state = 7;
        if (state > EventStorage.getProp('Estado')) {
            EventStorage.setProp('Estado', state);
        }

        Creator.goBack()
    }
}
$(document).ready(function () {
    Creator.Init();
})