using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace TestProjectBoilerplateCore.EntityFrameworkCore
{
    public static class TestProjectBoilerplateCoreDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<TestProjectBoilerplateCoreDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<TestProjectBoilerplateCoreDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}
