
function Editar(e)
{
    e.preventDefault();
    var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
    if (dataItem.Id) 
    {
        var evento ={
            IdEvento: dataItem.Id,
            IdEmpresario: dataItem.Empresario_id,
            NombreEvento: dataItem.Nombre
         };
        localStorage.setItem("evento", JSON.stringify(evento));
        var storage = JSON.parse(localStorage.getItem("evento"));
        storage["IdEmpersa"] = 0;
        localStorage.setItem("evento", JSON.stringify(storage));
        window.location.href = '/Staff/AsociarPersonasEmpresasEvento';

        //var url = '@Url.Action("getEventById", "Event")';
        //$.get(url, { id: dataItem.Id }, function (e) {
        //    EventStorage.set(e.evento);
        //    TicketStorage.set(e.ticketData);
        //    MapStorage.set(e.Mapa);
        //    window.location.href = '@(Url.Action("Index", "Event"))';
        //});
    }
}
