
var JsAlertSuccess = function (mensagem, div, setTimeOut, callback) {

    var divAlert = '#' + div;
    var divMessage = divAlert + '-message';

    $(divAlert).removeClass().addClass("server-response-success");
    var msg = '<i class="fa fa-check"></i>&nbsp;&nbsp;' + mensagem;
    $(divMessage).html(msg);
    $(divAlert).show("normal");

    if (setTimeOut) {
        setTimeout(function () {
            $(divAlert).hide("normal");
            callback();
        }, 2000);
    }

}


var JsAlertWarning = function (mensagem, div, setTimeOut, callback) {

    var divAlert = '#' + div;
    var divMessage = divAlert + '-message';

    $(divAlert).removeClass().addClass("server-response-warning");
    var msg = '<i class="fa fa-triangle-exclamation"></i>&nbsp;&nbsp;' + mensagem;
    $(divMessage).html(msg);
    $(divAlert).show("normal");

    if (setTimeOut) {
        setTimeout(function () {
            $(divAlert).hide("normal");
            callback();
        }, 2000);
    }
}

var JsAlertError = function (mensagem, div, setTimeOut, callback) {

    var divAlert = '#' + div;
    var divMessage = divAlert + '-message';

    $(divAlert).removeClass().addClass("server-response-error");
    var msg = '<i class="fa fa-ban"></i>&nbsp;&nbsp;' + mensagem;
    $(divMessage).html(msg);
    $(divAlert).show("normal");

    if (setTimeOut) {
        setTimeout(function () {
            $(divAlert).hide("normal");
            callback();
        }, 2000);
    }

}

$("#server-response-close").click(function () {
    $("#server-response").hide("fast");
});