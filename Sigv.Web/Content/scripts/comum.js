

var popularCombustiveis = function (selecionado) {

    $.ajax({
        url: "/Veiculo/ListarCombustiveis",
        method: "get",
        success: function (data) {

            var option = '<option value="0">--- Selecione ---</option>'

            for (var i = 0; i < data.length; i++) {

                var selected = data[i].CombustivelId == selecionado ? "selected" : "";

                option += '<option ' + selected + ' value="' + data[i].CombustivelId + '">' + data[i].Nome + '</option>'
            }

            $("#txCombustivelId").html(option);

        }
    });

}

var popularCondicoes = function (selecionado) {

    $.ajax({
        url: "/Veiculo/ListarCondicoes",
        method: "get",
        success: function (data) {

            var option = '<option value="0">--- Selecione ---</option>'

            for (var i = 0; i < data.length; i++) {

                var selected = data[i].CondicaoId == selecionado ? "selected" : "";

                option += '<option ' + selected + ' value="' + data[i].CondicaoId + '">' + data[i].Nome + '</option>'
            }

            $("#txCondicaoId").html(option);

        }
    });

}

var popularEspecies = function (selecionado) {

    $.ajax({
        url: "/Veiculo/ListarEspecies",
        method: "get",
        success: function (data) {

            var option = '<option value="0">--- Selecione ---</option>'

            for (var i = 0; i < data.length; i++) {

                var selected = data[i].EspecieId == selecionado ? "selected" : "";

                option += '<option ' + selected + ' value="' + data[i].EspecieId + '">' + data[i].Nome + '</option>'
            }

            $("#txEspecieId").html(option);

        }
    });

}

var popularStatus = function (selecionado) {

    $.ajax({
        url: "/Veiculo/ListarStatus",
        method: "get",
        success: function (data) {

            var option = '<option value="0">--- Selecione ---</option>'

            for (var i = 0; i < data.length; i++) {

                var selected = data[i].StatusId == selecionado ? "selected" : "";

                option += '<option ' + selected + ' value="' + data[i].StatusId + '">' + data[i].Nome + '</option>'
            }

            $("#txStatusId").html(option);

        }
    });

}