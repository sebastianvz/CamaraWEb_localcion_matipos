; (function (trv, $) {
    "use strict";

    var sr = {
        controllerNotInitialized: 'Controlador no inicializado.',
        noReportInstance: 'No existe instancia del reporte.',
        missingTemplate: 'Plantilla del report viewer esta vacia. por favor chequee las opciones.',
        noReport: 'Sin reporte.',
        noReportDocument: 'Sin documento.',
        invalidParameter: 'Por favor ingrese un valor valido.',
        invalidDateTimeValue: 'Por favor ingrese una fecha valida.',
        parameterIsEmpty: 'El parametro no puede ser vacio.',
        cannotValidateType: 'No puede validar el tipo de parametro.',
        loadingFormats: 'Cargando...',
        loadingReport: 'Cargando reporte...',
        preparingDownload: 'Preparando documento para descargar. Por favor espere...',
        preparingPrint: 'Preparando documento para imprimir. Por favor espere...',
        errorLoadingTemplates: 'Error cargando las plantillas del reporteador.',
        loadingReportPagesInProgress: '{0} páginas cargadas ...',
        loadedReportPagesComplete: 'Listo. Total {0} páginas cargadas.',
        noPageToDisplay: "No hay páginas.",
        errorDeletingReportInstance: 'Error borrando la instancia del reporte: {0}',
        errorRegisteringViewer: 'Error registrando el visor con el servicio..',
        noServiceClient: 'Ningun servicio ha sido especificado en el controlador.',
        errorRegisteringClientInstance: 'Error registrando instancia en el cliente',
        errorCreatingReportInstance: 'Error creando instancia de reporte(Report = {0})',
        errorCreatingReportDocument: 'Error creando documento de reporte (Report = {0}; Format = {1})',
        unableToGetReportParameters: "Incapaz de recibir los parametros del reporte",
        MissingReportSource: "hola",
    };

    trv.sr = $.extend(trv.sr, sr);

}(window.telerikReportViewer = window.telerikReportViewer || {}, jQuery));