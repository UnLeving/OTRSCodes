﻿function NetworkSelected(e) {
    document.getElementById("HexSaver").value = $(e).css('background-color');
    document.getElementById("NetworkIdSaver").value = e.id;
}

$("#ddlCountries").change(function () {
    $.ajax({
        url: "/Countries/Index",
        type: "GET",
        data: { id: $("#ddlCountries").val() }
    })
        .done(function (partialViewResult) {
            $("#CodesContent").html(partialViewResult);
            RetreiveNetworks();
        });
});

function RetreiveNetworks() {
    $.ajax({
        url: "/Networks/Index",
        type: "GET",
        data: { id: $("#ddlCountries").val() }
    })
        .done(function (partialViewResult) {
            $("#NetworkContent").html(partialViewResult);
        });
}

$('body').on('click', 'td', function () {
    $(this).css('background-color', document.getElementById("HexSaver").value);
    $.ajax({
        url: "/Countries/AddCode",
        type: "POST",
        data: {
            CountryId: $("#ddlCountries").val(),
            NetworkId: document.getElementById("NetworkIdSaver").value,
            ZoneId: document.getElementById("RValue").value,
            Value: $(this).text()
        }
    })
        //.done(function (status) {
        //    alert(status);
        //})
        ;
});

$(document).bind('keydown', function (e) {
    key = e.key;
    if ((key >= 0 && key <= 9) || (key === 'ArrowUp' || key === 'ArrowDown')) {
        var $n = $('#tb1').find('#RValue').text();
        if (key === 'ArrowUp') {
            key = $n * 10;
        }
        else if (key === 'ArrowDown') {
            key = $n / 10;
        }
        $.ajax({
            url: "/Countries/Index",
            type: "GET",
            data: {
                id: $("#ddlCountries").val(),
                zoneId: key
            }
        })
            .done(function (response) {
                UpdateTable(response);
            });
    }
});

function UpdateTable(response) {
    var $bodyContent1 = $(response).find('#tb1 tbody').children();
    var $bodyContent2 = $(response).find('#tb2 tbody').children();
    var $table1 = $('#tb1');
    var $table2 = $('#tb2');
    $table1.find('tbody').empty().append($bodyContent1);
    $table2.find('tbody').empty().append($bodyContent2);
}