import { ProvisioningBusiness } from "./Business/ProvisioningBusiness";

let service = new ProvisioningBusiness();
if (service.IsAlreadyRegistered()) {
    console.log("no need to setup device");
} else {
    service.SetupDevice();
}