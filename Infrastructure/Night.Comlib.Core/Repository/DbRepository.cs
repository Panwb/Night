using System.Data;
using System.Diagnostics;

namespace Night.Comlib.Core.DAL
{
    [DebuggerStepThrough]
    public abstract class DbRepository
    {
        private readonly Database _database;

        protected DbRepository()
        {
            _database = new Database();
        }

        public IDbConnection DbConnection { get { return _database.Connection; } }
    }

    public interface IRepository
    {
        Database Database { get; }
    }
}