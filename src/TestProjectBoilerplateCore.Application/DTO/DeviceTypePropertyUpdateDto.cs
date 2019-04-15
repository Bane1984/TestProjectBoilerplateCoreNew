using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace TestProjectBoilerplateCore.DTO
{
    public class DeviceTypePropertyUpdateDto : EntityDto
    {
        public List<DeviceTypePropertyDto> Properties { get; set; } = new List<DeviceTypePropertyDto>();
    }
}
