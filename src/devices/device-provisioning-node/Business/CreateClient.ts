import { ConfigurationFileAccess } from "./ConfigurationFileAccess";
import { Http } from "azure-iot-provisioning-device-http";
import { ProvisioningDeviceClient } from "azure-iot-provisioning-device"
import { PROVISIONING, IDSCOPE } from "../Consts/Consts";
import { SymmetricKeySecurityClient } from "azure-iot-security-symmetric-key";

import { Client } from "azure-iot-device";

export const CreateClient = () => {

    const fileAccessor = new ConfigurationFileAccess();
    const config = fileAccessor.GetConfiguration();
    if (!config) {
        throw "configuration not found";
    } else {
        const securityClient = new SymmetricKeySecurityClient(config.DeviceId, config.PrimaryKey);
        Client.fromAuthenticationProvider(ss)
    }
}