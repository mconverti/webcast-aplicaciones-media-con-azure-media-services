$(document).ready(function () {
    jQuery.data(document.body, "firstPlay", true);
    UpdateVideosList();
    setInterval(UpdateVideosList, 15000);
});

function UpdateVideosList() {
    getVideos(function (videos) {
        var firstVideoSelected = false;

        $('#videoList').empty();

        $.each(videos, function (index, video) {
            var imageDiv = '<div id="' + video.Id + '"><p style="cursor:pointer">' + video.Title + '</p></div>';

            $('#videoList').append(imageDiv);

            $('#' + video.Id).click(function () {
                VideoSelected(video);
            });

            if (!firstVideoSelected && jQuery.data(document.body, "firstPlay")) {
                firstVideoSelected = true;
                VideoSelected(video);
            }

            if (video.Title == $("#videoTitle").text()) {
                $('#' + video.Id).addClass("selected");
            }
        });

        jQuery.data(document.body, "firstPlay", false);
    });
}

function VideoSelected(video) {
    $('#videoList div').removeClass("selected");
    $('#' + video.Id).addClass("selected");

    $("#videoTitle").text(video.Title);

    PlayVideo(video);
}

function getVideos(callback) {
    $.ajax({
        url: '/api/videos',
        dataType: 'json',
        cache: false,
        success: callback,
        error: function (err) {
            alert('Error: ' + err.responseText);
        }
    });
}