using Abp.Modules;
using Abp.Reflection.Extensions;

namespace TestProjectBoilerplateCore.IdentityServer4
{
    public class TestProjectBoilerplateCoreWebCoreModule:AbpModule
    {
        [DependsOn(typeof(TestProjectBoilerplateCoreWebCoreModule))]
        public class IdentityServerServiceModule : AbpModule
        {
            public override void PreInitialize()
            {
                Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            }

            public override void Initialize()
            {
                IocManager.RegisterAssemblyByConvention(typeof(IdentityServerServiceModule).GetAssembly());
            }
        }
    }
}
