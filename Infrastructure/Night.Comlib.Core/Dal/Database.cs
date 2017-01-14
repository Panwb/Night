using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Dapper;

namespace Night.Comlib.Core.DAL
{
    public sealed class Database
    {
        #region Properties
        /// <summary>
        /// Gets the connect string.
        /// </summary>
        /// <value>The connect string.</value>
        private string ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DbEntryConnectionString"].ConnectionString;
            }
        }

        private string ProviderName
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["DbEntryConnectionString"].ProviderName;
            }
        }

        public IDbConnection Connection { get { return CreateConnection(); } }

        #endregion
        
        #region Helper
        
        private IDbConnection CreateConnection()
        {
            IDbConnection connection = GetDbClientFactory().CreateConnection();
            connection.ConnectionString = ConnectionString;

            return connection;
        }

        private DbClientFactory GetDbClientFactory()
        {
            switch (ProviderName)
            {
                case "MySql.Data.MySqlClient":
                    return new MySqlClientFactory();
                default:
                    throw new NotSupportedException("Unknow DatabaseType. For Now, Only Supported MySQL.");
            }
        }
        #endregion
    }
}
