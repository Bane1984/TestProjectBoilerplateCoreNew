using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TestProjectBoilerplateCore;

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
