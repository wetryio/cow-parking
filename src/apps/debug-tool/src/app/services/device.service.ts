import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class DeviceService {

  debugUrl = "http://debugtools.tools.185.136.234.221.xip.io";
  constructor(private http: HttpClient) { }

  getDevices(contains: string) {
    return this
      .http
      .get(`${this.debugUrl}/debug/devices?contains=${contains}`);
  }
}
