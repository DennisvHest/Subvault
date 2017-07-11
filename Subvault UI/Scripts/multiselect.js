$(document).ready(function () {
    var initMultiSelectText = $('.multiselect button').text();
    var selectedLanguages = [];
    if (getCookie('selected-languages') != "") {
        selectedLanguages = JSON.parse(getCookie('selected-languages'));
    }

    $.each(selectedLanguages, function (index, value) {
        $('input[name="language-multiselect"][value="' + value + '"]')[0].checked = true;
        updateMultiselect();
    });

    $('.multiselect button').click(function () {
        $('.multi-dropdown').toggle();
    });

    $('input[name="language-multiselect"]').change(function () {
        if (this.checked) {
            selectedLanguages.push($(this).val());
            setCookie('selected-languages', JSON.stringify(selectedLanguages), 1);
            updateMultiselect();
        } else {
            selectedLanguages.splice($.inArray($(this).val(), selectedLanguages), 1);
            setCookie('selected-languages', JSON.stringify(selectedLanguages), 1);
            updateMultiselect();
        }
    });

    function updateMultiselect() {
        var multiSelectText = "";

        if (selectedLanguages.length == 0) {
            multiSelectText = initMultiSelectText;
        } else {
            for (i = 0; i < selectedLanguages.length; i++) {
                if (i == selectedLanguages.length - 1) {
                    multiSelectText += selectedLanguages[i];
                } else {
                    multiSelectText += selectedLanguages[i] + ", "
                }
            }
        }        

        $('.multiselect button').text(multiSelectText);
        $('.multiselect button').append('<i class="fa fa-chevron-down" aria-hidden="true"></i>');
    }
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