using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Web.Mvc.Configuration;
using TestProjectBoilerplateCore;

namespace IdentityServer4Abp
{
    [DependsOn(typeof(TestProjectBoilerplateCoreWebCoreModule))]
    public class IdentityServer4AbpModule:AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            //Configuration.Modules.AbpMvc().IsValidationEnabledForControllers = false;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(IdentityServer4AbpModule).GetAssembly());
        }
    }
}
