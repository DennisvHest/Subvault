var imageRootURL = "https://image.tmdb.org/t/p/w154";
var selectedItemId = null;

$('#upload-search-button').click(function () {
    searchItems()
});

$('#upload-search-input').keypress(function (event) {
    if (event.keyCode == 13) {
        searchItems();
    }
});

var searchItems = function () {
    //Empty the results
    $('#upload-search-results').empty();

    //Display loader
    $('#upload-search-results').append("<div class='loader'></div>");

    var query = $('#upload-search-input').val();

    //Request items with a title matching the query
    jQuery.getJSON("http://localhost:63346/api/API/Search?query=" + query, function (data, textStatus, jqXHR) {
        //Empty the results
        $('#upload-search-results').empty();

        console.log(jqXHR);

        //Get the items from the response
        var items = jqXHR.responseJSON;

        //For every item, create an item card
        for (i = 0; i < items.length; i++) {
            $('#upload-search-results').append(
                "<div class='item-card' data-id='" + items[i].Id + "'>"
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
            selectedItemId = $(this).data("id");
            $('#movieId').val(selectedItemId);
            $('#upload-search-results').children().each(function () {
                $(this).removeClass("active");
            });
            $(this).addClass("active");
            $('#subtitle-upload').fadeIn('fast');
        });
    });
}

$('#subtitles-input').change(function (event) {
    var filePath = $(this).val();
    var fileName = filePath.split(/(\\|\/)/g).pop();

    $('#subtitles-input + label').text(fileName);

    $('#meta-data-area').fadeIn('fast');
});