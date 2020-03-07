using Message.Worker.Infrastructure.Data.Tables;
using System.Threading.Tasks;

namespace Message.Worker.Repositories
{
    public interface IDeviceRepository
    {
        Task<int> InsertDevice(DeviceState device);
    }
}
