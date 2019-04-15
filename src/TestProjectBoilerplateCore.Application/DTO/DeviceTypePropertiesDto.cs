using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace TestProjectBoilerplateCore.DTO
{
    public class DeviceTypePropertiesDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? ParentId { get; set; }

        public List<DeviceTypePropertyDto> Properties { get; set; } = new List<DeviceTypePropertyDto>();
    }
}
