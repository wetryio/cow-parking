using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Worker.Infrastructure.Data.Tables
{
    public class DeviceState
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; }
        public bool Obstructed { get; set; }
        public int BatteryLevel { get; set; }
        public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    }
}
