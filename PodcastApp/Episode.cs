using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastApp
{
    public class Episode
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string ShowNotes { get; set; }
        public DateTime Release { get; set; }
        public TimeSpan PlaybackPosition { get; set; }
        public bool finished { get; set; }
    }
}
