using System;

namespace Entity.Service.Infrastructure.Data.Tables
{
    public class Entity
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }
    }
}
