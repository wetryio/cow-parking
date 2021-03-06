import { Component, Input, AfterViewInit, HostBinding, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AppService } from '../../app.service';
import { LayoutService } from '../layout.service';
import { Project } from 'src/app/models/project.model';
import { NgbModal, ModalDismissReasons } from '@ng-bootstrap/ng-bootstrap';
import { EntityService } from 'src/app/services/entity.service';

@Component({
	selector: 'app-layout-sidenav',
	templateUrl: './layout-sidenav.component.html',
	styles: [':host { display: block; }']
})
export class LayoutSidenavComponent implements AfterViewInit, OnInit {
	@Input() orientation = 'vertical';

	@HostBinding('class.layout-sidenav') hostClassVertical = false;
	@HostBinding('class.layout-sidenav-horizontal') hostClassHorizontal = false;
	@HostBinding('class.flex-grow-0') hostClassFlex = false;

	public entityName: string;
	public deviceCount = 10;
	public description: string;
	public postCode: string;

	public entityList: Project[] = new Array<Project>();

	constructor(private router: Router, private appService: AppService, private projectService: EntityService, private layoutService: LayoutService,
		private modalService: NgbModal) {
		// Set host classes
		this.hostClassVertical = this.orientation !== 'horizontal';
		this.hostClassHorizontal = !this.hostClassVertical;
		this.hostClassFlex = this.hostClassHorizontal;
	}

	ngAfterViewInit() {
		// Safari bugfix
		this.layoutService._redrawLayoutSidenav();
	}

	ngOnInit() {
		this.projectService.entities$().subscribe(s => {
			this.entityList = s;
			console.log(this.entityList);
		});
	}

	getClasses() {
		let bg = this.appService.layoutSidenavBg;

		if (this.orientation === 'horizontal' && (bg.indexOf(' sidenav-dark') !== -1 || bg.indexOf(' sidenav-light') !== -1)) {
			bg = bg
				.replace(' sidenav-dark', '')
				.replace(' sidenav-light', '')
				.replace('-darker', '')
				.replace('-dark', '');
		}

		return `${this.orientation === 'horizontal' ? 'container-p-x ' : ''} bg-${bg}`;
	}

	isActive(url) {
		return window.location.href.indexOf(url) > -1;
	}

	isMenuActive(url) {
		return this.router.isActive(url, false);
	}

	isMenuOpen(url) {
		return this.router.isActive(url, false) && this.orientation !== 'horizontal';
	}

	toggleSidenav() {
		this.layoutService.toggleCollapsed();
	}

	createProject() {
		this.projectService
			.entityCreateResponse$(this.entityName, this.description, this.deviceCount, this.postCode)
			.subscribe((s: any) => {
				this.projectService.entities$().subscribe(s => this.entityList = s);
				this.modalService.dismissAll();
				this.router.navigate(['/entity', s.id]);
			});
	}

	open(content, options = {}) {
		this.modalService.open(content, options).result.then((result) => {
			console.log(`Closed with: ${result}`);
		}, (reason) => {
			console.log(`Dismissed ${this.getDismissReason(reason)}`);
		});
	}

	private getDismissReason(reason: any): string {
		if (reason === ModalDismissReasons.ESC) {
			return 'by pressing ESC';
		} else if (reason === ModalDismissReasons.BACKDROP_CLICK) {
			return 'by clicking on a backdrop';
		} else {
			return `with: ${reason}`;
		}
	}
}
