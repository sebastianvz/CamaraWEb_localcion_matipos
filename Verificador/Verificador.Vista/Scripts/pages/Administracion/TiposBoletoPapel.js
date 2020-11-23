if ($(document).ready) {

    function showDetalle(e)
    {
        var divEdit = $("div[class='k-edit-form-container']");

        var labels = $("div[class='editor-label']");
        var fields = $("div[class='editor-field']");

        labels[0].style.display = "none";
        fields[0].style.display = "none";

        $("#AnchoCmm").kendoNumericTextBox({
            decimals: 2
        });

        $("#AltoCmm").kendoNumericTextBox({
            decimals: 2
        });
    }
}