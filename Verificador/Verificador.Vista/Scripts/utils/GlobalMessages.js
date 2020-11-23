var TypesMessage = {
    Success: "Success",
    Error: "Error",
    Warning: "Warning",
    Informative: "Informative",
};

var GlobalMessage = {
    BorderRadius: "0px 0px 0px 0px",
    Border: "0px",
    ChangeColor: false,
    Background: "#000",
    TestColor: "#FFF",
    Message: "",
    Time: 1500,
    TimeHidden: 6000,
    ChangeSize: false,
    Height: "80px",
    width: "300px",
    ChangeImg: false,
    img: "",
    Type: TypesMessage.Success, //Success, Error and warning
    Show: function (sType, sMessage) {
        GlobalMessage.Type = sType;
        GlobalMessage.Message = sMessage;

        var backGround;
        var Color;
        var icon;

        if (!GlobalMessage.ChangeColor) {
            if (GlobalMessage.Type == TypesMessage.Success) {
                backGround = "#05A679";
                Color = "#FFF";
            }
            else if (GlobalMessage.Type == TypesMessage.Error) {
                backGround = "#C90B33";
                Color = "#FFF";
            }
            else if (GlobalMessage.Type == TypesMessage.Warning || GlobalMessage.Type == TypesMessage.Informative) {
                backGround = "#F6B604";
                Color = "#FFF";
            }
            //else if (GlobalMessage.Type == TypesMessage.Informative) {
            //    backGround = "#F6B604";
            //    Color = "#FFF";
            //}
        }
        else {
            backGround = GlobalMessage.Background;
            Color = GlobalMessage.TestColor;
        }

        if (!GlobalMessage.ChangeImg) {
            if (GlobalMessage.Type == TypesMessage.Success) {
                icon = '<img src="' + UrlsGlobalMessage.Success + '" style="width:50px; margin-right: 10px;" />';
            }
            else if (GlobalMessage.Type == TypesMessage.Error) {
                icon = '<img src="' + UrlsGlobalMessage.Error + '" style="width:50px; margin-right: 10px;" />';
            }
            else if (GlobalMessage.Type == TypesMessage.Warning) {
                icon = '<img src="' + UrlsGlobalMessage.Warning + '" style="width:50px; margin-right: 10px;" />';
            }
            else if (GlobalMessage.Type == TypesMessage.Informative) {
                icon = '<img src="' + UrlsGlobalMessage.Informative + '" style="width:50px; margin-right: 10px;" />';
            }
        }
        else {
            icon = '<img src="' + UrlsGlobalMessage.CustomImage + img + '" style="width:50px; margin-right: 10px;" />';
        }

        var element = $("#" + GlobalMessage.Create(icon));

        // Styles to the message box
        if (GlobalMessage.ChangeSize) {
            element.css("min-height", GlobalMessage.Height);
            element.css("min-width", GlobalMessage.width);
        }

        element.css("border-radius", GlobalMessage.BorderRadius);
        element.css("-webkit-border-radius", GlobalMessage.BorderRadius);
        element.css("-moz-border-radius", GlobalMessage.BorderRadius);
        element.css("background", backGround);
        element.css("border", "solid " + GlobalMessage.Border + " " + Color);

        // Styles to the contents of the message box
        element.find(".lblMessage").css("color", Color);
        element.find(".lblMessage").html(GlobalMessage.Message);
        element.fadeIn(GlobalMessage.Time);

        setTimeout(function () { GlobalMessage.Hidden(element); }, GlobalMessage.TimeHidden);
    },
    Create: function (icon) {
        var elements = ($("#GlobalMessage").find("div")).length;
        var id = "SonMessage-" + elements;

        while ($("#" + id).length) {
            elements = elements + 1;
            id = "SonMessage-" + elements;
        }

        $("#GlobalMessage").append("<div id='" + id + "' class='SonMessage'></div>");
        $("#" + id).append("<table><tr><td>" + icon + "</td><td><label style='padding-right: 40px' class='lblMessage'></label></td></tr><tr>" +
            "<button class='btn' id=" + id + ">X</button></tr></table>");

        $('#' + id).off('click');
        $('#' + id).on('click', function () {
            $("#" + id).remove();
        });

        return id;
    },
    Hidden: function (element) {
        element.remove();
    }
};