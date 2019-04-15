using Abp.Application.Services.Dto;

namespace TestProjectBoilerplateCore.DTO
{
    public class DeviceDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string DeviceTypeName { get; set; }

    }
}
