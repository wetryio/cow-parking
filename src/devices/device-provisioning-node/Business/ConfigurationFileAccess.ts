import * as fs from "fs";
import { ConfigurationDevice } from "../Models/ConfigurationDevice";

export class ConfigurationFileAccess {
    private ConfigurationFile = "/etc/cow-device.conf";

    // if file exists, it means that device is already registered and nothing is needed
    public IsAlreadyRegistered = fs.existsSync(this.ConfigurationFile);

    public GetConfiguration(): ConfigurationDevice {
        const fileContent: string = fs.readFileSync(this.ConfigurationFile, "utf8");
        return JSON.parse(fileContent);
    }

    public StoreConfiguration(configuration: ConfigurationDevice): void {
        console.log('write file');
        fs.writeFileSync(this.ConfigurationFile, JSON.stringify(configuration));
    }
}