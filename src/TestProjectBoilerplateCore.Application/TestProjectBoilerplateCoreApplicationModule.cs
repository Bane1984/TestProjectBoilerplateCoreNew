using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TestProjectBoilerplateCore.Authorization;

namespace TestProjectBoilerplateCore
{
    [DependsOn(
        typeof(TestProjectBoilerplateCoreCoreModule), 
        typeof(AbpAutoMapperModule))]
    public class TestProjectBoilerplateCoreApplicationModule : AbpModule
    {
        public override void PreInitialize()
        {
            Configuration.Authorization.Providers.Add<TestProjectBoilerplateCoreAuthorizationProvider>();
        }

        public override void Initialize()
        {
            var thisAssembly = typeof(TestProjectBoilerplateCoreApplicationModule).GetAssembly();

            IocManager.RegisterAssemblyByConvention(thisAssembly);

            Configuration.Modules.AbpAutoMapper().Configurators.Add(
                // Scan the assembly for classes which inherit from AutoMapper.Profile
                cfg => cfg.AddProfiles(thisAssembly)
            );
        }
    }
}
