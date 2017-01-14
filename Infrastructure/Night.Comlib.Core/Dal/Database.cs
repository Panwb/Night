using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using Dapper;

namespace Night.Comlib.Core.DAL
{
    public sealed class Database
    {
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

        /// <summary>
        /// <para>When overridden in a derived class, gets the connection for this database.</para>
        /// <seealso cref="IDbConnection"/>        
        /// </summary>
        /// <returns>
        /// <para>The <see cref="IDbConnection"/> for this database.</para>
        /// </returns>
        public IDbConnection CreateConnection()
        {
            IDbConnection connection = GetDbClientFactory().CreateConnection();
            connection.ConnectionString = ConnectionString;

            return connection;
        }

        public ParameterCollection CreateParameters()
        {
            return new ParameterCollection();
        }

        public DbClientFactory GetDbClientFactory()
        {
            switch (ProviderName)
            {
                case "MySql.Data.MySqlClient":
                    return new MySqlClientFactory();
                default:
                    throw new NotSupportedException("Unknow DatabaseType. For Now, Only Supported MySQL.");
            }
        }

        public IEnumerable<object> Query(string sql, object param = null, IDbTransaction transaction = null, bool buffered = true, int? commandTimeout = null, CommandType? commandType = null)
        {
            return Execute(conn => conn.Query(sql, param, transaction, buffered, commandTimeout, commandType));
        }

        private T Execute<T>(Func<IDbConnection, T> exec)
        {

            var connection = CreateConnection();
            var result = exec(connection);
            if (connection.State != ConnectionState.Closed)
            {
                connection.Close();
            }
            return result;
        }
    }

    public class ParameterCollection : DynamicParameters
    {
    }
}
