using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace PodcastApp
{
    class gpoddernet
    {
        /// <summary>
        /// Temporary methods to get things working
        /// </summary>
        /// <param name="number">number of items to be returned</param>
        /// <returns>json string containing top podcasts from gpodder.net</returns>
        public static string TopList(int number = 10)
        {
            string url = "https://gpodder.net/toplist/" + number + ".json";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            request.UserAgent = Constants.UserAgent;
            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        /// <summary>
        /// Temporary methods to get things working
        /// </summary>
        /// <param name="number">number of items to be returned</param>
        /// <returns>list of podcasts containing top podcasts from gpodder.net</returns>
        public static List<Podcast> TopPodcasts(int number)
        {
            string url = "https://gpodder.net/toplist/" + number + ".json";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            request.UserAgent = Constants.UserAgent;
            Stream stream = response.GetResponseStream();

            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return JsonConvert.DeserializeObject<List<Podcast>>(reader.ReadToEnd());
            }
        }
    }
}
