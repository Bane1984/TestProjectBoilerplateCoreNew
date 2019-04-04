using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TestProjectBoilerplateCore.Configuration;
using TestProjectBoilerplateCore.Web;

namespace TestProjectBoilerplateCore.EntityFrameworkCore
{
    /* This class is needed to run "dotnet ef ..." commands from command line on development. Not used anywhere else */
    public class TestProjectBoilerplateCoreDbContextFactory : IDesignTimeDbContextFactory<TestProjectBoilerplateCoreDbContext>
    {
        public TestProjectBoilerplateCoreDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<TestProjectBoilerplateCoreDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder());

            TestProjectBoilerplateCoreDbContextConfigurer.Configure(builder, configuration.GetConnectionString(TestProjectBoilerplateCoreConsts.ConnectionStringName));

            return new TestProjectBoilerplateCoreDbContext(builder.Options);
        }
    }
}
