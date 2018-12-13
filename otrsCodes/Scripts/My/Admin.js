// Admin page Create actions
function CreateNew(path, id) {
    if ($(id).valid()) {
        $.ajax({
            url: path,
            type: 'POST',
            data: $(id).serialize(),
            success: $(id)[0].reset(),
            error: function (err) {
                alert("Error: " + err.statusText);
            }
        });
    }
}