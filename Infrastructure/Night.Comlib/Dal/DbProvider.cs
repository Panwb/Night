using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Night.Comlib.DAL
{
    public abstract class DbProvider
    {
        protected DbProvider(DbClientFactory dbClientFactory)
        {
            DbClientFactory = dbClientFactory;
        }

        public DbClientFactory DbClientFactory { get; }
    }
}
