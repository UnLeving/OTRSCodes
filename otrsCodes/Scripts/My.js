function onMyDropDownChange() {
    $.get('@Url.Action("Index", "Home")',
        function (resultData)
        {
            $('#myResultContent').html(resultData);
        });
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