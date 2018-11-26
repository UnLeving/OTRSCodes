function li_onclick_networks(e) {
    document.getElementById("HexSaver").value = $(e).css('background-color');
    document.getElementById("NetworkIdSaver").value = e.id;
}

$("#ddlCountries").change(function () {
    var countryId = $("#ddlCountries").val();
    document.getElementById("CountryIdSaver").value = countryId;

    $.ajax({
        url: "/Countries/Net",
        type: "GET",
        data: { id: countryId }
    })
        .done(function (partialViewResult) {
            $("#myResultContent").html(partialViewResult);

        });
});

$("td").click(function () {
    $(this).css('background-color', document.getElementById("HexSaver").value);
    $.ajax({
        url: "/Countries/AddCode",
        type: "POST",
        data: { CountryId: document.getElementById("CountryIdSaver").value, NetworkId: document.getElementById("NetworkIdSaver").value, Code: $(this).text() }
    })
        .done(function (status) {
            alert(status);
        });
});