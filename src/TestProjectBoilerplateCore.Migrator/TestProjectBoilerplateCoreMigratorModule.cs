using Microsoft.Extensions.Configuration;
using Castle.MicroKernel.Registration;
using Abp.Events.Bus;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TestProjectBoilerplateCore.Configuration;
using TestProjectBoilerplateCore.EntityFrameworkCore;
using TestProjectBoilerplateCore.Migrator.DependencyInjection;

namespace TestProjectBoilerplateCore.Migrator
{
    [DependsOn(typeof(TestProjectBoilerplateCoreEntityFrameworkModule))]
    public class TestProjectBoilerplateCoreMigratorModule : AbpModule
    {
        private readonly IConfigurationRoot _appConfiguration;

        public TestProjectBoilerplateCoreMigratorModule(TestProjectBoilerplateCoreEntityFrameworkModule abpProjectNameEntityFrameworkModule)
        {
            abpProjectNameEntityFrameworkModule.SkipDbSeed = true;

            _appConfiguration = AppConfigurations.Get(
                typeof(TestProjectBoilerplateCoreMigratorModule).GetAssembly().GetDirectoryPathOrNull()
            );
        }

        public override void PreInitialize()
        {
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                TestProjectBoilerplateCoreConsts.ConnectionStringName
            );

            Configuration.BackgroundJobs.IsJobExecutionEnabled = false;
            Configuration.ReplaceService(
                typeof(IEventBus), 
                () => IocManager.IocContainer.Register(
                    Component.For<IEventBus>().Instance(NullEventBus.Instance)
                )
            );
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TestProjectBoilerplateCoreMigratorModule).GetAssembly());
            ServiceCollectionRegistrar.Register(IocManager);
        }
    }
}
