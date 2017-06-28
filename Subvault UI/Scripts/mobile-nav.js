$(document).ready(function () {
    $('#mobile-nav button').click(function () {
        $('#side-nav').toggle();
    });

    /*$(window).resize(function () {
        if ($(window).width() > 767) {
            $('#side-nav').show();
            console.log('hallo');
        } else {
            $('#side-nav').show();
        }
    });*/
});