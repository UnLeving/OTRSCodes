function ddlCountries_changed(e) {
    $.ajax({
        url: "/Networks/NetworkDropDown",
        type: "GET",
        data: { id: e }
    })
        .done(function (partialViewResult) {
            $("#NetworkDD").html(partialViewResult);
        });
}

//function Generate_clicked() {
//    $.ajax({
//        url: "/Countries/GetRegExp",
//        type: "GET",
//        data: { id: $("#ddlNetworks").val() }
//    })
//        .done(function (ViewResult) {
//            $("#RegExp").val(ViewResult);
//        });
//}

$("#ddlCountries").change(function () {
    $.ajax({
        url: "/Countries/CodesTable",
        type: "GET",
        data: { id: $("#ddlCountries").val() }
    })
        .done(function (partialViewResult) {
            $("#CodesContent").html(partialViewResult);
            RetreiveNetworks();
        });
});

function RegionChanged(e) {
    $.ajax({
            url: "/Countries/CodesTable",
            type: "GET",
            data: {
                id: $("#ddlCountries").val(),
                zone: e
            }
        })
            .done(function (response) {
                UpdateTable(response);
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