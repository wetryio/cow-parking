﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Service.Contracts.Request
{
    public class EntityCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DeviceCount { get; set; }
        public string PostCode { get; set; }
    }
}
