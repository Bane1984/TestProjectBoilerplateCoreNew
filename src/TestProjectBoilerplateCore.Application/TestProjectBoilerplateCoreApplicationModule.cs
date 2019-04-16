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
                        .ForMember(dest => dest.Parentid, source => source.MapFrom(src => src.ParentDeviceTypeId));

                    cfg.CreateMap<DeviceTypeDto, DeviceType>()
                        .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Description, source => source.MapFrom(src => src.Description))
                        .ForMember(dest => dest.ParentDeviceTypeId, source => source.MapFrom(src => src.Parentid))
                        .ForMember(dest => dest.ParentDeviceType, source => source.Ignore());

                    //zbog potreba Fronta treba umjesto Children stoji Items
                    cfg.CreateMap<DeviceType, DeviceTypeNestedDto>()
                        .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Description, source => source.MapFrom(src => src.Description))
                        .ForMember(dest => dest.Parentid, source => source.MapFrom(src => src.ParentDeviceTypeId))
                        .ForMember(dest => dest.Children, source => source.Ignore());

                    cfg.CreateMap<DeviceTypeProperty, DeviceTypePropertyDto>()
                        .ForMember(dest => dest.NameProperty, source => source.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Required, source => source.MapFrom(src => src.isRequired))
                        .ForMember(dest => dest.Type, source => source.MapFrom(src => src.Type));

                    cfg.CreateMap<DeviceType, DeviceTypePropertiesDto>()
                        .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Description, source => source.MapFrom(src => src.Description))
                        .ForMember(dest => dest.ParentId, source => source.MapFrom(src => src.ParentDeviceType.Id))
                        .ForMember(dest => dest.Properties, source => source.MapFrom(src => src.DeviceTypeProperties));

                    cfg.CreateMap<DeviceTypePropertyUpdateDto, DeviceType>()
                        .ForMember(dest => dest.Id, source => source.MapFrom(src => src.Id))
                        .ForMember(dest => dest.DeviceTypeProperties, source => source.MapFrom(src => src.Properties));

                    cfg.CreateMap<Device, DeviceDto>()
                        .ForMember(dest => dest.Name, source => source.MapFrom(src => src.Name))
                        .ForMember(dest => dest.Description, source => source.MapFrom(src => src.Description))
                        .ForMember(dest => dest.DeviceTypeName, source => source.MapFrom(src => src.DeviceType.Name));

                });
        }
    }
}
