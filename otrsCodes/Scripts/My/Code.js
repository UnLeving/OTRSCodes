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

var cntrlIsPressed = false;

$(document).keydown(function (event) {
    if (event.key === 'Control') {
        cntrlIsPressed = true;
    }
});

$(document).keyup(function () {
    cntrlIsPressed = false;
});

// add code
$('body').on('click', 'td', function () {
    var codes = [];
    var tds = [];
    if (cntrlIsPressed === true) {
        //cntrlIsPressed = false;
        tds = $(this.parentNode).children('td');

        for (var i = 0; i < tds.length; ++i) {
            codes.push(tds[i].textContent);
        }
    } else {
        codes.push(this.textContent);
        tds.push(this);
    }
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
                //alert(tds.length);
                for (var i = 0; i <= tds.length; ++i) {
                    $(tds).css('background-color', document.getElementById("HexSaver").value);
                }
            }
            else {
                //alert("single");
                $(this).css('background-color', document.getElementById("HexSaver").value);
            }
            document.getElementById("Logs").value = "200 OK";
        },
        error: function (status) {
            document.getElementById("Logs").value = status.statusText;
        }
    });

});