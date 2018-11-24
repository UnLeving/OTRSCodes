function li_onclick_networks(e) {
    var hex = $(e).data('ass-id');
    document.getElementById("HexSaver").value = hex;
}

$("#ddlCountries").change(function () {
    var categoryId = $("#ddlCountries").val();
    $.ajax({
        url: "/Countries/Net",
        type: "GET",
        data: { id: categoryId }
    })
        .done(function (partialViewResult) {
            $("#myResultContent").html(partialViewResult);
        });

    //$("#myResultContent").load('@(Url.Action("Get","Networks",null, Request.Url.Scheme))?id=' + categoryId);
});

$("#tb1 td").click(function () {
    $(this).css('background-color', document.getElementById("HexSaver").value);
    alert(celltext);
});