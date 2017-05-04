using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Media.Imaging;
using System.Net;
using System.IO;
using Microsoft.Data.Sqlite;

namespace PodcastApp
{
    public class Podcast
    {
        /** json results **/
        public string Title { get; set; }
        public int Subscribers { get; set; }
        public int Subscribers_last_week { get; set; }
        public string Mygpo_link { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }
        public string Logo_url { get; set; }   // in image db, not podcast db
        public string Scaled_logo_url { get; set; }   // in image db, not podcast db
        /** end of standard gpodder json results **/


        /** additional info **/
        public List<Episode> Episodes { get; set; }
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
        public int DB_id { get; set; }

        public Podcast()
        {
            DB_id = -1;
            Title = "";
            Website = "";
            Description = "";

            Mygpo_link = "";
            Url = "";

            Subscribers = 0;
            Subscribers_last_week = 0;

            Logo_url = "";
            Scaled_logo_url = "";
            Episodes = new List<Episode>();
        }

        public Podcast(SqliteDataReader q)
        {

            DB_id = q.GetInt32(0);
            Title = q.GetString(1);
            Website = q.GetString(2);
            Description = q.GetString(3);

            Mygpo_link = q.GetString(4);
            Url = q.GetString(5);
            Subscribers = q.GetInt32(6);
            Subscribers_last_week = q.GetInt32(7);
            
            Logo_url = "";
            Scaled_logo_url = "";
            Episodes = new List<Episode>(); // TODO:: Retreive episode list
        }
        public override bool Equals(object obj)
        {
            if (!(obj is Podcast))
                return false;
            return Title.Equals(((Podcast)obj).Title);
        }
        public override int GetHashCode()
        {
            return Title.GetHashCode();
        }
    }
}
