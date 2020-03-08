using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Device.Service.Controllers
{
    public class DeviceState
    {
        public Guid Id { get; set; }
        public string DeviceId { get; set; }
        public bool Available { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
    }


    [ApiController]
    [Route("api/[controller]")]
    public class DeviceController : ControllerBase
    {
        private readonly ILogger<DeviceController> _logger;

        public DeviceController(ILogger<DeviceController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            List<DeviceState> devices = new List<DeviceState>();

            try
            {
                using (SqlConnection connection = new SqlConnection("Server=185.136.235.119;Initial Catalog=DEVICES;User Id=sa;Password=xxQb6FVes;"))
                {
                    connection.Open();
                    string sql = $"SELECT * FROM DeviceRegistration LEFT JOIN DeviceState ON DeviceRegistration.DeviceId = DeviceState.DeviceId WHERE DeviceRegistration.EntityId IS NOT NULL;";
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
                                    Available = !bool.Parse(reader[5].ToString()),
                                    Lat = float.Parse(reader[8].ToString()),
                                    Lng = float.Parse(reader[9].ToString())
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return Ok(devices);
        }

        [HttpGet("{entityId}")]
        public async Task<IActionResult> Get([FromRoute]string entityId)
        {
            List<DeviceState> devices = new List<DeviceState>();

            try
            {
                using (SqlConnection connection = new SqlConnection("Server=185.136.235.119;Initial Catalog=DEVICES;User Id=sa;Password=xxQb6FVes;"))
                {
                    connection.Open();
                    string sql = $"SELECT * FROM DeviceRegistration LEFT JOIN DeviceState ON DeviceRegistration.DeviceId = DeviceState.DeviceId WHERE DeviceRegistration.EntityId = '{entityId}'; ";
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
                                    Lat = float.Parse(reader[8].ToString()),
                                    Lng = float.Parse(reader[9].ToString())
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception)
            {

            }

            return Ok(devices.ToList());
        }

        [HttpPost("{entityId}")]
        public async Task<IActionResult> Post([FromRoute]string entityId, [FromBody]DeviceUpdate request)
        {
            List<DeviceState> devices = new List<DeviceState>();

            try
            {
                using (SqlConnection connection = new SqlConnection("Server=185.136.235.119;Initial Catalog=DEVICES;User Id=sa;Password=xxQb6FVes;"))
                {
                    connection.Open();
                    string sql = $"UPDATE DeviceState SET Latitude = {request.Lat}, Longitude = {request.Lng} WHERE [DeviceState].DeviceId = '{request.DeviceId}'; ";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
            }
            catch (Exception ex)
            {

            }

            return Ok(devices.ToList());
        }
    }


    public class DeviceUpdate
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("deviceId")]
        public string DeviceId { get; set; }

        [JsonProperty("lat")]
        public double Lat { get; set; }

        [JsonProperty("lng")]
        public double Lng { get; set; }

        [JsonProperty("lgn")]
        public double Lgn { get; set; }
    }
}
