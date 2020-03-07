import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EntityDetailComponent } from './components/entity-detail/entity-detail.component';
import { EntityRoutingModule } from './entity-routing.module';



@NgModule({
  declarations: [EntityDetailComponent],
  imports: [
    CommonModule,
    EntityRoutingModule
  ],
  providers: [
  ]
})
export class EntityModule { }
