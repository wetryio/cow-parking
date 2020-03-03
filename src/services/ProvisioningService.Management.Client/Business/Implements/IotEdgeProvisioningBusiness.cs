using Microsoft.Azure.Devices.Provisioning.Service;
using Microsoft.Azure.Devices.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace ProvisioningService.Management.Client.Business.Implements
{
    public class IotEdgeProvisioningBusiness : IIotEdgeProvisioningBusiness
    {
        private readonly ProvisioningServiceClient provisioningServiceClient;
        private static Random random = new Random();

        public IotEdgeProvisioningBusiness()
        {
            provisioningServiceClient = ProvisioningServiceClient.CreateFromConnectionString("HostName=CowParkingDevices.azure-devices-provisioning.net;SharedAccessKeyName=provisioningserviceowner;SharedAccessKey=TojavzxrP8SpAWNqUto+cwbohURmpobEOD5q2MNka08=");
        }


        public async Task CreateEnrollementGroup(string enrollName)
        {
            var primaryKey = Base64Encode(RandomString(50));
            var secondaryKey = Base64Encode(RandomString(50));

            var attestation = new SymmetricKeyAttestation(primaryKey, secondaryKey);
            var enrollementGroup = new EnrollmentGroup(enrollName, attestation);

            var tags = new TwinCollection();

            tags["group-Enrollment"] = enrollName;

            var properties = new TwinCollection()
            {
            };

            enrollementGroup.InitialTwinState = new TwinState(tags, properties);
            enrollementGroup.IotHubs = new List<string>();
            enrollementGroup.IotHubs.Add("CowHub.azure-devices.net");


            enrollementGroup.AllocationPolicy = AllocationPolicy.Hashed;
            enrollementGroup.ProvisioningStatus = ProvisioningStatus.Enabled;
            enrollementGroup.ReprovisionPolicy = new ReprovisionPolicy()
            {
                MigrateDeviceData = true,
                UpdateHubAssignment = true
            };

            await provisioningServiceClient.CreateOrUpdateEnrollmentGroupAsync(enrollementGroup);
        }

        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz/-+@ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static string Base64Encode(string plainText)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(plainText);
            return System.Convert.ToBase64String(plainTextBytes);
        }
    }
}
