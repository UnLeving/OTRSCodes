var cntrlIsPressed = false;

$(document).keydown(function (event) {
    if (event.key === 'Control') {
        cntrlIsPressed = true;
    }
});

$(document).keyup(function () {
    cntrlIsPressed = false;
});

// open region by middle btn click
$('body').on('mousedown', 'tbody td', function () {
    if (event.button === 1 && this.id !== 0) {
        window.event.preventDefault();
        document.getElementById("regionChange").value = $("#regionChange").val() + this.textContent.charAt(0);
        RegionChanged($("#regionChange").val());
    }
});

// DELETE ZONE

// select and delete codes in column 
$('body').on('contextmenu', 'thead th', function () {
    var ids = [];
    var tds = [];
    var flag = false;

    var tbl = this.closest('table');
    var n = parseInt(this.textContent, 10) + 2;
    for (var i = 1; i < tbl.rows.length; ++i) {
        ids.push(tbl.rows[i].cells[n].id);
        tds.push(tbl.rows[i].cells[n]);
    }
    DeleteCodes(ids, tds, flag);
    window.event.preventDefault();
});

// delete single code  and all codes by CTRL pressed
$('body').on('contextmenu', 'tbody td', function () {
    var ids = [];
    var tds = [];
    var flag;
    if (cntrlIsPressed) {
        flag = true;
        tds = $(this.closest('tbody')).children();
        for (var i = 0; i < tds.length; ++i) {
            for (var j = 2; j < 12; ++j) {
                ids.push(tds[i].cells[j].id);
            }
        }
    } else {
        flag = false;

        ids.push(this.id);
        tds.push(this);
    }
    if (this.id < 0) {
        DelInheritedCode(this.id, this.textContent);
    } else
        DeleteCodes(ids, tds, flag);

    window.event.preventDefault();
});

// select and delete codes in row
$('body').on('contextmenu', 'tbody th', function () {
    var ids = [];
    var tds = [];
    var flag = false;

    tds = $(this).parent().children('td');
    for (var i = 0; i < tds.length; ++i) {
        ids.push(tds[i].id);
    }

    DeleteCodes(ids, tds, flag);
    window.event.preventDefault();
});

function DeleteCodes(e, tds, flag) {
    $('#loader').show();
    $.ajax({
        url: "/Codes/Delete",
        type: "POST",
        data: {
            ids: e
        },
        success: function () {
            if (flag) {
                DelOnSucc2Array(tds);
            } else {
                DelOnSuccArray(tds);
            }
            document.getElementById("Logs").value = "200 OK";
            $('#loader').hide();
        },
        error: function (status) {
            document.getElementById("Logs").value = status.statusText;
            $('#loader').hide();
        }
    });
}

function DelOnSuccArray(tds) {
    for (var i = 0; i <= tds.length; ++i) {
        $(tds).css('background-color', '#FFFFFF');
    }
}

function DelOnSucc2Array(tds) {
    for (var i = 0; i < tds.length; ++i) {
        for (var j = 2; j < 12; ++j) {
            $(tds[i].cells[j]).css('background-color', '#FFFFFF');
        }
    }
}

function DelInheritedCode(rootId, code) {
    $('#loader').show();
    $.ajax({
        url: "/Codes/DeleteInheritedCode",
        type: "POST",
        data: {
            Id: rootId,
            CountryId: $("#ddlCountries").val(),
            Zone: $("#regionChange").val(),
            Value: code
        },
        success: function () {
            document.getElementById("Logs").value = "200 OK";
            $('#loader').hide();
        },
        error: function (status) {
            document.getElementById("Logs").value = status.statusText;
            $('#loader').hide();
        }
    });
}

// ADD ZONE

// select and add column 
$('body').on('click', 'thead th', function () {
    var codes = [];
    var tds = [];
    var flag = false;

    var tbl = this.closest('table');
    var n = parseInt(this.textContent, 10) + 2;
    for (var i = 1; i < tbl.rows.length; ++i) {
        codes.push(tbl.rows[i].cells[n].textContent);
        tds.push(tbl.rows[i].cells[n]);
    }
    SendCodesOnServer(codes, tds, flag);
});

// select and add cell or all cells by CTRL pressed
$('body').on('click', 'tbody td', function () {
    var flag;
    var codes = [];
    var tds = [];
    if (cntrlIsPressed === true) {
        flag = true;
        tds = $(this.closest('tbody')).children();
        for (var i = 0; i < tds.length; ++i) {
            for (var j = 2; j < 12; ++j) {
                codes.push(tds[i].cells[j].textContent);
            }
        }
    }
    else {
        flag = false;
        tds.push(this);
        codes.push(this.textContent);
    }
    SendCodesOnServer(codes, tds, flag);
});

// select and add row
$('body').on('click', 'tbody th', function () {
    var codes = [];
    var tds = [];
    var flag = false;
    //codes.push(this.textContent);
    tds = $(this).parent().children('td');
    for (var i = 0; i < tds.length; ++i) {
        codes.push(tds[i].textContent);
    }

    SendCodesOnServer(codes, tds, flag);
});

function SendCodesOnServer(codes, tds, isTwoDemenArr) {
    $('#loader').show();
    $.ajax({
        url: "/Codes/CreateMulti",
        type: "POST",
        data: {
            CountryId: $("#ddlCountries").val(),
            NetworkId: document.getElementById("NetworkIdSaver").value,
            Zone: $("#regionChange").val(),
            Value: codes
        },
        success: function () {
            if (isTwoDemenArr) {
                OnSuccessTwoDimenArray(tds);
            } else {
                OnSuccessArray(tds);
            }
            document.getElementById("Logs").value = "200 OK";
            $('#loader').hide();
        },
        error: function (status) {
            document.getElementById("Logs").value = status.statusText;
            $('#loader').hide();
        }
    });
}

function OnSuccessTwoDimenArray(tds) {
    for (var i = 0; i < tds.length; ++i) {
        for (var j = 2; j < 12; ++j) {
            $(tds[i].cells[j]).css('background-color', document.getElementById("HexSaver").value);
        }
    }
}

function OnSuccessArray(tds) {
    for (var i = 0; i <= tds.length; ++i) {
        $(tds).css('background-color', document.getElementById("HexSaver").value);
    }
}

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