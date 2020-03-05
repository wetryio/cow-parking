using Message.Worker.Infrastructure.Data.Tables;
using System.Threading.Tasks;

namespace Message.Worker.Repositories
{
    public interface IDeviceRepository
    {
        Task<DeviceState> GetDevice(string deviceId);
        Task<int> InsertDevice(DeviceState device);
        Task<int> UpdateDevice(string deviceId, bool obstructed, int batteryLevel);
    }
}
