using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TestProjectBoilerplateCore.DTO;

namespace TestProjectBoilerplateCore.Services.DeviceTypeProperty
{
    public class DeviceTypePropertyService:TestProjectBoilerplateCoreAppServiceBase, IDeviceTypePropertyService
    {
        private readonly IRepository<Models.Device> _repositoryDevice;
        private readonly IRepository<Models.DeviceType> _repositoryDeviceType;
        private readonly IRepository<Models.DeviceTypeProperty> _repositoryDeviceTypeProperty;
        private readonly IRepository<Models.DevicePropertyValue> _repositoryDevicePropertyValue;
        private readonly IObjectMapper _objectMapper;

        public DeviceTypePropertyService(IRepository<Models.Device> repositoryDevice, IRepository<Models.DeviceType> repositoryDeviceType, IRepository<Models.DeviceTypeProperty> repositoryDeviceTypeProperty, IRepository<Models.DevicePropertyValue> repositoryDevicePropertyValue, IObjectMapper objectMapper)
        {
            _repositoryDevice = repositoryDevice;
            _repositoryDeviceType = repositoryDeviceType;
            _repositoryDeviceTypeProperty = repositoryDeviceTypeProperty;
            _repositoryDevicePropertyValue = repositoryDevicePropertyValue;
            _objectMapper = objectMapper;
        }


        //uzimanje liste propertija isto kao uzimanje ugnijezdenih tipova GetDeviceTypeNestedDtos
        public IEnumerable<DeviceTypePropertiesDto> GetDeviceTypePropertiesDto(int? deviceTypeId)
        {
            var deviceType = _repositoryDeviceType.GetAll().Include(c => c.DeviceTypeProperties)
                .First(c => c.Id == deviceTypeId);


            var newDeviceTypePropertiesDto = new DeviceTypePropertiesDto()
            {
                Id = deviceType.Id,
                Name = deviceType.Name,
                Description = deviceType.Description,
                ParentId = deviceType.ParentDeviceTypeId,
                Properties = _objectMapper.Map<List<DeviceTypePropertyDto>>(deviceType.DeviceTypeProperties)

            };

            var result = new List<DeviceTypePropertiesDto>();

            if (deviceType.ParentDeviceTypeId == null)
            {
                result.Add(newDeviceTypePropertiesDto);
                return result;
            }

            result.Add(newDeviceTypePropertiesDto);

            return result.Concat(GetDeviceTypePropertiesDto(deviceType.ParentDeviceTypeId)).OrderBy(c => c.Id);

        }
    }
}
