using Abp.Domain.Repositories;
using Abp.ObjectMapping;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestProjectBoilerplateCore.DTO;
using TestProjectBoilerplateCore.Services.Device;

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

        private readonly IDeviceService _deviceService;


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

        
        //izlistavanje ugnijezdenih tipova
        public List<DeviceTypeNestedDto> GetDeviceTypeNestedDtos(int? parentId)
        {
            var deviceTypes = _repositoryDeviceType.GetAll()
                .Where(c => c.ParentDeviceTypeId == parentId).ToList();

            var result = new List<DeviceTypeNestedDto>();

            foreach (var deviceType in deviceTypes)
            {
                var currentType = new DeviceTypeNestedDto
                {
                    Id = deviceType.Id,
                    Name = deviceType.Name,
                    Description = deviceType.Description,
                    Parentid = deviceType.ParentDeviceTypeId,
                    Children = GetDeviceTypeNestedDtos(deviceType.Id)
                };

                result.Add(currentType);
            }

            return result;
        }

        public IEnumerable<DeviceTypePropertiesDto> GetDeviceTypesFlatList(int? deviceTypeId)
        {
            var type = _repositoryDeviceType.GetAll().Include(x => x.DeviceTypeProperties)
                .First(x => x.Id == deviceTypeId);

            var result = new List<DeviceTypePropertiesDto>();

            var currentType = new DeviceTypePropertiesDto
            {
                Id = type.Id,
                Name = type.Name,
                Description = type.Description,
                ParentId = type.ParentDeviceTypeId,
                Properties = _objectMapper.Map<List<DeviceTypePropertyDto>>(type.DeviceTypeProperties)
            };

            if (type.ParentDeviceTypeId == null)
            {
                result.Add(currentType);
                return result;
            }

            result.Add(currentType);

            return result.Concat(GetDeviceTypesFlatList(type.ParentDeviceTypeId)).OrderBy(x => x.Id);
        }

        //Kreiranje Tipa
        public IEnumerable<DeviceTypePropertiesDto> CreateUpdateDeviceType(DeviceTypeDto deviceTypeDto)
        {
            if (deviceTypeDto.Id == 0)
            {
                Models.DeviceType newType = _objectMapper.Map<Models.DeviceType>(deviceTypeDto);

                var id = _repositoryDeviceType.InsertAndGetId(newType);

                var deviceTypes = GetDeviceTypesFlatList(id);

                return deviceTypes;
            }

            var targetDeviceType = _repositoryDeviceType.Get(deviceTypeDto.Id);

            _objectMapper.Map(deviceTypeDto, targetDeviceType);

            var updatedDeviceTypes = GetDeviceTypesFlatList(targetDeviceType.Id);

            return updatedDeviceTypes;
        }



        // kreiranje propertija tipa
        public void UpdateDeviceTypeProperties(DeviceTypePropertyUpdateDto deviceTypePropertyUpdateDto)
        {
            var deviceType = _repositoryDeviceType.GetAll().Include(x => x.DeviceTypeProperties)
                .First(x => x.Id == deviceTypePropertyUpdateDto.Id);

            foreach (var property in deviceTypePropertyUpdateDto.Properties)
                _repositoryDeviceTypeProperty.Insert(new Models.DeviceTypeProperty
                {
                    Name = property.NameProperty,
                    isRequired = property.Required,
                    Type = property.Type,
                    DeviceTypeId = deviceType.Id
                });
        }


        public IEnumerable<Models.DeviceType> GetDeviceTypeWithChildren(int parentId)
        {
            var type = _repositoryDeviceType.GetAll().Include(x => x.Devices).ThenInclude(x => x.DevicePropertyValues)
                .Include(x => x.DeviceTypeProperties)
                .First(x => x.Id == parentId);

            var children = _repositoryDeviceType.GetAll().Include(x => x.Devices).ThenInclude(x => x.DevicePropertyValues)
                .Include(x => x.DeviceTypeProperties)
                .Where(x => x.ParentDeviceTypeId == parentId).ToList();

            var list = new List<Models.DeviceType>();

            if (!children.Any())
            {
                list.Add(type);
                return list;
            }

            foreach (var child in children)
            {
                list.AddRange(GetDeviceTypeWithChildren(child.Id));
            }

            list.Add(type);
            return list;
        }

        //Brisanje Tipova

        public void Delete(int id)
        {
            var types = GetDeviceTypeWithChildren(id).ToList();

            var orderedTypes = types.OrderByDescending(x => x.Id);

            foreach (var type in orderedTypes)
            {
                var devices = type.Devices;

                foreach (var device in devices)
                {
                    var values = device.DevicePropertyValues;

                    foreach (var value in values)
                    {
                        _repositoryDevicePropertyValue.Delete(value);
                    }

                    _repositoryDevice.Delete(device);
                }

                _repositoryDeviceType.Delete(type);
            }
        }



        //public IEnumerable<DeviceTypePropertiesDto> CreateDeviceType(DeviceTypeDto deviceTypeDto)
        //{
        //    if (deviceTypeDto.Id == 0)
        //    {
        //        var newDeviceType = ObjectMapper.Map<Models.DeviceType>(deviceTypeDto);

        //        var id = _repositoryDeviceType.InsertAndGetId(newDeviceType);

        //        var deviceTypes = _deviceService.GetDeviceTypePropertiesDto()

        //        return deviceTypes;
        //    }

        //    var targetDeviceType = _repositoryDeviceType.Get(deviceTypeDto.Id);

        //    ObjectMapper.Map(deviceTypeDto, targetDeviceType);

        //    var updatedDeviceTypes = GetdDeviceTypePropertiesDtos(targetDeviceType.Id);

        //    return updatedDeviceTypes;
        //}

    }
}
