import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';

import { EntityDetailComponent } from './components/entity-detail/entity-detail.component';

const routes: Routes = [{ path: ':id', component: EntityDetailComponent }];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class EntityRoutingModule { }
