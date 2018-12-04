﻿function NetworkSelected(e) {
    document.getElementById("HexSaver").value = $(e).css('background-color');
    document.getElementById("NetworkIdSaver").value = e.id;
}

$("#ddlCountries").change(function () {
    var countryId = $("#ddlCountries").val();
    document.getElementById("CountryIdSaver").value = countryId;

    $.ajax({
        url: "/Countries/Index",
        type: "GET",
        data: { id: countryId }
    })
        .done(function (partialViewResult) {
            $("#CodesContent").html(partialViewResult);
            RetreiveCodes(countryId);
        });
});

function RetreiveCodes(countryId) {
    $.ajax({
        url: "/Networks/Index",
        type: "GET",
        data: {
            id: countryId,
            zoneId: document.getElementById("RValue").value
        }
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
            CountryId: document.getElementById("CountryIdSaver").value,
            NetworkId: document.getElementById("NetworkIdSaver").value,
            Zone: document.getElementById("RValue").value,
            Code: $(this).text()
        }
    })
        .done(function (status) {
            alert(status);
        });
});

$(document).bind('keydown', function (e) {
    key = e.key;
    if ((key >= 0 && key <= 9) || (key === 'ArrowUp' || key === 'ArrowDown')) {
        var $n = $('#tb1').find('#RValue').text();
        if (key === 'ArrowUp') {
            key = $n * 10;
        }
        else if (key === 'ArrowDown')
        {
            key = $n/10;
        }
        $.ajax({
            url: "/Countries/Index",
            type: "GET",
            data: { num: key }
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