//CREAR EL CAMPO 'TOKEN' EN LA TABLA DE ASIENTOSLOCALIDAD

var _canvas = null,
    _lastMousePos = null,
    _currentObject = null,
    _countSeats = 0;


var Creator = {
    //Props
    Stages: [],
    canvas: null,
    Colors: {
        backColorStage: '#d3d3d3',
        strokeColorStage: '#333',
        strokeColorSelected: "blue",
        ColorText: '#000',
        backGroundColor: "white",
        selectedStrokeColor: "green",
        selectedBackGroundColor: "white",
        backColorDisableStage: "#efefef",
        strokeColorDisableStage: "#676767",
    },
    StageType: {
        Aforo: 'Aforo',
        FilasSillas: 'FilasSillas',
        MesasSillas: 'MesasSillas',
        Palco: 'Palco',
    },
    CurrentStage: null,
    currentStageName: '',
    currentObjects: [],
    removedSeats: [],
    CurrentZoom: 1025,
    clicked: true,
    editLayer: null,
    editing: false,
    editingSeat: false,
    editingObjects: [],
    lastZoom: 1,
    AforoAcmulado: () => _Array.sum(Creator.Stages.map(e => e.Aforo)),
    AforoTotal: 0,
    ControlAforo: 0,
    numerator: 0,
    getPos: false,
    zoomToPos: null,
    numerationType: {
        pair: '2,4,6,...',
        odd: '1,3,5,...',
        numeric: '1,2,3,...',
        letters: 'A,B,C,...',
    },
    SeatRestored: false,


    //Functions
    Init: function () {
        Creator.Config();
        Creator.LoadEventTitle();
        Creator.LoadCanvas();
        Creator.LoadStages();
        Creator.LoadControlAforo();
        //Creator.LoadConfigStages();
    },
    Config: function () {
        $("#btnRedirectToEvent").off("click");
        $("#btnRedirectToEvent").on('click', function () {
            let url = urlBase + 'Event/Index';
            window.location.href = url;
        })
        //$(".create-stage").on('click', function () {
        //    var rect = new fabric.Rect({
        //        left: 100,
        //        top: 100,
        //        fill: 'blue',
        //        width: 80,
        //        height: 50
        //    });
        //    _canvas.add(rect);
        //    $("#modal-typeStages").modal("hide");
        //});

        //$("#save-stage").off('click');
        //$("#save-stage").on('click', function () {
        //    Creator.NewAforo();
        //});
        $(".to-edit-stage").off('click');
        $(".to-edit-stage").on('click', function () {
            $("#Nombre").focus();
            Creator.NewStage(true);
            const type = $(this).attr('tipo');
            Creator.ShowForm(type);
            $("#apply-changes-stage").data("tipo", Creator.StageType[type]);
        });

        $("#save-stage").off("click");
        $("#save-stage").on("click", function () {
            let type = $(this).data("tipo");
            switch (type) {
                case 'Aforo':
                    Creator.NewAforo();
                    break;
                case 'FilasSillas':
                    Creator.NewFilasSillas();
                    break;
                case 'MesasSillas':
                    Creator.NewMesasSillas();
                    break;
                case 'Palco':
                    Creator.NewBox();
                    break;
            }
        });

        $("#btnNewStage").off("click");
        $("#btnNewStage").on("click", function () {
            Creator.NewStage();
        });

        $("#btnEditStage").off("click");
        $("#btnEditStage").on("click", function () {
            if (Creator.CurrentStage == null) {
                alert('Seleccione una localidad');
                return;
            }
            Creator.editing = true;
            Creator.getPos = true;
            const id = $("#apply-changes-stage").data("entity"),
                tipo = $("#apply-changes-stage").data("tipo");
            Creator.UnGroupSelectObject();
            Creator.ShowForm(tipo);
            Creator.SetStage(id);
            Creator.RestarAforo(tipo);
            Creator.DisableAllObjects(false);
            Creator.currentStageName = $("#btnEditStage").data("stageID");
            //$("#div-msCantidadMesas").show();
            //$("#div-PalcomsCantidadMesas").show();
            switch (tipo) {
                case Creator.StageType.Aforo:
                    break;
                case Creator.StageType.FilasSillas:
                    break;
                case Creator.StageType.MesasSillas:
                    //$("#div-msCantidadMesas").hide();
                    break;
                case Creator.StageType.Palco:
                    //$("#div-PalcomsCantidadMesas").hide();
                    break;
            }
            Creator.AddEditLayer();
            //Creator.OpenModalTypeStage();
        });

        $(".to-configure-stage").off("click");
        $(".to-configure-stage").on("click", function () {
            let type = $(this).attr("tipo");
            $(".stage-types").addClass('hide')
            $("#" + type).removeClass('hide');
            $("#save-stage").data("tipo", type);
        });

        $("#to-back-configure").off("click");
        $("#to-back-configure").on("click", function () {
            Creator.CleanModalStage('configure-stage', '');
        });

        $("#btnUngroupStage").off("click");
        $("#btnUngroupStage").on("click", function () {
            Creator.UnGroupSelectObject();
        });

        $("#form-edit-seat .btn-link").off("click")
        $("#form-edit-seat .btn-link").on("click", function () {
            Creator.AcceptChangesSeats();
        });

        $("#apply-changes-seat").off("click")
        $("#apply-changes-seat").on('click', function () {
            Creator.ModifySelectedSeats();
        });

        $("#remove-changes-seat").off("click")
        $("#remove-changes-seat").on('click', function () {
            Creator.RemoveSelectedSeats();
        });

        $("#cancel-changes-stage").off("click")
        $("#cancel-changes-stage").on("click", function () {
            if (Creator.CurrentStage != null) {
                //Creator.SetZoom(Creator.CurrentZoom);
                Creator.RestoreZoom();
            }
            const tipo = $("#apply-changes-stage").data("tipo");
            Creator.NewStage();
            Creator.UpdateAforo(tipo);

            // CUANDO SE CREA UNA LOCALIDAD DE FILAS Y SILLAS DE 50  X 10 (500) SOLO ESTA CREANDO 499
        });

        $("#apply-changes-stage").off("click");
        $("#apply-changes-stage").on("click", Creator.ApplyChangesStage);

        $("#btnEditSeats").off("click");
        $("#btnEditSeats").on("click", function () {
            Creator.numerator += 1;
            Creator.editingSeat = true;
            Creator.UnGroupSelectObject();
            Creator.editing = false;
            Creator.ShowFormEditSeats(false);
            Creator.setSeatInfo();
            Creator.DisableAllObjects(false);
            Creator.AddEditLayer();
            Creator.ControlAforo += Creator.removedSeats.length;
        });

        $("#restore-changes-seat").off("click");
        $("#restore-changes-seat").on("click", function () {
            Creator.RestoreRemovedSeats();
        });

        $("#btnCancelStage").off("click");
        $("#btnCancelStage").on("click", function () {
            Creator.CleanFormSeats();
            Creator.ShowSelectStageType();
            Creator.DisableAllObjects(true);
            Creator.RestoreZoom();
            _canvas.discardActiveObject();
            _canvas.discardActiveGroup();
            _canvas.renderAll();
            $("#apply-changes-stage").data("entity", "");
        });


        $("#btnRemoveStage").off("click");
        $("#btnRemoveStage").on("click", function () {
            if (Creator.CurrentStage != null) {
                if (confirm('¿Esta seguro de elminar la localidad seleccionada?')) {

                    const id = $("#apply-changes-stage").data("entity");
                    const localidad = Creator.Stages.find(e => e.Id == id);

                    if (localidad != null) {

                        let seatsCount = Creator.CurrentStage.seatsCount || 0
                        if (seatsCount > 0) {
                            localidad.Aforo -= seatsCount;
                        } else {
                            seatsCount = localidad.Aforo;
                        }

                        //Creator.AforoAcmulado -= seatsCount;

                        const ids = Creator.Stages.map(e => e.Id);
                        Creator.Stages.splice(ids.indexOf(id), 1);

                        $("#acumulatedValue-label").html(Creator.AforoAcmulado());
                        $("#aforoFull-label").html(Creator.AforoTotal);
                        $("#control-aforo").removeClass("form-invalid");
                    }

                    _canvas.remove(Creator.CurrentStage);
                    _canvas.renderAll();
                    Creator.ShowSelectStageType();
                    $("#apply-changes-stage").data("entity", "");
                }
            }
        });

        $("#btnCancelEditSeats").off("click");
        $("#btnCancelEditSeats").on("click", function () {
            let obj = Creator.CurrentStage.getObjects();
            if (obj.length === 0) {
                return;
            }
            obj.forEach(e => {
                if (e.objRemoved && e.numerator != Creator.numerator) {
                    e.setVisible(false);
                } else if (e.numerator == Creator.numerator) {
                    e.getObjects()[0].setFill(Creator.Colors.backColorStage);
                    e.getObjects()[0].setStroke(Creator.Colors.strokeColorStage);
                    e.getObjects()[1].setColor(Creator.Colors.ColorText);
                    e.objRemoved = false;
                }
            });

            Creator.ControlAforo = 0;
            Creator.CleanFormSeats();
            Creator.ShowSelectActionType();
            Creator.DisableAllObjects(true);
            Creator.RestoreZoom();
            _canvas.discardActiveObject();
            _canvas.discardActiveGroup();
            _canvas.renderAll();
            Creator.RemoveEditLayer();
        });

        $("#btnSaveMap").off("click")
        $("#btnSaveMap").on("click", function () {
            //const toJson = _canvas.toJSON([
            //    'id'
            //    , 'tipo'
            //    , 'objGroup'
            //    , 'stageType'
            //    , 'tableColor'
            //    , 'seatsCount'
            //    , 'objRemoved'
            //    , 'Order'
            //    , 'objLabel'
            //    , 'rowLabel'
            //    , 'objectType'
            //    , 'Order'
            //    , 'tipo'
            //    , 'rowLabel'
            //    , 'tipo'
            //    , 'token'
            //]);
            const toJson = Creator.BuildObjectsToSave();
            MapStorage.set(toJson);

            EventStorage.setProp("Localidades", Creator.Stages);

            //let url = urlBase + 'Event/Index?reload=true';
            let url = urlBase + 'Event/Index';
            window.location.href = url;
        })

        var angleControl = document.getElementById('angle-control');
        angleControl.oninput = function () {
            if (Creator.CurrentStage != null) {
                Creator.CurrentStage.set('angle', parseInt(this.value, 10)).setCoords();
                _canvas.renderAll();
            }
        };

        var skewXControl = document.getElementById('skewX-control');
        skewXControl.oninput = function () {
            if (Creator.CurrentStage != null) {
                Creator.CurrentStage.set('skewX', parseInt(this.value, 10)).setCoords();
                _canvas.renderAll();
            }
        };

        var skewYControl = document.getElementById('skewY-control');
        skewYControl.oninput = function () {
            if (Creator.CurrentStage != null) {
                Creator.CurrentStage.set('skewY', parseInt(this.value, 10)).setCoords();
                _canvas.renderAll();
            }
        };

        $(".form-blur").off('blur');
        $(".form-blur").on('blur', function () {
            const len = parseInt($(this).attr('blur')),
                value = parseInt($(this).val());

            if (value > len) {
                $(this).val(len);
            }
        });

        $(".btn-group-vertical.zoom-controls button").off('click');
        $(".btn-group-vertical.zoom-controls button").on('click', function () {
            const currentZoom = _canvas.getZoom(),
                type = $(this).find('.glyphicon-plus');
            let newZoom = currentZoom;

            if (type.length > 0) {
                newZoom += 0.1;
            } else {
                newZoom -= 0.1;
            }
            _canvas.setZoom(newZoom);
            _canvas.renderAll()
        });

        //Creator.TestStageByType(Creator.StageType.FilasSillas);

    },
    DisableAllObjects: function (state) {
        const stageName = (Creator.currentStageName || '');
        _canvas.forEachObject(function (e) {
            if (state) {
                e.selectable = state;
            }
            else if (e.objGroup != stageName) {
                e.selectable = state;
            }
        })
    },
    deg2rad: function deg2rad(number) {
        return (Math.PI / 180) * number;
    },
    EditStageData: function (id) {
        ///TERMINAR DE IMPLEMENTAR ESTE LETODO PARA QUE ACTULIZE LA LOCALIDAD EN EL STIRAGE

        Creator.CleanModalStage();

        const stage = Creator.Stages.filter(function (e) { return e.Id === id })[0];

        //if (stage.RangoTickets) {
        //    $("#section-range").removeClass("hide")
        //    document.getElementById("checkShowRange").checked = true;
        //    $("#NumeroTicketInicial").val(stage.NumeroTicketInicial);
        //    $("#NumeroTicketFinal").val(stage.NumeroTicketFinal);
        //}

        $("#Nombre").val(stage.Nombre);
        $("#ValorConsumo").val(stage.ValorConsumo);
        $("#DescripcionConsumo").val(stage.Consumo);

        switch (stage.IdTipoLocalidad) {
            case 1://Aforo
                showByType("aforo");
                $("#Capacidad").val(stage.Aforo);
                break;
            case 2://Filas Sillas
                showByType("FilasSillas");
                $("#fmFilas").val(stage.Filas);
                $("#fmColumnas").val(stage.Sillas);
                $("#fmOrientacionFilas").data("kendoDropDownList").value(stage.OrientacionFilas);
                $("#fmOrientacionSillas").data("kendoDropDownList").value(stage.OrientacionSillas);
                $("#fmTipoNumeracion").data("kendoDropDownList").value(stage.IdTipoNumeracion);
                break;
            case 3://Mesas Sillas
                showByType("MesasSillas");
                //$("#msCantidadMesas").val(stage.Filas);
                $("#msCantidadSillas").val(stage.Sillas);
                $("#msTipoNumeracion").data("kendoDropDownList").value(stage.IdTipoNumeracion);
                break;
            case 4://Palco
                showByType("Palco");
                //$("#PalcomsCantidadMesas").val(stage.Filas);
                $("#PalcomsCantidadSillas").val(stage.Sillas);
                $("#PalcomsTipoNumeracion").data("kendoDropDownList").value(stage.IdTipoNumeracion);
                break;
        }
        //Step3.AforoAcumulado -= Creator.CurrentStage.Aforo;
        //EventStorage.setProp('AforoAcmulado', Step3.AforoAcumulado)
        //Step3.ShowTipoLocation();
        //Loading(false);
    },

    AcceptChangesSeats: function () {
        Creator.HideSeatsRemoved();
        //Creator.ShowSelectActionType();
        Creator.ShowSelectStageType();
        Creator.DisableAllObjects(true);
        Creator.CleanFormSeats();
        //_canvas.discardActiveObject();
        //_canvas.discardActiveGroup();
        Creator.UnselectObjects(Creator.CurrentStage);
        Creator.RestoreZoom();
        Creator.RemoveEditLayer();
    },
    AddEditLayer: function () {
        Creator.editLayer = new fabric.Rect({
            left: Creator.zoomToPos.x - (_canvas.getWidth() * 10 / 2),
            top: Creator.zoomToPos.y - (_canvas.getHeight() * 10 / 2),
            width: _canvas.getWidth() * 10,
            height: _canvas.getHeight() * 10,
            fill: '#000',
            opacity: 0.6,
            selectable: false
        });
        _canvas.add(Creator.editLayer);
        Creator.CurrentStage.bringToFront();
    },
    RemoveEditLayer: function () {
        if (Creator.editLayer) {
            Creator.editLayer.remove();
            _canvas.renderAll();
        }
    },
    ApplyChangesStage: function () {
        const type = $("#apply-changes-stage").data("tipo");

        if (Creator.CurrentStage == null) {
            Creator.CreateStage(type);
            Creator.UpdateAforo();
            return;
        }

        //let localidad = Creator.Stages.find(e=>e.Id === id);
        //localidad = Creator.GetLocalidadData(id, type)
        const id = $("#apply-changes-stage").data("entity");
        let valid = false;

        switch (type) {
            case Creator.StageType.Aforo:

                if (!Creator.ValidateFields(type)) {
                    return false;
                }

                $("#Capacidad").removeClass('required-field');
                const aforo = parseInt($("#Capacidad").val());
                if (isNaN(aforo)) {
                    $("#Capacidad").addClass('required-field');
                    return;
                }

                const localidad = Creator.GetLocalidadData(id, type);
                //const localidad = Creator.Stages.filter(e => e.Id === id)[0];
                //localidad.Aforo = aforo;
                Creator.CurrentStage.getObjects()[1].setText(`${localidad.Nombre}(${aforo.toString()})`);
                Creator.RestoreZoom();
                valid = true;
                break;
            case Creator.StageType.FilasSillas:
                valid = Creator.EditFilasSillas(id);
                break;
            case Creator.StageType.MesasSillas:
                valid = Creator.EditMesasSillas(id);
                break;
            case Creator.StageType.Palco:
                valid = Creator.EditBox(id);
                break;
        }

        if (!valid) {
            return false;
        }

        //const id = $("#apply-changes-stage").data("entity"),
        //    name = $("#Nombre").val();

        //const satgesId = Creator.Stages.map(e => e.Id);
        //Creator.Stages[satgesId.indexOf(id)] = Creator.GetLocalidadData(id, type);
        Creator.UpdateAforo(type);

        /////Prueba cambio commit 
        Creator.CleanModalStage();
        _canvas.renderAll();
        //Creator.SetZoom(Creator.CurrentZoom);
        //Creator.lastZoom = _canvas.getZoom();

        //const _x = Creator.CurrentStage.getLeft(),
        //  _y = Creator.CurrentStage.getTop();
        //_canvas.zoomToPoint({ x: _x, y: _y }, Creator.lastZoom);

        Creator.NewStage();
        Creator.RemoveEditLayer();

    },
    BuildObjectsToSave: function () {
        //const extractSeats = objects => objects
        //    .filter(e => e.tipo !== "LABEL")
        //    .map(e => ({
        //        Order: e.Order,
        //        id: e.id,
        //        objLabel: e.objLabel,
        //        objRemoved: e.objRemoved,
        //        rowLabel: e.rowLabel,
        //        tipo: e.tipo,
        //    }));

        return _canvas.getObjects().map(obj => ({
            id: obj.id.replace('STAGE-', ''),
            seatsCount: obj.seatsCount,
            token: obj.token,
            stageType: obj.stageType,
            top: obj.top,
            left: obj.left,
            tableColor: obj.tableColor,
            height: obj.height,
            width: obj.width,
            stageType: obj.stageType,
            rowLabel: obj.rowLabel,
            //asientos: extractSeats(obj._objects),
        }));
    },
    CalculateCurvePoint: function (seatsCount) {
        const medium = seatsCount / 2;
        if (seatsCount % 2 === 0) {
            return [medium, medium + 1];
        } else {
            return [medium + 0.5];
        }
    },
    CleanFormSeats: function () {
        Creator.editingSeat = false;
        Creator.currentObjects = [];
        Creator.removedSeats = [];
        $("#form-edit-seat input").val('');
        $("#form-seat-tipoNumeracion").data("kendoDropDownList").value('');
    },
    CleanModalStage: function (div) {
        div = div || 'form-edit-stage';
        $("#" + div + " input").val("");
        $("#" + div + " textarea").val("");
        $("#PalcomsTipoNumeracion").data("kendoDropDownList").value(0);
        $("#msTipoNumeracion").data("kendoDropDownList").value(0);
        $("#fmTipoNumeracion").data("kendoDropDownList").value(0);
        $("#fmOrientacionSillas").data("kendoDropDownList").value(0);
        $("#fmOrientacionFilas").data("kendoDropDownList").value(0);
        $(".section-range").hide();
        if (document.getElementById('checkShowRange').checked) {
            document.getElementById('checkShowRange').click();
        }
    },
    CreateStage: function (type) {
        let valid = false;
        switch (type) {
            case Creator.StageType.Aforo:
                valid = Creator.NewAforo();
                break;
            case Creator.StageType.FilasSillas:
                valid = Creator.NewFilasSillas();
                break;
            case Creator.StageType.MesasSillas:
                valid = Creator.NewMesasSillas();
                break;
            case Creator.StageType.Palco:
                valid = Creator.NewBox();
                break;
        }
        if (valid) {
            //    Creator.UpdateAforo(type);
            Creator.NewStage();
        }
    },
    CurveStage: function (value) {
        const seats = Creator.CurrentStage.getObjects().filter(e => e.tipo === 'SEAT');
        const rows = _Array.distinct(seats.map(function (e) {
            return e.rowLabel
        })),
            seatsCount = Creator.CurrentStage.seatsCount,
            arY = [], arX = [];

        for (var x = seatsCount / 2; x > 0; x--) {
            arX.push(x);
        }

        for (var x = 1; x <= seatsCount / 2; x++) {
            arX.push(x);
        }

        const radius = 9 * value,
                spacing = 180 / seatsCount;

        const deg2rad = function deg2rad(number) {
            return (Math.PI / 180) * number;
        };


        for (var i = 0; i < rows.length; i++) {

            const itemsByRow = seats.filter(function (e) {
                return e.rowLabel === rows[i];
            });

            //itemsByRow.forEach(e=> {
            //    e.setTop(e.top + arY[aux] * 10)
            //    aux += 1;
            //});

            for (var j = 0; j < itemsByRow.length; j++) {
                const top = itemsByRow[j].top;
                //const sin = (2 * arX[j] ^ 2 + value * top ^ 2);
                const sin = Math.sin(deg2rad(spacing * j)) * radius;
                itemsByRow[j].setTop(top + sin);
                console.log('Datos', sin)
            }

        }

        _canvas.renderAll();
        /*
        //TODO: VOY AQUI

        //obtengo el valor de la x
        x = [];
        Creator.CurrentStage.getObjects().filter(e=>e.tipo!=='LABELS').filter(e=>e.rowLabel==='A').forEach(e=>x.push(e.left));

        //obtengo la y
        const y = [];
        x.forEach(e => {
	        y.push(1*Math.sin(1*e + 1 * Math.PI))
        })

        //aplico la formula
        let aux = 0
        Creator.CurrentStage.getObjects().filter(e=>e.tipo!=='LABELS').filter(e=>e.rowLabel==='A').forEach(e=>{
	        x.setTop(e.top + y[aux])
	        aux += 1;
        });
        _canvas.renderAll()

        //reestablece el indice en 0
        Creator.CurrentStage.getObjects().filter(e=>e.tipo==='SEAT').forEach(e=>{	
	        e.setTop(-17.5)
        });
        _canvas.renderAll()

        */
    },
    GetAforo: function (tipo) {
        var filas = 0;
        var sillas = 0;
        switch (tipo) {
            case 1:
                filas = $("#Capacidad").val();
                sillas = 1;
                break;
            case 2:
                filas = $("#fmFilas").val();
                sillas = $("#fmColumnas").val();
                break;
            case 3:
                filas = 1;//$("#msCantidadMesas").val();
                sillas = $("#msCantidadSillas").val();
                break;
            case 4:
                filas = 1;//$("#PalcomsCantidadMesas").val();
                sillas = $("#PalcomsCantidadSillas").val();
                break;
        }
        filas = filas == '' ? 0 : parseInt(filas);
        sillas = sillas == '' ? 0 : parseInt(sillas);
        return filas * sillas;
    },
    GetLocalidadData: function (id, stageType) {
        let localidad = {};
        if (id) {
            localidad = Creator.Stages.filter(e => e.Id === id)[0];
        } else {
            let newId = _Array.min(Creator.Stages.map(function (e) { return e.Id })) - 1;
            localidad.Id = newId >= 0 ? -1 : newId;
            Creator.Stages.push(localidad);
        }
        Creator.SetLocalidadData(localidad, stageType);
        return localidad;
    },
    SetLocalidadData: function (localidad, stageType) {

        let filas = 0,
            sillas = 0,
            numeracion = null,
            numeracionType = null,
            orientacionFilas = null,
            orientacionSillas = null;

        const tipoLocalidad = Creator.GetStageTypeId(stageType);

        switch (tipoLocalidad) {
            case 1:
                filas = 0;
                sillas = 0;
                break
            case 2:
                filas = $("#fmFilas").val();
                sillas = $("#fmColumnas").val();
                numeracion = $("#fmTipoNumeracion").data("kendoDropDownList").value();
                numeracionType = $("#fmTipoNumeracion").data("kendoDropDownList").text();
                orientacionFilas = $("#fmOrientacionFilas").data("kendoDropDownList").value();
                orientacionSillas = $("#fmOrientacionSillas").data("kendoDropDownList").value();
                break;
            case 3:
                filas = 1;//$("#msCantidadMesas").val();
                sillas = $("#msCantidadSillas").val();
                numeracion = $("#msTipoNumeracion").data("kendoDropDownList").value();
                numeracionType = $("#msTipoNumeracion").data("kendoDropDownList").text();
                break;
            case 4:
                filas = 1;//$("#PalcomsCantidadMesas").val();
                sillas = $("#PalcomsCantidadSillas").val();
                numeracion = $("#PalcomsTipoNumeracion").data("kendoDropDownList").value();
                numeracionType = $("#PalcomsTipoNumeracion").data("kendoDropDownList").text();
                break;
        }

        localidad.Nombre = $("#Nombre").val();
        localidad.Aforo = Creator.GetAforo(tipoLocalidad);
        localidad.Filas = filas;
        localidad.Sillas = sillas;
        localidad.IdTipoLocalidad = tipoLocalidad;
        localidad.TicketNumeroInicio = $("#NumeroTicketInicial").val();
        localidad.TicketNumeroFinal = $("#NumeroTicketFinal").val();
        localidad.RangoTickets = document.getElementById("checkShowRange").checked;
        localidad.IdTipoNumeracion = numeracion;
        localidad.Numeracion = numeracionType;
        localidad.OrientacionFilas = orientacionFilas;
        localidad.OrientacionSillas = orientacionSillas;
        localidad.Consumo = $("#DescripcionConsumo").val();
        localidad.ValorConsumo = $("#ValorConsumo").val();
        localidad.Tipo = stageType;
        localidad.AforoAcumulado = 0;

        return localidad;
    },
    GetRandomColor: function () {
        return '#' + '0123456789abcdef'.split('').map(function (v, i, a) {
            return i > 5 ? null : a[Math.floor(Math.random() * 16)]
        }).join('');
    },
    GetStageTypeName: function (tipo) {
        switch (tipo) {
            case Creator.StageType.Aforo:
                return 'Aforo';
            case Creator.StageType.FilasSillas:
                return 'Filas y Sillas';
            case Creator.StageType.MesasSillas:
                return 'Mesas y Sillas';
            case Creator.StageType.Palco:
                return 'Palco';
        }
    },
    GetStageTypeId: function (tipo) {
        switch (tipo) {
            case Creator.StageType.Aforo:
                return 1;
            case Creator.StageType.FilasSillas:
                return 2;
            case Creator.StageType.MesasSillas:
                return 3;
            case Creator.StageType.Palco:
                return 4;
        }
    },
    GetStageType: function (tipo) {
        switch (tipo) {
            case 1:
                return Creator.StageType.Aforo;
            case 2:
                return Creator.StageType.FilasSillas;
            case 3:
                return Creator.StageType.MesasSillas;
            case 4:
                return Creator.StageType.Palco;
        }
    },
    HideSeatsRemoved: function () {
        let restar = 0;
        let obj = Creator.CurrentStage.getObjects();
        if (obj.length === 0) {
            return;
        }

        const id = $("#apply-changes-stage").data("entity");
        const localidad = Creator.Stages.filter(e => e.Id == id)[0];

        obj.map(e => {
            if (e.objRemoved) {
                restar += 1;
                e.setVisible(false);

                const item = localidad.Asientos.filter(o => o.Token == e.id)[0]
                if(item != null) {
                    item.Visible = false;
                }
            }
        });
        
        if (localidad != null) {
            localidad.Aforo -= restar;
        }
        //Creator.AforoAcmulado = Creator.AforoAcmulado + Creator.ControlAforo - restar;
        Creator.ControlAforo = 0;
        $("#acumulatedValue-label").html(Creator.AforoAcmulado());
        $("#aforoFull-label").html(Creator.AforoTotal);
        $("#control-aforo").removeClass("form-invalid");
    },

    LoadCanvas: function () {
        let canvas = document.getElementById("canvas-preview");
        canvas.width = $("#map-container").width();
        canvas.height = $("#map-container").height();
        canvasW = canvas.width;
        canvasH = canvas.height;

        _canvas = Creator.canvas = new fabric.Canvas('canvas-preview');
        CanvasConfig();
    },
    LoadControlAforo: function () {
        Creator.AforoTotal = EventStorage.getProp("AforoTotal");
        $("#acumulatedValue-label").html(Creator.AforoAcmulado());
        $("#aforoFull-label").html(Creator.AforoTotal);
    },
    LoadConfigStages: function () {
        _canvas.getObjects().forEach(x => {
            const obj = Creator.Stages.filter(e => e.token == x.Token)[0];
            if (obj) {
                x.id = obj.Id;
            }
            if (x.stageType != Creator.StageType.Aforo) {
                x.hasControls = false;
            }
            Creator.ConfigStageByType(x, x.stageType);
        })
    },
    ConfigStageByType: function (obj, type, newId) {

        newId = newId || obj.id;

        obj.on("mouseup", Creator.selectGrupedObject);
        obj.on('deselected', function () { Creator.UnselectObjects(obj) });

        obj.on('selected', function (options) {
            Creator.zoomToPos = { x: options.e.x, y: options.e.y };
            Creator.CurrentStage = obj;
            $("#apply-changes-stage").data("tipo", type);
            $("#apply-changes-stage").data("entity", newId);
            $("#btnEditStage").data("stageID", ('STAGE-' + newId));
            Creator.currentStageName = ('STAGE-' + newId);

            switch (type) {
                case Creator.StageType.Aforo: {
                    $("#btnEditSeats").hide();
                    Creator.editing = true;
                    Creator.ShowSelectActionType();
                    $(".controls-stage").hide();
                }
                    break;
                case Creator.StageType.FilasSillas: {
                    $("#btnEditSeats").show();
                    //$("#apply-changes-stage").data("tipo", Creator.StageType.FilasSillas);
                    //$("#btnEditStage").data("entity", newId);                    
                    $("#menu-canvas-option-edit").click();
                    $(".controls-stage").show();
                    Creator.UpdateControls();
                }
                    break;
                case Creator.StageType.MesasSillas: {
                    $("#btnEditSeats").show();
                    Creator.CurrentStage = obj;
                    $("#menu-canvas-option-edit").click();
                    $(".controls-stage").hide();
                }
                    break;
                case Creator.StageType.Palco: {
                    $("#btnEditSeats").show();
                    Creator.CurrentStage = obj;
                    $("#menu-canvas-option-edit").click();
                    $(".controls-stage").hide();
                    $(".controls-stage.rotate").show();
                }
                    break;
            }

        });
    },
    LoadEventTitle: function () {
        let eventTitle = EventStorage.getProp("Nombre")
        $("#event-title").text(eventTitle);
    },
    LoadFilasSillasStage: function (object) {

        const { Asientos, Id, Nombre } = Creator.Stages.find(e => e.Token === object.token);

        let rows = 0,
           seats = 0,
           stageNameY = 0,
           stageNameX = 0,
           label = '',
           rowName = '',
           objects = [],
            rowLength = rows * 20,
            columnLength = (seats + 1) * 20,
            order = 0;
        

        const rowsLabels = _Array.distinct(Asientos.map(function (e) {
            return e.GroupName
        }));

        for (var i = 0, y = 0; i < rowsLabels.length; i++, y += 20) {
            
            order = 0;
            const rowItems = Asientos.filter(e => e.GroupName === rowsLabels[i]),
                seatsCount = rowItems.length + 2;

            for (var j = 0, x = 0; j < seatsCount; j++, x += 20) {

                if (x == 0 || x == (seatsCount - 1) * 20) {
                    rowName = rowsLabels[i];
                    objects.push(
                        new fabric.Text(rowName, {
                            fontFamily: 'Arial',
                            fontSize: 12,
                            left: x + 3,
                            top: y + 3,
                            fill: Creator.Colors.ColorText,
                            tipo: 'LABEL',
                            objGroup: ('STAGE-' + Id),
                        })
                    );
                    stageNameX = x;
                    continue;
                }

                const chair = rowItems[j-1];

                label = chair.Label;
                order++
                
                const silla = new fabric.Group(
                    [
                        new fabric.Circle({
                            radius: 9,
                            fill: Creator.Colors.backColorStage,
                            originX: 'center',
                            originY: 'center',
                            stroke: Creator.Colors.strokeColorStage,
                        }),
                        new fabric.Text(label, {
                            fontFamily: 'Arial',
                            fontSize: 12,
                            originX: 'center',
                            originY: 'center',
                            fill: Creator.Colors.ColorText,
                        })
                    ], {
                        left: x,
                        top: y,
                        tipo: 'SEAT',
                        rowLabel: rowName,
                        objLabel: label,
                        Order: order,
                        objRemoved: !chair.Visible,
                        visible: chair.Visible,
                        id: chair.Token,
                        objGroup: ('STAGE-' + Id)
                    });

                if(!chair.Visible){
                    Creator.ChangeProperties(silla);
                }
                objects.push(silla);
            }

            rowName = '';
            stageNameY = y + 20;

        }

        stageNameX = stageNameX / 2 - 7 * Nombre.length / 2;

        objects.push(new fabric.Text(Nombre, {
            fontFamily: 'Arial',
            fontSize: 12,
            left: stageNameX,
            top: stageNameY,
            fill: Creator.Colors.ColorText,
            tipo: 'NAME',
            objGroup: ('STAGE-' + Id)
        }));

        var group = new fabric.Group(objects, {
            left: object.left,
            top: object.top,
            hasControls: false,
            originX: 'center',
            originY: 'center',
            id: ('STAGE-' + Id),
            stageType: Creator.StageType.FilasSillas,
            token: object.token,
            seatsCount: object.seatsCount,
        });
        group.on("mouseup", Creator.selectGrupedObject);
        group.on('deselected', function () { Creator.UnselectObjects(group) });
        group.on('selected', function (options) {
            Creator.zoomToPos = { x: options.e.x, y: options.e.y };
            $("#btnEditSeats").show();
            Creator.CurrentStage = group;
            Creator.currentStageName = ('STAGE-' + Id);
            $("#apply-changes-stage").data("tipo", Creator.StageType.FilasSillas);
            $("#apply-changes-stage").data("entity", Id);
            $("#btnEditStage").data("stageID", ('STAGE-' + Id));
            $("#menu-canvas-option-edit").click();
            $(".controls-stage").show();
            Creator.UpdateControls();
        });
        _canvas.add(group);

        return true;
    },
    LoadMesasSillasStage(object){
        
        const { Asientos, Id, Nombre } = Creator.Stages.find(e => e.Token === object.token);

        const chairCount = Asientos.length;

        let margin = 0,//0
            left = object.left,
            top = object.top,
            _Y = 0,
            tableColor = object.tableColor

        const stageName = ('STAGE-' + Id);
        

        //--------------------------------------------------------------------------------


        let labelTable = Nombre,
            order = 1;

        var tableSize = 30 + 30 * (chairCount / 10);
        var chairSize = 9;
        var chairScale = 45 + 45 * (chairCount / 15);
        var chairsSpacing = 360 / chairCount;
        var tableAndChairs = [];

        var table = new fabric.Group(
            [
                new fabric.Circle({
                    radius: tableSize,
                    fill: Creator.Colors.backGroundColor,
                    stroke: tableColor,
                    border: 5,
                    originX: 'center',
                    originY: 'center',
                }),
                new fabric.Text(labelTable, {
                    fontFamily: 'Arial',
                    fontSize: 12,
                    originX: 'center',
                    originY: 'center',
                    fill: Creator.Colors.ColorText,
                })
            ], {
                left: left + chairSize + margin,
                top: top + chairSize + margin,
                objectType: "table",
            });

        let label = '';

        for (var i = 0; i < chairCount; i++) {
            const asiento = Asientos[i];

            label = asiento.Label;
            order++          
            
            const silla = new fabric.Group(
                [
                    new fabric.Circle({
                        radius: chairSize,
                        fill: Creator.Colors.backColorStage,
                        originX: 'center',
                        originY: 'center',
                        stroke: Creator.Colors.strokeColorStage,
                    }),
                    new fabric.Text(label + '', {
                        fontFamily: 'Arial',
                        fontSize: 12,
                        originX: 'center',
                        originY: 'center',
                        fill: Creator.Colors.ColorText
                    })
                ], {
                    left:
                        left +
                        Math.sin(Creator.deg2rad(chairsSpacing * i)) * chairScale +
                        tableSize +
                        margin,
                    top:
                        top +
                        Math.cos(Creator.deg2rad(chairsSpacing * i)) * chairScale +
                        tableSize +
                        margin,
                    tipo: 'SEAT',
                    rowLabel: labelTable,
                    objLabel: label,
                    objRemoved: !asiento.Visible,
                    visible: asiento.Visible,
                    id: asiento.Token,
                    objGroup: stageName,
                    objectType: "chair",
                    Order: order,
                });

            if(!asiento.Visible){
                Creator.ChangeProperties(silla);
            }
            tableAndChairs.push(silla);
        }

        tableAndChairs.push(table);

        const tableObject = new fabric.Group(tableAndChairs, {
            hasControls: false,
            id: ('STAGE-' + Id),
            tableColor: tableColor,
            seatsCount: chairCount,
            stageType: Creator.StageType.MesasSillas,
            token: object.token,
            rowLabel: labelTable,
        });

        //--------------------------------------------------------------------------------

        tableObject.on("mouseup", Creator.selectGrupedObject);
        tableObject.on('deselected', function () { Creator.UnselectObjects(tableObject) });
        tableObject.on('selected', function (options) {
            Creator.zoomToPos = { x: options.e.x, y: options.e.y };
            $("#btnEditSeats").show();
            Creator.CurrentStage = tableObject;
            Creator.currentStageName = stageName;
            $("#apply-changes-stage").data("tipo", Creator.StageType.MesasSillas);
            $("#apply-changes-stage").data("entity", Id);
            $("#btnEditStage").data("stageID", ('STAGE-' + Id));
            $("#menu-canvas-option-edit").click();
            $(".controls-stage").hide();
        });

        _canvas.add(tableObject);
    },
    LoadAforoStage(object){
        const { Aforo, Id, Nombre } = Creator.Stages.find(e => e.Token === object.token);

        var group = new fabric.Group([new fabric.Rect({
            originX: 'center',
            originY: 'center',
            fill: Creator.Colors.backColorStage,
            width: 220,
            height: 150,
            stroke: Creator.Colors.strokeColorStage,
        }), new fabric.Text(`${Nombre}\nCapacidad: ${Aforo}`, {
            fontFamily: 'Arial',
            fontSize: 14,
            originX: 'center',
            originY: 'center',
            fill: '#000',
            textAlign: 'center'
        })], {
            left: object.left,
            top: object.top,
            width: 220,
            height: 150,
            hasRotatingPoint: false,
            id: ('STAGE-' + Id),
            stageType: Creator.StageType.Aforo,
            token: object.token,
        });

        Creator.RemoveSizers(group);

        group.on('selected', function (options) {
            Creator.zoomToPos = { x: options.e.x, y: options.e.y };
            $("#btnEditSeats").hide();
            Creator.editing = true;
            Creator.CurrentStage = group;
            $("#apply-changes-stage").data("tipo", Creator.StageType.Aforo);
            $("#apply-changes-stage").data("entity", Id);
            Creator.ShowSelectActionType();
            Creator.currentStageName = ('STAGE-' + Id);
            $(".controls-stage").hide();
        });

        _canvas.add(group);

        $("#modal-typeStages").modal("hide");
        Creator.ShowSelectStageType('show');
        return true;

    },
    LoadPalcoStage(object){

        const { Asientos, Id, Nombre } = Creator.Stages.find(e => e.Token === object.token),
            color = object.tableColor,
            margin = 2,
            stageName = ('STAGE-' + Id);

        let _Y = 50,
            left = object.left,
            top = object.top,
            seatCounts = Asientos.length;

        //-----------------------------------------------------------------------------           

        const chairSize = 9,
            rowsCount = 2,
            seatsByBox = [],
            spacing = 20;

        let divisor = seatCounts / 2;

        if (seatCounts % 2 != 0) {
            divisor += 0.5;
        }

        let width = spacing * 3 + (margin * 2),
            heigth = (spacing + margin) * (divisor) + margin,
            contador = 1,
            label = '',
            _x = margin,
            _y = margin, 
            order = 1,
            index = 0;

        seatsByBox.push(new fabric.Rect({
            left: 0,
            top: 0,
            fill: Creator.Colors.backGroundColor,
            width: width,
            height: heigth,
            stroke: object.tableColor,
        }));

        for (var k = 0; k < rowsCount; k++) {
            _y = margin;
            for (var i = 0; i < divisor; i++) {

                const silla = Asientos[index];
                if (silla == null) {
                    break;
                }

                label = silla.Label;
                if (contador > seatCounts) {
                    break;
                }

                const puesto = new fabric.Group(
                        [
                            new fabric.Circle({
                                radius: chairSize,
                                fill: Creator.Colors.backColorStage,
                                originX: 'center',
                                originY: 'center',
                                stroke: Creator.Colors.strokeColorStage,
                            }),
                            new fabric.Text(label, {
                                fontFamily: 'Arial',
                                fontSize: 12,
                                originX: 'center',
                                originY: 'center',
                                fill: Creator.Colors.ColorText
                            })
                        ], {
                            left: _x,
                            top: _y,
                            tipo: 'SEAT',
                            rowLabel: Nombre,
                            objLabel: label,
                            objRemoved: !silla.Visible,
                            visible: silla.Visible,
                            id: silla.Token,
                            objGroup: Nombre,
                            objectType: "chair",
                            Order: order,
                        });
                if(!silla.Visible){
                    Creator.ChangeProperties(puesto);
                }
                seatsByBox.push(puesto);

                order++;
                contador += 1;
                _y += spacing + margin;
                index++;
            }
            _x += spacing * 2;

        }

        const xName = width / 2 - Nombre.length / 2,
            yName = heigth + margin;

        seatsByBox.push(new fabric.Text(Nombre, {
            fontFamily: 'Arial',
            fontSize: 12,
            left: xName,
            top: yName,
            fill: Creator.Colors.ColorText,
            tipo: 'NAME',
            objGroup: ('STAGE-' + Id),
        }));

        const box = new fabric.Group(seatsByBox, {
            hasControls: false,
            left: left,
            top: top,
            tableColor: object.tableColor,
            seatsCount: seatCounts,
            originX: 'center',
            originY: 'center',
            id: ('STAGE-' + Id),
            stageType: Creator.StageType.Palco,
            rowLabel: Nombre,
            token: object.token
        });



        //-----------------------------------------------------------------------------
        

        box.on("mouseup", Creator.selectGrupedObject);
        box.on('deselected', function () { Creator.UnselectObjects(box) });
        box.on('selected', function (options) {
            Creator.zoomToPos = { x: options.e.x, y: options.e.y };
            $("#btnEditSeats").show();
            Creator.currentStageName = stageName;
            Creator.CurrentStage = box;
            $("#apply-changes-stage").data("tipo", Creator.StageType.Palco);
            $("#apply-changes-stage").data("entity", Id);
            $("#btnEditStage").data("stageID", ('STAGE-' + Id));
            $("#menu-canvas-option-edit").click();
            $(".controls-stage").hide();
            $(".controls-stage.rotate").show();
        });

        _canvas.add(box);
    },
    LoadStages: function () {
        Creator.Stages = EventStorage.getProp("Localidades");
        Creator.Stages.forEach(e => {
            e.Tipo = Creator.GetStageType(e.IdTipoLocalidad)
        });

        const jsonStages = MapStorage.get();
        if ([null,''].indexOf(jsonStages) > -1) {
            return;
        }
        jsonStages.forEach((obj) => {
            switch (obj.stageType) {
                case Creator.StageType.Aforo:
                    Creator.LoadAforoStage(obj);
                    break;
                case Creator.StageType.FilasSillas:
                    Creator.LoadFilasSillasStage(obj);
                    break;
                case Creator.StageType.MesasSillas:
                    Creator.LoadMesasSillasStage(obj);
                    break;
                case Creator.StageType.Palco:
                    Creator.LoadPalcoStage(obj);
                    break;
            }
        });


        //if (Creator.Stages.length === 0) {
        //Creator.OpenModalTypeStage();
        //}
    },
    LockObject: function (element, state) {
        element.lockMovementX = state;
        element.lockMovementY = state;
        element.lockScalingX = state;
        element.lockScalingY = state;
        element.lockRotation = state;
    },
    ModifySelectedSeats: function () {
        var activeObjects = Creator.currentObjects;

        if (activeObjects.length === 0 && !Creator.SeatRestored) {
            alert('Seleccione al menos un asiento');
            return;
        }

        if (activeObjects.length > 1) {
            Creator.ReOrderSelectedSeat();
        } else {
            const seatLabel = $("#form-seat-label").val();
            if (seatLabel != '') {
                if (activeObjects.length > 0) {
                    activeObjects[0].getObjects()[1].setText(seatLabel);
                    activeObjects[0].objLabel = seatLabel;
                }
            }
        }
        Creator.AcceptChangesSeats();
        $("#form-seat-tipoNumeracion").data("kendoDropDownList").value(0);
        $("#form-seat-orientacion").data("kendoDropDownList").value(0);
    },
    onChangeSeatNumeration: function (e) {
        const isEmpty = $("#form-seat-startNumber").val() == '',
            type = this.value();
        let value = parseInt($("#form-seat-startNumber").val()),
            letter = $("#form-seat-startNumber").val();

        if (isNaN(value) && type !== Creator.numerationType.letters) {
            value = _Enumerations._09[letter];
        }

        switch (this.value()) {
            case Creator.numerationType.pair:
                value = !isEmpty ? isNaN(value) ? _Enumerations._09[value] : value : '2';
                value = value % 2 != 0 ? value += 1 : value;
                break;
            case Creator.numerationType.odd:
                value = !isEmpty ? isNaN(value) ? _Enumerations._09[value] : value : '1';
                value = value % 2 == 0 ? value -= 1 : value;
                break;
            case Creator.numerationType.numeric:
                value = !isEmpty ? isNaN(value) ? _Enumerations._09[value] : value : '1';
                break;
            case Creator.numerationType.letters:
                value = !isEmpty ? !isNaN(value) ? _Enumerations.AZ[value] : value : 'A';
                break;
        }
        $("#form-seat-startNumber").val(value);
        $("#form-seat-label").val(value);
    },
    OpenModalTypeStage: function () {
        $("#modal-typeStages").modal({
            backdrop: 'static',
            keyboard: false
        });
    },
    ChangeProperties: function (group) {
        group.getObjects()[0].setFill("#efefef");
        group.getObjects()[0].setStroke("#676767");
        group.getObjects()[1].setColor("#676767");
        group.objRemoved = true;
        group.numerator = Creator.numerator;
        Creator.removedSeats.push(group);
        //group.setVisible(false);
    },
    RemoveSelectedSeats: function () {
        var items = Creator.currentObjects;
        if (items.length > 0) {
            for (var i = 0; i < items.length; i++) {
                Creator.ChangeProperties(items[i]);
            }
        }
        _canvas.renderAll();
        //Creator.currentObjects = [];
        //Creator.AcceptChangesSeats();
    },
    RemoveSizers: function (group) {
        const controls = ['ml', 'mb', 'mr', 'mt'];

        for (var i = 0; i < controls.length; i++) {
            group.setControlVisible(controls[i], false);
        }
    },
    ReOrderSelectedSeat: function () {
        const type = $("#form-seat-tipoNumeracion").data("kendoDropDownList").value(),
            numeracionType = $("#form-seat-orientacion").data("kendoDropDownList").value();

        if (type == "" && numeracionType == "") {
            return;
        }

        items = Creator.currentObjects;
        if (items == null) {
            return;
        }

        let initialValue = $("#form-seat-startNumber").val(),
            acum = 0,
            aumneto = 1,
            text = '',
            isLetter = type == Creator.numerationType.letters,
            isPair = type == Creator.numerationType.pair;

        if (initialValue == '') {
            initialValue = items[0].objLabel;
            const aux = parseInt(initialValue);
            if (isNaN(aux)) {
                initialValue = _Enumerations._09[initialValue]
            }
            $("#form-seat-startNumber").val(initialValue)
        }

        if (isLetter && !isNaN(initialValue)) {
            initialValue = _Enumerations.AZ[initialValue]
            $("#form-seat-startNumber").val(initialValue)
            initialValue = _Enumerations._09[initialValue];
        } else if (isLetter) {
            initialValue = _Enumerations._09[initialValue.toUpperCase()];
        }

        if (!isLetter && isNaN(initialValue)) {
            initialValue = _Enumerations._09[initialValue];
        }

        switch (type) {
            case Creator.numerationType.pair:
                if (initialValue % 2 != 0) {
                    initialValue += 1;
                }
                aumneto = 2;
                break;
            case Creator.numerationType.odd:
                if (initialValue % 2 == 0) {
                    initialValue -= 1;
                }
                aumneto = 2;
                break;
            case Creator.numerationType.numeric:
                break;
            case Creator.numerationType.letters:
                if (!isNaN(initialValue)) {
                    $("#form-seat-startNumber").val(_Enumerations.AZ[initialValue]);
                }
                break;
        }

        initialValue = parseInt(initialValue);

        const rows = _Array.distinct(items.map(function (e) {
            return e.rowLabel
        })),
            auxInitialValue = initialValue,
            isLeft = numeracionType === 'left',
            iterator = (isLeft, length) => (isLeft ? i >= 0 : i < length);

        let length = 0;

        for (var j = 0; j < rows.length; j++) {

            const itemsByRow = items.filter(function (e) {
                return e.rowLabel === rows[j];
            });

            itemsByRow.sort(function (a, b) {
                var _a = a.Order;
                var _b = b.Order;
                if (_a == _b) {
                    return _a < _b ? -1 : _a > _b ? 1 : 0;
                } else {
                    return _a < _b ? -1 : 1;
                }
            });

            initialValue = auxInitialValue;
            length = itemsByRow.length;

            for (var i = (isLeft ? length - 1 : 0) ; iterator(isLeft, length) ; isLeft ? i-- : i++) {

                const item = itemsByRow[i];

                text = isLetter ? _Enumerations.AZ[initialValue] : (initialValue + '');

                item.getObjects()[1].setText(text);
                //item.getObjects()[0].set('fill', 'red')
                //item(1).setText(text);
                item.objLabel = text;

                initialValue += aumneto;
            }
        }
    },

    RestoreRemovedSeats: function () {
        for (var i = 0; i < Creator.removedSeats.length; i++) {
            Creator.removedSeats[i].getObjects()[0].setFill(Creator.Colors.backColorStage);
            Creator.removedSeats[i].getObjects()[0].setStroke(Creator.Colors.strokeColorStage);
            Creator.removedSeats[i].getObjects()[1].setColor(Creator.Colors.ColorText);
            Creator.removedSeats[i].objRemoved = false;
        }

        const restar = Creator.removedSeats.length;
        const id = $("#apply-changes-stage").data("entity");
        const localidad = Creator.Stages.filter(e => e.Id == id)[0];
        if (localidad != null) {
            localidad.Aforo += restar;
        }
        Creator.SeatRestored = true;
        $("#acumulatedValue-label").html(Creator.AforoAcmulado());
        $("#aforoFull-label").html(Creator.AforoTotal);

        Creator.removedSeats = [];
        _canvas.renderAll();
    },
    RestarAforo: function (tipo) {
        tipo = Creator.GetStageTypeId(tipo);
        if (Creator.CurrentStage.seatsCount != null) {
            Creator.ControlAforo = Creator.CurrentStage.seatsCount;
        } else {
            Creator.ControlAforo = Creator.GetAforo(tipo);
        }
        //Creator.AforoAcmulado -= Creator.ControlAforo;
    },
    RestoreZoom: function () {
        if (Creator.CurrentStage == null) {
            return;
        }

        _canvas.zoomToPoint(Creator.zoomToPos, Creator.lastZoom);
        Creator.lastZoom = 1;
    },

    NewStage: function (noRedirect) {
        Creator.CurrentStage = null;
        Creator.ControlAforo = 0;
        Creator.editing = false;
        Creator.currentStageName = '';
        Creator.CleanModalStage();
        $("#menu-container-pre li").removeClass("active");
        if (noRedirect == null) {
            Creator.ShowSelectStageType();
        }
        Creator.DisableAllObjects(true);
        Creator.RemoveEditLayer();
        $("#apply-changes-stage").data("entity", "");
        //$(".div-right .btn-link").click();
        //Creator.CleanModalStage();
        //Creator.OpenModalTypeStage();
    },

    selectGrupedObject: function selectGrupedObject(options) {
        if (!Creator.editingSeat) {
            return;
        }

        if (options.target._objects[1].objGroup !== Creator.currentStageName) {
            return;
        }

        var mousePos = _canvas.getPointer(options.e);
        //var mousePos = _canvas.getAbsoluteCenterPoint(options.e);
        if ((_lastMousePos.x != mousePos.x || _lastMousePos.y != mousePos.y)) {
            return;
        }
        if (!options.target || !options.target.isType("group")) return;
        options.target.forEachObject(function (object, i) {
            if (object.containsInGroupPoint) {
                if (object.containsInGroupPoint(mousePos)) {
                    const contains = Creator.currentObjects.filter(e => e.id === object.id).length > 0;
                    if (object.tipo === 'SEAT' && object.isType("group") && !object.objRemoved && !contains) {
                        object.getObjects()[0].setFill(Creator.Colors.backGroundColor);
                        object.getObjects()[0].setStroke(Creator.Colors.selectedStrokeColor);
                        object.getObjects()[1].setColor(Creator.Colors.selectedStrokeColor);
                        Creator.currentObjects.push(object);
                    }
                    _canvas.renderAll();
                }
            }
        });

        const counter = Creator.currentObjects.length;
        if (counter > 0) {
            Creator.ShowFormEditSeats(counter == 1);
            Creator.setSeatInfo();
        }
    },
    setSeatInfo: function () {
        let obj = Creator.CurrentStage.getObjects();
        if (obj.length === 0) {
            return;
        }
        obj.map(e => {
            if (e.objRemoved) {
                e.setVisible(true);
                Creator.removedSeats.push(e);
            }
        })
        $("#form-seat-label").val(obj[0].objLabel);
        $("#form-row-label").val(obj[0].rowLabel);
        $("#form-seat-startNumber").val(obj[0].objLabel);
        _canvas.renderAll();
    },
    SetStage: function (id) {
        Creator.CleanModalStage();

        const stage = Creator.Stages.find(function (e) { return e.Id === id });

        if (stage.RangoTickets) {
            $(".section-range").show()
            document.getElementById("checkShowRange").checked = true;
            $("#NumeroTicketInicial").val(stage.TicketNumeroInicio);
            $("#NumeroTicketFinal").val(stage.TicketNumeroFinal);
        }

        $("#Nombre").val(stage.Nombre);
        $("#ValorConsumo").val(stage.ValorConsumo);
        $("#DescripcionConsumo").val(stage.Consumo);

        switch (stage.IdTipoLocalidad) {
            case 1://Aforo
                $("#Capacidad").val(stage.Aforo);
                break;
            case 2://Filas Sillas
                $("#fmFilas").val(stage.Filas);
                $("#fmColumnas").val(stage.Sillas);
                $("#fmOrientacionFilas").data("kendoDropDownList").value(stage.OrientacionFilas);
                $("#fmOrientacionSillas").data("kendoDropDownList").value(stage.OrientacionSillas);
                $("#fmTipoNumeracion").data("kendoDropDownList").value(stage.IdTipoNumeracion);
                break;
            case 3://Mesas Sillas
                //$("#msCantidadMesas").val(stage.Filas);
                //$("#msCantidadSillas").val(stage.Sillas);
                $("#msCantidadSillas").val(stage.Aforo);
                $("#msTipoNumeracion").data("kendoDropDownList").value(stage.IdTipoNumeracion);
                break;
            case 4://Palco                
                //$("#PalcomsCantidadMesas").val(stage.Filas);
                //$("#PalcomsCantidadSillas").val(stage.Sillas);
                $("#PalcomsCantidadSillas").val(stage.Aforo);
                $("#PalcomsTipoNumeracion").data("kendoDropDownList").value(stage.IdTipoNumeracion);
                break;
        }
        Creator.ShowByType(stage.Tipo);
        //Step3.AforoAcumulado -= Creator.CurrentStage.Aforo;
        //EventStorage.setProp('AforoAcmulado', Step3.AforoAcumulado)
        //Step3.ShowTipoLocation();
        //Loading(false);
    },
    SetZoom: function (width) {
        var originWidth = _canvas.getWidth();

        var scale = width / _canvas.getWidth();
        height = scale * _canvas.getHeight();

        _canvas.setDimensions({
            "width": width,
            "height": height
        });

        _canvas.calcOffset();
        var objects = _canvas.getObjects();
        for (var i in objects) {
            var scaleX = objects[i].scaleX;
            var scaleY = objects[i].scaleY;
            var left = objects[i].left;
            var top = objects[i].top;

            objects[i].scaleX = scaleX * scale;
            objects[i].scaleY = scaleY * scale;
            objects[i].left = left * scale;
            objects[i].top = top * scale;

            objects[i].setCoords();
        }
        _canvas.renderAll();
    },
    ShowByType: function (type) {
        $(".stage-types").addClass('hide');
        $(".form-" + type).removeClass('hide');
        $("#save-stage").data("tipo", Creator.StageType[type]);
    },
    ShowFormEditSeats: function (type) {
        if (type) {
            $(".seat-label-content").show();
            $(".seat-numeracion-content").hide();
        } else {
            $(".seat-label-content").hide();
            $(".seat-numeracion-content").show();
        }
        $("#btnEditSeat").click();
    },

    ShowSelectStageType: function (visible) {
        $("#menu-container-pre li").removeClass("active");
        $('.nav-tabs[href="#canvas-menu1"]').tab('show');
    },
    ShowSelectActionType: function (visible) {
        $("#menu-container-pre li").removeClass("active");
        $('.nav-tabs a[href="#canvas-menu2"]').tab('show');
    },
    ShowForm: function (type) {
        Creator.CleanFormSeats();
        Creator.CleanModalStage();
        Creator.ShowByType(type);
        $('.nav-tabs[href="#form-edit-stage"]').tab('show');
    },

    TestStageByType: function (type) {
        setTimeout(function () {
            $("#Nombre").val("Nombre Localidad");

            $("#fmFilas").val('5');
            $("#fmColumnas").val('15');
            $("#fmTipoNumeracion").data("kendoDropDownList").value('2');
            $("#fmTipoNumeracion").data("kendoDropDownList").text('0-1, A-Z');
            $("#fmOrientacionFilas").data("kendoDropDownList").value('Up-Down');
            $("#fmOrientacionSillas").data("kendoDropDownList").value('R-L');

            $("#ValorConsumo").val("20000");
            $("#DescripcionConsumo").val("descripciòn del consumo");

            document.getElementById("checkShowRange").checked = true;
            $("#NumeroTicketInicial").val("1");
            $("#NumeroTicketFinal").val("100");


            let newId = _Array.min(Creator.Stages.map(function (e) { return e.Id })) - 1;
            Creator.Stages.push(Creator.GetLocalidadData(newId, Creator.StageType.Aforo));
            console.log("newId", newId);

            Creator.NewFilasSillas();
        }, 1000);
    },
    UpdateAforo: function () {
        $("#acumulatedValue-label").html(Creator.AforoAcmulado());
        $("#aforoFull-label").html(Creator.AforoTotal);
        $("#control-aforo").removeClass("form-invalid");
        return true;
    },
    UpdateControls: function () {
        if (Creator.CurrentStage != null) {
            document.getElementById("angle-control").value = Creator.CurrentStage.angle;
            document.getElementById("skewX-control").value = Creator.CurrentStage.skewX;
            document.getElementById("skewY-control").value = Creator.CurrentStage.skewY;
        }
    },
    UnselectObjects: function unselectObjects(options) {
        Creator.currentObjects = [];
        (options.target || options).forEachObject(function (object, i) {
            if (object.tipo === 'SEAT' && object.isType("group") && !object.objRemoved) {
                object.getObjects()[0].setFill(Creator.Colors.backColorStage);
                object.getObjects()[0].setStroke(Creator.Colors.strokeColorStage);
                object.getObjects()[1].setColor(Creator.Colors.strokeColorStage);
            }
        });
        _canvas.renderAll();
    },

    UnGroupSelectObject: function () {
        Creator.lastZoom = _canvas.getZoom();

        // const _x = Creator.zoomToPos.x,
        //     _y = Creator.zoomToPos.x;

        _canvas.zoomToPoint(Creator.zoomToPos, 2);

        // _canvas.viewportTransform[4] = Creator.zoomToPos.x;
        // _canvas.viewportTransform[5] = Creator.zoomToPos.y;
        _canvas.renderAll();


        Creator.editing = true;
    },
    ValidateAforo: function (type) {
        const tipo = Creator.GetStageTypeId(type),
            id = $("#apply-changes-stage").data("entity");
        const currentAforo = Creator.GetAforo(tipo);

        //Creator.AforoAcmulado = _arra Creator.Stages.map(e=>e.Aforo);

        if (Creator.Stages.length > 0) {
            const localidad = Creator.Stages.filter(e => e.Id == id)[0];
            const aforoResidual = Creator.AforoAcmulado() - (localidad || { Aforo: 0 }).Aforo;

            if ((aforoResidual + currentAforo) > Creator.AforoTotal) {
                return false;
            }
        } else if (currentAforo > Creator.AforoTotal) {
            return false;
        }

        return true;
    },

    ValidateFields: function (type) {
        if (!Validate.fields("form-stage-name")) {
            return false;
        }
        if (!Validate.fields("form-" + type)) {
            return false;
        }
        if (!Creator.ValidateAforo(type)) {
            $("#control-aforo").addClass("form-invalid");
            alert('La suma del aforo acumulado mas el aforo de esta localidad superan el aforo total');
            return false;
        }
        return true;
    },


    //Aforo =>
    EditAforo: function (group) {
        console.log(group);
    },
    NewAforo: function (id) {

        if (!Creator.ValidateFields('Aforo')) {
            return false;
        }

        //let newId = _Array.min(Creator.Stages.map(function (e) { return e.Id })) - 1;
        //newId = newId > 0 ? -1 : newId;
        const stage = Creator.GetLocalidadData(id, Creator.StageType.Aforo);
        const token = guid();
        stage.Token = token;
        id = id || stage.Id;
        //Creator.Stages.push(stage);

        const name = $("#Nombre").val();
        const capacity = $("#Capacidad").val();


        var group = new fabric.Group([new fabric.Rect({
            originX: 'center',
            originY: 'center',
            fill: Creator.Colors.backColorStage,
            width: 220,
            height: 150,
            stroke: Creator.Colors.strokeColorStage,
        }), new fabric.Text(`${name}\nCapacidad: ${capacity}`, {
            fontFamily: 'Arial',
            fontSize: 14,
            originX: 'center',
            originY: 'center',
            fill: '#000',
            textAlign: 'center'
        })], {
            left: 50,
            top: 50,
            width: 220,
            height: 150,
            hasRotatingPoint: false,
            id: ('STAGE-' + id),
            stageType: Creator.StageType.Aforo,
            token: token,
        });

        Creator.RemoveSizers(group);

        group.on('selected', function (options) {
            Creator.zoomToPos = { x: options.e.x, y: options.e.y };
            $("#btnEditSeats").hide();
            Creator.editing = true;
            Creator.CurrentStage = group;
            $("#apply-changes-stage").data("tipo", Creator.StageType.Aforo);
            $("#apply-changes-stage").data("entity", id);
            Creator.ShowSelectActionType();
            Creator.currentStageName = ('STAGE-' + id);
            $(".controls-stage").hide();
        });

        _canvas.add(group);

        $("#modal-typeStages").modal("hide");
        Creator.ShowSelectStageType('show');
        return true;
    },

    //Sillas Mesas =>
    EditMesasSillas: function (id) {
        const y = Creator.CurrentStage.getTop(),
            x = Creator.CurrentStage.getLeft(),
            token = Creator.CurrentStage.token;
        //id = Creator.CurrentStage.id.replace('STAGE-', '');
        //$("#msCantidadMesas").val('1');
        Creator.SetZoom(Creator.CurrentZoom);
        const valid = Creator.NewMesasSillas(x, y, Creator.CurrentStage.tableColor, token, id);
        if (!valid) {
            return false;
        }
        _canvas.remove(Creator.CurrentStage);
        Creator.RestoreZoom();
        Creator.CurrentStage = null;
        return valid;
    },
    getNumerationMF: function (i, row) {
        const numeration = $("#msTipoNumeracion").data("kendoDropDownList").text();
        let value = 0;
        if (numeration != "Seleccione") {
            numerations = numeration.replace(' ', '').split(',');
            if (numerations[row] === 'A-Z') {
                return _Enumerations.AZ[i];
            }
        } else if (row === 0) {
            return _Enumerations.AZ[i];
        }
        return i + '';
    },
    NewMesasSillas: function (_left, _top, color, token, id) {
        if (!Creator.ValidateFields('MesasSillas')) {
            return false;
        }

        const nombre = $("#Nombre").val(),
            //tablesCount = parseInt($("#msCantidadMesas").val()),
            chairsCount = parseInt($("#msCantidadSillas").val()),
            asientos = [];

        let margin = 5,
            width = _left || 50,
            height = _top || 50,
            _Y = 0,
            tableColor = color || Creator.GetRandomColor();

        let localidad = Creator.GetLocalidadData(id, Creator.StageType.MesasSillas);
        id = localidad.Id;
        token = token || guid();

        //for (var i = 0; i < tablesCount; i++) {
        //    if (i % 5 === 0 && i !== 0) {
        //        height += _Y + margin;
        //        width = 50
        //    }

        const stageName = ('STAGE-' + id);
        const table = Creator.createTableGroup(asientos, width, height, chairsCount, id, nombre, tableColor, token, stageName);
        table.on("mouseup", Creator.selectGrupedObject);
        table.on('deselected', function () { Creator.UnselectObjects(table) });
        table.on('selected', function (options) {
            Creator.zoomToPos = { x: options.e.x, y: options.e.y };
            $("#btnEditSeats").show();
            Creator.CurrentStage = table;
            Creator.currentStageName = stageName;
            $("#apply-changes-stage").data("tipo", Creator.StageType.MesasSillas);
            $("#apply-changes-stage").data("entity", id);
            $("#btnEditStage").data("stageID", ('STAGE-' + id));
            $("#menu-canvas-option-edit").click();
            $(".controls-stage").hide();
        });
        //if (_left) {
        //    table.setLeft(_left);
        //    table.setTop(_top);                
        //}

        _canvas.add(table);
        width += table.getWidth() + margin;
        _Y = table.getHeight();

        //}

        localidad.Asientos = asientos;
        localidad.Token = token;
        //Creator.Stages.push(localidad);

        $("#modal-typeStages").modal("hide");

        _canvas.renderAll();
        return true;
    },
    createTableGroup: function createTableGroup(asientos, Left, Top, ChairCount, Id, LabelTable, TableColor/*, rowOrder*/, token, stageName) {
        let left = Left || 0,
            top = Top || 0,
            chairCount = ChairCount || 10,
            labelTable = LabelTable || '',
            tableColor = TableColor || _strokeColor,
            id = Id || 0,
            order = 1;

        var tableSize = 30 + 30 * (chairCount / 10);
        var chairSize = 9;
        var margin = 0;
        var chairScale = 45 + 45 * (chairCount / 15);
        var chairsSpacing = 360 / chairCount;
        var tableAndChairs = [];

        var table = new fabric.Group(
            [
                new fabric.Circle({
                    radius: tableSize,
                    fill: Creator.Colors.backGroundColor,
                    stroke: tableColor,
                    border: 5,
                    originX: 'center',
                    originY: 'center',
                }),
                new fabric.Text(labelTable, {
                    fontFamily: 'Arial',
                    fontSize: 12,
                    originX: 'center',
                    originY: 'center',
                    fill: Creator.Colors.ColorText,
                })
            ], {
                left: left + chairSize + margin,
                top: top + chairSize + margin,
                objectType: "table",
            });

        let label = '';

        for (var i = 0; i < chairCount; i++) {
            label = Creator.getNumerationMF(i + 1, 1);
            order++
            asientos.push({
                Id: _countSeats--,
                IdLocalidad: 0,
                GroupName: LabelTable,
                Token: ('SEAT-' + id + '-' + label),
                Order: order,
                Label: label,
                Visible: true
            });
            tableAndChairs.push(
                new fabric.Group(
                    [
                        new fabric.Circle({
                            radius: chairSize,
                            fill: Creator.Colors.backColorStage,
                            originX: 'center',
                            originY: 'center',
                            stroke: Creator.Colors.strokeColorStage,
                        }),
                        new fabric.Text(label + '', {
                            fontFamily: 'Arial',
                            fontSize: 12,
                            originX: 'center',
                            originY: 'center',
                            fill: Creator.Colors.ColorText
                        })
                    ], {
                        left:
                            left +
                            Math.sin(Creator.deg2rad(chairsSpacing * i)) * chairScale +
                            tableSize +
                            margin,
                        top:
                            top +
                            Math.cos(Creator.deg2rad(chairsSpacing * i)) * chairScale +
                            tableSize +
                            margin,
                        tipo: 'SEAT',
                        rowLabel: labelTable,
                        objLabel: label,
                        objRemoved: false,
                        id: ('SEAT-' + id + '-' + label),
                        objGroup: stageName,
                        objectType: "chair",
                        Order: order,
                    })
            );
        }
        tableAndChairs.push(table);
        return new fabric.Group(tableAndChairs, {
            hasControls: false,
            id: ('STAGE-' + id),
            tableColor: tableColor,
            seatsCount: chairCount,
            stageType: Creator.StageType.MesasSillas,
            token: token,
            rowLabel: labelTable,
        });
    },

    //Filas Sillas =>
    getNumeration: function (i, row) {
        const numeration = $("#fmTipoNumeracion").data("kendoDropDownList").text();
        let value = 0;
        if (numeration != "Seleccione") {
            numerations = numeration.replace(' ', '').split(',');
            if (numerations[row] === 'A-Z') {
                return _Enumerations.AZ[i];
            }
        } else if (row === 0) {
            return _Enumerations.AZ[i];
        }
        return i + '';
    },
    EditFilasSillas: function (id) {
        const y = Creator.CurrentStage.getTop(),
            x = Creator.CurrentStage.getLeft(),
            token = Creator.CurrentStage.token;
        Creator.SetZoom(Creator.CurrentZoom);
        const valid = Creator.NewFilasSillas(x, y, token, id);
        if (!valid) {
            return valid;
        }
        _canvas.remove(Creator.CurrentStage);
        Creator.RestoreZoom();
        Creator.CurrentStage = null;
        return valid;
    },
    NewFilasSillas: function (_X, _Y, token, id) {

        if (!Creator.ValidateFields('FilasSillas')) {
            return false;
        }

        //let newId = _Array.min(Creator.Stages.map(function (e) { return e.Id })) - 1;
        //newId = newId >= 0 ? -1 : newId;
        let localidad = Creator.GetLocalidadData(id, Creator.StageType.FilasSillas);
        const newId = localidad.Id;





        let rows = 0,
            seats = 0,
            stageNameY = 0,
            stageNameX = 0,
            label = '',
            rowName = '',
            isDownToTop = $("#fmOrientacionFilas").data("kendoDropDownList").value() == 'Down-Up',
            isRisghtToLeft = $("#fmOrientacionSillas").data("kendoDropDownList").value() == 'R-L',
            asientos = [];

        const name = $("#Nombre").val();
        rows = parseInt(document.getElementById('fmFilas').value);
        seats = parseInt(document.getElementById('fmColumnas').value);


        if (rows > 50) {
            document.getElementById('fmFilas').value = 50;
            alert('La cantidad de filas no puede ser mayor a 50');
            return;
        }

        if (seats > 100) {
            document.getElementById('fmColumnas').value = 100;
            alert('La cantidad de sillas no puede ser mayor a 100');
            return;
        }

        let objects = [],
            rowLength = rows * 20,
            columnLength = (seats + 1) * 20,
            order = 0;

        for (var i = (isDownToTop ? rows : 1), y = 0, rowOrder = 0; y < rows * 20; y += 20, (isDownToTop ? i-- : i++), rowOrder++) {
            order = 0;
            for (var j = (isRisghtToLeft ? seats + 1 : 0), x = 0; x <= columnLength; x += 20, (isRisghtToLeft ? j-- : j++)) {

                if (x == 0 || x == (seats + 1) * 20) {
                    rowName = Creator.getNumeration(i, 0);
                    objects.push(
                        new fabric.Text(rowName, {
                            fontFamily: 'Arial',
                            fontSize: 12,
                            left: x + 3,
                            top: y + 3,
                            fill: Creator.Colors.ColorText,
                            tipo: 'LABEL',
                            objGroup: ('STAGE-' + newId),
                        })
                    );
                    stageNameX = x;
                    continue;
                }

                label = Creator.getNumeration(j, 1);
                order++
                asientos.push({
                    Id: _countSeats--,
                    IdLocalidad: 0,
                    GroupName: rowName,
                    GroupOrder: rowOrder,
                    Order: order,
                    Label: label,
                    Token: ('SEAT-' + newId + '_' + rowName + '-' + label),
                    Visible: true
                });

                objects.push(
                    new fabric.Group(
                        [
                            new fabric.Circle({
                                radius: 9,
                                fill: Creator.Colors.backColorStage,
                                originX: 'center',
                                originY: 'center',
                                stroke: Creator.Colors.strokeColorStage,
                            }),
                            new fabric.Text(label, {
                                fontFamily: 'Arial',
                                fontSize: 12,
                                originX: 'center',
                                originY: 'center',
                                fill: Creator.Colors.ColorText,
                            })
                        ], {
                            left: x,
                            top: y,
                            tipo: 'SEAT',
                            rowLabel: rowName,
                            objLabel: label,
                            Order: order,
                            objRemoved: false,
                            id: ('SEAT-' + newId + '_' + rowName + '-' + label),
                            objGroup: ('STAGE-' + newId)
                        })
                );
            }
            rowName = '';
            stageNameY = y + 20;
        }

        stageNameX = stageNameX / 2 - 7 * name.length / 2;

        objects.push(new fabric.Text(name, {
            fontFamily: 'Arial',
            fontSize: 12,
            left: stageNameX,
            top: stageNameY,
            fill: Creator.Colors.ColorText,
            tipo: 'NAME',
            objGroup: ('STAGE-' + newId)
        }));

        token = token || guid();

        var group = new fabric.Group(objects, {
            left: _X || 50,
            top: _Y || 50,
            hasControls: false,
            originX: 'center',
            originY: 'center',
            id: ('STAGE-' + newId),
            stageType: Creator.StageType.FilasSillas,
            token: token,
            //curvePoint: Creator.CalculateCurvePoint(seats),
            seatsCount: seats,
        });

        group.on("mouseup", Creator.selectGrupedObject);
        group.on('deselected', function () { Creator.UnselectObjects(group) });
        group.on('selected', function (options) {
            Creator.zoomToPos = { x: options.e.x, y: options.e.y };
            $("#btnEditSeats").show();
            Creator.CurrentStage = group;
            Creator.currentStageName = ('STAGE-' + newId);
            $("#apply-changes-stage").data("tipo", Creator.StageType.FilasSillas);
            $("#apply-changes-stage").data("entity", newId);
            //$("#apply-changes-stage").data("tipo", Creator.StageType.FilasSillas);
            //$("#btnEditStage").data("entity", newId);
            $("#btnEditStage").data("stageID", ('STAGE-' + newId));
            $("#menu-canvas-option-edit").click();
            $(".controls-stage").show();
            Creator.UpdateControls();
        });

        _canvas.add(group);

        $("#modal-typeStages").modal("hide");

        localidad.Asientos = asientos;
        localidad.Token = token;
        //Creator.Stages.push(localidad);
        return true;
    },


    //Palcos =>
    EditBox: function (id) {
        const y = Creator.CurrentStage.getTop(),
            x = Creator.CurrentStage.getLeft(),
            token = Creator.CurrentStage.token;
        //$("#PalcomsCantidadMesas").val('1');
        Creator.SetZoom(Creator.CurrentZoom);
        const valid = Creator.NewBox(x, y, Creator.CurrentStage.tableColor, token, id);
        if (!valid) {
            return false;
        }
        _canvas.remove(Creator.CurrentStage);
        Creator.RestoreZoom();
        Creator.CurrentStage = null;
        return valid;
    },
    getNumerationBox: function (i, row) {
        const numeration = $("#PalcomsTipoNumeracion").data("kendoDropDownList").text();
        let value = 0;
        if (numeration != "Seleccione") {
            numerations = numeration.replace(' ', '').split(',');
            if (numerations[row] === 'A-Z') {
                return _Enumerations.AZ[i];
            }
        } else if (row === 0) {
            return _Enumerations.AZ[i];
        }
        return i + '';
    },
    CreateBox: function (asientos, rowOrder, seatCounts, left, top, id, boxLabel, boxColor, stageName, token) {
        left = left || 0;
        top = top || 0;
        id = id || 0;
        boxLabel = boxLabel || '';
        boxColor = boxColor || Creator.Colors.strokeColorStage;

        const margin = 2,
            chairSize = 9,
            rowsCount = 2,
            seatsByBox = [],
            spacing = 20;
        let divisor = seatCounts / 2;

        if (seatCounts % 2 != 0) {
            divisor += 0.5;
        }

        let width = spacing * 3 + (margin * 2),
            heigth = (spacing + margin) * (divisor) + margin,
            contador = 1,
            label = '',
            _x = margin,
            _y = margin, order = 1;

        seatsByBox.push(new fabric.Rect({
            left: 0,
            top: 0,
            fill: Creator.Colors.backGroundColor,
            width: width,
            height: heigth,
            stroke: boxColor,
        }));

        for (var k = 0; k < rowsCount; k++) {
            _y = margin;
            for (var i = 0; i < divisor; i++) {
                label = Creator.getNumerationBox(contador, 1);
                if (contador > seatCounts) {
                    break;
                }

                seatsByBox.push(
                    new fabric.Group(
                        [
                            new fabric.Circle({
                                radius: chairSize,
                                fill: Creator.Colors.backColorStage,
                                originX: 'center',
                                originY: 'center',
                                stroke: Creator.Colors.strokeColorStage,
                            }),
                            new fabric.Text(label, {
                                fontFamily: 'Arial',
                                fontSize: 12,
                                originX: 'center',
                                originY: 'center',
                                fill: Creator.Colors.ColorText
                            })
                        ], {
                            left: _x,
                            top: _y,
                            tipo: 'SEAT',
                            rowLabel: boxLabel,
                            objLabel: label,
                            objRemoved: false,
                            id: ('SEAT-' + id + '-' + label),
                            objGroup: stageName,
                            objectType: "chair",
                            Order: order,
                        })
                );
                asientos.push({
                    Id: _countSeats--,
                    IdLocalidad: 0,
                    GroupName: boxLabel,
                    GroupOrder: rowOrder,
                    Order: order,
                    Label: label,
                    Visible: true,
                    stageType: Creator.StageType.Palco,
                    Token: ('SEAT-' + id + '-' + label),
                });
                order++;
                contador += 1;
                _y += spacing + margin;
            }
            _x += spacing * 2;

        }

        const xName = width / 2 - boxLabel.length / 2,
            yName = heigth + margin;

        seatsByBox.push(new fabric.Text(boxLabel, {
            fontFamily: 'Arial',
            fontSize: 12,
            left: xName,
            top: yName,
            fill: Creator.Colors.ColorText,
            tipo: 'NAME',
            objGroup: ('STAGE-' + id),
        }));

        return new fabric.Group(seatsByBox, {
            hasControls: false,
            left: left,
            top: top,
            tableColor: boxColor,
            seatsCount: seatCounts,
            originX: 'center',
            originY: 'center',
            token: token,
            id: ('STAGE-' + id),
            stageType: Creator.StageType.Palco,
            rowLabel: boxLabel,
        });
    },
    NewBox: function (_left, _top, tableColor, token, id) {
        if (!Creator.ValidateFields('Palco')) {
            return false;
        }

        let newId,
            _Y = 50,
            left = _left || 50,
            top = _top || 50;

        const localidad = Creator.GetLocalidadData(id, Creator.StageType.Palco);
        newId = localidad.Id;

        const cols = parseInt($("#PalcomsCantidadSillas").val()),
            nombre = $("#Nombre").val();
        //rows = parseInt($("#PalcomsCantidadMesas").val()),
        numeracion = $("#PalcomsTipoNumeracion").data("kendoDropDownList").value(),
        color = tableColor || Creator.GetRandomColor(),
        margin = 5,
        asientos = [];

        token = token || guid();

        //for (var i = 0; i < rows; i++) {
        //    if (i % 3 === 0 && i !== 0) {
        //        top += _Y + margin;
        //        left = 50
        //    }
        const stageName = ('STAGE-' + newId);
        const box = Creator.CreateBox(asientos, 1, cols, left, top, newId, nombre/* || Creator.getNumerationBox(i + 1, 0)*/, color, stageName, token)
        box.on("mouseup", Creator.selectGrupedObject);
        box.on('deselected', function () { Creator.UnselectObjects(box) });
        box.on('selected', function (options) {
            Creator.zoomToPos = { x: options.e.x, y: options.e.y };
            $("#btnEditSeats").show();
            Creator.currentStageName = stageName;
            Creator.CurrentStage = box;
            $("#apply-changes-stage").data("tipo", Creator.StageType.Palco);
            $("#apply-changes-stage").data("entity", newId);
            $("#btnEditStage").data("stageID", ('STAGE-' + newId));
            $("#menu-canvas-option-edit").click();
            $(".controls-stage").hide();
            $(".controls-stage.rotate").show();
        });

        _canvas.add(box);

        left += box.getWidth() + margin;
        _Y = box.getHeight();
        //}

        localidad.Asientos = asientos;
        localidad.Token = token;
        //Creator.Stages.push(localidad);

        return true;
    },

}
$(document).ready(function () {
    Creator.Init();
});



function CanvasConfig() {
    _canvas.selection = false;
    _canvas.on('mouse:wheel', function (opt) {
        var e = opt.e;
        if (!e.ctrlKey) {
            return;
        }
        var newZoom = _canvas.getZoom() + e.deltaY / 300;
        if (parseFloat(newZoom.toFixed(2)) >= 0.3 && parseFloat(newZoom.toFixed(2)) <= 2.00) {
            _canvas.zoomToPoint({ x: e.offsetX, y: e.offsetY }, newZoom);
        }
        e.preventDefault();
        return false;
    });

    var viewportLeft = 0,
        viewportTop = 0,
        mouseLeft,
        mouseTop,
        _drawSelection = _canvas._drawSelection,
        isDown = false,
        canvasWidth = _canvas.getWidth(),
        canvasHeight = _canvas.getHeight();

    _canvas.on('mouse:down', function (options) {
        _lastMousePos = _canvas.getPointer(options.e);
        if (options.e.altKey) {
            isDown = true;

            viewportLeft = _canvas.viewportTransform[4];
            viewportTop = _canvas.viewportTransform[5];

            mouseLeft = options.e.x;
            mouseTop = options.e.y;
            _drawSelection = _canvas._drawSelection;
            _canvas._drawSelection = function () { };
        }
    });

    _canvas.on('selection:created', function (options) {
        //if (Creator.editing) {
        //    Creator.ShowFormEditSeats(false);
        //    Creator.setSeatInfo();
        //}
    });

    _canvas.on('mouse:move', function (options) {
        if (options.e.altKey && isDown) {
            var currentMouseLeft = options.e.x;
            var currentMouseTop = options.e.y;

            var deltaLeft = currentMouseLeft - mouseLeft,
                deltaTop = currentMouseTop - mouseTop;

            _canvas.viewportTransform[4] = viewportLeft + deltaLeft;
            _canvas.viewportTransform[5] = viewportTop + deltaTop;

            _canvas.renderAll();
        }
    });

    _canvas.on('mouse:up', function () {
        _canvas._drawSelection = _drawSelection;
        if (isDown) {
            Creator.SetZoom(_canvas.getWidth());
        }
        isDown = false;
    });

    _canvas.on('selection:cleared', function (options) {
        if (Creator.editing) {
            //if (Creator.clicked) {
            //    $("#btnEditSeat").click();
            //    Creator.clicked = false;
            //}
            //Creator.UnselectObjects()
            //$("#form-edit-seat .btn-link").click();
        }
    });

    fabric.util.object.extend(fabric.Object.prototype, {
        getAbsoluteCenterPoint: function () {
            var point = this.getCenterPoint();
            if (!this.group) return point;
            var groupPoint = this.group.getAbsoluteCenterPoint();
            var rotation = this.group.get("angle");
            var rotatedPoint = rotate(0, 0, point.x, point.y, rotation * -1);
            //_canvas.add(new fabric.Circle({radius: 1, fill: _backGroundColor, stroke: _strokeColor, left: (rotatedPoint.x * (this.group.getWidth()/this.group.width)) + groupPoint.x, top: (rotatedPoint.y * (this.group.getHeight()/this.group.height)) + groupPoint.y}));
            return {
                x:
                    rotatedPoint.x * (this.group.getWidth() / this.group.width) +
                    groupPoint.x,
                y:
                    rotatedPoint.y * (this.group.getHeight() / this.group.height) +
                    groupPoint.y
            };
        },
        containsInGroupPoint: function (point) {
            if (!this.group) return this.containsPoint(point);

            var center = this.getAbsoluteCenterPoint();

            var thisPos = {
                xStart:
                    center.x -
                    (this.width * (this.group.getWidth() / this.group.width)) / 2,
                xEnd:
                    center.x +
                    (this.width * (this.group.getWidth() / this.group.width)) / 2,
                yStart:
                    center.y -
                    (this.height * (this.group.getHeight() / this.group.height)) / 2,
                yEnd:
                    center.y +
                    (this.height * (this.group.getHeight() / this.group.height)) / 2
            };

            if (point.x >= thisPos.xStart && point.x <= thisPos.xEnd) {
                if (point.y >= thisPos.yStart && point.y <= thisPos.yEnd) {
                    return true;
                }
            }
            return false;
        }
    });

    function rotate(cx, cy, x, y, angle) {
        var radians = Creator.deg2rad(angle),
            cos = Math.cos(radians),
            sin = Math.sin(radians),
            nx = cos * (x - cx) + sin * (y - cy) + cx;
        ny = cos * (y - cy) - sin * (x - cx) + cy;
        return { x: nx, y: ny };
    }

    //function LoadImageBackground() {
    //    var background = new fabric.Rect({
    //        left: 0,
    //        top: 0,
    //        stroke: 'White',
    //        width: canvasWidth,
    //        height: canvasHeight,
    //        scaleX: 1,
    //        scaleY: 1,
    //        selectable: false,
    //        zoomScale: 1,
    //        isBackground: true
    //    });
    //    _canvas.add(background);
    //    fabric.util.loadImage('../../../Images/400px-Graph-paper.svg.png', function (img) {
    //        background.setPatternFill({
    //            source: img,
    //            repeat: 'repeat'
    //        });
    //        _canvas.renderAll();
    //    })
    //}
}

