const EVENT_KEY = '3v3ntD4t4K34';
const TICET_KEY = 'T1ck3tD4t4';
const MAP_KEY = 'T0k3n.M4p';
var EventStorage = {
    get: function () {
        if (localStorage.getItem(EVENT_KEY) == null) {
            this.set(this.setDefaultValue());
        } 
        return JSON.parse(localStorage.getItem(EVENT_KEY));
    },
    getJson: function () {
        return localStorage.getItem(EVENT_KEY);
    },
    getProp: function(key){
        const data = this.get();
        return data[key];
    },
    set: function (data) {
        localStorage.setItem(EVENT_KEY, JSON.stringify(data));
    },
    setProp: function (key, value) {
        let data = this.get();
        data[key] = value;
        this.set(data);
    },
    remove: function () {
        localStorage.removeItem(EVENT_KEY);
    },
    setDefaultValue: function () {
        return {
            Id: 0,
            Estado: -1,
            TipoRegisro: 0,
            Nombre: '',
            Multifuncion: false,
            //{
            //    get
            //    {
            //        if (this.Funciones != null)
            //        {
            //            return this.Funciones.Count() > 1;
            //        }
            //        return false;
            //    }
            //}
            CantidadFunciones: 0,
            //{
            //    get
            //    {
            //        if (this.Funciones != null)
            //        {
            //            return this.Funciones.Count();
            //        }
            //        return 0;
            //    }
            //}
            FechaInicial: new Date(),
            //{
            //    get
            //    {
            //        if (this.Funciones != null)
            //        {
            //            return this.Funciones.Min(e => e.FechaInicial);
            //        }
            //        return fechaInicial;
            //    }
            //    set { fechaInicial = value; }
            //}
            FechaFinal: new Date(),
            //{
            //    get
            //    {
            //        if (this.Funciones != null)
            //        {
            //            return this.Funciones.Max(e => e.FechaFinal);
            //        } 
            //        return fechaFinal;
            //    }
            //    set { fechaFinal = value; }
            //}
            Venue: {}, //VenueViewModel
            Venue_id: 0,

            Empresario: {}, //EmpresarioViewModel
            Empresario_id: 0,
            TipoEvento_id: 0,
            Foto: '',

            Descripcion: '',

            UrlVideo: '',
            EdadMinima: 0,

            EventoPublico: '',
            TipoRecaudo_id: 0,
            EntradasxPedidoOcompra: 0,
            EntradasxUsuario: 0,
            AplicaServicio: 0,
            CantidadDiasValides: 0,
            AforoTotal: 0,
            AforoAcmulado: 0,
            FechaCreacion: new Date(),
            Categoria: {}, // CategoriaViewModel
            Categoria_id: 0,

            Funciones: [], //FuncionesEventoViewModel

            Impuestos: [],//ImpuestoViewModel

            TipoServicios: [], //TipoServicioViewModel
            Localidades:[],
            Etapas: [], //EtapasViewModel

            Tickets: {},//TicketsEventoViewModel,

            Tickets_id: 0
        };
        return event;
    }
}

var TicketStorage = {
    get: function () {
        if (localStorage.getItem(TICET_KEY) == null) {
            return '';
        }
        return JSON.parse(localStorage.getItem(TICET_KEY));
    },
    set: function(data){
        localStorage.setItem(TICET_KEY, JSON.stringify(data));
    },
    remove: function () {
        localStorage.removeItem(TICET_KEY);
    },
}

var MapStorage = {
    get: function () {
        if (localStorage.getItem(MAP_KEY) == null) {
            return '';
        }
        const data = JSON.parse(localStorage.getItem(MAP_KEY));
        if (typeof data === "string") {
            return JSON.parse(data);
        }
        return data;    
    },
    getJSON: function () {
        return localStorage.getItem(MAP_KEY);
    },
    set: function (data) {
        localStorage.setItem(MAP_KEY, JSON.stringify(data));
    },
    remove: function () {
        localStorage.removeItem(MAP_KEY);
    },
}

