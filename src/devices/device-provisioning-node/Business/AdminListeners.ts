import { CreateClient } from "./CreateClient";
import { Message, Client, DeviceMethodRequest, DeviceMethodResponse } from "azure-iot-device";
const uuid = require('uuid');

export class AdminListeners {

    private client: Client|null;

    constructor() {
        this.client = CreateClient();
        this.client.open(err => {
            if (err) {
                this.client = null;
                throw new Error('Could not connect: ' + err.message);
            }
        });
    }

    public listenMethod1(callback: (request: DeviceMethodRequest, response: DeviceMethodResponse) => void) {
        if (!this.client) {
            throw new Error('client not opened');
        }
        this.client.onDeviceMethod('listenMethod1', callback);
    }

    public sentMessage(message: { hasObstacle: boolean, batteryLevel: number }) {
        const messageObj = new Message(JSON.stringify(message));
        messageObj.messageId = uuid.v4();
        return this.client?.sendEvent(messageObj);
    }

}
