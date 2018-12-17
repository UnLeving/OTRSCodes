var cntrlIsPressed = false;

$(document).keydown(function (event) {
    if (event.key === 'Control') {
        cntrlIsPressed = true;
    }
});

$(document).keyup(function () {
    cntrlIsPressed = false;
});

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
        done: function (status) {
            document.getElementById("Logs").value = status.statusText;
        }
    });
    window.event.preventDefault();
});

{// add code
    //$('body').on('click', 'td', function () {
    //    $(this).css('background-color', document.getElementById("HexSaver").value);
    //    $.ajax({
    //        url: "/Codes/Create",
    //        type: "POST",
    //        data: {
    //            CountryId: $("#ddlCountries").val(),
    //            NetworkId: document.getElementById("NetworkIdSaver").value,
    //            Zone: document.getElementById("RValue").value,
    //            Value: $(this).text()
    //        }
    //    }).fail(function (status) {
    //        alert(status.statusText);
    //    });
    //});

    //function SendCodesOnBack(codes) {
    //    $.ajax({
    //        url: "/Codes/CreateMulti",
    //        type: "POST",
    //        data: {
    //            CountryId: $("#ddlCountries").val(),
    //            NetworkId: document.getElementById("NetworkIdSaver").value,
    //            Zone: document.getElementById("RValue").value,
    //            Value: codes
    //        },
    //        success: function () {
    //            document.getElementById("Logs").value = "200 OK";
    //        },
    //        error: function (status) {
    //            document.getElementById("Logs").value = status.statusText;
    //        }
    //    });
    //}

    //$(this).css('background-color', document.getElementById("HexSaver").value);
    //$.ajax({
    //    url: "/Codes/CreateMulti",
    //    type: "POST",
    //    data: {
    //        CountryId: $("#ddlCountries").val(),
    //        NetworkId: document.getElementById("NetworkIdSaver").value,
    //        Zone: document.getElementById("RValue").value,
    //        Value: $(this).text()
    //    },
    //    success: function () {
    //        document.getElementById("Logs").value = "200 OK";
    //    },
    //    error: function (status) {
    //        document.getElementById("Logs").value = status.statusText;
    //    }
    //});

    //$('body').on('click', 'td', function () {
    //    var isMouseDown = false;
    //    var codes = [];

    //    $('body td')
    //        .mousedown(function () {
    //            isMouseDown = true;
    //            $(this).css('background-color', document.getElementById("HexSaver").value);
    //            codes.push(this.textContent);
    //            return false;
    //        })
    //        .mouseover(function () {
    //            if (isMouseDown) {
    //                $(this).css('background-color', document.getElementById("HexSaver").value);
    //                codes.push(this.textContent);
    //            }
    //        })
    //        .bind("selectstart", function () {
    //            return false;
    //        })
    //        .mouseup(function () {
    //            isMouseDown = false;
    //            SendCodesOnBack(codes);
    //            codes.length = 0;
    //        });
    //});
}

$('body').on('click', 'thead th', function () {
    var codes = [];
    var tds = [];
    var tbl = this.closest('table');
    
    var n = parseInt(this.textContent,10) + 2;
    for (var i = 1; i < tbl.rows.length; ++i) {
        codes.push(tbl.rows[i].cells[n].textContent);
        tds.push(tbl.rows[i].cells[n]);
    }
    SendCodesOnServer(codes, tds);
});

// add code
$('body').on('click', 'tbody td', function () {
    var codes = [];
    var tds = [];
    if (cntrlIsPressed === true) {
        tds = $(this.closest('tbody')).children();
        for (var i = 0; i < tds.length; ++i) {
            codes.push(tds[i].textContent);
        }
    } else {
        codes.push(this.textContent);
        tds.push(this);
    }
    //document.getElementById("Logs").value = tds.length;
    //SendCodesOnServer(codes, tds);
});

function SendCodesOnServer(codes, tds) {
    $.ajax({
        url: "/Codes/CreateMulti",
        type: "POST",
        data: {
            CountryId: $("#ddlCountries").val(),
            NetworkId: document.getElementById("NetworkIdSaver").value,
            Zone: document.getElementById("RValue").value,
            Value: codes
        },
        success: function () {
            if (tds.length > 0) {
                for (var i = 0; i <= tds.length; ++i) {
                    $(tds).css('background-color', document.getElementById("HexSaver").value);
                }
            }
            else {
                $(this).css('background-color', document.getElementById("HexSaver").value);
            }
            document.getElementById("Logs").value = "200 OK";
        },
        error: function (status) {
            document.getElementById("Logs").value = status.statusText;
        }
    });
}