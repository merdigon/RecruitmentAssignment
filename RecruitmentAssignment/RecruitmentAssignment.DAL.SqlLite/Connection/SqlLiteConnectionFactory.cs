using Microsoft.Data.Sqlite;
using System;
using System.IO;

namespace RecruitmentAssignment.DAL.SqlLite.Connection
{
    public class SqlLiteConnectionFactory: ISqlLiteConnectionFactory
    {
        private SqliteConnectionStringBuilder _connectionStringBuilder;

        public SqlLiteConnectionFactory()
        {
            _connectionStringBuilder = new SqliteConnectionStringBuilder();

            _connectionStringBuilder.DataSource = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "accountDb.db"); ;
        }

        public SqliteConnection CreateConnection()
        {
            return new SqliteConnection(_connectionStringBuilder.ConnectionString);
        }
    }
}
