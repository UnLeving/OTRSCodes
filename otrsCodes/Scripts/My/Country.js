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

function Generate_clicked() {
    $.ajax({
        url: "/Countries/GetRegExp",
        type: "GET",
        data: { id: $("#ddlNetworks").val() }
    })
        .done(function (ViewResult) {
            $("#RegExp").val(ViewResult);
        });
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