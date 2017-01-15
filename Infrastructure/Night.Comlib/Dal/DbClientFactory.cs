using System.Data.Common;
using System.Diagnostics;

namespace Night.Comlib.DAL
{
    public abstract class DbClientFactory
    {
        public abstract DbConnection CreateConnection();
    }
}