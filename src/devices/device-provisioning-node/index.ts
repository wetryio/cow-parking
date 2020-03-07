import { ProvisioningBusiness } from "./Business/ProvisioningBusiness";
import { AdminListeners } from "./Business/AdminListeners";
import { runDeviceController } from './Business/DeviceController';

const provisioningBusiness = new ProvisioningBusiness();
if (provisioningBusiness.IsAlreadyRegistered()) {
    console.log("no need to setup device");
} else {
    provisioningBusiness.SetupDevice();
}

// Admin management
const adminListener = new AdminListeners();

adminListener.listenMethod1((req, resp) => {
    console.log(req, resp);
});

adminListener.sentMessage({ hasObstable: false, batteryLevel: 10 })?.then(() => {
    console.log('sent');
});

// Device management
runDeviceController((status: boolean) => {
    console.log(status);
});
