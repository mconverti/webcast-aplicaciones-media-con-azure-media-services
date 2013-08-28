namespace Players.ViewModels
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot("Video")]
    public class ApiAssetItemViewModel
    {
        public string Id { get; set; }

        public DateTime CreatedDate { get; set; }

        public string Title { get; set; }

        public string Type { get; set; }

        public Uri HssUrl { get; set; }

        public Uri HlsUrl { get; set; }

        public Uri MpegDashUrl { get; set; }
    }
}