// Prevent keydown on admin page
$('#Code').bind('keydown', function (e) {
    document.getElementById("FLAG").value = "0";
});

// Handle digits and arrows UP/DOWN clicked
$(document).bind('keydown', function (e) {
    key = e.key;
    if (document.getElementById("FLAG").value === "0") {
        document.getElementById("FLAG").value = "1";
        return;
    }
    if ((key >= 0 && key <= 9) || (key === 'ArrowUp' || key === 'ArrowDown')) {
        var $n = $('#tb1').find('#RValue').text();
        if (key === 'ArrowUp') {
            key = $n * 10;
        }
        else if (key === 'ArrowDown') {
            key = $n / 10;
        }
        $.ajax({
            url: "/Countries/Index",
            type: "GET",
            data: {
                id: $("#ddlCountries").val(),
                zone: key
            }
        })
            .done(function (response) {
                UpdateTable(response);
            });
    }

});

// Fill tables
function UpdateTable(response) {
    var $bodyContent1 = $(response).find('#tb1 tbody').children();
    var $bodyContent2 = $(response).find('#tb2 tbody').children();
    var $table1 = $('#tb1');
    var $table2 = $('#tb2');
    $table1.find('tbody').empty().append($bodyContent1);
    $table2.find('tbody').empty().append($bodyContent2);
}