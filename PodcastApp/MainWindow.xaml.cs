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
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Data.Sqlite;
using Microsoft.Data.Sqlite.Internal;

namespace PodcastApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        List<Podcast> pl = new List<Podcast>();

        public MainWindow()
        {
            InitializeComponent();
            Database.Initialize();

            //Database.Load();
            pl = Database.GetPodcasts();
            pl.ForEach(a => UI_LV_Podcasts.Items.Add(a));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //RefreshList(gpoddernet.TopList());
            RefreshList(gpoddernet.TopList(25));
            // debugging
            //RefreshList(File.ReadAllText(Constants.Resources + "top10.json"));
        }

        private void RefreshList(string source)
        {
     
                pl = JsonConvert.DeserializeObject<List<Podcast>>(source);

            int count = 0;
            List<Podcast> temp = new List<Podcast>();

                temp = new List<Podcast>(pl);

            foreach (Podcast p in temp)
            {
                //if (pl.Contains(p)) continue;
                // no need to hammer image downloading servers during development
                PodcastHelper.DownloadImages(p);
                // UI_LV_Podcasts.Items.Add(p);
                Console.WriteLine(count + " Images Downloaded");
                UI_LV_Podcasts.Items.Add(p);
                Database.AddPodcast(p);
            };
            Console.WriteLine("All Images Downloaded");
        }
    }
}
