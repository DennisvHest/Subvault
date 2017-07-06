$(document).ready(function () {
    var selectedLanguages = [];
    if (getCookie('selected-languages') != "") {
        selectedLanguages = JSON.parse(getCookie('selected-languages'));
    }

    $.each(selectedLanguages, function (index, value) {
        $('input[name="language-multiselect"][value="' + value + '"]')[0].checked = true;
    });

    $('.multiselect button').click(function () {
        $('.multi-dropdown').toggle();
    });

    $('input[name="language-multiselect"]').change(function () {
        if (this.checked) {
            selectedLanguages.push($(this).val());
            setCookie('selected-languages', JSON.stringify(selectedLanguages), 1);
        } else {
            selectedLanguages.splice($.inArray($(this).val(), selectedLanguages), 1);
            setCookie('selected-languages', JSON.stringify(selectedLanguages), 1);
        }
    });
});

function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
    var expires = "expires=" + d.toUTCString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

function getCookie(cname) {
    var name = cname + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return "";
}