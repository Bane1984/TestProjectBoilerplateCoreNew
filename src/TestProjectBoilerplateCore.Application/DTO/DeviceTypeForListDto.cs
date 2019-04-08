using Abp.Application.Services.Dto;

namespace TestProjectBoilerplateCore.DTO
{
    public class DeviceTypeForListDto : EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Parentid { get; set; }
    }
}
