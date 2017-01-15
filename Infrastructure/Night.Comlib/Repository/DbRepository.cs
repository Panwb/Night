using System.Data;
using System.Diagnostics;

namespace Night.Comlib.DAL
{
    [DebuggerStepThrough]
    public abstract class DbRepository : IRepository
    {
        private readonly Database _database;

        protected DbRepository()
        {
            _database = new Database();
        }

        public IDbConnection Database { get { return _database.Connection; } }
    }

    public interface IRepository
    {
        IDbConnection Database { get; }
    }
}