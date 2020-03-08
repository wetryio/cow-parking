import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { switchMap, take, tap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { EntityService } from 'src/app/services/entity.service';
import * as atlas from 'azure-maps-control';
import { DeviceService } from 'src/app/services/device.service';
import * as _ from 'lodash';
import { AuthenticationType } from 'azure-maps-control';

@Component({
	selector: 'app-entity-detail',
	templateUrl: './entity-detail.component.html',
	styleUrls: ['./entity-detail.component.scss']
})
export class EntityDetailComponent implements OnInit, AfterViewInit {

	private entity$: Observable<any>;
	private map: atlas.Map;

	private device$: Observable<any>;
	private dataSource = new atlas.source.DataSource();

	private devices;

	constructor(private route: ActivatedRoute,
		private router: Router, private entityService: EntityService,
		private deviceService: DeviceService) { }

	ngOnInit() {
		this.entity$ = this.route.paramMap.pipe(
			switchMap((params: ParamMap) =>
				this.entityService.entity$(params.get('id')))
		);


	}

	ngAfterViewInit(): void {
		this.device$ = this.route.paramMap.pipe(
			switchMap((params: ParamMap) => this.deviceService.entities$(params.get('id'))),
			tap(devices => { this.devices = devices; this.initMap(); })
		);
	}

	public initMap() {
		this.map = new atlas.Map('map', {
			center: [5.5303308, 50.5931765],
			zoom: 12,
			language: 'en-US',
			authOptions: {
				authType: AuthenticationType.subscriptionKey,
				subscriptionKey: 'uQoYKL8oYqJSFLOciOLJ1cD3PTti8wlw4wo87oG9Xbs'
			}
		});

		this.map.events.add('ready', () => {
			for (var i = 0; i < this.devices.length; i++) {
				console.log(this.devices[i]);
				var marker = new atlas.HtmlMarker({
					draggable: true,
					htmlContent: '<div class="pulseIcon" deviceid=' + this.devices[i].deviceId + '></div>',
					position: [this.devices[i].lat, this.devices[i].lng]
				});
				this.map.events.add('dragend', marker, (e) => this.updateMarkerPosition(e));
				this.map.markers.add(marker);
			}
		});
	}

	public updateMarkerPosition(e: atlas.TargetedEvent) {
		var deviceId = e.target.getOptions().htmlContent;
		deviceId = deviceId.replace('<div class="pulseIcon" deviceid=', '').replace('></div>', '');
		var tmpDevice: any = _.find(this.devices, (device) => device.deviceId == deviceId);
		console.log(deviceId);
		console.log(tmpDevice);
		console.log(this.devices);

		tmpDevice.lat = e.target.getOptions().position[0];
		tmpDevice.lng = e.target.getOptions().position[1];
		this.deviceService.attributeDevice(tmpDevice).subscribe(s => console.log(s));
	}

	public addMarker(device: any) {
		var marker = new atlas.HtmlMarker({
			draggable: true,
			htmlContent: '<div class="pulseIcon" deviceid=' + device.deviceId + '></div>',
			position: [5.5303308, 50.5931765]
		});
		device.lat = 5.5303308;
		device.lng = 50.5931765;
		this.map.events.add('dragend', marker, (e) => this.updateMarkerPosition(e));
		this.map.markers.add(marker);
	}
}
