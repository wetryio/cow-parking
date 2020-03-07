using System;
using System.ComponentModel.DataAnnotations;

namespace Entity.Service.Infrastructure.Data.Tables
{
    public class Entity
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PostCode { get; set; }
        public int DeviceCount { get; set; }

        public DateTime CreateAt { get; set; } = DateTime.UtcNow;
        public string CreateBy { get; set; }
    }
}
