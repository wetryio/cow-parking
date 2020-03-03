using System;

namespace Project.Service.Infrastructure.Data.Tables
{
    public class Project
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }


        public DateTime CreateAt { get; set; }
        public string CreateBy { get; set; }
    }
}
