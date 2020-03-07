import { Component, OnInit } from '@angular/core';

import { DeviceService } from 'src/app/services/device.service';
import { DeviceState } from './../../models/devicestate';
import { take } from 'rxjs/operators';
import { timer } from 'rxjs';

@Component({
  selector: 'app-devices-debug',
  templateUrl: './devices-debug.component.html',
  styleUrls: ['./devices-debug.component.scss']
})
export class DevicesDebugComponent implements OnInit {
  displayedColumns: string[] = ['id', 'deviceId', 'obstructed', 'batteryLevel', 'updateAt'];
  devices: DeviceState[];

  constructor(private deviceService: DeviceService) {

  }

  ngOnInit(): void {

    this.deviceService.getDevices().subscribe(s => {
      console.log(s);
      this.devices = s as DeviceState[];
    });
    setTimeout(() => this.getDevices(), 1000);
  }

  getDevices() {
    this.deviceService.getDevices().subscribe(s => {
      console.log(s);
      this.devices = s as DeviceState[];
    });

    setTimeout(() => this.getDevices(), 1000);
  }
}
