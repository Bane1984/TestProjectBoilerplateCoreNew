using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero;

namespace TestProjectBoilerplateCore.IdentityServer4
{
    public class TestProjectBoilerplateCoreIdentityModule:AbpModule
    {
        [DependsOn(typeof(TestProjectBoilerplateCoreWebCoreModule),
            typeof(AbpZeroCommonModule))]
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
