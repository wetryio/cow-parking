using Microsoft.Azure.EventHubs;
using System.Threading.Tasks;

namespace Message.Worker.Business
{
    public interface IDeviceBusiness
    {
        Task ProcessDevice(EventData eventData);
    }
}
