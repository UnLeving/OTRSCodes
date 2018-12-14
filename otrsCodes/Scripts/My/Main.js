// Prevent keydown on admin page
$('#Code').bind('keydown', function (e) {
    document.getElementById("FLAG").value = "0";
});
