using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Net;
using System.IO;

namespace PodcastApp
{
    public class Podcast
    {
        /** json results **/
        public int Subscribers { get; set; }
        public string Title { get; set; }
        public string Scaled_logo_url { get; set; }
        public int Subscribers_last_week { get; set; }
        public string Mygpo_link { get; set; }
        public string Url { get; set; }
        public string Logo_url { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        /** end of json results **/


        /** additional info **/
        public string ImageScaledPath
        {
            get
            {
                string imgScaled = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
+ @"/PodcastApp/Podcasts/" + Title + @"/Images/Scaled.jpg";

                string imgFull = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
+ @"/PodcastApp/Podcasts/" + Title + @"/Images/Full.jpg";
                if (File.Exists(imgScaled))
                    return imgScaled;
                else if (File.Exists(imgFull))
                    return imgFull;
                else
                    return Constants.Resources + "generic-podcast-image.jpg";

            }
        }
        public string ImageFullPath
        {
            get
            {
                string imgScaled = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
+ @"/PodcastApp/Podcasts/" + Title + @"/Images/Scaled.jpg";

                string imgFull = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData)
+ @"/PodcastApp/Podcasts/" + Title + @"/Images/Full.jpg";
                if (File.Exists(imgFull))
                    return imgFull;
                else if (File.Exists(imgScaled))
                    return imgScaled;
                else
                    return Constants.Resources + "generic-podcast-image.jpg";
            }
        }
        public string ShortDescription
        {
            get
            {
                int maxLength = 50;
                if (Description.Length > maxLength)
                    return Description.Substring(0, maxLength);
                else
                    return Description;
            }
        }
    }
}
