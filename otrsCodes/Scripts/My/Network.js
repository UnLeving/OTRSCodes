function NetworkSelected(e) {
    document.getElementById("HexSaver").value = $(e).css('background-color');
    document.getElementById("NetworkIdSaver").value = e.id;
}

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