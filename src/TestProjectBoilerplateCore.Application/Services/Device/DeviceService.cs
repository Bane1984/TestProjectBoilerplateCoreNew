using System.Collections.Generic;
using System.Linq;
using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using TestProjectBoilerplateCore.Models;

namespace TestProjectBoilerplateCore.Services.Device
{
    public class DeviceService:TestProjectBoilerplateCoreAppServiceBase, IDeviceService
    {
        private readonly IRepository<Models.Device> _repositoryDevice;
        private readonly IRepository<Models.DeviceType> _repositoryDeviceType;
        private readonly IRepository<Models.DeviceTypeProperty> _repositoryDeviceTypeProperty;
        private readonly IRepository<Models.DevicePropertyValue> _repositoryDevicePropertyValue;
        private readonly IObjectMapper _objectMapper;

        public DeviceService(IRepository<Models.Device> repositoryDevice, IRepository<Models.DeviceType> repositoryDeviceType, IRepository<Models.DeviceTypeProperty> repositoryDeviceTypeProperty, IRepository<Models.DevicePropertyValue> repositoryDevicePropertyValue, IObjectMapper objectMapper)
        {
            _repositoryDevice = repositoryDevice;
            _repositoryDeviceType = repositoryDeviceType;
            _repositoryDeviceTypeProperty = repositoryDeviceTypeProperty;
            _repositoryDevicePropertyValue = repositoryDevicePropertyValue;
            _objectMapper = objectMapper;
        }

        //Listu svih Uredjaja ukljucujuci i Tip uredjaja i Propertije
        public List<Models.Device> GetAllDevices()
        {
            var all = _repositoryDevice.GetAll().Include(c => c.DeviceType).ThenInclude(c => c.DeviceTypeProperties)
                .ToList();
            return all;
        }

        public Models.Device GetByIdDevice(int id)
        {
            var deviceById = _repositoryDevice.GetAll().Include(c => c.DeviceType)
                .ThenInclude(c => c.DeviceTypeProperties).FirstOrDefault(c => c.Id == id);
            return deviceById;
        }

        public void InsertDeviceType(List<Models.Device> devices)
        {
            
        }

    }
}
