using System.Threading.Tasks;
using SQLite;

namespace mpeg2_player.Data
{
    public class Repository<T> where T : new()
    {
        private SQLiteAsyncConnection db;
        public Repository(SQLiteAsyncConnection db)
        {
            this.db = db;
        }

        public AsyncTableQuery<T> Items
        {
            get
            {
                return db.Table<T>();
            }
        }

        public async Task<int> Create(T newEntity)
        {
            return await db.InsertAsync(newEntity);
        }

        public async Task<int> Update(T entity)
        {
            return await db.UpdateAsync(entity);
        }

        public async Task<int> Delete(T entity)
        {
            return await db.DeleteAsync(entity);
        }
    }
}
