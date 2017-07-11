$(document).ready(function () {
    var options = {
        valueNames: ['language', 'languabe-icon', 'file-name', 'synced-with', 'hearing-impaired', 'foreign-only', 'uploader', 'download-button']
    };

    var subtitlesList = new List('subtitles-table', options);

    var selectedLanguages = [];
    if (getCookie('selected-languages') != "") {
        selectedLanguages = JSON.parse(getCookie('selected-languages'));
    }

    if (selectedLanguages.length != 0) {
        var multiSelectText = "";

        for (i = 0; i < selectedLanguages.length; i++) {
            if (i == selectedLanguages.length - 1) {
                multiSelectText += selectedLanguages[i];
            } else {
                multiSelectText += selectedLanguages[i] + ", "
            }
        }

        $('#language-select').append('<option value="selected-languages" selected="true">' + multiSelectText + '</option>');
    }

    if (selectedLanguages.length != 0) {
        subtitlesList.filter(function (item) {
            var found = false;

            $.each(selectedLanguages, function (index, value) {
                if (item.values().language == value) {
                     found = true;
                }
            });

            return found;
        });
    }

    $('#language-select').change(function () {
        var selectedLanguage = $(this).val();

        if (selectedLanguage == "all") {
            subtitlesList.filter();
        } else if(selectedLanguage == "selected-languages") {
            subtitlesList.filter(function (item) {
                var found = false;

                $.each(selectedLanguages, function (index, value) {
                    if (item.values().language == value) {
                        found = true;
                    }
                });

                return found;
            });
        } else {
            subtitlesList.filter(function (item) {
                if (item.values().language == selectedLanguage) {
                    return true;
                } else {
                    return false;
                }
            });
        }
    });
});