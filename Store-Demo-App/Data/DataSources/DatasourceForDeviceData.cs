using mpeg2_player.Data;
using mpeg2_player.Data.DataModels;
using mpeg2_player.libs;
using SQLite;
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

            channelsData = new Repository<channels>(Db);
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

            //-- offline_bookmarks
            if (existingTables.Any(x => x.name == "channels") != true)
            {
                Db.CreateTableAsync<channels>().GetAwaiter().GetResult();
            }
        }

        public Repository<channels> channelsData { get; set; }

        public void Dispose()
        {
            this.Db = null;
        }
    }
}
