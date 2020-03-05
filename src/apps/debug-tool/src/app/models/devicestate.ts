export interface DeviceState {
    id: AAGUID;
    deviceId: String;
    obstructed: boolean;
    batteryLevel: number;
    updateAt: Date;
}