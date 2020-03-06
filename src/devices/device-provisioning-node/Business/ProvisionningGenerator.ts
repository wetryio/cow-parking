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
    // as I am not sure what is doing this scope, I let it as is.
    // for unknown reasons, my raspberry pi cannot read it from /etc/environment nor /etc/profile/device-registration.sh. Not even a reboot.
    private idScope = process.env.SCOPE_ID; // "0ne000CEB9C";
    private EnrollmentGroupPrimaryKey = process.env.PRIMARY_KEY; // "NrEVzOt8iXlv4LlWkFnQms /PPFreOvaHTv84LtHmTvMoSN/GFKiKu3fV7ZjWpuZpBRgrkWDRvbUKjUiMjUFHAg==";
    private EnrollmentGroupSecondaryKey = process.env.SECONDARY_KEY; //"ozi3vfEQ/mLsPVGBAz7Dz3Utne9sR+sshIMaCI5tlpGyaxuwQ1JpRgxiqm4ZBxwy/uY5oz8exztdQA/piHdtrw==";

    Generate(deviceId: string): ProvisioningGeneratorResult {
        if (!this.idScope) {
            console.log("ProvisioningDeviceClientSymmetricKey <IDScope>");
            return ProvisioningGeneratorResult.MissingScopeId();
        }

        let primaryKey = "";
        let secondaryKey = "";
        if (!this.CanGenerateKeys(deviceId)) {
            primaryKey = this.ComputeDerivedSymmetricKey(this.EnrollmentGroupPrimaryKey ?? "", deviceId);
            secondaryKey = this.ComputeDerivedSymmetricKey(this.EnrollmentGroupSecondaryKey ?? "", deviceId);
        }
        else {
            console.log("Invalid configuration provided, must provide group enrollment keys or individual enrollment keys");
            return ProvisioningGeneratorResult.CannotGenerateKeys();
        }

        return new ProvisioningGeneratorResult({ IdScope: this.idScope, PrimaryKey: primaryKey, SecondaryKey: secondaryKey });
    }

    CanGenerateKeys(deviceId: string): boolean {
        return !!deviceId || !!this.EnrollmentGroupPrimaryKey || !!this.EnrollmentGroupSecondaryKey;
    }


    ComputeDerivedSymmetricKey(masterKey: string, registrationId: string): string {
        const hmac = crypto.createHmac("sha256", masterKey);
        hmac.update(registrationId);
        return hmac.digest('hex');
    }
}