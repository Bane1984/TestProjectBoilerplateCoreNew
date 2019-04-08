using Abp.Application.Services.Dto;

namespace TestProjectBoilerplateCore.DTO
{
    public class DeviceTypePropertyDto:EntityDto
    {
        public string NameProperty { get; set; }
        public string Type { get; set; }
        public bool Required { get; set; }
    }
}
