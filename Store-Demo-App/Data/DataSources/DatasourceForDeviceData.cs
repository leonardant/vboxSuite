using mpeg2_player.Data;
using mpeg2_player.Data.DataModels;
using mpeg2_player.libs;
using SQLite;
using Store_Demo_App.Data.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace Store_Demo_App.Data.DataSources
{
    public class DatasourceForDeviceData : IDisposable
    {
        protected StorageFolder UserFolder { get; set; }
        public SQLiteAsyncConnection Db { get; set; }

        public DatasourceForDeviceData(string DBFILENAME)
        {
            this.UserFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            var dbPath = Path.Combine(UserFolder.Path, DBFILENAME);
            this.Db = new SQLiteAsyncConnection(dbPath);

            channelsData = new Repository<channel>(Db);
            programmesData = new Repository<programme>(Db);
            recordingsData = new Repository<recording>(Db);
        }

        public void InitDatabase(string DBFILENAME)
        {
            //Check to ensure db file exists
            try
            {
                //Try to read the database file
                UserFolder.GetFileAsync(DBFILENAME).GetAwaiter().GetResult();
            }
            catch
            {
                //Will throw an exception if not found
                UserFolder.CreateFileAsync(DBFILENAME).GetAwaiter().GetResult();
            }
        }

        public void CreateTables()
        {
            var existingTables =
                Db.QueryAsync<sqlite_master>("SELECT name FROM sqlite_master WHERE type='table' ORDER BY name;")
                  .GetAwaiter()
                  .GetResult();

            //-- channels
            if (existingTables.Any(x => x.name == "channel") != true)
            {
                Db.CreateTableAsync<channel>().GetAwaiter().GetResult();
            }

            //-- programmes
            if (existingTables.Any(x => x.name == "programme") != true)
            {
                Db.CreateTableAsync<programme>().GetAwaiter().GetResult();
            }

            //-- recordings
            if (existingTables.Any(x => x.name == "recording") != true)
            {
                Db.CreateTableAsync<recording>().GetAwaiter().GetResult();
            }
        }

        public Repository<channel> channelsData { get; set; }
        public Repository<programme> programmesData { get; set; }
        public Repository<recording> recordingsData { get; set; }

        public void Dispose()
        {
            this.Db = null;
        }
    }
}
