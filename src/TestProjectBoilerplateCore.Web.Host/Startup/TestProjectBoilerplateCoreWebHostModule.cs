using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TestProjectBoilerplateCore.Configuration;

namespace TestProjectBoilerplateCore.Web.Host.Startup
{
    [DependsOn(
       typeof(TestProjectBoilerplateCoreWebCoreModule))]
    public class TestProjectBoilerplateCoreWebHostModule: AbpModule
    {
        private readonly IHostingEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public TestProjectBoilerplateCoreWebHostModule(IHostingEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TestProjectBoilerplateCoreWebHostModule).GetAssembly());
        }
    }
}
