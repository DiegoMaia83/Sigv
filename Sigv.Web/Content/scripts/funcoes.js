
var callAlertSuccess = function (mensagem, callback) {
    var div = "#server-response";
    $(div).removeClass().addClass("server-response-success");
    var msg = '<i class="fa fa-check"></i>&nbsp;&nbsp;' + mensagem;
    $(div).html(msg).show("normal");
    setTimeout(function () {
        $(div).hide("normal");
        callback();
    }, 2000);
}

var callAlertWarning = function (mensagem, callback) {
    var div = "#server-response";
    $(div).removeClass().addClass("server-response-warning");
    var msg = '<i class="fa fa-triangle-exclamation"></i>&nbsp;&nbsp;' + mensagem;
    $(div).html(msg).show("normal");
    setTimeout(function () {
        $(div).hide("normal");
        callback();
    }, 2000);
}

var callAlertError = function (mensagem, callback) {
    var div = "#server-response";
    $(div).removeClass().addClass("server-response-error");
    var msg = '<i class="fa fa-xmark"></i>&nbsp;&nbsp;' + mensagem;
    $(div).html(msg).show("normal");
    setTimeout(function () {
        $(div).hide("normal");
        callback();
    }, 2000);
}