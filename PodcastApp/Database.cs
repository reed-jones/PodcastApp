using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.Sqlite;
using Microsoft.Data.Sqlite.Internal;

namespace PodcastApp
{
    public class Database
    {
        public Database()
        {
            //SqliteEngine.UseWinSqlite3(); //Configuring library to use SDK version of SQLite
        }
        public static void Initialize()
        {
            //SqliteEngine.UseWinSqlite3();

            /**** PODCAST TABLE *****/
            string makePodcastTable = "CREATE TABLE IF NOT EXISTS podcasts (" +
                "id                     INTEGER PRIMARY KEY AUTOINCREMENT, " +  // primary key
                "title                  NVARCHAR(50)    NOT NULL," +            // podcast title
                "website                NVARCHAR(192)   NULL," +                // podast homepage
                "description            NVARCHAR(1024)  NULL," +                // podcast description
                "mygpo_link             NVARCHAR(192)   NULL," +                // gpodder url
                "url                    NVARCHAR(192)   NULL," +                // feed url
                "subscribers            INTEGER         NULL," +                // number of subscribers
                "subscribers_last_week  INTEGER         NULL" +                 // subscribers last week
                ")";

            /**** IMAGE TABLE *****/
            string makeImageTable = "CREATE TABLE IF NOT EXISTS images (" +
               "image_id            INTEGER PRIMARY KEY AUTOINCREMENT, " +  // primary key
               "podcast_id          INTEGER         NOT NULL, " +           // foreign key
               "episode_id          INTEGER         NOT NULL," +            // foreign key
               "logo_url            NVARCHAR(192)   NULL," +                // logo image url
               "scaled_logo_url     NVARCHAR(192)   NULL" +                // scaled image url
               ")";

            /**** EPISODE TABLE *****/
            string makeEpisodeTable = "CREATE TABLE IF NOT EXISTS episodes (" +
               "episode_id       INTEGER PRIMARY KEY AUTOINCREMENT, " + // primary key
               "podcast_id     INTEGER          NOT NULL, " +           // foreign key
               "title          NVARCHAR(50)    NOT NULL," +              // podcast title
               "show_notes        NVARCHAR(192)   NULL," +               // podast homepage
               "episode_image_url    NVARCHAR(1024)  NULL," +            // podcast description
               "url            NVARCHAR(192)   NULL" +                  // feed url
               ")";

           
            Execute(makePodcastTable);
            Execute(makeImageTable);
            Execute(makeEpisodeTable);


        }

        public static bool AddPodcast(Podcast p)
        {
            string sqlIfExists = "SELECT * FROM podcasts WHERE (title = \"" + p.Title + "\")";
            List<Podcast> tmp = GetPodcasts(sqlIfExists);
            if (tmp.Count != 0) return false;
            /**** PODCAST TABLE *****/
            string sqlAddPodcast = "INSERT INTO podcasts " +
                "(title, website, description, mygpo_link, url, subscribers, subscribers_last_week )" +
                " VALUES " +
                "(\"" + 
                p.Title + "\" , \"" + 
                p.Website + "\" , \"" +
                p.Description + "\" , \"" + 
                p.Mygpo_link + "\" , \"" +
                p.Url + "\" , " +
                p.Subscribers + " , " + 
                p.Subscribers_last_week + " )";


            bool b;

            b = Execute(sqlAddPodcast) != 0;
            Console.WriteLine("Podcast Added: " + b);
            return b; // true if podcast successfully added
        }

        public static List<Podcast> GetPodcasts(string sqlQuery = "SELECT * from podcasts")
        {
            List<Podcast> entries = new List<Podcast>();
            using (SqliteConnection db = new SqliteConnection(Constants.SQLiteConnection))
            {
                db.Open();
                SqliteCommand selectCommand = new SqliteCommand(sqlQuery, db);
                SqliteDataReader q;
                try
                {
                    q = selectCommand.ExecuteReader();
                }
                catch (SqliteException error)
                {
                    //Handle error
                    return entries;
                }
                while (q.Read())
                {
                    Podcast p = new Podcast(q);

                    entries.Add(p);
                    
                }
                db.Close();
            }
            return entries;
        }

        private static int Execute(String sqlCommand)
        {
            int returnValue = 0;
            using (SqliteConnection db = new SqliteConnection(Constants.SQLiteConnection))
            {
                db.Open();
                //new SQLiteCommand("CREATE TABLE PHOTOS(ID INTEGER PRIMARY KEY AUTOINCREMENT, PHOTO BLOB)", connection))
                SqliteCommand createTable = new SqliteCommand(sqlCommand, db);
                try
                {
                    //createTable.ExecuteReader();
                    returnValue += createTable.ExecuteNonQuery();
                }
                catch (SqliteException e)
                {
                    //Do nothing
                }
                db.Close();
            }
            return returnValue;
        }
    }
}
