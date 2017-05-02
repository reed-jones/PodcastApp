using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.IO;
using Newtonsoft.Json;

namespace PodcastApp
{
    public class Account
    {
        public string Email { get; set; }
        public bool Active { get; set; }
        public DateTime CreatedDate { get; set; }
        public IList<string> Roles { get; set; }
    }
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Podcast> pl = new List<Podcast>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // clear list if needed
            UI_LV_Podcasts.Items.Clear();
            //string podList = TopList(5);
            string podList = File.ReadAllText(@"E:\Reed Jones\Documents\Documents\GitHub\PodcastApp\PodcastApp\Resources\top10.json");

            pl = JsonConvert.DeserializeObject<List<Podcast>>(podList);
            pl.ToList().ForEach(p =>
            {
                // ensure AppData path
                
                PodcastHelper.DownloadImages(p);
            });

            UI_LV_Podcasts.ItemsSource = pl;
        }
        public string TopList(int number)
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
