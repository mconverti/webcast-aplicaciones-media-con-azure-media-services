﻿namespace VodWorkflow
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;
    using Microsoft.WindowsAzure.MediaServices.Client;

    public class Program
    {
        static void Main(string[] args)
        {
            try
            {
                string mediaServicesAccountName = ConfigurationManager.AppSettings["AccountName"];
                string mediaServicesAccountKey = ConfigurationManager.AppSettings["AccountKey"];

                CloudMediaContext context = new CloudMediaContext(mediaServicesAccountName, mediaServicesAccountKey);

                Console.WriteLine("Creando nuevo asset desde un archivo local...");

                // 1. Create a new asset by uploading a mezzanine file from a local path.
                IAsset inputAsset = context.CreateAssetFromFile(
                    "big_buck_bunny_720p_stereo.mp4",
                    AssetCreationOptions.None,
                    (af, p) =>
                    {
                        Console.WriteLine("Subiendo '{0}' - Progreso {1:0.##}%", af.Name, p.Progress);
                    });

                Console.WriteLine("Asset creado.");

                // 2. Prepare a job with a single task to transcode the previous mezzanine asset
                //    into a multi-bitrate asset.
                IJob job = context.PrepareJobWithSingleTask("Windows Azure Media Encoder", "H264 Adaptive Bitrate MP4 Set 720p", inputAsset, "Big Buck Bunny Dynamic Packaging", AssetCreationOptions.None);

                Console.WriteLine("Enviando transcoding job...");

                // 3. Submit the job and wait until it is completed.
                job.Submit();
                job = context.StartExecutionProgressTask(
                    job,
                    j =>
                    {
                        Console.WriteLine("Estado del job: {0}", j.State);
                        Console.WriteLine("Progreso del job: {0:0.##}%", j.GetOverallProgress());
                    },
                    CancellationToken.None).Result;

                Console.WriteLine("Transcoding job terminado.");

                IAsset outputAsset = job.OutputMediaAssets[0];

                Console.WriteLine("Publicando output asset...");

                // 4. Publish the output asset by creating an Origin locator for adaptive streaming, and a SAS locator for progressive download.
                context.CreateLocator(outputAsset, LocatorType.OnDemandOrigin, AccessPermissions.Read, TimeSpan.FromDays(30));
                context.CreateLocator(outputAsset, LocatorType.Sas, AccessPermissions.Read, TimeSpan.FromDays(30));

                IEnumerable<IAssetFile> mp4AssetFiles = outputAsset
                        .AssetFiles
                        .ToList()
                        .Where(af => af.Name.EndsWith(".mp4", StringComparison.OrdinalIgnoreCase));

                // 5. Generate the Smooth Streaming, HLS and MPEG-DASH URLs for adaptive streaming, and the Progressive Download URL.
                Uri smoothStreamingUri = outputAsset.GetSmoothStreamingUri();
                Uri hlsUri = outputAsset.GetHlsUri();
                Uri mpegDashUri = outputAsset.GetMpegDashUri();
                List<Uri> mp4ProgressiveDownloadUris = mp4AssetFiles.Select(af => af.GetSasUri()).ToList();

                Console.WriteLine(smoothStreamingUri);
                Console.WriteLine(hlsUri);
                Console.WriteLine(mpegDashUri);
                mp4ProgressiveDownloadUris.ForEach(uri => Console.WriteLine(uri));

                string filePath = @"asset-urls.txt";

                // 6. Save the URLs to a local file.
                smoothStreamingUri.Save(filePath);
                hlsUri.Save(filePath);
                mpegDashUri.Save(filePath);
                mp4ProgressiveDownloadUris.ForEach(uri => uri.Save(filePath));

                Console.WriteLine("Output asset disponible para adaptive streaming.");

                Console.WriteLine("VOD workflow terminado.");
            }
            catch (Exception exception)
            {
                // Parse the XML error message in the Media Services response and create a new 
                // exception with its content.
                exception = MediaServicesExceptionParser.Parse(exception);

                Trace.TraceError(exception.Message);
            }
            finally
            {
                Console.ReadLine();
            }
        }
    }
}
