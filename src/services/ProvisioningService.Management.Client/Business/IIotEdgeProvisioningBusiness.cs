using System.Threading.Tasks;

namespace ProvisioningService.Management.Client.Business
{
    public interface IIotEdgeProvisioningBusiness
    {
        Task CreateEnrollementGroup(string enrollName);
    }
}
