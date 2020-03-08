import { ConfigurationFileAccess } from "./ConfigurationFileAccess";
import { ProvisioningDeviceClient } from "azure-iot-provisioning-device"
import { PROVISIONING, IDSCOPE, PROVISIONING_CONNECTION_STRING } from "../Consts/Consts";
import { SymmetricKeySecurityClient } from "azure-iot-security-symmetric-key";

import { Client } from "azure-iot-device";
import { Mqtt } from "azure-iot-device-mqtt";
// import { Http } from "azure-iot-device-http";

export const CreateClient = () => {

    const fileAccessor = new ConfigurationFileAccess();
    const config = fileAccessor.GetConfiguration();
    // const config = { DeviceId: '10000000497b8d77' }
    if (!config) {
        throw "configuration not found";
    } else {
        // const securityClient = new SymmetricKeySecurityClient(config.DeviceId, config.PrimaryKey);
        const connectionString = `${PROVISIONING_CONNECTION_STRING};DeviceId=${config.DeviceId}`;
        // const connectionString = `HostName=${YourIoTHubName}.azure-devices.net;DeviceId=${config.DeviceId};SharedAccessKey=${YourSharedAccessKey}`;
        const client = Client.fromConnectionString(connectionString, Mqtt);
        client.on('error', function (err) {
            console.error(err.message);
            process.exit(-1);
        });
        return client;
    }
}