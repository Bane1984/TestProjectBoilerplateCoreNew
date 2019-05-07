using Abp.Application.Services.Dto;
using System.Collections.Generic;

namespace TestProjectBoilerplateCore.DTO
{
    public class DeviceTypeNestedDto : EntityDto
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public int? Parentid { get; set; }

        //promijeniti Children u Items
        public List<DeviceTypeNestedDto> Children { get; set; } = new List<DeviceTypeNestedDto>();
    }
}
