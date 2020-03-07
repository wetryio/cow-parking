using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Service.Contracts.Response
{
    public class EntityResponse
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PostCode { get; set; }
        public int DeviceCount { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
