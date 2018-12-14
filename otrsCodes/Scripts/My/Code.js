// delete code
$('body').on('contextmenu', 'td', function () {
    if (this.id === '0')
        return;

    $(this).css('background-color', '#32383e');
    $.ajax({
        url: "/Codes/Delete",
        type: "POST",
        data: {
            id: this.id
        },
        done: function (response) {
            alert(response.statusText);
        }
    });
    window.event.preventDefault();
});

// add code
$('body').on('click', 'td', function () {
    $(this).css('background-color', document.getElementById("HexSaver").value);
    $.ajax({
        url: "/Codes/Create",
        type: "POST",
        data: {
            CountryId: $("#ddlCountries").val(),
            NetworkId: document.getElementById("NetworkIdSaver").value,
            Zone: document.getElementById("RValue").value,
            Value: $(this).text()
        }
    }).fail(function (status) {
        alert(status.statusText);
    });
});