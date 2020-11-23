var Validate = {
    isNullOrEMptyInt: function (value) {
        return value === '' || value == null ? 0 : value;
    },
    fields: function (formName) {
        let valid = true;
        $('#' + formName + ' .form-control.required').removeClass('required-field');
        $('#' + formName + ' .form-control.required.required-field').removeClass('required-field')
        //$('#' + formName + ' .form-control.required').not('span').each(function (i, v) {
        //    const $v = $(v);
        //    if ($v.val() === '' || $v.val() === '0') {
        //        $v.addClass('required-field');
        //        valid = false;
        //    }
        //})
        $('#' + formName + ' .form-control.required').not('span').each(function (i, v) {
            const $v = $(v);
            if ($v.val() === '' || $v.val() === '0') {
                if ($v[0].parentElement.localName == 'span') {
                    $($v[0].parentElement).addClass('required-field');
                }
                $v.addClass('required-field');
                valid = false;
            }
        })
        return valid;
    }
}

var guid = function guid() {
    function s4() {
        return Math.floor((1 + Math.random()) * 0x10000)
          .toString(16)
          .substring(1);
    }
    return s4() + s4() + '-' + s4() + '-' + s4() + '-' + s4() + '-' + s4() + s4() + s4();
};

var _Array = {
    min: function arrayMin(arr) {
        if (arr.length === 0) {
            return 0;
        }
        return arr.reduce(function (p, v) {
            return (p < v ? p : v);
        });
    },
    max: function arrayMax(arr) {
        if (arr.length === 0) {
            return 0;
        }
        return arr.reduce(function (p, v) {
            return (p > v ? p : v);
        });
    },
    sum: function (arr) {
        if (arr.length === 0) {
            return 0;
        }
        return arr.reduce(function (a, b) { return a + b }, 0);
    },
    distinct: function (arr) {
        var aux = [];
        for (var i = 0; i < arr.length; i++) {
            if (!aux.includes(arr[i])) {
                aux.push(arr[i]);
            }
        }
        return aux;
    }
}

var Getters = {
    dropDown: function (dropdownId) {
        const dropdown = $("#" + dropdownId).data("kendoDropDownList");
        if (dropdown) {
            return dropdown.value();
        }
        return 0;
    }
}

var Message = {
    show: function ShowMessage(Message) {
        GlobalMessage.Show(Message.ResponseType, Message.Message);
    },
    confirm: function ShowModalConfirm(message) {
        var modalConfirm = $('#modalConfirmGeneric');
        var btnAgreeEdit = $('#btnAgreeEditDynamic');
        var btnDisagreeEdit = $('#btnDisagreeEditDynamic');

        modalConfirm.find('h5').text(message.Title);
        modalConfirm.find('p').text(message.Message);

        modalConfirm.modal({ backdrop: 'static', keyboard: false });

        btnAgreeEdit.off('click.Agree');
        btnAgreeEdit.on('click.Agree', function () {
            var agree = message.Agree;
            if (agree) {
                agree.Event(agree.Parameters);
            }
            modalConfirm.modal('hide');
        });

        btnDisagreeEdit.off('click.Disagree');
        btnDisagreeEdit.on('click.Disagree', function () {
            btnAgreeEdit.off('click.Agree');
            var disgree = message.Disagree;
            if (disgree) {
                disgree.Event(disgree.Parameters);
            }
            modalConfirm.modal('hide');
        });
    },
}

var Request = {
    Generate: function (url, data, type, successCallBack) {
        $.ajax({
            url: url,
            contentType: false,
            processData: false,
            type: type,
            cache: false,
            async: true,
            data: data,
            success: successCallBack,
            error: function (err) {
                console.log('Error', err);
            }
        });
    }
}

var _Date = {
    format: "YYYY-MM-DD HH:mm",
    validateRangeArray: function (_start, _end, id, obj) {
        if (_start == '' || _end == '') {
            alert("La fecha de inicio y final son obligatorias");
            return false;
        }

        const start = moment(_start, _Date.format),
                end = moment(_end, _Date.format),
                newObj = obj.filter(function (e) { return e.id !== id }),
                now = moment(new Date(), _Date.format);
        let valid = true;

        newObj.forEach(function (v, i) {
            const inicio = moment(v.start, _Date.format),
                fin = moment(v.end, _Date.format);
                
            if (start.isBetween(inicio, fin, null, '[]') || end.isBetween(inicio, fin, null, '[]')) {
                alert('Las fechas no se pueden interseptar');
                valid = false;
            }

            if (inicio.isBetween(start, end, null, '[]') || fin.isBetween(start, end, null, '[]')) {
                alert('Las fechas no se pueden interseptar');
                valid = false;
            }
        });

        if (start.isAfter(end)) {
            alert("La fecha de inicio no puede ser mayor a la final");
            valid = false;
        }

        if (now.isAfter(start) || now.isAfter(end)) {
            alert('La fecha de inicio o fnal no puedes ser menores a la fecha actual')
            valid = false;
        }
        
        return valid;
    }
}

_Enumerations = {
    type: {
        AZAZ:1,
        _01AZ:2,
        AZ01:3,
        _0101: 4
    },
    AZ: {
        1: 'A',
        2: 'B',
        3: 'C',
        4: 'D',
        5: 'E',
        6: 'F',
        7: 'G',
        8: 'H',
        9: 'I',
        10: 'J',
        11: 'K',
        12: 'L',
        13: 'M',
        14: 'N',
        15: 'O',
        16: 'P',
        17: 'Q',
        18: 'R',
        19: 'S',
        20: 'T',
        21: 'U',
        22: 'V',
        23: 'W',
        24: 'X',
        25: 'Y',
        26: 'Z',
        27: 'AA',
        28: 'AB',
        29: 'AC',
        30: 'AD',
        31: 'AE',
        32: 'AF',
        33: 'AG',
        34: 'AH',
        35: 'AI',
        36: 'AJ',
        37: 'AK',
        38: 'AL',
        39: 'AM',
        40: 'AN',
        41: 'AO',
        42: 'AP',
        43: 'AQ',
        44: 'AR',
        45: 'AS',
        46: 'AT',
        47: 'AU',
        48: 'AV',
        49: 'AW',
        50: 'AX',
        51: 'AY',
        52: 'AZ',
        53: 'BA',
        54: 'BB',
        55: 'BC',
        56: 'BD',
        57: 'BE',
        58: 'BF',
        59: 'BG',
        60: 'BH',
        61: 'BI',
        62: 'BJ',
        63: 'BK',
        64: 'BL',
        65: 'BM',
        66: 'BN',
        67: 'BO',
        68: 'BP',
        69: 'BQ',
        70: 'BR',
        71: 'BS',
        72: 'BT',
        73: 'BU',
        74: 'BV',
        75: 'BW',
        76: 'BX',
        77: 'BY',
        78: 'BZ',
        79: 'CA',
        80: 'CB',
        81: 'CC',
        82: 'CD',
        83: 'CE',
        84: 'CF',
        85: 'CG',
        86: 'CH',
        87: 'CI',
        88: 'CJ',
        89: 'CK',
        90: 'CL',
        91: 'CM',
        92: 'CN',
        93: 'CO',
        94: 'CP',
        95: 'CQ',
        96: 'CR',
        97: 'CS',
        98: 'CT',
        99: 'CU',
        100: 'CV',
        101: 'CW',
        102: 'CX',
        103: 'CY',
        104: 'CZ',
    },
    _09: {
        'A': 1, 
        'B': 2, 
        'C': 3, 
        'D': 4, 
        'E': 5, 
        'F': 6, 
        'G': 7, 
        'H': 8, 
        'I': 9, 
        'J': 10,
        'K': 11,
        'L': 12,
        'M': 13,
        'N': 14,
        'O': 15,
        'P': 16,
        'Q': 17,
        'R': 18,
        'S': 19,
        'T': 20,
        'U': 21,
        'V': 22,
        'W': 23,
        'X': 24,
        'Y': 25,
        'Z': 26,
        'AA': 27,
        'AB': 28,
        'AC': 29,
        'AD': 30,
        'AE': 31,
        'AF': 32,
        'AG': 33,
        'AH': 34,
        'AI': 35,
        'AJ': 36,
        'AK': 37,
        'AL': 38,
        'AM': 39,
        'AN': 40,
        'AO': 41,
        'AP': 42,
        'AQ': 43,
        'AR': 44,
        'AS': 45,
        'AT': 46,
        'AU': 47,
        'AV': 48,
        'AW': 49,
        'AX': 50,
        'AY': 51,
        'AZ': 52,
        'BA': 53,
        'BB': 54,
        'BC': 55,
        'BD': 56,
        'BE': 57,
        'BF': 58,
        'BG': 59,
        'BH': 60,
        'BI': 61,
        'BJ': 62,
        'BK': 63,
        'BL': 64,
        'BM': 65,
        'BN': 66,
        'BO': 67,
        'BP': 68,
        'BQ': 69,
        'BR': 70,
        'BS': 71,
        'BT': 72,
        'BU': 73,
        'BV': 74,
        'BW': 75,
        'BX': 76,
        'BY': 77,
        'BZ': 78,
        'CA': 79,
        'CB': 80,
        'CC': 81,
        'CD': 82,
        'CE': 83,
        'CF': 84,
        'CG': 85,
        'CH': 86,
        'CI': 87,
        'CJ': 88,
        'CK': 89,
        'CL': 90,
        'CM': 91,
        'CN': 92,
        'CO': 93,
        'CP': 94,
        'CQ': 95,
        'CR': 96,
        'CS': 97,
        'CT': 98,
        'CU': 99,
        'CV': 100,
        'CW': 101,
        'CX': 102,
        'CY': 103,
        'CZ': 104,
    }
};