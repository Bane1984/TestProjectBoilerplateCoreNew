using Abp.Modules;
using Abp.Reflection.Extensions;

namespace TestProjectBoilerplateCore.IdentityServer4
{
    [DependsOn(typeof(TestProjectBoilerplateCoreWebCoreModule))]
    public class TestProjectBoilerplateCoreIdentityServer4Module:AbpModule
    {

        public override void PreInitialize()
        {
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TestProjectBoilerplateCoreIdentityServer4Module).GetAssembly());
        }
    }
}

//TestProjectBoilerplateCoreWebCoreModule