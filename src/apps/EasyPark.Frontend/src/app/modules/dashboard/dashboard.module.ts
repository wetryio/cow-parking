import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';

import { DashboardRoutingModule } from './dashboard-routing.module';
import { DashboardComponent } from './dashboard.component';
import { HomeComponent } from './components/home/home.component';


@NgModule({
  declarations: [DashboardComponent, HomeComponent],
  imports: [
    CommonModule,
    DashboardRoutingModule
  ],
  providers: [
  ]
})
export class DashboardModule { }
