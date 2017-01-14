using System.Diagnostics;

namespace Night.Comlib.Core.DAL
{
    [DebuggerStepThrough]
    public abstract class DbRepository
    {
        protected DbRepository()
        {
            Database = new Database();
        }
        
        public Database Database { get; }
    }

    public interface IRepository
    {
        Database Database { get; }
    }
}