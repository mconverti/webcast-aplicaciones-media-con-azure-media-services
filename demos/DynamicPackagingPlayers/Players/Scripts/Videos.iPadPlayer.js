function PlayVideo(videoItem) {
    var player = $("video#player");

    player.attr("src", videoItem.HlsUrl);

    player[0].controls = true;
}
