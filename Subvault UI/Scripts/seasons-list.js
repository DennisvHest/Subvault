$(document).ready(function () {
    $('.season-toggle').click(function () {
        console.log("hallo");
        var seasonNumber = $(this).data('season-number');
        $('#season-' + seasonNumber).toggle();
    });
});