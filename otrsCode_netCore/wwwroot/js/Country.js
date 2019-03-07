$("#ddlCountries").change(function () {
    $.ajax({
        url: "/Countries/CodesTable",
        type: "GET",
        data: { countryId: $("#ddlCountries").val() },
        success: function (partialViewResult) {
            $("#CodesContent").html(partialViewResult);
            RetreiveNetworks();
            UpdateList();
        },
        error: function (status) {
            document.getElementById("Logs").value = status.statusText;
        }
    });
});

function RegionChanged(e) {
    $('#loader').show();
    $.ajax({
        url: "/Countries/CodesTable",
        type: "GET",
        data: {
            countryId: $("#ddlCountries").val(),
            R: e
        },
        success: function (response) {
            UpdateTable(response);
            UpdateList();
            $('#loader').hide();
        },
        error: function (status) {
            document.getElementById("Logs").value = status.statusText;
            $('#loader').hide();
        }
    });
}

// Fill tables
function UpdateTable(response) {
    var $bodyContent1 = $(response).find('#tb1 tbody').children();
    var $bodyContent2 = $(response).find('#tb2 tbody').children();
    var $table1 = $('#tb1');
    var $table2 = $('#tb2');
    $table1.find('tbody').empty().append($bodyContent1);
    $table2.find('tbody').empty().append($bodyContent2);
}

function UpdateList() {
    $.ajax({
        url: "/Countries/CodesList",
        type: "GET",
        data: {
            countryId: $("#ddlCountries").val()
        },
        success: function (response) {
            $("#CodesList").html(response);
        }
    });
}

function ddlCountries_changed(id) {
    $.ajax({
        url: "/Networks/NetworkDropDown",
        type: "GET",
        data: {
            countryId: id
        },
        success: function (response) {
            $("#NetworkDD").html(response);
        }
    });
}