import { ProvisioningBusiness } from "./Business/ProvisioningBusiness";

let service = new ProvisioningBusiness();
if (!service.IsAlreadyRegistered) {
    service.SetupDevice();
}