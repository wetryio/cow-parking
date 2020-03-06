using Message.Worker.Infrastructure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;

namespace DebugTools.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DebugController : ControllerBase
    {
        private readonly ILogger<DebugController> _logger;

        public DebugController(ILogger<DebugController> logger)
        {
            _logger = logger;
        }

        [HttpGet("devices")]
        public IActionResult Get()
        {
            List<DeviceState> devices = new List<DeviceState>();
            try
            {
                using (SqlConnection connection = new SqlConnection("Server=185.136.235.119;Initial Catalog=DEVICES;User Id=sa;Password=xxQb6FVes;"))
                {
                    connection.Open();
                    string sql = $"SELECT * FROM dbo.DeviceState;";
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
            catch (Exception e)
            {

            }

            return Ok(devices.OrderByDescending(d => d.UpdateAt).ToList());
        }
    }
}
