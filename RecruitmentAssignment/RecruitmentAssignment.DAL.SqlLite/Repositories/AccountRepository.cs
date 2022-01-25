using Microsoft.Data.Sqlite;
using RecruitmentAssignment.DAL.Models;
using RecruitmentAssignment.DAL.Repositories;
using RecruitmentAssignment.DAL.SqlLite.Connection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentAssignment.DAL.SqlLite.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ISqlLiteConnectionFactory _sqlLiteConnectionFactory;

        public AccountRepository(ISqlLiteConnectionFactory sqlLiteConnectionFactory)
        {
            _sqlLiteConnectionFactory = sqlLiteConnectionFactory;
        }

        public async Task<long> Create(Account model)
        {
            var sql = @"INSERT INTO Accounts(ExternalId, Type, Summary, Amount, PostingDate, IsCleared, ClearedDate)
                        VALUES (@ExternalId, @Type, @Summary, @Amount, @PostingDate, @IsCleared, @ClearedDate)";
            using (var connection = _sqlLiteConnectionFactory.Create())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.Add(new SqliteParameter("@ExternalId", model.ExternalId.ToString()));
                command.Parameters.Add(new SqliteParameter("@Type", (int)model.Type));
                command.Parameters.Add(new SqliteParameter("@Summary", (int)model.Summary));
                command.Parameters.Add(new SqliteParameter("@Amount", model.Amount));
                command.Parameters.Add(new SqliteParameter("@PostingDate", model.PostingDate));
                command.Parameters.Add(new SqliteParameter("@IsCleared", model.IsCleared));
                command.Parameters.Add(new SqliteParameter("@ClearedDate", model.ClearedDate));
                await command.ExecuteNonQueryAsync();

                command.CommandText = "select last_insert_rowid();";

                object patientId = await command.ExecuteScalarAsync();

                return (long)patientId;
            }
        }

        public async Task<bool> Delete(int id)
        {
            var sql = @"DELETE FROM Accounts
                        WHERE Id=@Id";
            using (var connection = _sqlLiteConnectionFactory.Create())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.Add(new SqliteParameter("@Id", id));
                var deletedRows = await command.ExecuteNonQueryAsync();

                return deletedRows > 0;
            }
        }

        public async Task Edit(int id, Account model)
        {
            var sql = @"UPDATE Accounts
                        SET 
                            ExternalId=@ExternalId,
                            Type=@Type, 
                            Summary=@Summary, 
                            Amount=@Amount, 
                            PostingDate=@PostingDate,
                            IsCleared=@IsCleared,
                            ClearedDate=@ClearedDate
                        WHERE Id=@Id";
            using (var connection = _sqlLiteConnectionFactory.Create())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = sql;
                command.Parameters.Add(new SqliteParameter("@Id", id));
                command.Parameters.Add(new SqliteParameter("@ExternalId", model.ExternalId.ToString()));
                command.Parameters.Add(new SqliteParameter("@Type", (int)model.Type));
                command.Parameters.Add(new SqliteParameter("@Summary", (int)model.Summary));
                command.Parameters.Add(new SqliteParameter("@Amount", model.Amount));
                command.Parameters.Add(new SqliteParameter("@PostingDate", model.PostingDate));
                command.Parameters.Add(new SqliteParameter("@IsCleared", model.IsCleared));
                command.Parameters.Add(new SqliteParameter("@ClearedDate", model.ClearedDate));
                await command.ExecuteNonQueryAsync();
            }
        }


        string selectSql = @"SELECT
                                Id,
                                ExternalId,
                                Type, 
                                Summary, 
                                Amount, 
                                PostingDate,
                                IsCleared,
                                ClearedDate
                            FROM Accounts";
        public async Task<Account> Get(int id)
        {
            using (var connection = _sqlLiteConnectionFactory.Create())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = selectSql + " WHERE Id=@Id";
                command.Parameters.Add(new SqliteParameter("@Id", id));

                using (SqliteDataReader rdr = await command.ExecuteReaderAsync())
                {
                    if (rdr.Read())
                    {
                        return ReadAccount(rdr);
                    }

                    return null;
                }
            }
        }

        public async Task<IEnumerable<Account>> GetBy(AccountSummary? summary, AccountType? type, decimal? amount, DateTime? postingDate, bool? isCleared, DateTime? clearedDate)
        {
            var accounts = new List<Account>();
            using (var connection = _sqlLiteConnectionFactory.Create())
            {
                connection.Open();

                var command = connection.CreateCommand();
                //TODO Change into SQL Parameters
                command.CommandText = selectSql + GetFilteringConditions(summary, type, amount, postingDate, isCleared, clearedDate);

                using (SqliteDataReader rdr = await command.ExecuteReaderAsync())
                {
                    while (rdr.Read())
                    {
                        accounts.Add(ReadAccount(rdr));
                    }
                }
            }

            return accounts;
        }

        private string GetFilteringConditions(AccountSummary? summary, AccountType? type, decimal? amount, DateTime? postingDate, bool? isCleared, DateTime? clearedDate)
        {
            List<string> whereParameters = new List<string>();

            if(summary.HasValue)
                whereParameters.Add($@"Summary={(int)summary.Value}");

            if (type.HasValue)
                whereParameters.Add($@"Type={(int)type.Value}");

            if (amount.HasValue)
                whereParameters.Add($"Amount={amount.Value.ToString("0.00", System.Globalization.CultureInfo.InvariantCulture)}");

            if (postingDate.HasValue)
                whereParameters.Add($"PostingDate={postingDate.Value}");

            if (isCleared.HasValue)
                whereParameters.Add($"IsCleared={isCleared.Value}");

            if (clearedDate.HasValue)
                whereParameters.Add($"ClearedDate={clearedDate.Value}");

            if (whereParameters.Count > 0)
                return $" WHERE {string.Join(" AND ", whereParameters)};";
            return "";
        }

        private Account ReadAccount(SqliteDataReader reader)
        {
            return new Account
            {
                Id = reader.GetInt32(0),
                ExternalId = Guid.Parse(reader.GetString(1)),
                Type = (AccountType)reader.GetInt32(2),
                Summary = (AccountSummary)reader.GetInt32(3),
                Amount = reader.GetDecimal(4),
                PostingDate = DateTime.Parse(reader.GetString(5)),
                IsCleared = reader.GetBoolean(6),
                ClearedDate = reader.IsDBNull(7) ? null : DateTime.Parse(reader.GetString(7))
            };
        }
    }
}
