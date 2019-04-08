using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace TestProjectBoilerplateCore.DTO
{
    public class DeviceTypeDto:EntityDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int? Parentid { get; set; }
        public List<DeviceTypePropertyDto> Properties { get; set; } = new List<DeviceTypePropertyDto>();
    }
}
