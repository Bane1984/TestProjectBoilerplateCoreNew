using Abp.AutoMapper;
using Abp.Modules;
using Abp.Reflection.Extensions;
using TestProjectBoilerplateCore.Authorization;
using TestProjectBoilerplateCore.DTO;
using TestProjectBoilerplateCore.Models;

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
                cfg =>
                {
                    cfg.AddProfiles(thisAssembly);
                    cfg.CreateMap<DeviceType, DeviceTypeDto>()
                        .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Description, source => source.MapFrom(src => src.Description))
                        .ForMember(dest => dest.Parentid, source => source.MapFrom(src => src.ParentDeviceTypeId))
                        .ForMember(dest => dest.Properties, source => source.MapFrom(src => src.DeviceTypeProperties));

                    cfg.CreateMap<DeviceType, DeviceTypeForListDto>()
                        .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Description, source => source.MapFrom(src => src.Description))
                        .ForMember(dest => dest.Parentid, source => source.MapFrom(src => src.ParentDeviceTypeId));

                    cfg.CreateMap<DeviceType, DeviceTypeNestedDto>()
                        .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Description, source => source.MapFrom(src => src.Description))
                        .ForMember(dest => dest.Parentid, source => source.MapFrom(src => src.ParentDeviceTypeId))
                        .ForMember(dest => dest.Children, source => source.Ignore());

                    cfg.CreateMap<DeviceTypeProperty, DeviceTypePropertyDto>()
                        .ForMember(dest => dest.NameProperty, source => source.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Required, source => source.MapFrom(src => src.isRequired))
                        .ForMember(dest => dest.Type, source => source.MapFrom(src => src.Type));
                });
        }
    }
}
