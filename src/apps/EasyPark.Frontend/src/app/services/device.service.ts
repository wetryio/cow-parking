import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class DeviceService {

    private baseUrl = 'http://device-service.services.185.136.234.221.xip.io/';
    // private baseUrl = 'https://localhost:5001/';

    constructor(private http: HttpClient) { }

    entities$(id: string): Observable<any> {
        return this.http.get(this.baseUrl + 'api/device/' + id);
    }

    attributeDevice(device: any) {
        return this.http.post(this.baseUrl + 'api/device/' + device.id, device);
    }
}
