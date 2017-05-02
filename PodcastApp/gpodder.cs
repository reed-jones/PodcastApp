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
    class Gpodder
    {
        public static string TopList(int number)
        {
            string url = "https://gpodder.net/toplist/" + number + ".json";


            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            using (StreamReader reader = new StreamReader(stream, Encoding.UTF8))
            {
                return reader.ReadToEnd();
                //return JsonConvert.SerializeObject(reader.ReadToEnd(), Formatting.Indented);
                //JsonConvert.DeserializeObject<Podcast>(reader.ReadToEnd());
            }
        }
    }
}
