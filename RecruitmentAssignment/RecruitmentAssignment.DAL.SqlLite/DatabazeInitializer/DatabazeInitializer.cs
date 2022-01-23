using Microsoft.Data.Sqlite;
using RecruitmentAssignment.DAL.Models;
using RecruitmentAssignment.DAL.SqlLite.Connection;
using RecruitmentAssignment.DAL.SqlLite.Repositories;
using System;
using System.IO;
using System.Reflection;

namespace RecruitmentAssignment.DAL.SqlLite.DatabazeInitializer
{
    public class DatabazeInitializer
    {
        //NOT PART OF FINAL SOLUTION
        public void Init(Account[] initData)
        {
            var repo = new AccountRepository(new SqlLiteConnectionFactory());

            var dbFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "accountDb.db");            

            var _connectionStringBuilder = new SqliteConnectionStringBuilder();
            _connectionStringBuilder.DataSource = dbFile;

            using (var connection = new SqliteConnection(_connectionStringBuilder.ConnectionString))
            {
                connection.Open();
                var ass = AppDomain.CurrentDomain.BaseDirectory;

                var command = connection.CreateCommand();
                command.CommandText = @"CREATE TABLE Accounts(
                                        Id INTEGER PRIMARY KEY,
                                        ExternalId TEXT NOT NULL,
                                        Summary INTEGER NOT NULL,
                                        Type INTEGER NOT NULL,
                                        Amount REAL NOT NULL,
                                        PostingDate DATETIME NOT NULL,
                                        IsCleared INTEGER NOT NULL,
                                        ClearedDate TEXT)";
                command.ExecuteNonQuery();

                foreach (var acc in initData)
                {
                    repo.Create(acc).Wait();
                }
            }
        }
    }
}
