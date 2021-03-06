﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;

namespace PodcastApp
{
    class PodcastHelper
    {
        internal static void DownloadImages(Podcast p)
        {
            // save base path in AppData/Roaming/PodcastApp
            string baseFilePath = Constants.AppData + "Podcasts/" + p.Title + @"/Images/";
            System.IO.FileInfo file = new System.IO.FileInfo(baseFilePath);
            file.Directory.Create();

            if(!File.Exists(baseFilePath + "Scaled.jpg"))
                GetImagePaths(p.Scaled_logo_url, baseFilePath + "Scaled.jpg");

            if (!File.Exists(baseFilePath + "Full.jpg"))
                GetImagePaths(p.Logo_url, baseFilePath + "Full.jpg");
        }
        private static void GetImagePaths(string url, string filename)
        {
            if (url == null) return;
            string contentType = url.Substring(0,19).Contains("gpodder.net") ? "text/plain" : "image";
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.UserAgent = Constants.UserAgent;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                // Check that the remote file was found. The ContentType
                // check is performed since a request for a non-existent
                // image file might be redirected to a 404-page, which would
                // yield the StatusCode "OK", even though the image was not
                // found.
                Console.WriteLine(response.StatusCode + " : " + response.ContentType);
                if ((response.StatusCode == HttpStatusCode.OK ||
                    response.StatusCode == HttpStatusCode.Moved ||
                    response.StatusCode == HttpStatusCode.Redirect) &&
                    response.ContentType.StartsWith(contentType, StringComparison.OrdinalIgnoreCase))
                {

                    // if the remote file was found, download it
                    using (Stream inputStream = response.GetResponseStream())
                    {
                        using (Stream outputStream = File.OpenWrite(filename))
                        {
                            byte[] buffer = new byte[4096];
                            int bytesRead;
                            do
                            {
                                bytesRead = inputStream.Read(buffer, 0, buffer.Length);
                                outputStream.Write(buffer, 0, bytesRead);
                            } while (bytesRead != 0);
                            Console.WriteLine("Downloaded Image: " + filename);
                        }
                    }
                }
            }
            catch (Exception err)
            {
                Console.WriteLine("Could Not Save: \n" + filename + "\nGetImagePaths() Error: " + err.Message);
            }
        }
    }
}
