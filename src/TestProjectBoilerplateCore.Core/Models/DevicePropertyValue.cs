using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;

namespace TestProjectBoilerplateCore.Models
{
    public class DevicePropertyValue:Entity
    {
        public string Value { get; set; }

        public int DeviceTypePropertyId { get; set; }
        [ForeignKey("DeviceTypePropertyId")]
        public DeviceTypeProperty DeviceTypeProperty { get; set; }

        public int DeviceId { get; set; }
        [ForeignKey("DeviceId")]
        public Device Device { get; set; }

    }
}
