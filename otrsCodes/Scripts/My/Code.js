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
    if (event.button === 1) {
        window.event.preventDefault();
        document.getElementById("regionChange").value = $("#regionChange").val() + this.textContent.charAt(0);
        RegionChanged($("#regionChange").val());
    }
});

// DELETE ZONE

// select and delete codes in column 
$('body').on('contextmenu', 'thead th', function () {
    var codesIDs = [];
    var columnCells = [];
    var flag = false;

    var tableCodes = this.closest('table');
    var selectedColumn = parseInt(this.textContent, 10) + 2;
    for (var i = 1; i < tableCodes.rows.length; ++i) {
        if (tableCodes.rows[i].cells[selectedColumn].id !== "0") {
            codesIDs.push(tableCodes.rows[i].cells[selectedColumn].id);
            columnCells.push(tableCodes.rows[i].cells[selectedColumn]);
        }
    }
    if (codesIDs.length === 0) {
        document.getElementById("Logs").value = "Client: nothing to delete";
        return;
    }

    DeleteCodes(codesIDs, columnCells, flag);
    window.event.preventDefault();
});

// delete single code  OR all codes by CTRL pressed
$('body').on('contextmenu', 'tbody td', function () {
    var codesIDs = [];
    var rowCells = [];
    var flag;
    if (cntrlIsPressed) {
        flag = true;
        rowCells = $(this.closest('tbody')).children();
        for (var i = 0; i < rowCells.length; ++i) {
            for (var j = 2; j < 12; ++j) {
                codesIDs.push(rowCells[i].cells[j].id);
            }
        }
    } else if (this.id < 0) {
        DeleteInheritedCode(this.id, this.textContent);
        window.event.preventDefault();
        return;
    } else {
        flag = false;
        if (this.id === '0') {
            document.getElementById("Logs").value = "Client: nothing to delete";
            return;
        }
        codesIDs.push(this.id);
        rowCells.push(this);
    }

    DeleteCodes(codesIDs, rowCells, flag);
    window.event.preventDefault();
});

// select and delete codes in row
$('body').on('contextmenu', 'tbody th', function () {
    var flag = false;
    var rowCells = $(this).parent().children('td');
    var codesIDs = [];

    for (var i = 0; i < rowCells.length; ++i) {
        if (rowCells[i].id !== "0")
            codesIDs.push(rowCells[i].id);
    }

    if (codesIDs.length === 0) {
        document.getElementById("Logs").value = "Client: nothing to delete";
        return;
    }
    DeleteCodes(codesIDs, rowCells, flag);
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

function DeleteInheritedCode(rootId, code) {
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
    if (!IsNetworkSelected()) return;
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
    if (!IsNetworkSelected()) {
        return;
    }
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
    if (!IsNetworkSelected()) return;
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

function IsNetworkSelected() {
    if (document.getElementById("NetworkIdSaver").value === "0") {
        document.getElementById("Logs").value = "Client: network not selected";
        return false;
    } else {
        return true;
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