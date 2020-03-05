using Message.Worker.Infrastructure.Data.Tables;
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
        public async Task<DeviceState> GetDevice(string deviceId)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=185.136.235.119;Initial Catalog=DEVICES;User Id=sa;Password=xxQb6FVes;"))
                {
                    connection.Open();
                    string sql = $"SELECT * FROM dbo.DeviceState WHERE DeviceId = '{deviceId}';";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                return new DeviceState()
                                {
                                    Id = Guid.Parse(reader[0].ToString()),
                                    DeviceId = reader[1].ToString(),
                                    Obstructed = bool.Parse(reader[2].ToString()),
                                    BatteryLevel = int.Parse(reader[3].ToString()),
                                    UpdateAt = DateTime.Parse(reader[4].ToString())
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {

            }

            return null;
        }

        public Task<int> InsertDevice(DeviceState device)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=185.136.235.119;Initial Catalog=DEVICES;User Id=sa;Password=xxQb6FVes;"))
                {
                    connection.Open();
                    string sql = "INSERT INTO DeviceState(DeviceId, Obstructed, BatteryLevel, UpdateAt) VALUES(@deviceId, @obstructed, @batteryLevel, @updateAt)";
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

            }

            return Task.FromResult(0);
        }

        public Task<int> UpdateDevice(string deviceId, bool obstructed, int batteryLevel)
        {
            var isObstructed = (obstructed) ? 1 : 0;

            try
            {
                using (SqlConnection connection = new SqlConnection("Server=185.136.235.119;Initial Catalog=DEVICES;User Id=sa;Password=xxQb6FVes;"))
                {
                    connection.Open();
                    string sql = $"UPDATE DeviceState SET Obstructed = {isObstructed}, BatteryLevel = {batteryLevel}, UpdateAt = '{DateTime.UtcNow}' WHERE DeviceId = '{deviceId}'";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.CommandType = CommandType.Text;
                        return Task.FromResult(cmd.ExecuteNonQuery());
                    }
                }
            }
            catch (Exception e)
            {

            }

            return Task.FromResult(0);
        }
    }
}
