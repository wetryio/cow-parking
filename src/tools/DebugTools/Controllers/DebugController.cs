using Message.Worker.Infrastructure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Devices;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace DebugTools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DebugController : ControllerBase
    {
        private readonly ILogger<DebugController> logger;
        private readonly ServiceClient serviceClient;

        public DebugController(ILogger<DebugController> logger)
        {
            this.logger = logger;
            this.serviceClient = ServiceClient.CreateFromConnectionString("HostName=IotAdmin.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=67sJpyzluy5nnrsRBWht/nPptY9LFFrv50z7H6Xm67M=");
        }

        [HttpGet("devices")]
        public IActionResult Get([FromQuery]string contains)
        {
            List<DeviceState> devices = new List<DeviceState>();
            string sql;

            try
            {
                using (SqlConnection connection = new SqlConnection("Server=185.136.235.119;Initial Catalog=DEVICES;User Id=sa;Password=xxQb6FVes;"))
                {
                    connection.Open();

                    if (string.IsNullOrEmpty(contains))
                    {
                        sql = $"SELECT * FROM dbo.DeviceState;";
                    }
                    else
                    {
                        sql = $"SELECT * FROM dbo.DeviceState WHERE DeviceId NOT LIKE '%{contains}%';";
                    }

                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                devices.Add(new DeviceState()
                                {
                                    Id = Guid.Parse(reader[0].ToString()),
                                    DeviceId = reader[1].ToString(),
                                    Obstructed = bool.Parse(reader[2].ToString()),
                                    BatteryLevel = int.Parse(reader[3].ToString()),
                                    UpdateAt = DateTime.Parse(reader[4].ToString())
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return Ok(devices.OrderByDescending(d => d.UpdateAt).ToList());
        }


        [HttpGet("devices/{deviceId}/reset")]
        public async Task<IActionResult> ResetDeviceAsync(string deviceId)
        {
            var methodInvocation = new CloudToDeviceMethod("ResetDevice") { ResponseTimeout = TimeSpan.FromSeconds(30) };
            _ = await serviceClient.InvokeDeviceMethodAsync(deviceId, methodInvocation);
            return Ok();
        }
    }
}
