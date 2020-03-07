import { Component, OnInit, ViewChild, AfterViewInit } from '@angular/core';
import { ActivatedRoute, Router, ParamMap } from '@angular/router';
import { switchMap } from 'rxjs/operators';
import { Observable } from 'rxjs';
import { EntityService } from 'src/app/services/entity.service';
import { } from 'googlemaps';

@Component({
	selector: 'app-entity-detail',
	templateUrl: './entity-detail.component.html',
	styleUrls: ['./entity-detail.component.scss']
})
export class EntityDetailComponent implements OnInit, AfterViewInit {

	private entity$: Observable<any>;
	@ViewChild('map', { static: true })
	mapElement: any;
	map: google.maps.Map;

	constructor(private route: ActivatedRoute,
		private router: Router, private entityService: EntityService) { }

	ngOnInit() {
		this.entity$ = this.route.paramMap.pipe(
			switchMap((params: ParamMap) =>
				this.entityService.entity$(params.get('id')))
		);
	}

	ngAfterViewInit(): void {
		const mapProperties = {
			center: new google.maps.LatLng(50.5931765, 5.5303308),
			zoom: 15,
			mapTypeId: google.maps.MapTypeId.ROADMAP
		};
		this.map = new google.maps.Map(this.mapElement.nativeElement, mapProperties);
		var marker = new google.maps.Marker(
			{
				map: this.map,
				draggable: true,
				animation: google.maps.Animation.DROP,
				position: new google.maps.LatLng(50.5931765, 5.5303308)
			});

		google.maps.event.addListener(marker, 'dragend', function () {
			console.log(marker.getPosition().toJSON());
		});
	}
}
