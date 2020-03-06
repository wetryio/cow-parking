


const GlobalDeviceEndpoint: string = "global.azure-devices-provisioning.net";

export class ProvisioningGenerator {
    // as I am not sure what is doing this scope, I let it as is.
    // for unknown reasons, my raspberry pi cannot read it from /etc/environment nor /etc/profile/device-registration.sh. Not even a reboot.
    private idScope = process.env.SCOPE_ID; // "0ne000CEB9C";
    private EnrollmentGroupPrimaryKey = process.env.PRIMARY_KEY; // "NrEVzOt8iXlv4LlWkFnQms /PPFreOvaHTv84LtHmTvMoSN/GFKiKu3fV7ZjWpuZpBRgrkWDRvbUKjUiMjUFHAg==";
    private EnrollmentGroupSecondaryKey = process.env.SECONDARY_KEY; //"ozi3vfEQ/mLsPVGBAz7Dz3Utne9sR+sshIMaCI5tlpGyaxuwQ1JpRgxiqm4ZBxwy/uY5oz8exztdQA/piHdtrw==";

    Generate(deviceId: string): ProvisioningGeneratorResult {
        if (!this.idScope) {
            console.log("ProvisioningDeviceClientSymmetricKey <IDScope>");
            return ProvisioningGeneratorResult.MissingScopeId;
        }

        let primaryKey = "";
        let secondaryKey = "";
        if (!this.CanGenerateKeys(deviceId)) {
            primaryKey = this.ComputeDerivedSymmetricKey(Convert.FromBase64String(EnrollmentGroupPrimaryKey), deviceId);
            secondaryKey = this.ComputeDerivedSymmetricKey(Convert.FromBase64String(EnrollmentGroupSecondaryKey), deviceId);
        }
        else {
            Console.WriteLine("Invalid configuration provided, must provide group enrollment keys or individual enrollment keys");
            return ProvisioningGeneratorResult.CannotGenerateKeys;
        }

        return

        new ProvisioningGeneratorResult(idScope, primaryKey, secondaryKey);
    }

    private CanGenerateKeys(deviceId: string): boolean {
        return !!deviceId || !!this.EnrollmentGroupPrimaryKey || !!this.EnrollmentGroupSecondaryKey;
    }

    //     /// <summary>
    //     /// Generate the derived symmetric key for the provisioned device from the enrollment group symmetric key used in attestation
    //     /// </summary>
    //     /// <param name="masterKey">Symmetric key enrollment group primary/secondary key value</param>
    //     /// <param name="registrationId">the registration id to create</param>
    //     /// <returns>the primary/secondary key for the member of the enrollment group</returns>
        public static  ComputeDerivedSymmetricKey(masterKey: byte[] , registrationId: string ) :string {
            using(var hmac = new HMACSHA256(masterKey))
                {
                    return Convert.ToBase64String(hmac.ComputeHash(Encoding.UTF8.GetBytes(registrationId)));
        }
    // }
    //     }
    // public class ProvisioningGeneratorResult {
    //     private ProvisioningGeneratorResult(ProvisioningGeneratorResultType type) {
    //         Type = type;
    //     }

    //     internal ProvisioningGeneratorResult(string idScope, string primaryKey, string secondaryKey) {
    //         this.Type = ProvisioningGeneratorResultType.Ok;
    //         this.GlobalDeviceEndpoint = GlobalDeviceEndpoint;
    //         this.IdScope = idScope;
    //         this.PrimaryKey = primaryKey;
    //         this.SecondaryKey = secondaryKey;
    //     }

    //     public ProvisioningGeneratorResultType Type;

    //     public string GlobalDeviceEndpoint { get; }
    //         public string IdScope { get; }
    //         public string PrimaryKey { get; }
    //         public string SecondaryKey { get; }

    // internal static ProvisioningGeneratorResult MissingScopeId => new ProvisioningGeneratorResult(ProvisioningGeneratorResultType.MissingScopeId);
    // internal static ProvisioningGeneratorResult CannotGenerateKeys => new ProvisioningGeneratorResult(ProvisioningGeneratorResultType.CannotGenerateKeys);
}

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