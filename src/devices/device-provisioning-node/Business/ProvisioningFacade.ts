import { ConfigurationDevice } from "../Models/ConfigurationDevice";
import { ProvisioningDeviceClient } from "azure-iot-provisioning-device";
import { Http } from "azure-iot-provisioning-device-http";
import { SymmetricKeySecurityClient } from "azure-iot-security-symmetric-key";
import { ConfigurationFileAccess } from "./ConfigurationFileAccess";
import { PROVISIONING, IDSCOPE, ENROLLMENTGROUPPRIMARYKEY } from "../Consts/Consts";

export class ProvisioningFacade {
    register(config: ConfigurationDevice) {
        const securityClient = new SymmetricKeySecurityClient(config.DeviceId, ENROLLMENTGROUPPRIMARYKEY);
        const client = ProvisioningDeviceClient.create(PROVISIONING, IDSCOPE, new Http(), securityClient);
        client.register((err, result) => {
            if (err ) {
                console.log("unable to register. Please Retry");
                console.log(err);
                return;
            }
            const fileAccess = new ConfigurationFileAccess();
            config.IsRegistered = true;
            fileAccess.StoreConfiguration(config);
        })

    }
}