import { ENROLLMENTGROUPPRIMARYKEY, ENROLLMENTGROUPSECONDARYKEY, IDSCOPE } from "../Consts/Consts";

const crypto = require("crypto");


const GlobalDeviceEndpoint: string = "global.azure-devices-provisioning.net";

export enum ProvisioningGeneratorResultType {
    MissingScopeId,
    CannotGenerateKeys,
    Ok
}

export class ProvisioningGeneratorResult {
    public GlobalDeviceEndpoint: string | null = null;
    public IdScope: string | null = null;
    public PrimaryKey: string | null = null;
    public SecondaryKey: string | null = null;
    public Type: ProvisioningGeneratorResultType | null = null;

    public constructor(obj: Partial<ProvisioningGeneratorResult>) {
        this.Type = obj.Type ?? ProvisioningGeneratorResultType.Ok;
        this.GlobalDeviceEndpoint = GlobalDeviceEndpoint;
        this.IdScope = obj.IdScope ?? null;
        this.PrimaryKey = obj.PrimaryKey ?? null;
        this.SecondaryKey = obj.SecondaryKey ?? null;
    }

    static MissingScopeId = (): ProvisioningGeneratorResult =>
        new ProvisioningGeneratorResult({ Type: ProvisioningGeneratorResultType.MissingScopeId })
    static CannotGenerateKeys = (): ProvisioningGeneratorResult =>
        new ProvisioningGeneratorResult({ Type: ProvisioningGeneratorResultType.CannotGenerateKeys })
}

export class ProvisioningGenerator {
    Generate(deviceId: string): ProvisioningGeneratorResult {
        if (this.CanGenerateKeys(deviceId)) {
            const primaryKey = this.ComputeDerivedSymmetricKey(ENROLLMENTGROUPPRIMARYKEY ?? "", deviceId);
            const secondaryKey = this.ComputeDerivedSymmetricKey(ENROLLMENTGROUPSECONDARYKEY ?? "", deviceId);
            console.log('ProvisioningGenerator.Generate successful');
            return new ProvisioningGeneratorResult({ IdScope: IDSCOPE, PrimaryKey: primaryKey, SecondaryKey: secondaryKey });
        } else {
            console.log("Invalid configuration provided, must provide group enrollment keys or individual enrollment keys");
            return ProvisioningGeneratorResult.CannotGenerateKeys();
        }
    }

    CanGenerateKeys(deviceId: string): boolean {
        return !!deviceId || !!ENROLLMENTGROUPPRIMARYKEY || !!ENROLLMENTGROUPSECONDARYKEY;
    }

    ComputeDerivedSymmetricKey(masterKey: string, registrationId: string): string {
        const hmac = crypto.createHmac("sha256", Buffer.from(masterKey, "base64"));
        hmac.update(registrationId);
        return hmac.digest('base64');
    }
}