let service = new ProvisioningBusiness();
if (service.IsAlreadyRegistered) return; // No need to reload config file
service.SetupDevice();