import { ConfigurationFileAccess } from "./ConfigurationFileAccess";
import { ProvisioningGenerator, ProvisioningGeneratorResultType } from "./ProvisionningGenerator";
import * as fs from "fs";
import { ConfigurationDevice } from "../Models/ConfigurationDevice";

export class ProvisioningBusiness {

    private fileAccessor = new ConfigurationFileAccess();
    private provisioningGenerator = new ProvisioningGenerator();
    private generatedDeviceId = this.ReadDeviceId();

    public SetupDevice(): void {
        const result = this.provisioningGenerator.Generate(this.generatedDeviceId);
        if (result.Type !== ProvisioningGeneratorResultType.Ok) {
            throw `Cannot generate info for configuration. type : ${result.Type}`;
        }
        const configuration: ConfigurationDevice = {
            DeviceId: this.generatedDeviceId,
            GlobalDeviceEndpoint: result.GlobalDeviceEndpoint ?? "",
            IdScope: result.IdScope ?? "",
            PrimaryKey: result.PrimaryKey ?? "",
            SecondaryKey: result.SecondaryKey ?? "",
        };
        this.fileAccessor.StoreConfiguration(configuration);

        // TODO: Use ProvisioningDeviceClient to register in azure
    }

    // // If file exists, it means that device is already registered and nothing is needed
    IsAlreadyRegistered(): boolean {
        if (!this.fileAccessor.IsAlreadyRegistered) {
            return false;
        }
        else {
            return this.IsDeviceIdFromConfigIsMatching();
        };
    }

    private IsDeviceIdFromConfigIsMatching(): boolean {
        const configuration = this.fileAccessor.GetConfiguration();
        return this.generatedDeviceId != configuration?.DeviceId;
    }

    private ReadDeviceId(): string {
        const deviceInfos = fs.readFileSync("/proc/cpuinfo", "utf8");
        if (!deviceInfos) {
            throw "Impossible to extract serial number from /proc/cpuinfo file";
        }
        const reg = new RegExp("Serial\s*:\s([0-9a-f]{16})");
        const results = reg.exec(deviceInfos);

        if (!results) {
            throw "No regex match";
        } else {
            const firstResult = results[0];
            return firstResult
                .replace(" ", "")
                .split(":")[1];
        }

    }
}
