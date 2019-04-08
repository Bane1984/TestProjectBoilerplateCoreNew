using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using TestProjectBoilerplateCore.DTO;

//u cijeloj strukturi prvo kreiramo DeviceType, u njemu propertije a zatim kreiramo Device..
namespace TestProjectBoilerplateCore.Services.DeviceType
{
    public class DeviceTypeService:TestProjectBoilerplateCoreAppServiceBase, IDeviceTypeService
    {
        private readonly IRepository<Models.Device> _repositoryDevice;
        private readonly IRepository<Models.DeviceType> _repositoryDeviceType;
        private readonly IRepository<Models.DeviceTypeProperty> _repositoryDeviceTypeProperty;
        private readonly IRepository<Models.DevicePropertyValue> _repositoryDevicePropertyValue;
        private readonly IObjectMapper _objectMapper;

        public DeviceTypeService(IRepository<Models.Device> repositoryDevice, IRepository<Models.DeviceType> repositoryDeviceType, IRepository<Models.DeviceTypeProperty> repositoryDeviceTypeProperty, IRepository<Models.DevicePropertyValue> repositoryDevicePropertyValue, IObjectMapper objectMapper)
        {
            _repositoryDevice = repositoryDevice;
            _repositoryDeviceType = repositoryDeviceType;
            _repositoryDeviceTypeProperty = repositoryDeviceTypeProperty;
            _repositoryDevicePropertyValue = repositoryDevicePropertyValue;
            _objectMapper = objectMapper;
        }


        public List<DeviceTypeForListDto> GetAllDeviceTypes()
        {
             var getAll = _repositoryDeviceType.GetAll().Include(c => c.ParentDeviceType)
                .ToList();
            List<DeviceTypeForListDto> getAllMap = _objectMapper.Map<List<DeviceTypeForListDto>>(getAll);

            return getAllMap;
        }

        public Models.DeviceType GetByIdDeviceType(int id)
        {
            var getById = _repositoryDeviceType.Get(id);
            return getById;
        }
    }
}
