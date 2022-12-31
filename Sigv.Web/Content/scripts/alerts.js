﻿
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
    var msg = '<i class="fa fa-xmark"></i>&nbsp;&nbsp;' + mensagem;
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

var loadingButton = function (button, text) {

    button.attr('disabled', 'disabled');

    var str = '';
    str += '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> ' + text;

    button.html(str);

};

var loading = function (div, text) {

    var str = '';
    str += '<span class="spinner-border spinner-border-sm" role="status" aria-hidden="true"></span> ' + text;
    div.html(str);

};

var recoveryButton = function (button, text) {

    button.removeAttr('disabled', 'disabled');
    button.html(text);

};