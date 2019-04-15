using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace TestProjectBoilerplateCore.DTO
{
    public class DeviceTypeCreateDeviceDto : EntityDto
    {
        public int DeviceTypeId { get; set; }
        public List<PropertyValuesCreateDeviceDto> PropValues { get; set; }
    }
}
