using DeviceProvisioning.Business.Implements;
using System.Threading.Tasks;

namespace DeviceProvisioning
{
    class Program
    {
        public static void Main(string[] args)
        {
            var service = new ProvisioningBusiness();
            if (service.IsAlreadyRegistered) return; // No need to reload config file
            service.SetupDevice();
        }
    }
}
