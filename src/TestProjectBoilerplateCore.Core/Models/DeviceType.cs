using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Abp.Domain.Entities;
using JetBrains.Annotations;

namespace TestProjectBoilerplateCore.Models
{
    public class DeviceType:Entity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        public int? ParentDeviceTypeId { get; set; }
        [CanBeNull]
        [ForeignKey("ParentDeviceTypeId")]
        public DeviceType ParentDeviceType { get; set; }

        public List<Device> Devices { get; set; } = new List<Device>();
        public List<DeviceTypeProperty> DeviceTypeProperties { get; set; } = new List<DeviceTypeProperty>();


    }
}
