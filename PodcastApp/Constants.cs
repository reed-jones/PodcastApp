using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastApp
{
    internal static class Constants
    {
        public static string AppData = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"/PodcastApp/";
        public static string Resources = System.AppDomain.CurrentDomain.BaseDirectory + "../../Resources/";
        public static string SQLiteConnection = "Filename=PodcastAppSqlite.db";
        public static string UserAgent = "new .Net podcast client";
    }
}
