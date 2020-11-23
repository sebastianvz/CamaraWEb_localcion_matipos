var Step2 = {
    //props
    VenueID: 0,
    GeoCode: null,

    //Functions
    Init: function () {
        this.Config();
        let data = EventStorage.getProp('Venue');
        if (data.length !== 0) {
            Step2.SetForm(data);
        }
        
    },
    Config: function () {
        $("#formDireccioffMAP .Next").off('click');
        $('#ImportSteges').off('click');
        $("#formDireccioffMAP .Previous").off('click');

        $("#formDireccionMAP .Next").on('click', function () {
            if (Validate.fields('formDireccionMAP')) {
                Step2.Save();
            }
        });
        $('#ImportSteges').on('click', function () {
            EventStorage.setProp('ImportStages', this.checked);
            EventStorage.setProp('VenueID', Step2.VenueID);
        });
        $("#formDireccionMAP .Previous").on('click', function () { Evento.OpenTab('tab-2') });
        $("#VenueName").kendoAutoComplete({
            template: '<div class="VenueID-id" style="display:none;" onchange="setVenue(this);">#= Id #</div> #= Nombre #',
            dataTextField: "label",
            height: 520,
            dataSource: {
                transport: {
                    read: urlBase + 'Evento/getVenueByName',
                    parameterMap: function () {
                        return { query: $("#VenueName").data("kendoAutoComplete").value() };
                    }
                },
                pageSize: 80,
                serverPaging: true,
                serverFiltering: true
            },
            select: Step2.SelectItem,
        });
    },
    NewVenue: function(){
        $("#formDireccionMAP .form-control").val("");
        Step2.VenueID = 0;
        Step2.GeoCode = null;
    },
    Save: function () {
        let data = EventStorage.getProp('Venue');        
        data.Nombre = $("#VenueName").val();
        data.Direccion = $("#Direccion").val();
        data.CityName = $("#locality").val();
        data.Ciudad_id = $("#Ciudad_id").val();
        data.DepartametName = $("#administrative_area_level_1").val();
        data.CountryName = $("#country").val();
        data.Longitud = $("#lon").text();
        data.Latitud = $("#lat").text();
        data.Telefono = $("#Telefono").val();
        data.E_mail = $("#E_mail").val();
        data.GeoCode = $("#GeoCode").val();
        data.PostalCode = $("#postal_code").val();

        EventStorage.setProp('Venue', data);
        EventStorage.setProp('Progress', 48);
        Evento.SetState(Evento.State.localidades);
    },
    SelectItem: function (e) {
        Step2.NewVenue();
        var item = this.dataItem(e.item.index())
        e.sender.element["0"].value = item.Nombre;

        Step2.SetForm(item);

        $("#div-switch").show();
        initMap();
    },
    SetForm: function (data) {
        Step2.VenueID = data.Id;
        Step2.GeoCode = data.GeoCode;
        $("#VenueName").val(data.Nombre);
        $("#Direccion").val(data.Direccion);
        $("#Ciudad_id").val(data.Ciudad_id);
        $("#locality").val(data.CityName);
        $("#administrative_area_level_1").val(data.DepartametName);
        $("#country").val(data.CountryName);
        $("#GeoCode").val(data.GeoCode);
        $("#postal_code").val(data.PostalCode);
        $("#lon").text(data.Longitud);
        $("#lat").text(data.Latitud);
        $("#Telefono").val(data.Telefono);
        $("#E_mail").val(data.E_mail);
    },
}
$(document).ready(function () {
    Step2.Init();
})


