﻿using System;
using System.Collections.Generic;
using System.Text;
using Abp.Application.Services.Dto;

namespace TestProjectBoilerplateCore.DTO
{
    public class PropertyValuesCreateDeviceDto : EntityDto
    {
        public string PropName { get; set; }
        public string Value { get; set; }
    }
}
