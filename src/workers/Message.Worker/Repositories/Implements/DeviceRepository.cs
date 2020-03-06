using Message.Worker.Infrastructure.Data.Tables;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace Message.Worker.Repositories.Implements
{
    public class DeviceRepository : IDeviceRepository
    {
        private readonly ILogger<DeviceRepository> logger;

        public DeviceRepository(ILogger<DeviceRepository> logger)
        {
            this.logger = logger;
        }

        public Task<int> InsertDevice(DeviceState device)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=185.136.235.119;Initial Catalog=DEVICES;User Id=sa;Password=xxQb6FVes;"))
                {
                    connection.Open();
                    string sql = "INSERT INTO DeviceStateHistory(DeviceId, Obstructed, BatteryLevel, EventDate) VALUES(@deviceId, @obstructed, @batteryLevel, @updateAt)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.AddWithValue("@deviceId", device.DeviceId);
                        cmd.Parameters.AddWithValue("@obstructed", device.Obstructed);
                        cmd.Parameters.AddWithValue("@batteryLevel", device.BatteryLevel);
                        cmd.Parameters.AddWithValue("@updateAt", device.UpdateAt);
                        cmd.CommandType = CommandType.Text;
                        return Task.FromResult(cmd.ExecuteNonQuery());
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Error on insert to history");
            }

            return Task.FromResult(0);
        }
    }
}
