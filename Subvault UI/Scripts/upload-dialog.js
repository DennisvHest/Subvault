$(document).ready(function () {
    $('#upload-button').click(function () {
        $('#upload-dialog').fadeIn('fast');
    });

    $('#upload-dialog .dialog-close').click(function () {
        $('#upload-dialog').fadeOut('fast');
    });
});