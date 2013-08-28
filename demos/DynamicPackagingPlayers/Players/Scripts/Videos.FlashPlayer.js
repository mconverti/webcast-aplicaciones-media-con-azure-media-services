function PlayVideo(videoItem) {
    $("#playerContainer").empty();
    $("#playerContainer").append(
        '<object width="780" height="500">' +
        '    <param name="movie" value="/ClientBin/StrobeMediaPlayback.swf" />' +
        '    <param name="flashvars" value="src=' + videoItem.HssUrl + '&autoPlay=true&plugin_AdaptiveStreamingPlugin=%2FClientBin%2FMSAdaptiveStreamingPlugin-v1.0.5-osmf2.0.swf&bufferingOverlay=true&bufferTime=5" />' +
        '    <param name="allowFullScreen" value="true" />' +
        '    <param name="allowscriptaccess" value="always" />' +
        '    <param name="wmode" value="direct" />' +
        '    <embed src="/ClientBin/StrobeMediaPlayback.swf" type="application/x-shockwave-flash"' +
        '           allowscriptaccess="always" allowfullscreen="true" wmode="direct" width="870" height="490"' +
        '           flashvars="src=' + videoItem.HssUrl + '&autoPlay=true&plugin_AdaptiveStreamingPlugin=%2FClientBin%2FMSAdaptiveStreamingPlugin-v1.0.5-osmf2.0.swf&bufferingOverlay=true&bufferTime=5" />' +
        '</object>');
}


