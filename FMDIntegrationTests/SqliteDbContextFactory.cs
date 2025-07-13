using FMDInfra.Data;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDIntegrationTests
{
    internal class SqliteDbContextFactory
    {
        private readonly DbConnection _connection;
        public SqliteDbContextFactory()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();
        }

        internal AppDbContext Create()
        {
            var options = new DbContextOptionsBuilder<AppDbContext>().UseSqlite(_connection).Options;

            var context = new AppDbContext(options);

            context.Database.EnsureCreated();

            return context;
        }
    }
}
