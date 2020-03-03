using DeviceProvisioning.Business.Implements;
using System.Threading.Tasks;

namespace DeviceProvisioning
{
    class Program
    {
        public static async Task Main(string[] args)
        {
            ProvisioningBusiness provisioningBusiness = new ProvisioningBusiness();
            await provisioningBusiness.SetupDevice();
        }
    }
}
