using System;
using System.Collections.Generic;
using System.Text;

namespace Message.Worker.Contracts.Event
{
    public class DeviceEvent
    {
        public bool HasObstacle { get; set; }
        public double BatteryLevel { get; set; }
    }
}
