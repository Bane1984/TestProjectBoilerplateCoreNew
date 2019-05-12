using Abp.Domain.Repositories;
using Abp.ObjectMapping;
//using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using TestProjectBoilerplateCore.DTO;

namespace TestProjectBoilerplateCore.Services.Device
{
    //[Authorize]
    public class DeviceService:TestProjectBoilerplateCoreAppServiceBase, IDeviceService
    {
        private readonly IRepository<Models.Device> _repositoryDevice;
        private readonly IRepository<Models.DeviceTypeProperty> _repositoryDeviceTypeProperty;
        private readonly IRepository<Models.DevicePropertyValue> _repositoryDevicePropertyValue;
        private readonly IObjectMapper _objectMapper;

        public DeviceService(IRepository<Models.Device> repositoryDevice, IRepository<Models.DeviceTypeProperty> repositoryDeviceTypeProperty, IRepository<Models.DevicePropertyValue> repositoryDevicePropertyValue, IObjectMapper objectMapper)
        {
            _repositoryDevice = repositoryDevice;
            _repositoryDeviceTypeProperty = repositoryDeviceTypeProperty;
            _repositoryDevicePropertyValue = repositoryDevicePropertyValue;
            _objectMapper = objectMapper;
        }

        //Listu svih Uredjaja ukljucujuci i Tip uredjaja
        public List<DeviceDto> GetAllDevices()
        {
            var all = _repositoryDevice.GetAll().Include(c => c.DeviceType).ToList();
            var addMap = _objectMapper.Map<List<DeviceDto>>(all);
            return addMap;
        }

        public DeviceDto GetByIdDevice(int id)
        {
            var deviceById = _repositoryDevice.GetAll().Include(c => c.DeviceType)
                .ThenInclude(c => c.DeviceTypeProperties).FirstOrDefault(c => c.Id == id);
            var deviceId = _objectMapper.Map<DeviceDto>(deviceById);
            return deviceId;
        }

        //public void InsertDeviceType(List<Models.Device> devices)
        //{
            
        //}

        //brisanje uredjaja
        public void DeleteDevice(int id)
        {
            var deviceId = _repositoryDevice.Get(id);
            _repositoryDevice.Delete(deviceId);
        }

        //kreiranje uredjaja
        //Front nam salje Id - ukoliko nam posalje Id = 0 (to je prvi korak u kreiranju na onom mokapu sto imamo) kreiramo novi uredjaj
        //bilo koji drugi Id radimo apdejt
        //Id nam salju u trenutku klikom na Save, kada popune sve sto je potreno za kreiranje novog Device-a
        /// <summary>
        /// Creates or update devices.
        /// </summary>
        /// <param name="createDto">The create dto.</param>
        public void CreateUpdateDevices(DeviceCreateDto createDto)
        {
            //dio koji se odnosi na Step 2 - Properties gdje unosimo Ime i Opis uredjaja i za selektovani tip dobijamo spisak propertija
            //i ukoliko ima nestovanih tipova da imamo i spisak nestovanih propertija
            var device = new DeviceCreateDto()
            {
                DeviceName = createDto.DeviceName,
                Description = createDto.Description,
                DeviceTypes = createDto.DeviceTypes
            };

            foreach (var deviceType in device.DeviceTypes)
            {
                var listaDeviceType = new DeviceTypeCreateDeviceDto()
                {
                    DeviceTypeId = deviceType.DeviceTypeId,
                    PropValues = new List<PropertyValuesCreateDeviceDto>()
                };

                foreach (var listaDevTypeProp in listaDeviceType.PropValues)
                {
                    var listaDeviceTypeProperties = new PropertyValuesCreateDeviceDto()
                    {
                        PropName = listaDevTypeProp.PropName,
                        Value = listaDevTypeProp.Value
                    };
                }

                if (createDto.Id == 0)
                {
                    var novi = new Models.Device()
                    {
                        Name = device.DeviceName,
                        Description = device.Description,
                        DevicePropertyValues = new List<Models.DevicePropertyValue>()
                    };

                    var tipovi = createDto.DeviceTypes;

                    foreach (var tip in tipovi)
                    {
                        foreach (var properiValues in tip.PropValues)
                        {
                            var pId = _repositoryDeviceTypeProperty.GetAll().Include(c => c.DeviceType).First(c => c.Name == properiValues.PropName && c.DeviceTypeId == tip.DeviceTypeId).Id;
                            novi.DevicePropertyValues.Add(new Models.DevicePropertyValue()
                            {
                                DeviceId = novi.Id,
                                Value = properiValues.Value,
                                DeviceTypePropertyId = pId
                            });
                        }
                        novi.DeviceTypeId = createDto.DeviceTypes.Max(c => c.DeviceTypeId);
                        _repositoryDevice.Insert(novi);
                    }
                }

                else
                {
                    var updateDevice = _repositoryDevice.GetAll().Include(c => c.DevicePropertyValues).FirstOrDefault(c => c.Id == createDto.Id);
                    updateDevice.Name = createDto.DeviceName;
                    updateDevice.Description = createDto.Description;

                    var tipovi = createDto.DeviceTypes;
                    foreach (var tip in tipovi)
                    {
                        foreach (var prop in tip.PropValues)
                        {
                            var propValue = _repositoryDevicePropertyValue.GetAll().Include(c => c.Device)
                                .Include(x => x.DeviceTypeProperty);

                            var deviceTake = propValue.Where(c =>
                                c.DeviceId == updateDevice.Id && c.DeviceTypeProperty.Name == prop.PropName).First();

                            deviceTake.Value = prop.Value;
                        }
                    }
                }
            }

        }

        
        public List<Models.Device> QuerySearch(QueryInfo.QueryInfo query)
        {
            var device = _repositoryDevice.GetAll().ToList();
            var queryResult = query.QueriInfoExpression<Models.Device>(query, device);

            return queryResult.ToList();
        }

    }
}
