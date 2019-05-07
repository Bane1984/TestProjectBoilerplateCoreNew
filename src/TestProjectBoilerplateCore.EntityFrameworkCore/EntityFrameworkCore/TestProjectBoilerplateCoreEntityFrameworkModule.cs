using Abp.EntityFrameworkCore.Configuration;
using Abp.IdentityServer4;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Zero.EntityFrameworkCore;
using TestProjectBoilerplateCore.EntityFrameworkCore.Seed;

namespace TestProjectBoilerplateCore.EntityFrameworkCore
{
    [DependsOn(
        typeof(TestProjectBoilerplateCoreCoreModule), 
        typeof(AbpZeroCoreEntityFrameworkCoreModule), typeof(AbpZeroCoreIdentityServerEntityFrameworkCoreModule))]
    public class TestProjectBoilerplateCoreEntityFrameworkModule : AbpModule
    {
        /* Used it tests to skip dbcontext registration, in order to use in-memory database of EF Core */
        public bool SkipDbContextRegistration { get; set; }

        public bool SkipDbSeed { get; set; }

        public override void PreInitialize()
        {
            if (!SkipDbContextRegistration)
            {
                Configuration.Modules.AbpEfCore().AddDbContext<TestProjectBoilerplateCoreDbContext>(options =>
                {
                    if (options.ExistingConnection != null)
                    {
                        TestProjectBoilerplateCoreDbContextConfigurer.Configure(options.DbContextOptions, options.ExistingConnection);
                    }
                    else
                    {
                        TestProjectBoilerplateCoreDbContextConfigurer.Configure(options.DbContextOptions, options.ConnectionString);
                    }
                });
            }
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(TestProjectBoilerplateCoreEntityFrameworkModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            if (!SkipDbSeed)
            {
                SeedHelper.SeedHostDb(IocManager);
            }
        }
    }
}
