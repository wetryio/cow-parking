import { Component, OnInit } from '@angular/core';
import { DeviceService } from 'src/app/services/device.service';
import { timer } from 'rxjs';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-devices-debug',
  templateUrl: './devices-debug.component.html',
  styleUrls: ['./devices-debug.component.scss']
})
export class DevicesDebugComponent implements OnInit {

  devices: any;
  constructor(private deviceService: DeviceService) {

  }

  ngOnInit(): void {

    this.deviceService.getDevices().subscribe(s => {
      console.log(s);
      this.devices = s;
    });
    setTimeout(() => this.getDevices(), 1000);
  }

  getDevices() {
    this.deviceService.getDevices().subscribe(s => {
      console.log(s);
      this.devices = s;
    });

    setTimeout(() => this.getDevices(), 1000);
  }
}
