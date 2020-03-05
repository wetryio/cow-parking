using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entity.Service.Contracts.Request
{
    public class ProjectCreateRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
