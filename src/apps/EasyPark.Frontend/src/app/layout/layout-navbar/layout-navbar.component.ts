import { Component, Input, HostBinding } from '@angular/core';
import { AppService } from '../../app.service';
import { LayoutService } from '../../layout/layout.service';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-layout-navbar',
  templateUrl: './layout-navbar.component.html',
  styles: [':host { display: block; }']
})
export class LayoutNavbarComponent {
  isExpanded = false;
  isRTL: boolean;

  @Input() sidenavToggle = true;

  @HostBinding('class.layout-navbar') hostClassMain = true;

  constructor(private appService: AppService, private layoutService: LayoutService, public auth: AuthService) {
    this.isRTL = appService.isRTL;
    this.auth.userProfile$.subscribe(s => console.log(s));
  }

  currentBg() {
    return `bg-${this.appService.layoutNavbarBg}`;
  }

  toggleSidenav() {
    this.layoutService.toggleCollapsed();
  }
}
