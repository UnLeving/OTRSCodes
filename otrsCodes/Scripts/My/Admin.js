// Admin page Create actions
function CreateNew(path, id) {
    if ($(id).valid()) {
        $('#loader').show();
        $.ajax({
            url: path,
            type: 'POST',
            data: $(id).serialize(),
            success: function () {
                $(id)[0].reset();
                document.getElementById("Logs").value = "200 OK";
                $('#loader').hide();
            },
            error: function (status) {
                document.getElementById("Logs").value = status.statusText;
                $('#loader').hide();
            }
        });
    }
}