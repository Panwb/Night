using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace Night.Comlib.Core.DAL
{
    public class MySqlClientFactory : DbClientFactory
    {
        public override DbConnection CreateConnection()
        {
            return new MySqlConnection();
        }
    }
}
