using Microsoft.Azure.Devices;
using Microsoft.Azure.Devices.Provisioning.Service;
using Microsoft.Azure.Devices.Shared;
using Newtonsoft.Json;
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
        private readonly RegistryManager registryManager;
        private readonly ServiceClient serviceClient;

        private static Random random = new Random();

        public IotEdgeProvisioningBusiness()
        {
            registryManager = RegistryManager.CreateFromConnectionString("HostName=CowHub.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=rc2TO9FzL8IQpQkmwdEXct8Tv5fJzchVgexEQJ6nPGk=");
            provisioningServiceClient = ProvisioningServiceClient.CreateFromConnectionString("HostName=CowParkingDevices.azure-devices-provisioning.net;SharedAccessKeyName=provisioningserviceowner;SharedAccessKey=TojavzxrP8SpAWNqUto+cwbohURmpobEOD5q2MNka08=");
            serviceClient = ServiceClient.CreateFromConnectionString("HostName=IotAdmin.azure-devices.net;SharedAccessKeyName=iothubowner;SharedAccessKey=67sJpyzluy5nnrsRBWht/nPptY9LFFrv50z7H6Xm67M=");
        }

        public async Task<bool> SetupDevice(string deviceId, string pk, string sk, string enrollmentGroup)
        {
            var methodInvocation = new CloudToDeviceMethod("SetupDevice") { ResponseTimeout = TimeSpan.FromSeconds(30) };
            var deviceSetupeRequest = new
            {
                primaryKey = pk,
                secondaryKey = sk,
                enrollmentGroup = enrollmentGroup
            };
            
            methodInvocation.SetPayloadJson(JsonConvert.SerializeObject(deviceSetupeRequest));
            var setupDeviceResponse = await serviceClient.InvokeDeviceMethodAsync(deviceId, methodInvocation);

            return setupDeviceResponse.Status == 200;
        }

        public async Task<(string primaryKey, string secondaryKey)> CreateEnrollementGroup(string enrollName)
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

            return (primaryKey, secondaryKey);
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
