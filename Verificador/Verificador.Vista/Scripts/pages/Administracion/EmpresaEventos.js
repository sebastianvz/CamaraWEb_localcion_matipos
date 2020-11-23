if ($(document).ready) {

    var divDigito = document.querySelector("#divDigito");
    var tipoIdenticacion = 0;

    //Mostrar Datos del grid en el popout
    function showDetalle(e) {
        var grid = $("#grid").data("kendoGrid");
        gridActual = grid;
        var mostrar = true;
        if (e.model.Digito == null) {
            mostrar = false;
            ShowDiv(mostrar)
        } else {
            ShowDiv(mostrar)
        }
        e.container.data("kendoWindow").bind("close", function () {
            gridActual.dataSource.read();
        })
    }

    //Mostrar el div del dígito de verificación
    function ShowDiv(mostrar) {
        if (mostrar) {
            document.querySelector("#divDigito").style.display = "block";
        } else {
            document.querySelector("#divDigito").style.display = "none";
        }
    }

    //Obtner NIT y mostrar div de dígito
    function ChangeEmpresa(e) {
        tipoIdenticacion = e.sender.selectedIndex;
        if (tipoIdenticacion == 2) {
            ShowDiv(true);
        } else {
            ShowDiv(false);
            $("#Digito").val("");
            $("#Digito").trigger("change")

        }
    }

    //Calcular dígito de verificación
    function AgregarDigito() {
        if (tipoIdenticacion == 2) {
            var digito = document.querySelector("#Digito").value;
            if (digito != "") {
                digito = "";
            }
            var vpri,
                x,
                y,
                z;
            var myNit = document.querySelector("#Identificacion").value;
            // Se limpia el Nit
            myNit = myNit.replace(/\s/g, ""); // Espacios
            myNit = myNit.replace(/,/g, ""); // Comas
            myNit = myNit.replace(/\./g, ""); // Puntos
            myNit = myNit.replace(/-/g, ""); // Guiones

            // Se valida el nit
            if (isNaN(myNit)) {
                console.log("El nit/cédula '" + myNit + "' no es válido(a).");
                return "";
            };
            // Procedimiento
            vpri = new Array(16);
            z = myNit.length;
            vpri[1] = 3;
            vpri[2] = 7;
            vpri[3] = 13;
            vpri[4] = 17;
            vpri[5] = 19;
            vpri[6] = 23;
            vpri[7] = 29;
            vpri[8] = 37;
            vpri[9] = 41;
            vpri[10] = 43;
            vpri[11] = 47;
            vpri[12] = 53;
            vpri[13] = 59;
            vpri[14] = 67;
            vpri[15] = 71;
            x = 0;
            y = 0;
            for (var i = 0; i < z; i++) {
                y = (myNit.substr(i, 1));
                // console.log ( y + "x" + vpri[z-i] + ":" ) ;

                x += (y * vpri[z - i]);
                // console.log ( x ) ;
            }
            y = x % 11;
            if (y > 1) {
                digito = 11 - y;
            } else {
                digito = y;
            }
            $("#Digito").val(digito);
            $("#Digito").trigger("change")
        }
    }
}
