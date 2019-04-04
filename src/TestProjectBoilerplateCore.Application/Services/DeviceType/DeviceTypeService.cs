using System;
using System.Collections.Generic;
using System.Text;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using TestProjectBoilerplateCore.Models;

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
    }
}
