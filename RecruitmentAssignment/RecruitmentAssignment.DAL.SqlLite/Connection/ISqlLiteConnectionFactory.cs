using Microsoft.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecruitmentAssignment.DAL.SqlLite.Connection
{
    public interface ISqlLiteConnectionFactory
    {
        SqliteConnection Create();
    }
}
