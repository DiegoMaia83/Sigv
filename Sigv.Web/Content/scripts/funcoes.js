
function checkPasswordComplexity(password) {
    var score = 0;
    if (!password)
        return score;

    // award every unique letter until 5 repetitions
    var letters = new Object();
    for (var i = 0; i < password.length; i++) {
        letters[password[i]] = (letters[password[i]] || 0) + 1;
        score += 5.0 / letters[password[i]];
    }

    // bonus points for mixing it up
    var variations = {
        digits: /\d/.test(password),
        lower: /[a-z]/.test(password),
        upper: /[A-Z]/.test(password),
        nonWords: /\W/.test(password),
    }

    variationCount = 0;
    for (var check in variations) {
        variationCount += (variations[check] == true) ? 1 : 0;
    }
    score += (variationCount - 1) * 10;

    return parseInt(score);
}


var alterarSenha = function (senhaAtual, senhaNova) {

    var data = {
        senhaAtual: senhaAtual,
        senhaNova: senhaNova
    };

    var resultPassComplexity = checkPasswordComplexity(data.senhaNova);

    if (data.senhaAtual == "" || data.senhaAtual == undefined) {
        JsDivError("Necessário informar a senha atual!", "server-response-div", true, function () {

        });
        return false;
    }

    if (data.senhaNova == "" || data.senhaNova == undefined) {
        JsDivError("Necessário informar a nova senha!", "server-response-div", true, function () {

        });
        return false;
    }

    if (data.senhaNova == data.senhaAtual) {
        JsDivError("A nova senha não pode ser igual a senha atual!", "server-response-div", true, function () {

        });
        return false;
    }

    if (resultPassComplexity < 30) {
        JsDivError("Sua senha é muito simples! Tente novamente.", "server-response-div", true, function () {

        });
        return false;
    }
    else if (resultPassComplexity < 70) {
        JsDivError("Sua senha deve conter letras, números e caracteres especiais!", "server-response-div", true, function () {

        });
        return false;
    }

    $.ajax({
        url: "/Admin/AlterarSenha",
        method: "post",
        data: data,
        success: function (data) {

            if (data.Sucesso == false) {
                JsDivError(data.Mensagem, "server-response-div", true, function () {

                });
                return false;
            }

            JsDivSuccess(data.Mensagem, "server-response-div", true, function () {
                location.reload();
            });
            return false;
        },
        error: function (xhr, textStatus, error) {
            JsAlertError("Houve um erro ao processar a rotina! " + error, "server-response-div", false, function () {
                // Sem callback
            });
        }
    })

}