if ($(document).ready) {
    localStorage.clear();
    function showDetalle(e) {
        var gridPlantilla = $("#gridPlantillasBoletos").data("kendoGrid");
        e.container.data("kendoWindow").bind("close", function () {
            gridPlantilla.dataSource.read();
        })
    }

    function editarDiseno(e) {
        e.preventDefault();
        var dataItem = this.dataItem($(e.currentTarget).closest("tr"));
        if (dataItem.Id) {
            var disenador = {
                NombrePlantilla:dataItem.Nombre,
                IdPlantilla: dataItem.Id,
                Alto: dataItem.TipoBoletaPapel.AltoCmm,
                Ancho: dataItem.TipoBoletaPapel.AnchoCmm,
                canvas: dataItem.CanvasJson,
                imagenBoelto: dataItem.TipoBoletaPapel.Imagen,
            };
            localStorage.setItem("disenador", JSON.stringify(disenador));
            window.location.href = '/Administracion/Diseñador';
        }
    }

}
