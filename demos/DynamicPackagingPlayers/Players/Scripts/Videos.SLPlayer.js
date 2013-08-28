function PlayVideo(videoItem) {
    $("#playerContainer").empty();
    $("#playerContainer").append(
        '<object data="data:application/x-silverlight-2," height="500px" type="application/x-silverlight-2" width="780px">' +
        '    <param name="source" value="/ClientBin/Player.xap" />' + 
        '    <param name="minRuntimeVersion" value="4.0.50401.0" />' + 
        '    <param name="autoUpgrade" value="true" />' + 
        '    <param name="InitParams" value="deliverymethod=AdaptiveStreaming, mediaurl=' + videoItem.HssUrl + '"/>' +
        '</object>');
}