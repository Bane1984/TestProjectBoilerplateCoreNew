using Abp.Domain.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TestProjectBoilerplateCore.Models
{
    public class DeviceTypeProperty : Entity
    {
        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public string MachineKey { get; set; }

        public int DeviceTypeId { get; set; }
        [ForeignKey("DeviceTypeId")]
        public DeviceType DeviceType { get; set; }

        //Text, TextArea
        public string Type { get; set; }
        public bool isRequired { get; set; }
    }
}
