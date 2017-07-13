var imageRootURL = "https://image.tmdb.org/t/p/w154";
var selectedItemIndex = null;
var selectedItemId = null;
var uploadType = $('#upload-script').data('type');
var items = null;

$('#upload-search-button').click(function () {
    searchItems()
});

$('#upload-search-input').keypress(function (event) {
    if (event.keyCode == 13) {
        searchItems();
    }
});

var uploadSteps = $('.upload-step');

var nextStep = function (step) {
    $(uploadSteps[step]).fadeIn('fast');
    var id = $(uploadSteps[step]).attr('id');
    $('#' + id + ' .step-number').text(step + 1);
}

var searchItems = function () {
    //Empty the results
    $('#upload-search-results').empty();

    //Display loader
    $('#upload-search-results').append("<div class='loader'></div>");

    var query = $('#upload-search-input').val();

    //Request items with a title matching the query
    jQuery.getJSON("http://localhost:63346/api/" + uploadType + "/Search?query=" + query, function (data, textStatus, jqXHR) {
        //Empty the results
        $('#upload-search-results').empty();

        console.log(jqXHR);

        //Get the items from the response
        items = jqXHR.responseJSON;

        //For every item, create an item card
        for (i = 0; i < items.length; i++) {
            $('#upload-search-results').append(
                "<div class='item-card' data-index='" + i + "' data-id='" + items[i].Id + "'>"
                    + "<div class='poster'>"
                        + "<img src='" + imageRootURL + items[i].PosterURL + "' />"
                    + "</div>"
                    + "<div class='info'>"
                        + "<h2>" + items[i].Title + "</h2>"
                        + "<div class='subtitle-icon'>"
                            + "<i class='fa fa-cc' aria-hidden='true'></i>"
                        + "</div>"
                        + "<div class='available-languages'>"
                            + "<p>English, Dutch, Spanish, French...</p>"
                        + "</div>"
                    + "</div>"
                + "</div>"
            );
        }

        //On item clicked
        $('.item-card').click(function (event) {
            selectedItemIndex = $(this).data("index");
            if (uploadType == "Series") {
                $('#season-select').empty();
                $('#season-select').append('<option disabled selected>Select season...</option>');
                for (s = 0; s < items[selectedItemIndex].Seasons.length; s++) {
                    var season = items[selectedItemIndex].Seasons[s];
                    $('#season-select').append(
                        '<option value="' + season.SeasonNumber + '" data-index="' + s + '">Season ' + season.SeasonNumber + '</option>'
                    );
                }
            }
            selectedItemId = $(this).data("id");
            $('#itemId').val(selectedItemId);
            $('#upload-search-results').children().each(function () {
                $(this).removeClass("active");
            });
            $(this).addClass("active");
            nextStep(1);
        });
    });
}

$('#season-select').change(function (event) {
    var selectedSeason = $(this).find(':selected');
    var selectedSeasonNr = $(this).find(':selected').val();
    var selectedSeasonIndex = $(selectedSeason).data("index");

    $('#episode-select').empty();
    $('#episode-select').append('<option disabled selected>Select episode...</option>');
    for (e = 0; e < items[selectedItemIndex].Seasons[selectedSeasonIndex].Episodes.length; e++) {
        var episode = items[selectedItemIndex].Seasons[selectedSeasonIndex].Episodes[e];
        $('#episode-select').append(
            '<option value="' + episode.Id + '">' + episode.EpisodeNumber + ' | ' + episode.Name + '</option>'
        );
    }
    nextStep(2);
});

$('#episode-select').change(function (event) {
    $('#episodeId').val($(this).find(':selected').val());
    nextStep(3);
});

$('#subtitles-input').change(function (event) {
    var filePath = $(this).val();
    var fileName = filePath.split(/(\\|\/)/g).pop();

    $('#subtitles-input + label').text(fileName);

    if (uploadType == "Series") {
        nextStep(4);
    } else {
        nextStep(2);
    }
});