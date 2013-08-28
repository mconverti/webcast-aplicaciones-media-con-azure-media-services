namespace Players.Controllers
{
    using System.Configuration;
    using System.Linq;
    using System.Web.Http;
    using Microsoft.WindowsAzure.MediaServices.Client;
    using Players.ViewModels;

    public class VideosController : ApiController
    {
        private readonly CloudMediaContext context;

        public VideosController()
            : this(new CloudMediaContext(ConfigurationManager.AppSettings["AccountName"], ConfigurationManager.AppSettings["AccountKey"]))
        {
        }

        public VideosController(CloudMediaContext context)
        {
            this.context = context;
        }

        public ApiAssetItemListViewModel Get()
        {
            var result = new ApiAssetItemListViewModel();
            var assets = this.context
                .Assets
                .ToList()
                .Where(a => a.Locators.ToList().Any(l => l.Type == LocatorType.OnDemandOrigin))
                .OrderByDescending(a => a.Created);

            foreach (var asset in assets)
            {
                var item = new ApiAssetItemViewModel
                {
                    Id = asset.Id,
                    Title = asset.Name,
                    Type = "VOD",
                    CreatedDate = asset.Created,
                    HssUrl = asset.GetSmoothStreamingUri(),
                    HlsUrl = asset.GetHlsUri(),
                    MpegDashUrl = asset.GetMpegDashUri(),
                };

                result.Add(item);
            }

            return result;
        }
    }
}
