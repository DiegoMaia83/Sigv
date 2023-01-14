
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

function returnColorPassword(password) {

    var color = "";

    if (password <= 30) {
        color = "#DB5644";
    }
    else if (password <= 60) {
        color = "#FAC146";
    }
    else if (password <= 75) {
        color = "#F7E945";
    }
    else {
        color = "#2FC44F";
    }

    return color;

}

function returnMsgPassword(password) {

    var color = "";

    if (password <= 30) {
        color = "Muito Fraca";
    }
    else if (password <= 60) {
        color = "Fraca";
    }
    else if (password <= 75) {
        color = "Média";
    }
    else {
        color = "Ótima";
    }

    return color;

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

    if (resultPassComplexity <= 60) {
        JsDivError("A nova senha é muito fraca! Utilize números, letras maiúsculas, letras minúsculas e caracteres especiais.", "server-response-div", true, function () {

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

var formataPlaca = function (input) {

    // Obtém o elemento de input
    const plateInput = document.getElementById(input);

    // Adiciona um ouvinte de eventos input ao elemento de input
    plateInput.addEventListener("input", function (event) {
        // Obtém o valor atual do input
        let currentValue = event.target.value.replace(/[^a-zA-Z0-9]/g, '').toUpperCase();
        let currentLength = currentValue.length;
        let newValue = '';
        // formata o valor
        if (currentLength > 3 && currentValue.indexOf("-") === -1) {
            newValue = currentValue.slice(0, 3) + '-' + currentValue.slice(3);
        } else if (currentValue.indexOf("-") !== -1 && currentLength < 4) {
            newValue = currentValue.replace(/-/g, '');
        } else {
            newValue = currentValue;
        }
        // Atualiza o valor do input com o valor formatado
        event.target.value = newValue;
    });
}