using System.Threading.Tasks;

namespace ProvisioningService.Management.Client.Business
{
    public interface IIotEdgeProvisioningBusiness
    {
        Task<(string primaryKey, string secondaryKey)> CreateEnrollementGroup(string enrollName);
        Task<bool> SetupDevice(string deviceId, string pk, string sk, string enrollmentGroup);
    }
}
