// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using Abp.Modules;
using Abp.Reflection.Extensions;

namespace TestProjectBoilerplateCore.IdentityServer4
{
    [DependsOn(typeof(TestProjectBoilerplateCoreWebCoreModule))]
        public class TestProjectBoilerplateCoreIdentityModule : AbpModule
        {
            public override void PreInitialize()
            {
                Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            }

            public override void Initialize()
            {
                IocManager.RegisterAssemblyByConvention(typeof(TestProjectBoilerplateCoreIdentityModule).GetAssembly());
            }
        }
}
