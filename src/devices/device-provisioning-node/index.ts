import { ProvisioningBusiness } from "./Business/ProvisioningBusiness";

const provisioningBusiness = new ProvisioningBusiness();
if (provisioningBusiness.IsAlreadyRegistered()) {
    console.log("no need to setup device");
} else {
    provisioningBusiness.SetupDevice();
}